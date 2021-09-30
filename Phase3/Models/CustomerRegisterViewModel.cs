using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phase3.Models
{
    public class CustomerRegisterViewModel
    {
        [Required]
        
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string city { get; set; }

        [Required]
        public string Mobile_number { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password Should be same")]
        public string ConfirmPassword { get; set; }

    }
}
