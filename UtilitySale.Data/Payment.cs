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
    
    public partial class Payment
    {
        public int PaymentID { get; set; }
        public int peopleID { get; set; }
        public long PaymentPay { get; set; }
        public string Date { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentSave { get; set; }
    
        public virtual People People { get; set; }
    }
}
