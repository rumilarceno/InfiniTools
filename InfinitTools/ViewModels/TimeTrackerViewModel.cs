using InfinitTools.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfinitTools.ViewModels
{
    public class TimeTrackerViewModel : INotifyPropertyChanged
    {
        private const string DATE_FORMAT = "MM/dd/yyyy";
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

        private void OnOpenCalendarCommandHandler()
        {
            IsCalendarVisible = true;   
        }

        private void OnSubmitTaskTimeCommandHandler()
        {
           
        }

        private void OnTodayCommandHandler()
        {
            SelectedDate = DateTime.Today;
        }

        private void OnNextDateCommandHandler()
        {
            SelectedDate = SelectedDate.AddDays(1);
        }

        private void OnPreviousDateCommandHandler()
        {
            SelectedDate = SelectedDate.AddDays(-1);
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
    }
}
