using DataRepository.Repositories;
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

namespace InfinitTools.UserControls
{
    /// <summary>
    /// Interaction logic for GuestTrackerControl.xaml
    /// </summary>
    public partial class GuestTrackerControl : UserControl
    {
        private GuestTrackerViewModel guestTrackerViewModel = null;
        public GuestTrackerControl()
        {
            InitializeComponent();
            guestTrackerViewModel = new GuestTrackerViewModel(new GuestTrackerRepository());
            DataContext = guestTrackerViewModel;
        }
    }
}
