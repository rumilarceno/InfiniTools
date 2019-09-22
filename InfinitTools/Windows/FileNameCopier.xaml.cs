using InfinitTools.ViewModels;
using System;
using System.Windows;
using forms = System.Windows.Forms;

namespace InfinitTools.Windows
{
    /// <summary>
    /// Interaction logic for FileNameCopier.xaml
    /// </summary>
    public partial class FileNameCopier : Window
    {
        private FilenameCopierViewModel _fileNameCopierViewModel = null;
        public FileNameCopier()
        {
            InitializeComponent();
            _fileNameCopierViewModel = new FilenameCopierViewModel();
            _fileNameCopierViewModel.BrowseFolderEvent += OnBrowseFolderEventHandler;
            _fileNameCopierViewModel.ShowDialogMessage += OnShowDialogMessageHandler;
            DataContext = _fileNameCopierViewModel;
        }

        private void OnShowDialogMessageHandler(string message)
        {
            MessageBox.Show(this, message);
        }

        private void OnBrowseFolderEventHandler(object sender, EventArgs e)
        {
            forms.FolderBrowserDialog fbd = new forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == forms.DialogResult.OK)
            {
                _fileNameCopierViewModel.FolderPath = fbd.SelectedPath;
            }
            _fileNameCopierViewModel.Copied = false;
        }
    }
}
