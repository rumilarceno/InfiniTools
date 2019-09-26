using DataRepository.Models;
using DataRepository.Repositories;
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
            Employee emp = new Employee();
            emp.ID = 4;
            emp.Project = new Project();
            emp.Project.ID = 1;

            timeTrackerViewModel = new TimeTrackerViewModel(new TimeTrackerRepository(), emp);
            DataContext = timeTrackerViewModel;
        }
    }
}
