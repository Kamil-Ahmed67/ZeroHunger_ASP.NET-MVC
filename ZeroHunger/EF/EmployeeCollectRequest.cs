//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZeroHunger.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeCollectRequest
    {
        public int Id { get; set; }
        public int CollectRequet_Id { get; set; }
        public int EmpId { get; set; }
    
        public virtual CollectRequest CollectRequest { get; set; }
        public virtual Emplyee Emplyee { get; set; }
    }
}
