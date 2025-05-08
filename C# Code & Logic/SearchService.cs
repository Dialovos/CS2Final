using Lucene.Net.Analysis.Standard; // for breaking text into words (tokens) for easy process (self note)
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// libraries for reading specific file types
// for modern .docx files
// alias to avoid confusion with other document types
using DocumentFormat.OpenXml.Packaging;
using OpenXmlWord = DocumentFormat.OpenXml.Wordprocessing;

// for older .doc files
using NPOI.HWPF;
using NPOI.HWPF.Extractor;

// for .pdf files
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

// Lucene aliases to avoid confliction with DocumentFormat.OpenXML
using LuceneDocument = Lucene.Net.Documents.Document;
using LuceneQuery = Lucene.Net.Search.Query;
using LuceneTerm = Lucene.Net.Index.Term;

namespace CS2_Final // make sure SearchResult class is also in this namespace
{
    // this class handles all the Lucene indexing and searching logic
    public class SearchService
    {
        // tell Lucene which version's rules we're using
        private const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

        // names for the fields we store in each Lucene document
        // using constants makes it easier to avoid typos later
        private const string FieldPath = "filepath";                  // full path like c:\folder\file.txt
        private const string FieldFileName = "filename";              // just file.txt
        private const string FieldParentFolderName = "parentfolder";  // just folder
        private const string FieldFullDirectoryPath = "fulldirpath";  // just c:\folder
        private const string FieldLineNumber = "linenum";             // line number if it's a text file line
        private const string FieldContent = "content";                // the actual text we search in
        private const string FieldSourceType = "sourcetype";          // 'text line', 'docx', 'pdf', etc

        // names for the different types of things we index 
        // helps the ui display results nicely
        public const string SourceTypeTextLine = "text line";         // a line from a plain text file
        public const string SourceTypeDoc = "doc";                    // content from an older word document (.doc)
        public const string SourceTypeDocx = "docx";                  // content from a modern word document (.docx)
        public const string SourceTypePdf = "pdf";                    // content from a pdf document
        public const string SourceTypeFolder = "folder";              // an indexed folder/directory itself
        public const string SourceTypeFilePath = "file (path only)";  // a file indexed only by name/path, not content

        // class member variables
        private readonly string _indexPath;                           // path to the folder where Lucene index lives
        private readonly StandardAnalyzer _analyzer;                  // the text analyzer instance
        private readonly FSDirectory _indexDirectory;                 // the Lucene directory object
        private readonly HashSet<string> _justPlainTextExtensions;    // list of extensions to treat as plain text

        /// <summary>
        /// constructor runs when for new SearchService()
        /// </summary>
        public SearchService()
        {
            // make a unique temp folder name for the index each time the app runs
            // prevents using old data and avoids file lock issues if app crashes
            _indexPath = Path.Combine(Path.GetTempPath(), "filetextsearcher_luceneindex_" + Guid.NewGuid().ToString("n").Substring(0, 8));
            // make sure the folder actually exists
            if (!System.IO.Directory.Exists(_indexPath))
            {
                System.IO.Directory.CreateDirectory(_indexPath);
            }

            // create the analyzer we'll use for indexing and searching
            _analyzer = new StandardAnalyzer(AppLuceneVersion);
            // tell Lucene to use the folder we just created
            _indexDirectory = FSDirectory.Open(new DirectoryInfo(_indexPath));

            // build our list of plain text file types
            // OrdinalIgnoreCase is for case insensitive
            _justPlainTextExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".txt", ".log", ".cs", ".xml", ".json", ".csv", ".md", ".html", ".htm", ".ini", ".config",
                ".yml", ".properties", ".py", ".rb", ".java", ".php", ".pl", ".js",
                ".ts", ".sh", ".bat", ".ps1", ".sql", ".css", ".scss", ".less", ".asp", ".jsp",
                ".c", ".cpp", ".h", ".hpp"
                // add more types here if needed
            };
        }

