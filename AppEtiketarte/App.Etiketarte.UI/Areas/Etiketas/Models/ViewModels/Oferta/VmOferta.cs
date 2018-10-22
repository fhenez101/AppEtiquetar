using App.Etiketarte.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels.Oferta
{
    public class VmOferta
    {
        public int IdDetalleOferta { get; set; }
        public int IdOferta { get; set; }
        public int UnidadPorPaquete { get; set; }
        public string NombreEtiketa { get; set; }
        public string NombreConfEtiketa { get; set; }
        public int Cantidad { get; set; }
        public bool Estado { get; set; }
    }
}