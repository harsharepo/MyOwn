//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VedicaModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TreatmentDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desription { get; set; }
        public int TreatmentsId { get; set; }
        public string ImageId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> OrderId { get; set; }
        public string Visibility { get; set; }
    
        public virtual ImageFiles ImageFiles { get; set; }
        public virtual Treatments Treatments { get; set; }
    }
}
