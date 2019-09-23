﻿using DataRepository.Repositories;
using InfinitTools.ViewModels;
using System.Windows;

namespace InfinitTools.Windows
{
    /// <summary>
    /// Interaction logic for TimeTrackerWindow.xaml
    /// </summary>
    public partial class TimeTrackerWindow : Window
    {
        private TimeTrackerViewModel timeTrackerViewModel;
        public TimeTrackerWindow()
        {
            InitializeComponent();
            timeTrackerViewModel = new TimeTrackerViewModel(new TimeTrackerRepository(), new DataRepository.Models.Employee());
            DataContext = timeTrackerViewModel;
        }
    }
}
