using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMS_Portal.Models
{
    public class SDept
    {
        [Key]
        [Required(ErrorMessage = "Enter Sub-Department")]
        public int SDeptID { get; set; }

        public int DeptID { get; set; }
      
     
        [Required(ErrorMessage = "Enter Sub-Department")]
       
        [StringLength(20)]
        public string SubDepartment { get; set; }
       
    }
}
