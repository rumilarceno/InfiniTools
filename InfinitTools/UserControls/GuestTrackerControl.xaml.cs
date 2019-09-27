using DataRepository.Repositories;
using InfinitTools.ViewModels;
using System;
using System.Windows.Controls;

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

        private void IdNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            guestTrackerViewModel.IdNumber = ((TextBlock)sender).Text;
        }

        private void Timeout_PreviewLostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                var text = tb.Text;
                if (!String.IsNullOrEmpty(text))
                {
                    guestTrackerViewModel.TimeOut = text;
                }
            }
        }

        private void Idnumber_PreviewLostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                var text = tb.Text;
                if (!String.IsNullOrEmpty(text))
                {
                    guestTrackerViewModel.IdNumber = text;
                }
            }
        }
    }
}
