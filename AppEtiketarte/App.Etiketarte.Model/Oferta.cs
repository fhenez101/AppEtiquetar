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
    
    public partial class Oferta : Common.ModelBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oferta()
        {
            this.DetalleOfertas = new HashSet<DetalleOferta>();
        }
    
        public int IdOferta { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public byte[] RowVersion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleOferta> DetalleOfertas { get; set; }
    }
}
