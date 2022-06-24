using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMS_project.Models
{
    public class Bunit
    {
        [Key]

        public int BUnitID { get; set; }

        [Required(ErrorMessage = "Enter Business Unit")]
        [StringLength(20)]
        public string Businessunit { get; set; }
        [Required]
        public bool Bunit_status { get; set; }

       
    }
}
