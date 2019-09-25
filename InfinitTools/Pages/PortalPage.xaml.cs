using System;
using System.Windows;
using System.Windows.Controls;

namespace InfinitTools.Pages
{
    /// <summary>
    /// Interaction logic for PortalPage.xaml
    /// </summary>
    public partial class PortalPage : Page
    {
        public PortalPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/ToolsPage.xaml", UriKind.Relative));
        }
    }
}
