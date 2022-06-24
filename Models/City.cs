using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Models
{
    public class City
    {
        [Key]
        public int cityId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int StateId { get; set; }
    }
}
