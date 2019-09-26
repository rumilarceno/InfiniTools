using InfinitTools.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataRepository.Interfaces;
using DataRepository.Models;
using System.Collections.ObjectModel;
using InfinitTools.Models;
using System.Linq;

namespace InfinitTools.ViewModels
{
    public class GuestTrackerViewModel : INotifyPropertyChanged
    {
        private IGuestTrackerRepository _guestTrackerRepository = null;
        public GuestTrackerViewModel(IGuestTrackerRepository guestTrackerRepository)
        {
            _guestTrackerRepository = guestTrackerRepository;
            GuestList = ConvertGuestRecords(_guestTrackerRepository.GetGuests(DateTime.Now));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _firstName = string.Empty;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _timeOut = string.Empty;
        public string TimeOut
        {
            get
            {
                return _timeOut;
            }
            set
            {
                _timeOut = value;
                OnPropertyChanged();
            }
        }

        private string _idNumber = string.Empty;
        public string IdNumber
        {
            get
            {
                return _idNumber;
            }
            set
            {
                if (!_idNumber.Equals(value))
                {
                    _idNumber = value;

                    if (!String.IsNullOrEmpty(_idNumber))
                    {
                        Guest guest = new Guest()
                        {
                            TimeIn = GetDateTimeFromStrHour(SelectedGuestRecord.TimeIn).Value,
                            TimeOut = GetDateTimeFromStrHour(SelectedGuestRecord.TimeOut),
                            Purpose = SelectedGuestRecord.Purpose,
                            ContactPerson = SelectedGuestRecord.ContactPerson,
                            FirstName = SelectedGuestRecord.FirstName,
                            LastName = SelectedGuestRecord.LastName,
                            IdNumber = SelectedGuestRecord.IdNumber
                        };
                        _guestTrackerRepository.UpdateGuest(guest);
                    }
                    OnPropertyChanged();
                }
                
            }
        }

        private GuestRecord _selectedGuestRecord = null;
        public GuestRecord SelectedGuestRecord
        {
            get
            {
                return _selectedGuestRecord;
            }
            set
            {
                _selectedGuestRecord = value;
                OnPropertyChanged();
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _contact = string.Empty;
        public string Contact
        {
            get
            {
                return _contact;
            }
            set
            {
                _contact = value;
                OnPropertyChanged();
            }
        }

        private string _purpose = string.Empty;
        public string Purpose
        {
            get
            {
                return _purpose;
            }
            set
            {
                _purpose = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GuestRecord> _guestList = null;
        public ObservableCollection<GuestRecord> GuestList
        {
            get
            {
                return _guestList = _guestList ?? new ObservableCollection<GuestRecord>();
            }
            set
            {
                _guestList = value;
                OnPropertyChanged();
            }
        }

        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                return _submitCommand = _submitCommand ?? new CommandHandler(OnSubmitCommandHandler, true);
            }
        }

        private void OnSubmitCommandHandler()
        {
            Guest guest = new Guest()
            {
                ContactPerson = Contact,
                FirstName = FirstName,
                LastName = LastName,
                Purpose = Purpose,
                TimeIn = DateTime.Now
            };

            _guestTrackerRepository.PostGuest(guest);
        }

        private ICommand _updateGuestsCommand;
        public ICommand UpdateGuestsCommand
        {
            get
            {
                return _updateGuestsCommand = _updateGuestsCommand ?? new CommandHandler(OnUpdateGuestsCommandHandler, true);
            }
        }


        private void OnUpdateGuestsCommandHandler()
        {
            GuestList = ConvertGuestRecords(_guestTrackerRepository.GetGuests(DateTime.Now));
        }

        private ObservableCollection<GuestRecord> ConvertGuestRecords(List<Guest> guests)
        {
            guests = guests.OrderByDescending(g => g.TimeIn).ToList();
            ObservableCollection<GuestRecord> guestRecords = new ObservableCollection<GuestRecord>();
            foreach (var guest in guests)
            {
                GuestRecord guestRecord = new GuestRecord()
                {
                    FirstName = guest.FirstName,
                    LastName = guest.LastName,
                    Purpose = guest.FirstName,
                    ContactPerson = guest.ContactPerson,
                    IdNumber = guest.IdNumber,
                    TimeIn = guest.TimeIn.ToString("HHmm"),
                    TimeOut = guest.TimeOut.HasValue ? guest.TimeIn.ToString("HHmm") : string.Empty,
                };

                guestRecords.Add(guestRecord);
            }
            return guestRecords;
        }

        private DateTime? GetDateTimeFromStrHour(string time)
        {
            int val = 0;
            if (int.TryParse(time, out val))
            {
                var hour = (int)(val / 100);
                var minutes = val % 100;
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minutes, 0);
            }
            return null;
        }
    }
}
