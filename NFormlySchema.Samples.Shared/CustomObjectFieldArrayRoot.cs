using System;
using System.ComponentModel.DataAnnotations;

namespace NFormlySchema.Samples.Shared
{

    public class CustomObjectFieldArrayRoot : CustomObjectFieldArrayRoot<FieldArrayItem[]> { }

    /// <summary>
    /// Made this generic incase adding support for different types of arrays/enumerables/etc
    /// Can just change the type of the array and run the test case
    /// </summary>
    /// <typeparam name="FieldArrayType">Collection/Array/Enumerable for the "collection property"</typeparam>
    public class CustomObjectFieldArrayRoot<FieldArrayType>
    {
        [Display(Name = "Purchase Order Ref")]
        public string PurchaseOrderRef { get; set; }
        [Display(Name = "Purchase Timestamp")]
        public DateTime PurchaseTimestamp { get; set; }
        [Display(Name = "Ordered Items")]
        [Wrapper("panel")]
        public FieldArrayType OrderedItems { get; set; }


    }

}
