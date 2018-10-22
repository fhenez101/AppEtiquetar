using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Model.ViewModels
{
    public class VmEtiketasML
    {
        public int IdArte { get; set; }
        public int IdArteForma { get; set; }
        public int IdConfEtiketa { get; set; }
        public List<VmTipografia> Tipografias { get; set; }
        public List<VmColor> Colores { get; set; }
        public string TextTop { get; set; }
        public string TextLeft { get; set; }
        public string TextAlign { get; set; }
        public int NumeroLineas { get; set; }
        public int CaracteresLinea { get; set; }
        public int FontMinSize { get; set; }
        public int FontMaxSize { get; set; }
        public string ContainerWidth { get; set; }
    }

    public class VmTipografia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class VmColor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Hexadecimal { get; set; }
    }
}
