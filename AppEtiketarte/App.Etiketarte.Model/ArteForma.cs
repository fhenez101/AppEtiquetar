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
    
    public partial class ArteForma : Common.ModelBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArteForma()
        {
            this.DetalleColors = new HashSet<DetalleColor>();
            this.DetalleTipografias = new HashSet<DetalleTipografia>();
            this.InputTexts = new HashSet<InputText>();
        }
    
        public int IdArteForma { get; set; }
        public int IdConfEtiketa { get; set; }
        public int IdArte { get; set; }
        public int IdForma { get; set; }
        public string TextTop { get; set; }
        public string TextLeft { get; set; }
        public string TextAlign { get; set; }
        public int NumeroLineas { get; set; }
        public int CaracteresLinea { get; set; }
        public int FontMinSize { get; set; }
        public int FontMaxSize { get; set; }
        public string ContainerWidth { get; set; }
    
        public virtual Arte Arte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleColor> DetalleColors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleTipografia> DetalleTipografias { get; set; }
        public virtual ConfEtiketa ConfEtiketa { get; set; }
        public virtual Forma Forma { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InputText> InputTexts { get; set; }
    }
}
