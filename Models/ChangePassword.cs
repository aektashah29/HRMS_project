using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Models
{
    public class ChangePassword
    {
        [Key]
        [Required, DataType(DataType.Password), Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage ="Confirm new password doen not match")]
        public string ConfirmPassword { get; set; }


    }
}
