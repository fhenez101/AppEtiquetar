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
    
    public partial class LogoEtiketa : Common.ModelBase
    {
        public int IdLogoEtiketa { get; set; }
        public int IdEtiketa { get; set; }
        public int IdLogo { get; set; }
        public bool Estado { get; set; }
    
        public virtual Etiketa Etiketa { get; set; }
        public virtual Logo Logo { get; set; }
    }
}
