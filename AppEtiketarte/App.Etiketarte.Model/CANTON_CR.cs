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
    
    public partial class CANTON_CR : Common.ModelBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CANTON_CR()
        {
            this.Ordens = new HashSet<Orden>();
            this.DISTRITO_CR = new HashSet<DISTRITO_CR>();
        }
    
        public short codigo_canton { get; set; }
        public short codigo_provincia { get; set; }
        public string nombre_canton { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orden> Ordens { get; set; }
        public virtual PROVINCIA_CR PROVINCIA_CR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DISTRITO_CR> DISTRITO_CR { get; set; }
    }
}
