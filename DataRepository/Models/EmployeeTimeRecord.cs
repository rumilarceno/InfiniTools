using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataRepository.Models
{
    public class EmployeeTimeRecord
    {
        [Required, Range(1, 999999999)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int StartTimeMinutes { get; set; }
        public int EndTimeMinutes { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public string Task { get; set; }
    }
}
