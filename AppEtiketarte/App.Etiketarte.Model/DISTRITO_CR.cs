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
    
    public partial class DISTRITO_CR : Common.ModelBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DISTRITO_CR()
        {
            this.Ordens = new HashSet<Orden>();
        }
    
        public int codigo_distrito { get; set; }
        public short codigo_canton { get; set; }
        public string nombre_distrito { get; set; }
    
        public virtual CANTON_CR CANTON_CR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orden> Ordens { get; set; }
    }
}
