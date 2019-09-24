using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataRepository.Models
{
    public class Project
    {
        [Required, Range(1, 999999999)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
    }
}
