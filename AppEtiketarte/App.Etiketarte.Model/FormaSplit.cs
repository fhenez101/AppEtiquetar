//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App.Etiketarte.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class FormaSplit : Common.ModelBase
    {
        public int IdFormaSplit { get; set; }
        public int IdForma { get; set; }
        public string Nombre { get; set; }
    
        public virtual Forma Forma { get; set; }
    }
}