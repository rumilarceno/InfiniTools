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

        public Project GetProject(int projectId)
        {
            return _timeTrackerDbContext.Project.Where(p => p.ID == projectId).FirstOrDefault();
        }

        public List<ProjectTask> GetProjectTasks(int projectId)
        {
            return _timeTrackerDbContext.ProjectTasks.Where(pt => pt.Project.ID == projectId).ToList();
        }

        public int PostEmployeeTimeRecord(EmployeeTimeRecord employeeTimeRecord)
        {
            var count = _timeTrackerDbContext.EmployeeTimeRecord.Count();
            employeeTimeRecord.ID = ++count;
            _timeTrackerDbContext.EmployeeTimeRecord.Add(employeeTimeRecord);
            _timeTrackerDbContext.SaveChanges();
            return 1;
        }

        public int PostProjectTasks(ProjectTask projectTask)
        {
            var count = _timeTrackerDbContext.ProjectTasks.Count();
            projectTask.ID = ++count;
            _timeTrackerDbContext.ProjectTasks.Add(projectTask);
            _timeTrackerDbContext.SaveChanges();
            return 1;
        }
    }
}
