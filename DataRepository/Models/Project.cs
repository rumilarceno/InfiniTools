using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataRepository.Models
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
    }
}
