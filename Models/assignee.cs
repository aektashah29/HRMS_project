using System.ComponentModel.DataAnnotations;

namespace HRMS_project.Models
{
    public class assignee
    {
        [Key]
        public int AssigneeId { get; set; }
        [Required]
        [StringLength(20)]
        public string AssigneeName { get; set;}
    }
}
