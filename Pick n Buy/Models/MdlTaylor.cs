using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OTM.Models
{
    public class MdlTaylor
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string About { get; set; }
        
        [Display(Name = "Profile Image")]
        public string Image { get; set; }
        [Required]
        [Display(Name = "Service 1")]
        public string Skill1 { get; set; }
        [Required]
        [Display(Name = "Service 2")]
        public string Skill2 { get; set; }
        [Required]
        [Display(Name = "Service 3")]
        public string Skill3 { get; set; }
        [Required]
        [Display(Name = "Service 1 Description")]
        public string S1Des { get; set; }
        [Required]
        [Display(Name = "Service 2 Description")]
        public string S2Des { get; set; }
        [Required]
        [Display(Name = "Service 3 Description")]
        public string S3Des { get; set; }
     
        public string S1Image { get; set; }
  
        public string S2Image { get; set; }

        public string S3Image { get; set; }

        public MdlSeller Seller { get; set; }
}
}