using DataRepository.Repositories;
using InfinitTools.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataRepository.Interfaces;
using DataRepository.Models;
using System.Collections.ObjectModel;
using InfinitTools.Models;

namespace InfinitTools.ViewModels
{
    public class GuestTrackerViewModel : INotifyPropertyChanged
    {
        private IGuestTrackerRepository _guestTrackerRepository = null;
        public GuestTrackerViewModel(IGuestTrackerRepository guestTrackerRepository)
        {
            _guestTrackerRepository = guestTrackerRepository;
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
            GuestList = ConvertGuestRecords(_guestTrackerRepository.GetGuests());
        }

        private ObservableCollection<GuestRecord> ConvertGuestRecords(List<Guest> guests)
        {
            ObservableCollection<GuestRecord> guestRecords = new ObservableCollection<GuestRecord>();
            foreach (var guest in guests)
            {
                GuestRecord guestRecord = new GuestRecord()
                {
                    FirstName = guest.FirstName,
                    LastName = guest.LastName,
                    Purpose = guest.FirstName,
                    ContactPerson = guest.ContactPerson,
                    TimeIn = guest.TimeIn.ToString("MM/dd HHmm"),
                    TimeOut = guest.TimeOut.HasValue ? guest.TimeIn.ToString("MM/dd HHmm") : string.Empty,
                };

                guestRecords.Add(guestRecord);
            }
            return guestRecords;
        }
    }
}
