using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Models
{
    public class Register
    {


        [Required]
        [StringLength(10, ErrorMessage = "The name cannot be more than 10 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please select Gender")]
        public string Gender { get; set; }
        [Required]
        [StringLength(500,ErrorMessage = "The Address cannot be more than 500 characters")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please select Country")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Please select State")]
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Please select City")]
        [Display(Name = "City")]
        public int cityId { get; set; }
        [Required(ErrorMessage = "Please select PostalCode")]
        [Display(Name = "PostalCode")]
        public int ZipId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter,and one special character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter confirmPassword")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter,and one special character.")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }

    }
}
