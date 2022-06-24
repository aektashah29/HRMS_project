using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Please select Gender")]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        [Display(Name = "City")]
        public int cityId { get; set; }

        [Display(Name = "PostalCode")]
        public int ZipId { get; set; }
      
        

    }
}
