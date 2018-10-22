using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels
{
    public class VmEtiketa
    {
        public int IdEtiketa { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public IEnumerable<SelectListItem> Producto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public byte[] RowVersion { get; set; }
    }
}