using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using HRMS_Portal.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMS.Models
{
    public class RequestModel
    {
       [Key]
        public int EmpID { get; set; }

        [Required(ErrorMessage ="Enter Firstname!!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed!!")]
        [StringLength(10, ErrorMessage = "The Firstname cannot be more than 10 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Enter Lastname")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed!!")]
        [StringLength(10, ErrorMessage = "The Lastname cannot be more than 10 characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Enter Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Mobile No.")]
        [StringLength(10, ErrorMessage = "Wrong mobile")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter Date of Joining")]
        //[DisplayFormat(DataFormatString = "{0:DD/mm/yyyy}", ApplyFormatInEditMode = true)]
      
        public DateTime Doj { get; set; }

        [Required(ErrorMessage = "Enter Business Unit")]
        [Display(Name ="Business Unit")]
       
        public int BUnitID { get; set; }

        [Required(ErrorMessage = "Enter Department")]
        [Display(Name = "Department")]
    
        public int DeptId { get; set; }

      
      
        [Required(ErrorMessage = "Enter SubDepartment")]
        [Display(Name = "SubDepartment")]
        public int SDeptID { get; set; }

        [Required(ErrorMessage = "Enter Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Enter Reporting Manager")]
        [Display(Name = "Reporting Manager")]
       
        public int RMID { get; set; }

        [NotMapped]
        public string Businessunit { get; set; }
        [NotMapped]
        public string DeptName { get; set; }

        [NotMapped]
        public string SubDepartment { get; set; }
        [NotMapped]
        public string ReportingManager { get; set; }
    }
}
