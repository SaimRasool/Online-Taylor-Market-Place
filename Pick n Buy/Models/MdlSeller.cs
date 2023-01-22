using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OTM.Models
{
    public class MdlSeller
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Comapny { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string About { get; set; }
        [Required]
        public string Logo { get; set; }
    }
}