        /// <summary>
        /// gets text from modern word files (.docx)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string ExtractTextFromDocx(string filePath)
        {
            try
            {
                // open the file in read only
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
                {
                    // find the main content part
                    OpenXmlWord.Body body = wordDoc.MainDocumentPart?.Document?.Body;
                    // return the text inside, or empty string if no body/text
                    return body?.InnerText ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                // log errors
                Console.WriteLine($"error extracting text from docx {filePath} error {ex.Message}");
                return string.Empty; // return nothing on error
            }
        }

        /// <summary>
        /// gets text from old word files using NPOI
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string ExtractTextFromDoc(string filePath)
        {
            try
            {
                // NPOI needs a stream to read the file
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // create the NPOI document object
                    HWPFDocument document = new HWPFDocument(fileStream);
                    // use the extractor helper to get the text
                    WordExtractor extractor = new WordExtractor(document);
                    return extractor.Text ?? string.Empty; // return text or empty string
                }
            }
            catch (Exception ex)
            {
                // log errors for corrupt file, NPOI issue
                Console.WriteLine($"error extracting text from doc {filePath} error {ex.Message}");
                return string.Empty; // return nothing on error
            }
        }

        /// <summary>
        /// gets text from pdf files using pdfpig
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string ExtractTextFromPdf(string filePath)
        {
            try
            {
                // use string builder to collect text from all pages
                var sb = new StringBuilder();
                // open the pdf
                using (PdfDocument document = PdfDocument.Open(filePath))
                {
                    // loop through each page
                    foreach (Page page in document.GetPages())
                    {
                        // add the page's text to our builder
                        sb.Append(page.Text);
                        sb.AppendLine(); // add a newline between pages
                    }
                }
                return sb.ToString();    // return all collected text
            }
            catch (Exception exc)
            {
                // log errors like file not found, password protected, corrupt pdf
                Console.WriteLine($"error extracting text from pdf {filePath} error {exc.Message}");
                return string.Empty; // return nothing on error
            }
        }

        /// <summary>
        /// helper to get the name of the folder containing an item
        /// </summary>
        /// <param name="itemPath"></param>
        /// <param name="containingDirPath"></param>
        /// <returns></returns>
        private string GetParentFolderName(string itemPath, string containingDirPath)
        {
            // get the full path of the directory containing the item
            string parentDirectoryPath = Path.GetDirectoryName(itemPath);
            // if that's empty, item is just "file.txt" or maybe a root drive
            if (string.IsNullOrEmpty(parentDirectoryPath))
            {
                // check if the item itself is a root drive like "c:\"
                if (Path.GetPathRoot(itemPath) == itemPath) { return "root_drive"; }
                // otherwise, fall back to using the path of the folder we are currently scanning
                parentDirectoryPath = containingDirPath;
            }
            // now get just the name part of that parent path
            string parentName = Path.GetFileName(parentDirectoryPath);
            // if the name is still empty (happens for roots like "c:\")
            if (string.IsNullOrEmpty(parentName) && parentDirectoryPath != null)
            {
                // double check if it's really a root
                DirectoryInfo dirInfo = new DirectoryInfo(parentDirectoryPath);
                if (dirInfo.Parent == null) { return "root_drive"; }
                // otherwise use the directory info name
                return dirInfo.Name;
            }
                // return the name, or a placeholder if it's somehow still empty
            return string.IsNullOrEmpty(parentName) ? "unknown_parent_folder" : parentName;
        }

        /// <summary>
        /// helper to create a Lucene document with the fields we always add
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="parentFolderName"></param>
        /// <param name="fullDirectoryPath"></param>
        /// <param name="sourceType"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        private LuceneDocument CreateBaseLuceneDocument(string filePath, string fileName, string parentFolderName, string fullDirectoryPath, string sourceType, int lineNumber = 0)
        {
            var doc = new LuceneDocument();
            // store path exactly, don't analyze (StringField)
            doc.Add(new StringField(FieldPath, filePath, Field.Store.YES));
            // store names/paths as text so we can search within them (TextField)
            doc.Add(new TextField(FieldFileName, fileName, Field.Store.YES));
            doc.Add(new TextField(FieldParentFolderName, parentFolderName, Field.Store.YES));
            doc.Add(new TextField(FieldFullDirectoryPath, fullDirectoryPath, Field.Store.YES));
            // store type exactly (StringField)
            doc.Add(new StringField(FieldSourceType, sourceType, Field.Store.YES));
            // store line number as a number (Int32Field)
            doc.Add(new Int32Field(FieldLineNumber, lineNumber, Field.Store.YES));
            return doc;
        }

