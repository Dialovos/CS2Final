# File Search & Weather

A Windows Forms application built with C# and .NET Framework
Semester Project in spring 2025

## Features

* File Content Search:
    * Recursive search content within files in selected dir
    * Powered by Lucene.NET for fast indexing & searching
    * Supports line by line indexing for plain text files
    * Additional content extraction for Docx, DOC (Word 97-2003), and PDF documents.
    
* Search Options:
    * Case sensitive mode
    * Option for Name/Path Matching to index and search all file and folder names/paths
    * Cancel & Clear search
    
* Results Display:
    * Clear list view showing:
        * File/Folder Name
        * Type
        * Line Number 
        * Content / Details

* Weather Integration:
    * Displays current weather info for selected city
    * Uses the OpenWeatherMap API
    
* User Interface:
    * Windows Forms GUI
    * Separate panels for search config and weather display.
    * Status bar for feedback on ongoing search

## Tech Stack & Requirements

* Language/Framework: C# with .NET Framework (e.g., v4.8 - specify your proj target version)
* UI: WinForms
* Core Search Library: Lucene.NET (v4.8.0-beta00016)

* Document Parsing:
    * DocumentFormat.OpenXml (for DOCX)
    * **NPOI (for DOC - Word 97-2003 files)**
    * PdfPig (for PDF)
    
* JSON Parsing: Newtonsoft.Json (for OpenWeatherMap API response)
* API: OpenWeatherMap API key
* Development Environment: Visual Studio 2019/2022
* Runtime: .NET Framework Runtime

## Setup & Installation

1.  Clone the Repository:
    ```bash
    git clone [https://github.com/Dialovos/CS2Final.git](https://github.com/Dialovos/CS2_Final.git)
    cd CS2Final
    ```

2.  Open in Visual Studio:
    Open the solution file (`.sln`) in Visual Studio.

3.  Restore NuGet Packages:
    NuGet packages should restore automatically when you build. If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages."

4.  Set Up OpenWeatherMap API Key:
    * You need a free API key from [OpenWeatherMap](https://openweathermap.org/).
    * Once you have your key, open the `Form1.cs` file in the project.
    * Locate the line where the API key is defined (in `ApiKeys`): 
        ```csharp
        // private static readonly string OpenWeatherMapApiKey_Static = "YOUR_API_KEY";
        ```
    * Replace `"YOUR_API_KEY"` with your API key.
        
5.  Build the Solution:
    In Visual Studio, go to `Build` -> `Rebuild Solution`.
