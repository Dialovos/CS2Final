namespace CS2_Final
{
    /// <summary>
    /// a single result found by the file search
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Name of the file or folder
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Full path to the file or folder
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Line number where the search term was found 
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Type of the search result
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// Display content.
        /// </summary>
        public string DisplayContent { get; set; }
    }
}