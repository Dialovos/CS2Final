// Make sure to back up your Form1.Designer.cs before replacing this section.
// This shows the relevant changes for checkBox1 becoming chkSearchLegacyDoc.
// You'll need to integrate this carefully.

namespace CS2_Final
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDirectoryPrompt = new System.Windows.Forms.Label();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblSearchTermPrompt = new System.Windows.Forms.Label();
            this.txtSearchTerm = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lvResults = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLineNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDetails = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxSearchOptions = new System.Windows.Forms.GroupBox();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.chkSearchDocxPdf = new System.Windows.Forms.CheckBox();
            this.chkSearchLegacyDoc = new System.Windows.Forms.CheckBox();
            this.chkComprehensiveNamePath = new System.Windows.Forms.CheckBox();
            this.groupBoxWeather = new System.Windows.Forms.GroupBox();
            this.lblWeatherDisplay = new System.Windows.Forms.Label();
            this.btnGetWeather = new System.Windows.Forms.Button();
            this.txtCityWeather = new System.Windows.Forms.TextBox();
            this.lblCityInputPrompt = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelSearchSetup = new System.Windows.Forms.Panel();
            this.flpSearchActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancelSearch = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.groupBoxSearchOptions.SuspendLayout();
            this.groupBoxWeather.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.panelSearchSetup.SuspendLayout();
            this.flpSearchActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDirectoryPrompt
            // 
            this.lblDirectoryPrompt.AutoSize = true;
            this.lblDirectoryPrompt.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDirectoryPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblDirectoryPrompt.Location = new System.Drawing.Point(10, 12);
            this.lblDirectoryPrompt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDirectoryPrompt.Name = "lblDirectoryPrompt";
            this.lblDirectoryPrompt.Size = new System.Drawing.Size(94, 25);
            this.lblDirectoryPrompt.TabIndex = 0;
            this.lblDirectoryPrompt.Text = "Directory:";
            // 
            // txtDirectory
            // 
            this.txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectory.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDirectory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDirectory.Location = new System.Drawing.Point(118, 9);
            this.txtDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.ReadOnly = true;
            this.txtDirectory.Size = new System.Drawing.Size(588, 31);
            this.txtDirectory.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnBrowse.Location = new System.Drawing.Point(715, 6);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(120, 35);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblSearchTermPrompt
            // 
            this.lblSearchTermPrompt.AutoSize = true;
            this.lblSearchTermPrompt.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearchTermPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblSearchTermPrompt.Location = new System.Drawing.Point(10, 52);
            this.lblSearchTermPrompt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchTermPrompt.Name = "lblSearchTermPrompt";
            this.lblSearchTermPrompt.Size = new System.Drawing.Size(116, 25);
            this.lblSearchTermPrompt.TabIndex = 3;
            this.lblSearchTermPrompt.Text = "Search Term:";
            // 
            // txtSearchTerm
            // 
            this.txtSearchTerm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchTerm.BackColor = System.Drawing.Color.White;
            this.txtSearchTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchTerm.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearchTerm.Location = new System.Drawing.Point(118, 49);
            this.txtSearchTerm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearchTerm.Name = "txtSearchTerm";
            this.txtSearchTerm.Size = new System.Drawing.Size(717, 31);
            this.txtSearchTerm.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(100)))), ((int)(((byte)(40)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(150)))), ((int)(((byte)(60)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(317, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 15, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(150, 46);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(40)))));
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(60)))), ((int)(((byte)(50)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(4, 5);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(147, 46);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear Results";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lvResults
            // 
            this.lvResults.BackColor = System.Drawing.Color.White;
            this.lvResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colType,
            this.colLineNum,
            this.colDetails});
            this.lvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvResults.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvResults.FullRowSelect = true;
            this.lvResults.GridLines = true;
            this.lvResults.HideSelection = false;
            this.lvResults.Location = new System.Drawing.Point(16, 328);
            this.lvResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvResults.Name = "lvResults";
            this.lvResults.OwnerDraw = true;
            this.lvResults.Size = new System.Drawing.Size(844, 337);
            this.lvResults.TabIndex = 2;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            this.lvResults.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvResults_DrawColumnHeader);
            this.lvResults.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvResults_DrawItem);
            this.lvResults.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvResults_DrawSubItem);
            // 
            // colFileName
            // 
            this.colFileName.Text = "File/Folder Name";
            this.colFileName.Width = 220;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 100;
            // 
            // colLineNum
            // 
            this.colLineNum.Text = "Line No.";
            this.colLineNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colLineNum.Width = 70;
            // 
            // colDetails
            // 
            this.colDetails.Text = "Content / Details";
            this.colDetails.Width = 400;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 682);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1242, 32);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1219, 25);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxSearchOptions
            // 
            this.groupBoxSearchOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSearchOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBoxSearchOptions.Controls.Add(this.chkCaseSensitive);
            this.groupBoxSearchOptions.Controls.Add(this.chkSearchDocxPdf);
            this.groupBoxSearchOptions.Controls.Add(this.chkSearchLegacyDoc);
            this.groupBoxSearchOptions.Controls.Add(this.chkComprehensiveNamePath);
            this.groupBoxSearchOptions.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxSearchOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.groupBoxSearchOptions.Location = new System.Drawing.Point(10, 88);
            this.groupBoxSearchOptions.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.groupBoxSearchOptions.Name = "groupBoxSearchOptions";
            this.groupBoxSearchOptions.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.groupBoxSearchOptions.Size = new System.Drawing.Size(825, 130);
            this.groupBoxSearchOptions.TabIndex = 5;
            this.groupBoxSearchOptions.TabStop = false;
            this.groupBoxSearchOptions.Text = "Search Options";
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkCaseSensitive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.chkCaseSensitive.Location = new System.Drawing.Point(20, 29);
            this.chkCaseSensitive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(129, 29);
            this.chkCaseSensitive.TabIndex = 0;
            this.chkCaseSensitive.Text = "Case Match";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // chkSearchDocxPdf
            // 
            this.chkSearchDocxPdf.AutoSize = true;
            this.chkSearchDocxPdf.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkSearchDocxPdf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.chkSearchDocxPdf.Location = new System.Drawing.Point(338, 29);
            this.chkSearchDocxPdf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkSearchDocxPdf.Name = "chkSearchDocxPdf";
            this.chkSearchDocxPdf.Size = new System.Drawing.Size(244, 29);
            this.chkSearchDocxPdf.TabIndex = 1;
            this.chkSearchDocxPdf.Text = "Search .docx/.pdf Content";
            this.chkSearchDocxPdf.UseVisualStyleBackColor = true;
            // 
            // chkSearchLegacyDoc
            // 
            this.chkSearchLegacyDoc.AutoSize = true;
            this.chkSearchLegacyDoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkSearchLegacyDoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.chkSearchLegacyDoc.Location = new System.Drawing.Point(338, 64);
            this.chkSearchLegacyDoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkSearchLegacyDoc.Name = "chkSearchLegacyDoc";
            this.chkSearchLegacyDoc.Size = new System.Drawing.Size(197, 29);
            this.chkSearchLegacyDoc.TabIndex = 3;
            this.chkSearchLegacyDoc.Text = "Search .doc Content";
            this.chkSearchLegacyDoc.UseVisualStyleBackColor = true;
            // 
            // chkComprehensiveNamePath
            // 
            this.chkComprehensiveNamePath.AutoSize = true;
            this.chkComprehensiveNamePath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkComprehensiveNamePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.chkComprehensiveNamePath.Location = new System.Drawing.Point(20, 64);
            this.chkComprehensiveNamePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.chkComprehensiveNamePath.Name = "chkComprehensiveNamePath";
            this.chkComprehensiveNamePath.Size = new System.Drawing.Size(180, 29);
            this.chkComprehensiveNamePath.TabIndex = 2;
            this.chkComprehensiveNamePath.Text = "Name/Path Match";
            this.chkComprehensiveNamePath.UseVisualStyleBackColor = true;
            // 
            // groupBoxWeather
            // 
            this.groupBoxWeather.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(235)))), ((int)(((byte)(245)))));
            this.groupBoxWeather.Controls.Add(this.lblWeatherDisplay);
            this.groupBoxWeather.Controls.Add(this.btnGetWeather);
            this.groupBoxWeather.Controls.Add(this.txtCityWeather);
            this.groupBoxWeather.Controls.Add(this.lblCityInputPrompt);
            this.groupBoxWeather.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxWeather.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxWeather.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.groupBoxWeather.Location = new System.Drawing.Point(868, 17);
            this.groupBoxWeather.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxWeather.Name = "groupBoxWeather";
            this.groupBoxWeather.Padding = new System.Windows.Forms.Padding(10);
            this.tlpMain.SetRowSpan(this.groupBoxWeather, 3);
            this.groupBoxWeather.Size = new System.Drawing.Size(358, 648);
            this.groupBoxWeather.TabIndex = 3;
            this.groupBoxWeather.TabStop = false;
            this.groupBoxWeather.Text = "Current Outside Grass Detail";
            // 
            // lblWeatherDisplay
            // 
            this.lblWeatherDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWeatherDisplay.BackColor = System.Drawing.Color.White;
            this.lblWeatherDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWeatherDisplay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWeatherDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblWeatherDisplay.Location = new System.Drawing.Point(14, 82);
            this.lblWeatherDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeatherDisplay.Name = "lblWeatherDisplay";
            this.lblWeatherDisplay.Padding = new System.Windows.Forms.Padding(8);
            this.lblWeatherDisplay.Size = new System.Drawing.Size(330, 552);
            this.lblWeatherDisplay.TabIndex = 3;
            this.lblWeatherDisplay.Text = "Enter city and click \"Get\"";
            // 
            // btnGetWeather
            // 
            this.btnGetWeather.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetWeather.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnGetWeather.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.btnGetWeather.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            this.btnGetWeather.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(130)))), ((int)(((byte)(220)))));
            this.btnGetWeather.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetWeather.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnGetWeather.ForeColor = System.Drawing.Color.White;
            this.btnGetWeather.Location = new System.Drawing.Point(256, 36);
            this.btnGetWeather.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGetWeather.Name = "btnGetWeather";
            this.btnGetWeather.Size = new System.Drawing.Size(88, 35);
            this.btnGetWeather.TabIndex = 2;
            this.btnGetWeather.Text = "Get";
            this.btnGetWeather.UseVisualStyleBackColor = false;
            this.btnGetWeather.Click += new System.EventHandler(this.btnGetWeather_Click);
            // 
            // txtCityWeather
            // 
            this.txtCityWeather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCityWeather.BackColor = System.Drawing.Color.White;
            this.txtCityWeather.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCityWeather.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCityWeather.Location = new System.Drawing.Point(64, 40);
            this.txtCityWeather.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCityWeather.Name = "txtCityWeather";
            this.txtCityWeather.Size = new System.Drawing.Size(184, 31);
            this.txtCityWeather.TabIndex = 1;
            this.txtCityWeather.Text = "Nashville";
            // 
            // lblCityInputPrompt
            // 
            this.lblCityInputPrompt.AutoSize = true;
            this.lblCityInputPrompt.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCityInputPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblCityInputPrompt.Location = new System.Drawing.Point(10, 43);
            this.lblCityInputPrompt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCityInputPrompt.Name = "lblCityInputPrompt";
            this.lblCityInputPrompt.Size = new System.Drawing.Size(48, 25);
            this.lblCityInputPrompt.TabIndex = 0;
            this.lblCityInputPrompt.Text = "City:";
            // 
            // tlpMain
            // 
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpMain.Controls.Add(this.groupBoxWeather, 1, 0);
            this.tlpMain.Controls.Add(this.lvResults, 0, 2);
            this.tlpMain.Controls.Add(this.panelSearchSetup, 0, 0);
            this.tlpMain.Controls.Add(this.flpSearchActions, 0, 1);
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(12);
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1242, 682);
            this.tlpMain.TabIndex = 0;
            // 
            // panelSearchSetup
            // 
            this.panelSearchSetup.Controls.Add(this.lblDirectoryPrompt);
            this.panelSearchSetup.Controls.Add(this.groupBoxSearchOptions);
            this.panelSearchSetup.Controls.Add(this.txtDirectory);
            this.panelSearchSetup.Controls.Add(this.btnBrowse);
            this.panelSearchSetup.Controls.Add(this.txtSearchTerm);
            this.panelSearchSetup.Controls.Add(this.lblSearchTermPrompt);
            this.panelSearchSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSearchSetup.Location = new System.Drawing.Point(16, 17);
            this.panelSearchSetup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSearchSetup.Name = "panelSearchSetup";
            this.panelSearchSetup.Padding = new System.Windows.Forms.Padding(5);
            this.panelSearchSetup.Size = new System.Drawing.Size(844, 225);
            this.panelSearchSetup.TabIndex = 0;
            // 
            // flpSearchActions
            // 
            this.flpSearchActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flpSearchActions.AutoSize = true;
            this.flpSearchActions.Controls.Add(this.btnSearch);
            this.flpSearchActions.Controls.Add(this.btnCancelSearch);
            this.flpSearchActions.Controls.Add(this.btnClear);
            this.flpSearchActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpSearchActions.Location = new System.Drawing.Point(378, 252);
            this.flpSearchActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 15);
            this.flpSearchActions.Name = "flpSearchActions";
            this.flpSearchActions.Size = new System.Drawing.Size(482, 56);
            this.flpSearchActions.TabIndex = 1;
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnCancelSearch.Enabled = false;
            this.btnCancelSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(120)))), ((int)(((byte)(0)))));
            this.btnCancelSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.btnCancelSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(130)))), ((int)(((byte)(0)))));
            this.btnCancelSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnCancelSearch.ForeColor = System.Drawing.Color.White;
            this.btnCancelSearch.Location = new System.Drawing.Point(159, 5);
            this.btnCancelSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.Size = new System.Drawing.Size(150, 46);
            this.btnCancelSearch.TabIndex = 2;
            this.btnCancelSearch.Text = "Cancel Search";
            this.btnCancelSearch.UseVisualStyleBackColor = false;
            this.btnCancelSearch.Click += new System.EventHandler(this.btnCancelSearch_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1242, 714);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1260, 761);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File & Weather Search";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxSearchOptions.ResumeLayout(false);
            this.groupBoxSearchOptions.PerformLayout();
            this.groupBoxWeather.ResumeLayout(false);
            this.groupBoxWeather.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.panelSearchSetup.ResumeLayout(false);
            this.panelSearchSetup.PerformLayout();
            this.flpSearchActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDirectoryPrompt;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblSearchTermPrompt;
        private System.Windows.Forms.TextBox txtSearchTerm;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colLineNum;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBoxSearchOptions;
        private System.Windows.Forms.CheckBox chkComprehensiveNamePath;
        private System.Windows.Forms.CheckBox chkSearchDocxPdf; // For .docx AND .pdf
        private System.Windows.Forms.CheckBox chkCaseSensitive;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colDetails;
        private System.Windows.Forms.GroupBox groupBoxWeather;
        private System.Windows.Forms.Label lblWeatherDisplay;
        private System.Windows.Forms.Button btnGetWeather;
        private System.Windows.Forms.TextBox txtCityWeather;
        private System.Windows.Forms.Label lblCityInputPrompt;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel panelSearchSetup;
        private System.Windows.Forms.FlowLayoutPanel flpSearchActions;
        private System.Windows.Forms.Button btnCancelSearch;
        private System.Windows.Forms.CheckBox chkSearchLegacyDoc; // Declaration for the new checkbox
    }
}
