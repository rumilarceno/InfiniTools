using DataRepository.Models;
using System.Collections.Generic;

namespace DataRepository.Interfaces
{
    public interface ITimeTrackerRepository
    {
        List<EmployeeTimeRecord> GetEmployeeTimeRecords(int employeeID);
        int PostEmployeeTimeRecord(EmployeeTimeRecord employeeTimeRecord);
    }
}
