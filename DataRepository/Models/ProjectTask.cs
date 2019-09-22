using System.ComponentModel.DataAnnotations.Schema;

namespace DataRepository.Models
{
    public class ProjectTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
