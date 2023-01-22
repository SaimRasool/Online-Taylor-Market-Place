using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OTM.Models
{
    public class Mdl_Portfolio
    {
        [Key]
        public int ID { get; set; }
        public string Image { get; set; }
        [Required]
        public string About { get; set; }

        public List<MdlTaylor> TaylorList { get; set; }
    }
}