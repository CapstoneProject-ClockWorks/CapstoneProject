//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapstoneProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Step
    {
        public int ID { get; set; }
        public int Process_ID { get; set; }
        public string NodeData { get; set; }
    
        public virtual Process Process { get; set; }
    }
}
