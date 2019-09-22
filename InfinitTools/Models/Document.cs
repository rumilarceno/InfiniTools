using System;
using System.IO;

namespace InfinitTools.Models
{
    class Document : DirectoryObject
    {
        private string _fileName = string.Empty;
        public string Filename
        {
            get
            {
                if (String.IsNullOrEmpty(_fileName))
                {
                    _fileName = Path.GetFileNameWithoutExtension(FullDirectoryName);
                }
                return _fileName;
            }
        }

        private string _extension = string.Empty;
        public string Extension
        {
            get
            {
                if (String.IsNullOrEmpty(_extension))
                {
                    _extension = Path.GetExtension(FullDirectoryName);
                }
                return _extension;
            }
        }

        private string _fullFIleName = string.Empty;
        public string FullFileName
        {
            get
            {
                if (String.IsNullOrEmpty(_fullFIleName))
                {
                    _fullFIleName = Path.GetFileName(FullDirectoryName);
                }
                return _fullFIleName;
            }
        }

        public string FullDirectoryName { get; set; }
    }
}