        /// <summary>
        /// goes through folders and adds files/folders to the Lucene index
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="currentDirPath"></param>
        /// <param name="progressCallback"></param>
        /// <param name="searchDocxContent"></param>
        /// <param name="searchPdfContent"></param>
        /// <param name="searchLegacyDocContent"></param>
        /// <param name="indexPathAndNames"></param>
        /// <param name="token"></param>
        private void IndexDirectory(IndexWriter writer, string currentDirPath, Action<string> progressCallback,
                                    bool searchDocxContent, bool searchPdfContent, bool searchLegacyDocContent,
                                    bool indexPathAndNames, CancellationToken token)
        {
            token.ThrowIfCancellationRequested(); // check for cancel at start of each folder

            // index files in this folder
            try
            {
                // loop through each file directly in this folder
                foreach (string filePath in System.IO.Directory.EnumerateFiles(currentDirPath, "*", SearchOption.TopDirectoryOnly))
                {
                    token.ThrowIfCancellationRequested(); // check for cancel for each file

                    // get file details
                    string fileName = Path.GetFileName(filePath);
                    string fileExtension = Path.GetExtension(filePath).ToLowerInvariant();
                    string fileItsDirectoryPath = Path.GetDirectoryName(filePath) ?? currentDirPath;
                    string parentFolderName = GetParentFolderName(filePath, currentDirPath);

                    progressCallback?.Invoke($"processing {fileName}"); // update ui status
                    bool contentWasProcessed = false;                   // track if we indexed content
                    string extractedText = null;                        // holds text from rich files
                    string currentSourceType = null;                    // holds the type for rich files

                    // try extracting text based on type and options
                    if (searchDocxContent && fileExtension == ".docx")
                    {
                        extractedText = ExtractTextFromDocx(filePath);
                        currentSourceType = SourceTypeDocx;
                    }
                    else if (searchLegacyDocContent && fileExtension == ".doc")
                    {
                        extractedText = ExtractTextFromDoc(filePath);
                        currentSourceType = SourceTypeDoc;
                    }
                    else if (searchPdfContent && fileExtension == ".pdf")
                    {
                        extractedText = ExtractTextFromPdf(filePath);
                        currentSourceType = SourceTypePdf;
                    }

                    // if we got text from docx/doc/pdf
                    if (extractedText != null)
                    {
                        // only add if text was actually extracted
                        if (!string.IsNullOrEmpty(extractedText))
                        {
                            var doc = CreateBaseLuceneDocument(filePath, fileName, parentFolderName, fileItsDirectoryPath, currentSourceType);
                            // add the full extracted text as the content field
                            doc.Add(new TextField(FieldContent, extractedText, Field.Store.YES));
                            writer.AddDocument(doc);
                        }
                        contentWasProcessed = true; // mark as handled
                    }
                    // else if it's a plain text file type
                    else if (_justPlainTextExtensions.Contains(fileExtension))
                    {
                        int lineNumber = 0;
                        // read line by line
                        foreach (string line in File.ReadLines(filePath))
                        {
                            token.ThrowIfCancellationRequested(); // check often for large files
                            lineNumber++;
                            var doc = CreateBaseLuceneDocument(filePath, fileName, parentFolderName, fileItsDirectoryPath, SourceTypeTextLine, lineNumber);
                            // add just this line as the content
                            doc.Add(new TextField(FieldContent, line, Field.Store.YES));
                            writer.AddDocument(doc);
                        }
                        contentWasProcessed = true; // mark as handled (even if empty file)
                    }

                    // if we need to index by path/name AND we didn't process content above
                    if (indexPathAndNames && !contentWasProcessed)
                    {
                        var doc = CreateBaseLuceneDocument(filePath, fileName, parentFolderName, fileItsDirectoryPath, SourceTypeFilePath);
                        // add empty content, don't store it (Field.Store.NO)
                        doc.Add(new TextField(FieldContent, string.Empty, Field.Store.NO));
                        writer.AddDocument(doc);
                    }
                }
            }
            catch (OperationCanceledException) { throw; } // pass cancel up
            catch (UnauthorizedAccessException) { progressCallback?.Invoke($"access denied to files in {currentDirPath} skipping"); }
            catch (Exception exc) { progressCallback?.Invoke($"error processing files in {currentDirPath} {exc.Message} skipping directory's files"); }

            token.ThrowIfCancellationRequested(); // check before handling subfolders

            // index subfolders
            try
            {
                // loop through subdirectories directly in this folder
                foreach (string subDirPath in System.IO.Directory.EnumerateDirectories(currentDirPath, "*", SearchOption.TopDirectoryOnly))
                {
                    token.ThrowIfCancellationRequested(); // check for each subfolder

                    // if indexing paths/names, add an entry for the folder itself
                    if (indexPathAndNames)
                    {
                        string dirName = Path.GetFileName(subDirPath);
                        string parentOfSubDir = GetParentFolderName(subDirPath, currentDirPath);
                        var dirDoc = CreateBaseLuceneDocument(subDirPath, dirName, parentOfSubDir, subDirPath, SourceTypeFolder);
                        // add simple content like "folder: name" so folder itself is findable
                        dirDoc.Add(new TextField(FieldContent, $"folder {dirName}", Field.Store.YES));
                        writer.AddDocument(dirDoc);
                        progressCallback?.Invoke($"indexing directory entry {dirName}");
                    }

                    // RECURSION search
                    // call this same method again for the subfolder
                    IndexDirectory(writer, subDirPath, progressCallback, searchDocxContent, searchPdfContent, searchLegacyDocContent, indexPathAndNames, token);
                }
            }
            catch (OperationCanceledException) { throw; } // pass cancel up
            catch (UnauthorizedAccessException) { progressCallback?.Invoke($"access denied to subdirectories in {currentDirPath} skipping"); }
            catch (Exception exc) { progressCallback?.Invoke($"error enumerating subdirectories in {currentDirPath} {exc.Message} skipping further subdirectories here"); }
        }

