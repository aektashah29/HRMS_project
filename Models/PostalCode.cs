using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Models
{
    public class PostalCode
    {
        [Key]
        public int ZipId { get; set; }
        [Required]
        [StringLength(20)]
        public string zip { get; set; }

        public int cityId { get; set; }
    }
}
