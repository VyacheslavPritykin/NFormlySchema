using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NFormlySchema.Samples.Shared
{
    public class FieldArrayItem
    {
        [Display(Name = "Item Key")]
        public string ItemKey { get; set; }
        [Display(Name = "Item Price")]
        public decimal ItemPrice { get; set; }
        [Display(Name = "Item Quantity")]
        public int ItemQuantity { get; set; }

    }
}
