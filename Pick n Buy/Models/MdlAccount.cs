using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OTM.Models
{
    public class MdlAccount
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Required( ErrorMessage = "Email is Required")]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must include at least one upper case letter, one lower case letter, and one numeric digit and one special chracter.")]
        [DataType(DataType.Password)]

        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Mobile Phone")]
        public long Phone { get; set; }

        public int IsTaylor { get; set; }

        public bool UserType { get; set; }


        public System.Guid ActivationCode { get; set; }
    }
}