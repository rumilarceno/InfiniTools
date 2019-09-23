using DataRepository.DBContexts;
using DataRepository.Interfaces;
using DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataRepository.Repositories
{
    public class TimeTrackerRepository : ITimeTrackerRepository
    {
        private TimeTrackerDbContext _timeTrackerDbContext = null;
        public TimeTrackerRepository()
        {
            _timeTrackerDbContext = new TimeTrackerDbContext();
        }
        public List<EmployeeTimeRecord> GetEmployeeTimeRecords(int employeeID, DateTime datetTime)
        {
            return _timeTrackerDbContext.EmployeeTimeRecord.Where(e => e.EmployeeID == employeeID)
                                                           .Where(f => f.Date.Year == datetTime.Year && f.Date.Month == datetTime.Month && f.Date.Day == datetTime.Day)
                                                           .ToList();
        }

        public int PostEmployeeTimeRecord(EmployeeTimeRecord employeeTimeRecord)
        {
            _timeTrackerDbContext.EmployeeTimeRecord.Add(employeeTimeRecord);
            _timeTrackerDbContext.SaveChanges();
            return 1;
        }
    }
}
