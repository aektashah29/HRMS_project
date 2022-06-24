 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
