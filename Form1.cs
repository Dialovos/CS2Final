using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CS2_Final
{
    public partial class Form1 : Form
    {

        // use one httpclient for all web request
        private static readonly HttpClient httpClient = new HttpClient();

        #region Search service

        // holds the search logic object
        private SearchService _searchService;
        // used to cancel a search if the user clicks the cancel button
        private CancellationTokenSource _searchCts;

        /// <summary>
        /// constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            _searchService = new SearchService();
        }

        // runs when the form first loads up
        private void Form1_Load(object sender, EventArgs e)
        {
            // status bar at the bottom
            UpdateStatus("ready for searching");
        }

        // enables or disables controls depending on if a search is running
        // makes sure controls are updated on the correct ui
        private void SetSearchInAction(bool isSearching)
        {
            // check if need to switch threads to update ui
            if (this.InvokeRequired)
            {
                        // if yes, ask the ui thread to run this method again
                this.BeginInvoke(new Action<bool>(SetSearchInAction), isSearching);
                return; // exit this background thread call
            }

            // if we are on the ui thread, update the controls
            // to prevent user from crashing the program except cancel button
            bool enableControls = !isSearching;

            txtDirectory.Enabled = enableControls;
            btnBrowse.Enabled = enableControls;
            txtSearchTerm.Enabled = enableControls;
            groupBoxSearchOptions.Enabled = enableControls;

            btnSearch.Enabled = enableControls;
            btnClear.Enabled = enableControls;
            btnCancelSearch.Enabled = isSearching;

            txtCityWeather.Enabled = enableControls;
            btnGetWeather.Enabled = enableControls;

            // change mouse cursor to hourglass if searching, default otherwise
            this.Cursor = isSearching ? Cursors.WaitCursor : Cursors.Default;
        }

        /// <summary>
        /// runs when the browse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "select directory to search";
            folderBrowserDialog1.ShowNewFolderButton = false; // don't let user make new folders here

            // if there's already a valid path in the box, start the browser there
            if (!string.IsNullOrWhiteSpace(txtDirectory.Text) && System.IO.Directory.Exists(txtDirectory.Text))
            {
                folderBrowserDialog1.SelectedPath = txtDirectory.Text;
            }

            // show the folder picker
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // if user clicked ok, put the chosen path in the textbox
                txtDirectory.Text = folderBrowserDialog1.SelectedPath;
                UpdateStatus("directory selected input term for search if necessary");
            }
        }

        /// <summary>
        /// async void run in the background without freezing the ui
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            // get the directory and search term from the textboxes
            string directory = txtDirectory.Text;
            string searchTerm = txtSearchTerm.Text;

            // check if the directory is valid
            if (string.IsNullOrWhiteSpace(directory) || !System.IO.Directory.Exists(directory))
            {
                MessageBox.Show("select a valid directory", "invalid directory selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // stop if directory is bad
            }

                                   // get ready for a new search, clean any old one
            _searchCts?.Dispose(); // clean up the old cancel source if it exists
            _searchCts = new CancellationTokenSource(); // make a new one for this search
            CancellationToken token = _searchCts.Token; // get the token to pass to the search service

            lvResults.Items.Clear(); // clear out old results from the list view
            SetSearchInAction(true); // disable controls, enable cancel button, show wait cursor
            UpdateStatus("starting search");

            try
            {
                // get the options from the checkboxes
                bool caseSensitiveSearch = chkCaseSensitive.Checked;
                bool searchDocxAndPdfContent = chkSearchDocxPdf.Checked;    // covers both docx and pdf
                bool searchLegacyDoc = chkSearchLegacyDoc.Checked;          // for older .doc files
                bool comprehensiveNames = chkComprehensiveNamePath.Checked; // search names/paths too

                // call the search service to do the work
                // await -> wait here without freezing the ui until the search is done
                List<SearchResult> results = await _searchService.IndexAndSearchFilesAsync(
                    directory,
                    searchTerm,
                    caseSensitiveSearch,
                    UpdateStatus, // pass status update method so the service can send messages
                    searchDocxAndPdfContent, // flag for docx
                    searchDocxAndPdfContent, // flag for pdf
                    searchLegacyDoc,         // flag for doc
                    comprehensiveNames,
                    token // pass the cancel token
                );

                // check if the user cancel while waiting for the search
                if (!token.IsCancellationRequested)
                {
                    // if not cancel, show the results
                    DisplayResults(results);
                    UpdateStatus($"search done found {results.Count} results");

                    // give a message if nothing was found
                    if (results.Count == 0 && !string.IsNullOrWhiteSpace(searchTerm))
                    {
                        UpdateStatus($"search done no results found for {searchTerm}");
                    }
                }
                // if it was cancel, the catch block below
            }
            catch (OperationCanceledException)
            {
                // this happens if the token was cancel
                UpdateStatus("search cancelled by user");
            }
            catch (ArgumentException argEx)
            {
                // handle errors like a bad directory path
                UpdateStatus($"input error {argEx.Message}");
                MessageBox.Show($"error {argEx.Message}");
            }
            catch (Exception ex)
            {
                // catch any other unexpected problems
                UpdateStatus($"error during search {ex.Message}");
                MessageBox.Show($"an unexpected error occurred {ex.Message}");
            }
            finally
            {
                // this code runs no matter what
                SetSearchInAction(false); // re enable controls, disable cancel button, reset cursor
                _searchCts?.Dispose();    // clean up the cancellation source
                _searchCts = null;        // mark that no search is active
            }
        }

        /// <summary>
        /// puts the search results into the list view control
        /// </summary>
        /// <param name="results"></param>
        private void DisplayResults(List<SearchResult> results)
        {
            lvResults.BeginUpdate(); // tells the list view to stop redrawing
            lvResults.Items.Clear(); // clear old items first

            // if no results, just finish
            if (results == null || results.Count == 0)
            {
                lvResults.EndUpdate(); // tell list view it can redraw now
                return;
            }

            // loop through each result we found
            foreach (var result in results)
            {
                // create a new row starting with the filename
                var listViewItem = new ListViewItem(result.FileName);
                // add the other columns
                listViewItem.SubItems.Add(result.SourceType ?? string.Empty);

                // show line number only if it's relevant num > 0 and is a text line type
                string lineNumDisplay = (result.SourceType == SearchService.SourceTypeTextLine && result.LineNumber > 0)
                                        ? result.LineNumber.ToString() : "-";

                listViewItem.SubItems.Add(lineNumDisplay);                        // display line number or '-'
                listViewItem.SubItems.Add(result.DisplayContent ?? string.Empty); // the content
                
                // store the full path in the tag property, maybe useful later
                listViewItem.Tag = result.FilePath;
                // add the completed row to the list view
                lvResults.Items.Add(listViewItem);
            }
            lvResults.EndUpdate(); // done adding items, let the list view redraw
        }

        /// <summary>
        /// runs when the clear results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            // clear the input boxes
            txtDirectory.Clear();
            txtSearchTerm.Clear();

            // uncheck all the option boxes
            chkCaseSensitive.Checked = false;
            chkSearchDocxPdf.Checked = false;
            chkSearchLegacyDoc.Checked = false;
            chkComprehensiveNamePath.Checked = false;

            // clear the results list
            lvResults.Items.Clear();
            UpdateStatus("ready"); // reset status message
                                   // reset the folder browser dialog path
            folderBrowserDialog1.SelectedPath = string.Empty;
        }

        /// <summary>
        /// runs when the cancel search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            // check if there's actually a search running that we can cancel
            if (_searchCts != null && !_searchCts.IsCancellationRequested)
            {
                UpdateStatus("cancellation requested");
                _searchCts.Cancel(); // signal the cancellation token
                // disable the cancel button right away so user can't click it again
                // it will be re enable/disabled when the search task finishes in the finally block
                btnCancelSearch.Enabled = false;
            }
        }

        // updates the message in the status bar at the bottom
        // makes sure it runs on the ui thread
        private void UpdateStatus(string message)
        {
            // omg is this call coming from a background thread?
            if (statusStrip1.InvokeRequired)
            {
                // ask the ui thread to run this method with the message
                statusStrip1.BeginInvoke(new Action<string>(UpdateStatus), message);
            }
            else
            {
                // update the label
                lblStatus.Text = message;
            }
        }

        /// <summary>
        /// runs just before the form closes
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
                                                      // tell the search service to clean up Lucene resources
            _searchService?.DisposeLuceneResources(); // the ?. -> call it if _searchService isn't null
            base.OnFormClosing(e);                    // make sure the form closing happens too
        }

        #endregion

        #region handle how the listview headers and items look (optional styling)

        // draws the column headers
        private void lvResults_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
            // centered vertically, aligned left for text
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, e.Bounds, Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            e.DrawDefault = false; // handled drawing, don't do the default windows style (self note)
        }

        // draws the main item
        private void lvResults_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        // draws the sub-items
        private void lvResults_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        #endregion

        #region weather api

        // runs when the 'get' weather button is clicked
        private async void btnGetWeather_Click(object sender, EventArgs e)
        {
            // get city name from the textbox, remove extra spaces
            string cityNameInput = txtCityWeather.Text.Trim();
            // make sure user typed something
            if (string.IsNullOrWhiteSpace(cityNameInput))
            {
                MessageBox.Show("please enter a city name", "input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // stop if no city name
            }

            // disable button while we fetch
            btnGetWeather.Enabled = false;
            lblWeatherDisplay.Text = "getting weather data";

            try
            {
                // build the url for the openweathermap api request
                // use the key from ApiKeys class now
                string apiKey = ApiKeys.OpenWeatherMapApiKey;
                if (string.IsNullOrEmpty(apiKey) || apiKey == "API_KEY")
                {
                    lblWeatherDisplay.Text = "error api key not set in ApiKeys cs";
                    MessageBox.Show("OpenWeatherMap api key is not config in ApiKeys.cs", "api key error error error error error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // stop if no key
                }

                // unit is imperial -> &units, can be changed to SI if nescessary
                string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(cityNameInput)}&appid={apiKey}&units=imperial";

                // make the web request (await means wait without freezing ui)
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                // did the request work (status code 200 ok)?
                if (response.IsSuccessStatusCode)
                {
                    // yes, read the response
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    // convert the json text into our WeatherData object using Newtonsoft.Json
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonResponse);

                    // check if we got valid data back
                    if (weatherData != null && weatherData.Main != null && weatherData.Weather != null && weatherData.Weather.Count > 0)
                    {
                        // build the display string piece by piece
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"city: {weatherData.cityName ?? cityNameInput}");
                        sb.AppendLine($"temperature: {weatherData.Main.Temp}°f");
                        sb.AppendLine($"feels like: {weatherData.Main.weatherFeelsLike}°f");
                        sb.AppendLine($"humidity: {weatherData.Main.Humidity}%");
                        sb.AppendLine($"condition: {weatherData.Weather[0].Description}");
                        if (weatherData.Wind != null)
                        {
                            sb.AppendLine($"wind: {weatherData.Wind.windSpeed} mph");
                        }
                        // show the final string in the label
                        lblWeatherDisplay.Text = sb.ToString();
                    }
                    else
                    {
                        // json was weird or missing parts
                        lblWeatherDisplay.Text = "information is missing";
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // api said city wasn't found (404 error)
                    lblWeatherDisplay.Text = $"city '{cityNameInput}' not found";
                }
                else
                {
                    // some other http error (bad api key 401)
                    lblWeatherDisplay.Text = $"error {response.ReasonPhrase} (code {(int)response.StatusCode})";
                }
            }
            catch (HttpRequestException httpEx)
            {
                // network problem 
                lblWeatherDisplay.Text = $"network error {httpEx.Message}";
                MessageBox.Show($"could not connect to the weather service {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                // problem reading the json data
                lblWeatherDisplay.Text = "error parsing weather data";
                MessageBox.Show($"error processing weather data {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                // any other unexpected error
                lblWeatherDisplay.Text = "an unexpected error occurred while fetching weather";
                MessageBox.Show($"an unexpected error occurred {ex.Message}");
            }
            finally
            {
                // always re-enable the button when done
                btnGetWeather.Enabled = true;
            }
        }

        #endregion
    }
}
