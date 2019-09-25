using DataRepository.Models;
using System.Collections.Generic;

namespace DataRepository.Interfaces
{
    public interface IGuestTrackerRepository
    {
        int PostGuest(Guest guest);
        List<Guest> GetGuests();
        Guest GetGuest(int guestId);
        int UpdateGuest(Guest guest);
    }
}
