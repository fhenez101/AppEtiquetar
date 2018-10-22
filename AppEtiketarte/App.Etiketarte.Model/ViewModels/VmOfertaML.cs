using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Model.ViewModels
{
    public class VmOfertaML
    {
        public int IdDetalleOferta { get; set; }
        public int IdOferta { get; set; }
        public int IdEtiketa { get; set; }
        public int IdConfEtiketa { get; set; }
        public int IdPresentacion { get; set; }
        public int UnidadPorPaquete { get; set; }
        public string NombreEtiketa { get; set; }
        public string NombreConfEtiketa { get; set; }
        public int Cantidad { get; set; }
        public bool Estado { get; set; }
    }
}