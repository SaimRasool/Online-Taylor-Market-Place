using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OTM.Models
{
    public class MdlContact
    {
        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Your Email")]
        [EmailAddress]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        [Required]
        public long ContactNo { get; set; }
        [Required]
        public string Message { get; set; }
    }
}