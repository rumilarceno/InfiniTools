using InfinitTools.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using forms = System.Windows.Forms;

namespace InfinitTools.UserControls
{
    /// <summary>
    /// Interaction logic for FileNameCopierControl.xaml
    /// </summary>
    public partial class FileNameCopierControl : UserControl
    {
        private FilenameCopierViewModel _fileNameCopierViewModel = null;
        public FileNameCopierControl()
        {
            InitializeComponent();
            _fileNameCopierViewModel = new FilenameCopierViewModel();
            _fileNameCopierViewModel.BrowseFolderEvent += OnBrowseFolderEventHandler;
            _fileNameCopierViewModel.ShowDialogMessage += OnShowDialogMessageHandler;
            DataContext = _fileNameCopierViewModel;
        }

        private void OnShowDialogMessageHandler(string message)
        {
            MessageBox.Show(message);
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
