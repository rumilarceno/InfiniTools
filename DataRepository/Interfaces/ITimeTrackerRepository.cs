using DataRepository.Models;
using System;
using System.Collections.Generic;

namespace DataRepository.Interfaces
{
    public interface ITimeTrackerRepository
    {
        List<EmployeeTimeRecord> GetEmployeeTimeRecords(int employeeID, DateTime date);
        int PostEmployeeTimeRecord(EmployeeTimeRecord employeeTimeRecord);
    }
}