        /// <summary>
        /// main public method called by the form to start indexing and searching
        /// async ensuring that the application remains responsive during long running operations
        /// performs asynchronous tasks such as fetching data from a database, reading a file, etc
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="searchTerm"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="progressCallback"></param>
        /// <param name="searchDocxContent"></param>
        /// <param name="searchPdfContent"></param>
        /// <param name="searchLegacyDocContent"></param>
        /// <param name="namePathMatching"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<SearchResult>> IndexAndSearchFilesAsync(
            string directoryPath, string searchTerm, bool caseSensitive,
            Action<string> progressCallback,
            bool searchDocxContent, bool searchPdfContent, bool searchLegacyDocContent,
            bool namePathMatching, CancellationToken token)
        {
            // check input directory
            if (string.IsNullOrWhiteSpace(directoryPath) || !System.IO.Directory.Exists(directoryPath))
            {
                throw new ArgumentException("invalid/ non existing directory path provided", nameof(directoryPath));
            }

            // indexing
            progressCallback?.Invoke("starting indexing");
            // run the indexing part on a background thread
            await Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();
                // config for the index writer, create mode wipes out old index
                var writerConfig = new IndexWriterConfig(AppLuceneVersion, _analyzer) { OpenMode = OpenMode.CREATE };
                // using statement ensures writer is closed/disposed properly
                using (var writer = new IndexWriter(_indexDirectory, writerConfig))
                {
                                                          // start the recursive indexing process
                    IndexDirectory(writer, directoryPath, progressCallback, searchDocxContent, searchPdfContent, searchLegacyDocContent, namePathMatching, token);
                    token.ThrowIfCancellationRequested(); // check after indexing finishes
                    writer.Commit();                      // save changes to disk
                }
            }, token); // pass token to task.run

            progressCallback?.Invoke("indexing complete starting search");
            token.ThrowIfCancellationRequested();   // check before searching

            var results = new List<SearchResult>(); // list to hold results

            // check if we actually need to search content
            bool anyContentSearchEnabled = searchDocxContent || searchPdfContent || searchLegacyDocContent;
            if (string.IsNullOrWhiteSpace(searchTerm) && !namePathMatching && !anyContentSearchEnabled)
            {
                progressCallback?.Invoke("no search term and no content/path listing options enabled no search performed");
                return results; // nothing to search for, return empty list
            }

            // searching phase
            // run the search part on a background thread too
            await Task.Run(() =>
            {
                token.ThrowIfCancellationRequested(); // check before starting search work
                                                      // make sure the index folder actually exists and has stuff in it
                if (!DirectoryReader.IndexExists(_indexDirectory))
                {
                    progressCallback?.Invoke("index does not exist or is empty nothing to search");
                    return;                           // exit if no index
                }

                // using statement ensures reader is closed/disposed
                using (var reader = DirectoryReader.Open(_indexDirectory))
                {
                    token.ThrowIfCancellationRequested();     // check after opening reader
                    var searcher = new IndexSearcher(reader); // the object that performs searches
                    LuceneQuery query;                        // holds the final Lucene query object

                    // if user didn't type a search term
                    if (string.IsNullOrWhiteSpace(searchTerm))
                    {
                        // but they want to list paths or rich docs, use MatchAllDocsQuery
                        query = new MatchAllDocsQuery();
                        progressCallback?.Invoke("no search term, listing all indexed items");
                    }
                    else// user typed a search term
                    {
                        // boolean query lets us combine searches 
                        var booleanQuery = new BooleanQuery();
                        // always search in the main content field
                        var fieldsToQuery = new List<string> { FieldContent };
                        // if name/path matching is on, add those fields too
                        if (namePathMatching)
                        {
                            fieldsToQuery.Add(FieldFileName);
                            fieldsToQuery.Add(FieldParentFolderName);
                            fieldsToQuery.Add(FieldFullDirectoryPath);
                        }

                        if (caseSensitive)
                        {
                            // standard analyzer lowercases, so TermQuery won't find "apple"
                            // this searches for the exact term as typed against the index
                            progressCallback?.Invoke($"performing case sensitive style search for '{searchTerm}'");
                            foreach (var field in fieldsToQuery)
                            {
                                // add a clause: find the exact term in this field (should occur)
                                booleanQuery.Add(new TermQuery(new LuceneTerm(field, searchTerm)), Occur.SHOULD);
                            }
                            query = (LuceneQuery)booleanQuery; // use the combined boolean query
                        }
                        else // case insensitive
                        {
                            // it uses the analyzer
                            var parser = new MultiFieldQueryParser(AppLuceneVersion, fieldsToQuery.ToArray(), _analyzer);
                            try
                            {
                                // escape special Lucene characters like * ? : etc
                                query = parser.Parse(QueryParserBase.Escape(searchTerm));
                            }
                            catch (ParseException exc)
                            {
                                // if query is invalid even after escaping
                                progressCallback?.Invoke($"error search query {exc.Message} no results will be returned");
                                query = new BooleanQuery(); // use an empty query
                            }
                            progressCallback?.Invoke($"performing case non-sensitive search for '{searchTerm}'");
                        }
                    }

                    token.ThrowIfCancellationRequested(); // check before running search

                    // run the search, limit to 100k results for now
                    TopDocs topDocs = searcher.Search(query, n: 100000);
                    progressCallback?.Invoke($"found {topDocs.TotalHits} potential matches, getting details");
                    token.ThrowIfCancellationRequested(); // check before processing results

                    // loop through the documents Lucene found
                    foreach (ScoreDoc scoreDoc in topDocs.ScoreDocs)
                    {
                        token.ThrowIfCancellationRequested(); // check for each result
                        // get the stored data for this document
                        LuceneDocument luceneDoc = searcher.Doc(scoreDoc.Doc);
                        string sourceType = luceneDoc.Get(FieldSourceType) ?? string.Empty;
                        string storedContent = luceneDoc.Get(FieldContent);
                        string displayContentForResult = "(n/a)"; // default display text

                        // format the display content based on the source type
                        if (sourceType == SourceTypeTextLine)
                        {
                            displayContentForResult = storedContent; // show the full line
                        }
                        else if (sourceType == SourceTypeDoc || sourceType == SourceTypeDocx || sourceType == SourceTypePdf)
                        {
                            // for rich docs, try to show a snippet
                            if (!string.IsNullOrEmpty(storedContent))
                            {
                                if (!string.IsNullOrWhiteSpace(searchTerm)) // if user searched for something
                                {
                                    int contentRadius = 70; // chars around the term
                                    StringComparison comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                                    int termIndex = storedContent.IndexOf(searchTerm, 0, comparison); // find first match

                                    if (termIndex != -1)    // if found
                                    {
                                        // calculate start/end for snippet
                                        int startIndex = Math.Max(0, termIndex - contentRadius);
                                        int desiredEndIndex = termIndex + searchTerm.Length + contentRadius;
                                        int actualEndIndex = Math.Min(storedContent.Length, desiredEndIndex);
                                        int length = actualEndIndex - startIndex;
                                        // build the snippet string
                                        var sbSnippet = new StringBuilder();
                                        if (startIndex > 0) sbSnippet.Append("... "); // show if text was cut off at start
                                        // add the snippet text, replace newlines with spaces for display
                                        sbSnippet.Append(storedContent.Substring(startIndex, length).Replace("\r", " ").Replace("\n", " "));
                                        if (actualEndIndex < storedContent.Length) sbSnippet.Append(" ..."); // show if text cut off at end
                                        displayContentForResult = sbSnippet.ToString();
                                    }
                                    else
                                    {
                                        // term not found by simple index of
                                        // show beginning of doc as fallback snippet
                                        int length = Math.Min(storedContent.Length, 150);
                                        displayContentForResult = storedContent.Substring(0, length).Replace("\r", " ").Replace("\n", " ") + (storedContent.Length > 150 ? "..." : "");
                                    }
                                }

                                else // no search term, just listing rich docs
                                {
                                    // show beginning of doc
                                    int length = Math.Min(storedContent.Length, 150);
                                    displayContentForResult = storedContent.Substring(0, length).Replace("\r", " ").Replace("\n", " ") + (storedContent.Length > 150 ? "..." : "");
                                }
                            }
                            else { displayContentForResult = "(content not extracted or empty)"; }
                        }
                        else if (sourceType == SourceTypeFolder) { displayContentForResult = storedContent; } // show "folder: name"
                        else if (sourceType == SourceTypeFilePath) { displayContentForResult = "file/folder (path match only)"; }

                        // create the SearchResult object for the ui ListVew
                        results.Add(new SearchResult
                        {
                            FilePath = luceneDoc.Get(FieldPath),
                            FileName = luceneDoc.Get(FieldFileName),
                            LineNumber = luceneDoc.GetField(FieldLineNumber)?.GetInt32Value() ?? 0, // get line number safely
                            SourceType = sourceType,
                            DisplayContent = displayContentForResult?.Trim() // trim extra whitespace
                        });
                    }
                }
            }, token); // pass token to task.run

            token.ThrowIfCancellationRequested(); // final check before returning
            return results;                       // return the list of found results
        }

        /// <summary>
        /// cleans up Lucene resources, especially the temporary index folder
        /// </summary>
        public void DisposeLuceneResources()
        {
            try
            {
                // dispose Lucene objects if they exist
                _analyzer?.Dispose();
                _indexDirectory?.Dispose();

                // try to delete the temp index folder
                if (System.IO.Directory.Exists(_indexPath))
                {
                    try
                    {
                        System.IO.Directory.Delete(_indexPath, true); // true = recursive delete
                        Console.WriteLine($"done deleting Lucene index directory {_indexPath}");
                    }
                    catch (IOException exc) { Console.WriteLine($"could not delete Lucene index directory {_indexPath} (maybe in use or access denied) {exc.Message}"); }
                    catch (Exception exc) { Console.WriteLine($"error deleting Lucene index directory {_indexPath} {exc.Message}"); }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"error disposing Lucene resources {exc.Message}");
            }
        }
    }
}
