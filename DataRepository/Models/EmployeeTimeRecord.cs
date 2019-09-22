using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataRepository.Models
{
    public class EmployeeTimeRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int StartTimeMinutes { get; set; }
        public int EndTimeMinutes { get; set; }
        public DateTime Date { get; set; }
    }
}
