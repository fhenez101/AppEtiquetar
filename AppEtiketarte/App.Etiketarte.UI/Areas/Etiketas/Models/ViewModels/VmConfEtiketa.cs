using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels
{
    public class VmConfEtiketa
    {
        public int IdConfEtiketa { get; set; }
        public int IdEtiketa { get; set; }
        public string NombreEtiketa { get; set; }
        public int UnidadPorPaquete { get; set; }
        public decimal Precio { get; set; }
        public IEnumerable<SelectListItem> Etiketa { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public byte[] RowVersion { get; set; }
    }
}