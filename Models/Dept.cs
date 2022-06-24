using System.ComponentModel.DataAnnotations;

namespace HRMS_project.Models
{
    public class Dept
    {
        [Key]
        public int DeptId { get; set; }
        [Required]
        public string DeptName { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
