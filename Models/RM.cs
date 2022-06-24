using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_Portal.Models
{
    public class RM
    {
        [Key]
        [Required(ErrorMessage = "Enter Reporting Manager")]
        public int RMID { get; set; }

        [Required(ErrorMessage = "Enter Reporting Manager")]
        
        [StringLength(20)]
        public string ReportingManager { get; set; }
    }
}
