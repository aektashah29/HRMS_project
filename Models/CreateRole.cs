using System.ComponentModel.DataAnnotations;

namespace HRMS_project.Models
{
    public class CreateRole
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
