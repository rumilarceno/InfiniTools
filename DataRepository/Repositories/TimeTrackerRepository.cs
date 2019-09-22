using DataRepository.DBContexts;
using DataRepository.Interfaces;
using DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Repositories
{
    public class TimeTrackerRepository : ITimeTrackerRepository
    {
        private TimeTrackerDbContext _timeTrackerDbContext = null;
        public TimeTrackerRepository()
        {
            _timeTrackerDbContext = new TimeTrackerDbContext();
        }
        public List<EmployeeTimeRecord> GetEmployeeTimeRecords(int employeeID)
        {
            return _timeTrackerDbContext.EmployeeTimeRecord.Where(e => e.EmployeeID == employeeID).ToList();
        }

        public int PostEmployeeTimeRecord(EmployeeTimeRecord employeeTimeRecord)
        {

            //_timeTrackerDbContext.EmployeeTimeRecord.Add()
            return 0;
        }
    }
}
