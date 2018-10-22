using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels.Oferta
{
    public class VmDetalleOferta
    {
        public int IdDetalleOferta { get; set; }
        public int IdOferta { get; set; }
        public int IdEtiketa { get; set; }
        public int IdConfEtiketa { get; set; }
        public int IdPresentacion { get; set; }
        public int Cantidad { get; set; }
        public bool Estado { get; set; }
        public IEnumerable<SelectListItem> CmbEtiketas { get; set; }
        public IEnumerable<SelectListItem> CmbConfEtiketas { get; set; }
        public IEnumerable<SelectListItem> CmbPresentaciones { get; set; }
    }
}