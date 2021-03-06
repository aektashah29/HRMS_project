using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS_project.Models
{
    public class onCheckpoint
    {
        [Key]
       
        public int OnCheckpointId { get; set; }
       
        [Required(ErrorMessage = "please enter CheckpointName")]
        [StringLength(20, ErrorMessage = "The CheckpointName cannot be more than 20 characters")]

        public string CheckpointName { get; set;}
       
        [Required(ErrorMessage = "please enter Businessunit")]
        public int BUnitID { get; set; }
        [Required(ErrorMessage = "please enter Department")]
        public int DeptId { get; set; }
        [Required(ErrorMessage = "please enter Assignee")]
        public int AssigneeId { get; set; }
        [Required(ErrorMessage = "please enter Description")]
        [StringLength(100, ErrorMessage = "The Description cannot be more than 100 characters")]
        public string Description { get; set; }

       
        [NotMapped]
        public string Businessunit { get; set; }
        [NotMapped]
        public string DeptName { get; set; }
        [NotMapped]
        public string AssigneeName { get; set; }
    }
}
