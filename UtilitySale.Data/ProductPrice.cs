//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UtilitySale.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductPrice
    {
        public int ProductPriceID { get; set; }
        public int ProductID { get; set; }
        public Nullable<long> ProductPriceBuy { get; set; }
        public Nullable<long> ProductPriceSell { get; set; }
        public string ProductPriceDesc { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
