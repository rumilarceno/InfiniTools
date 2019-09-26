using InfinitTools.Commands;
using InfinitTools.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace InfinitTools.ViewModels
{
    public delegate void MessageBoxHandler(string message);
    class FilenameCopierViewModel : INotifyPropertyChanged
    {
        public event EventHandler BrowseFolderEvent;
        public event MessageBoxHandler ShowDialogMessage;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnShowDialogMessage(string message)
        {
            ShowDialogMessage?.Invoke(message);
        }

        public bool IncludeChildFolder { get; set; } = true;

        private ObservableCollection<string> _extensionList = null;
        public ObservableCollection<string> ExtensionList
        {
            get
            {
                if (_extensionList == null)
                {
                    _extensionList = new ObservableCollection<string>();
                }
                return _extensionList;
            }
            set
            {
                _extensionList = value;
                OnPropertyChanged();
            }
        }

        private string _folderPath = string.Empty;
        public string FolderPath
        {
            get
            {
                return _folderPath;
            }
            set
            {
                _folderPath = value;
                OnPropertyChanged();
            }
        }

        private bool _copied = false;
        public bool Copied
        {
            get
            {
                return _copied;
            }
            set
            {
                _copied = value;
                OnPropertyChanged();
            }
        }
      
        private bool _copyFileName = true;
        public bool CopyFileName
        {
            get
            {
                return _copyFileName;
            }
            set
            {
                _copyFileName = value;
                OnPropertyChanged();
            }
        }

        private bool _copyExtension = false;
        public bool CopyExtension
        {
            get
            {
                return _copyExtension;
            }
            set
            {
                _copyExtension = value;
                OnPropertyChanged();
            }
        }

        public string ComboBoxText { get; set; }

        private ICommand _addExtensionCommand;
        public ICommand AddExtensionCommand
        {
            get
            {
                return _addExtensionCommand = _addExtensionCommand ?? new CommandHandler(OnAddExtension, true);
            }
        }

        

        private ICommand _removeExtensionCommand;
        public ICommand RemoveExtensionCommand
        {
            get
            {
                return _removeExtensionCommand = _removeExtensionCommand ?? new CommandHandler(OnRemoveExtension, true);
            }
        }

        private ICommand _browseFolderCommand;
        public ICommand BrowseFolderCommand
        {
            get
            {
                return _browseFolderCommand = _browseFolderCommand ?? new CommandHandler(OnBrowseFolder, true);
            }
        }

        private ICommand _copyCommand;
        public ICommand CopyCommand
        {
            get
            {
                return _copyCommand = _copyCommand ?? new CommandHandler(OnCopyDocumentFilenames, true);
            }
        }

        private void OnAddExtension()
        {
            if (!string.IsNullOrEmpty(ComboBoxText) && !ExtensionList.Contains(ComboBoxText))
            {
                ExtensionList.Add(ComboBoxText);
            }
        }

        private void OnRemoveExtension()
        {
            if (!string.IsNullOrEmpty(ComboBoxText) && ExtensionList.Contains(ComboBoxText))
            {
                ExtensionList.Remove(ComboBoxText);
            }
        }

        private void OnBrowseFolder()
        {
            BrowseFolderEvent?.Invoke(null, null);
        }

        private void OnCopyDocumentFilenames()
        {
            var folderPath = FolderPath;
            if (!String.IsNullOrEmpty(folderPath) && ExtensionList.Count > 0)
            {
                List<string> extensions = new List<string>();

                foreach (var item in ExtensionList)
                {
                    extensions.Add(item.ToString());
                }

                Folder folder = new Folder(folderPath);
                var files = folder.GetAllDocumentFileNames(extensions, IncludeChildFolder);


                if (files != null && files.Count() > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var file in files)
                    {
                        var filename = Path.GetFileNameWithoutExtension(file);
                        var extension = Path.GetExtension(file);

                        if (CopyFileName && CopyExtension)
                        {
                            filename += extension;
                        }
                        else if (CopyExtension)
                        {
                            filename = extension.Replace(".", string.Empty);
                        }

                        sb.Append(filename);
                        sb.Append(Environment.NewLine);
                    }

                    Clipboard.SetText(sb.ToString());
                    Copied = true;
                }
                else
                {
                    OnShowDialogMessage("No files found with extension!");
                }
            }
            else
            {
                OnShowDialogMessage("Please select a folder or add extensions.");
            }
        }
    }
}
