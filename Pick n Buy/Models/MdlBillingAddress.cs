using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OTM.Models
{
    public class MdlBillingAddress
    {
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Mobile Phone")]
        public int Phone { get; set; }

        [Required]
        [Display(Name = "Billing Address Same As Shipping ")]
        public bool IsBilling { get; set; }

        public MdlAccount User { get; set; }
    }
}