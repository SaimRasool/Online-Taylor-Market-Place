using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace OTM.Models
{
    public class MdlProduct
    {
          [Key]
        public int ID { get; set; }

        public int Category { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        public string Thumbnail { get; set; }

        [Required]
        public string Description { get; set; }

        [NotMapped]
        public int Quantity { get; set; }

        public List<MdlCategory> CategoryList { get; set; }


        public MdlProduct()
        {
            Quantity = 1;
        }
    }
}