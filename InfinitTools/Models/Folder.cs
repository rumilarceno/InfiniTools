using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InfinitTools.Models
{
    class Folder : DirectoryObject
    {
        public Folder(string fullDirecotryName)
        {
            FullDirectoryName = fullDirecotryName;
        }
        public string FullDirectoryName { get; set; }

        private List<string> GetSubFolderPaths()
        {
            return Directory.GetDirectories(FullDirectoryName).ToList();
        }

        private List<string> GetDocumentFileNames(List<string> docExtensions)
        {
            return Directory.EnumerateFiles(FullDirectoryName, "*.*", SearchOption.TopDirectoryOnly)
                                     .Where(s => docExtensions.Any(a => s.EndsWith(a))).ToList();
        }

        public List<string> GetAllDocumentFileNames(List<string> docExtensions, bool getSubfolderDocFileNames)
        {
            var subFolders = GetSubFolderPaths();
            List<string> fileNames = new List<string>();

            if (getSubfolderDocFileNames && subFolders != null && GetSubFolderPaths().Count > 0)
            {
                foreach (var subFolder in subFolders)
                {
                    var folder = new Folder(subFolder);
                    var subFolderDocFileNames = folder.GetAllDocumentFileNames(docExtensions, getSubfolderDocFileNames);
                    fileNames.AddRange(subFolderDocFileNames);
                }
            }

            var docFileNames = GetDocumentFileNames(docExtensions);
            if (docFileNames != null && docFileNames.Count > 0)
            {
                fileNames.AddRange(docFileNames);
            }

            return fileNames;
        }
    }
}
