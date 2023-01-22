using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OTM.Models
{
    public class MdlOrder
    {
        [Key]
        public int SID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        [Required]
        public double Total { get; set; }
    }
}