using DataRepository.Interfaces;
using DataRepository.Models;
using InfinitTools.Commands;
using InfinitTools.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace InfinitTools.ViewModels
{
    public class TimeTrackerViewModel : INotifyPropertyChanged
    {
        private const int HOUR_MINUTES = 60;
        private const int HALF_DAY_MINUTES = 1200;
        private ITimeTrackerRepository _timeTrackerRepository = null;
        private Employee _employee = null;

        public TimeTrackerViewModel(ITimeTrackerRepository timeTrackerRepository, Employee employee)
        {
            _timeTrackerRepository = timeTrackerRepository;
            _employee = employee;
            RecordedList = ConvertRecords(_timeTrackerRepository.GetEmployeeTimeRecords(employee.ID, DateTime.Now));
            TaskList = new ObservableCollection<string>(_timeTrackerRepository.GetProjectTasks(_employee.Project.ID).Select(pt => pt.Name));
        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _previousDateCommand = null;
        public ICommand PreviousDateCommand
        {
            get
            {
                return _previousDateCommand = _previousDateCommand ?? new CommandHandler(OnPreviousDateCommandHandler, true);
            }
        }

        private ICommand _nextDateCommand = null;
        public ICommand NextDateCommand
        {
            get
            {
                return _nextDateCommand = _nextDateCommand ?? new CommandHandler(OnNextDateCommandHandler, true);
            }
        }

        private ICommand _todayCommand = null;
        public ICommand TodayCommand
        {
            get
            {
                return _todayCommand = _todayCommand ?? new CommandHandler(OnTodayCommandHandler, true);
            }
        }

        private ICommand _submitTaskTimeCommand = null;
        public ICommand SubmitTaskTimeCommand
        {
            get
            {
                return _submitTaskTimeCommand = _submitTaskTimeCommand ?? new CommandHandler(OnSubmitTaskTimeCommandHandler, true);
            }
        }

        private ICommand _openCalendarCommand = null;
        public ICommand OpenCalendarCommand
        {
            get
            {
                return _openCalendarCommand = _openCalendarCommand ?? new CommandHandler(OnOpenCalendarCommandHandler, true);
            }
        }

        public int RecordTimeCount
        {
            get
            {
                return RecordedList.Count;
            }
        }

        private void OnOpenCalendarCommandHandler()
        {
            IsCalendarVisible = true;   
        }

        private void OnSubmitTaskTimeCommandHandler()
        {
            EmployeeTimeRecord employeeTimeRecord = new EmployeeTimeRecord();
            employeeTimeRecord.Date = SelectedDate;
            employeeTimeRecord.Comments = Comments;
            employeeTimeRecord.EmployeeID = _employee.ID;
            employeeTimeRecord.Task = TaskName;

            var projectTasks = _timeTrackerRepository.GetProjectTasks(_employee.Project.ID);

            bool containsSelectedTask = false;
            foreach (var item in projectTasks)
            {
                if (item.Name.Equals(TaskName))
                {
                    containsSelectedTask = true;
                    break;
                }
            }
            if (!containsSelectedTask)
            {
                ProjectTask projectTask = new ProjectTask();
                projectTask.Name = TaskName;
                projectTask.Project = _timeTrackerRepository.GetProject(_employee.Project.ID);
                _timeTrackerRepository.PostProjectTasks(projectTask);
            }

            int startTimeValueMinutes = 0;
            if (int.TryParse(StartTimeValue, out startTimeValueMinutes))
            {
                employeeTimeRecord.StartTimeMinutes = startTimeValueMinutes;
            }

            int endTimeValueMinutes = 0;
            if (int.TryParse(EndTimeValue, out endTimeValueMinutes))
            {
                employeeTimeRecord.EndTimeMinutes = endTimeValueMinutes;
            }

            _timeTrackerRepository.PostEmployeeTimeRecord(employeeTimeRecord);

            RecordedList.Clear();
            TaskList.Clear();
            RecordedList = ConvertRecords(_timeTrackerRepository.GetEmployeeTimeRecords(_employee.ID, SelectedDate));
            TaskList = new ObservableCollection<string>(_timeTrackerRepository.GetProjectTasks(_employee.Project.ID).Select(pt => pt.Name));
        }

        private void OnTodayCommandHandler()
        {
            SelectedDate = DateTime.Today;
            RecordedList.Clear();
            RecordedList = ConvertRecords(_timeTrackerRepository.GetEmployeeTimeRecords(_employee.ID, SelectedDate));
        }

        private void OnNextDateCommandHandler()
        {
            SelectedDate = SelectedDate.AddDays(1);
            RecordedList.Clear();
            RecordedList = ConvertRecords(_timeTrackerRepository.GetEmployeeTimeRecords(_employee.ID, SelectedDate));
        }

        private void OnPreviousDateCommandHandler()
        {
            SelectedDate = SelectedDate.AddDays(-1);
            RecordedList.Clear();
            RecordedList = ConvertRecords(_timeTrackerRepository.GetEmployeeTimeRecords(_employee.ID, SelectedDate));
        }

        private bool _isCalendarVisible = false;
        public bool IsCalendarVisible
        {
            get
            {
                return _isCalendarVisible;
            }
            set
            {
                _isCalendarVisible = value;
                OnPropertyChanged();
            }
        }

        private string _startTimeValue = string.Empty;
        public string StartTimeValue
        {
            get
            {
                return _startTimeValue;
            }
            set
            {
                _startTimeValue = value;
                OnPropertyChanged();
            }
        }

        private string _endTimeValue = string.Empty;
        public string EndTimeValue
        {
            get
            {
                return _endTimeValue;
            }
            set
            {
                _endTimeValue = value;
                OnPropertyChanged();
            }
        }

        private string _taskName = string.Empty;
        public string TaskName
        {
            get
            {
                return _taskName;
            }
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _taskList = null;
        public ObservableCollection<string> TaskList
        {
            get
            {
                return _taskList = _taskList ?? new ObservableCollection<string>();
            }
            set
            {
                _taskList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TimeRecord> _recordedList = null;
        public ObservableCollection<TimeRecord> RecordedList
        {
            get
            {
                return _recordedList = _recordedList ?? new ObservableCollection<TimeRecord>();
            }
            set
            {
                _recordedList = value;
                OnPropertyChanged();
            }
        }

        private string _comments = string.Empty;
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDate = DateTime.MinValue;
        public DateTime SelectedDate
        {
            get
            {
                if (_selectedDate == DateTime.MinValue)
                {
                    _selectedDate = DateTime.Now;
                }
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TimeRecord> ConvertRecords(List<EmployeeTimeRecord> employeeTimeRecords)
        {
            ObservableCollection<TimeRecord> timeRecords = new ObservableCollection<TimeRecord>();
            foreach (var record in employeeTimeRecords)
                timeRecords.Add(ExtractTimeRecord(record));
            return timeRecords;
        }

        private TimeRecord ExtractTimeRecord(EmployeeTimeRecord employeeTimeRecord)
        {
            TimeRecord tr = new TimeRecord();
            tr.Comments = employeeTimeRecord.Comments;

            tr.StartTimeRec = GetTimeString(employeeTimeRecord.StartTimeMinutes);
            tr.EndTimeRec = GetTimeString(employeeTimeRecord.EndTimeMinutes);
            tr.Task = employeeTimeRecord.Task;
            return tr;
        }

        private string GetTimeString(int minutes)
        {
            var isPM = (decimal)((double)minutes / (double)HALF_DAY_MINUTES) > 1;

            int hour = 0;
            int remainingMinutes = 0;
            if (isPM)
            {
                var minutesPastNoon = minutes - HALF_DAY_MINUTES;
                hour = (int)Math.Round((double)(minutesPastNoon / 100));
            }
            else
            {
                hour = (int)Math.Round((double)(minutes / 100));
            }
            remainingMinutes = minutes % 100;

            var meriDian = isPM ? "PM" : "AM";
            return $"{hour.ToString().PadLeft(2)}:{remainingMinutes.ToString("00")} {meriDian}";
        }

        //private int ParseTimeToMinutes(string dayInMinutes)
        //{
        //    int startTimeValueMinutes = 0;
        //    if (int.TryParse(dayInMinutes, out startTimeValueMinutes))
        //    {
        //        startTimeValueMinutes / 60;
        //    }
        //}
    }
}
