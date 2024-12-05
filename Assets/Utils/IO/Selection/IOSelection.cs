using SFB;
using UnityEngine;

namespace Utils.IO.Selection
{
    public class IOSelection
    {
        private static readonly string DefaultDirectory = Paths.GetPath(Paths.Desktop);
        private readonly string _startDirectory;

        public string SelectFile(string extension, bool multiSelect = false)
        {
            string[] paths = StandaloneFileBrowser.OpenFilePanel(
                "Select File",
                _startDirectory,
                extension,
                multiSelect);

            return GetPath(paths);
        }
        protected string SelectFile(ExtensionFilter[] extensions, bool multiSelect = false)
        {
            extensions ??= new ExtensionFilter[1]
            {
                new("All Files", "*")
            };
            
            string[] paths = StandaloneFileBrowser.OpenFilePanel(
                "Select File",
                _startDirectory,
                extensions,
                multiSelect);

            return GetPath(paths);
        }

        protected string SelectFolder(bool multiSelect = false)
        {
            string[] paths = StandaloneFileBrowser.OpenFolderPanel(
                "Select Folder",
                _startDirectory,
                multiSelect);

            return GetPath(paths);
        }

        private string GetPath(string[] paths)
        {
            if (paths.Length == 0)
            {
                Debug.LogWarning("No valid path was selected!");

                return null;
            }
            
            Debug.Log(paths[0]);

            return paths[0];
        }

        public string SaveFile(string defaultFileName, string filter)
        {
            return StandaloneFileBrowser.SaveFilePanel(
                "Save As",
                DefaultDirectory,
                defaultFileName,
                filter);
        }
        public string SaveFile(string defaultFileName, ExtensionFilter[] filters)
        {
            return StandaloneFileBrowser.SaveFilePanel(
                "Save As",
                DefaultDirectory,
                defaultFileName,
                filters);
        }
    }
}