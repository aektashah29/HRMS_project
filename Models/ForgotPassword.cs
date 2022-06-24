using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Models
{
    public class ForgotPassword
    {
        [Key]
        [Required]
        [EmailAddress]
        [Display(Name ="Registered Email address")]
        public string Email { get; set; }



    }
}
