using DataRepository.DBContexts;
using DataRepository.Interfaces;
using DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataRepository.Repositories
{
    public class GuestTrackerRepository : IGuestTrackerRepository
    {
        private GuestTrackerDbContext _dbContext = null;
        public GuestTrackerRepository()
        {
            _dbContext = new GuestTrackerDbContext();
        }
        public Guest GetGuest(int guestId)
        {
            return _dbContext.Guests.Where(g => g.ID == guestId).FirstOrDefault();
        }

        public List<Guest> GetGuests(DateTime dateTime)
        {
            return _dbContext.Guests.Where(g => g.TimeIn.Year.Equals(dateTime.Year) &&
                                                g.TimeIn.Month.Equals(dateTime.Month) &&
                                                g.TimeIn.Day.Equals(dateTime.Day)).ToList();
        }

        public int PostGuest(Guest guest)
        {
            var count = _dbContext.Guests.Count();
            guest.ID = ++count;
            _dbContext.Guests.Add(guest);
            _dbContext.SaveChanges();
            return 1;
        }

        public int UpdateGuest(Guest guest)
        {
            var dbGuest = GetGuest(guest.ID);
            dbGuest.LastName = guest.LastName;
            dbGuest.FirstName = guest.FirstName;
            dbGuest.ContactPerson = guest.ContactPerson;
            dbGuest.Purpose = guest.Purpose;
            dbGuest.TimeIn = guest.TimeIn;
            dbGuest.TimeOut = guest.TimeOut;
            dbGuest.IdNumber = guest.IdNumber;
            _dbContext.SaveChanges();
            return 1;
        }
    }
}
