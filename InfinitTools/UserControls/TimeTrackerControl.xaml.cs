using DataRepository.Models;
using DataRepository.Repositories;
using InfinitTools.ViewModels;
using System.Windows.Controls;

namespace InfinitTools.UserControls
{
    /// <summary>
    /// Interaction logic for TimeTrackerControl.xaml
    /// </summary>
    public partial class TimeTrackerControl : UserControl
    {
        private TimeTrackerViewModel timeTrackerViewModel;
        public TimeTrackerControl()
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
