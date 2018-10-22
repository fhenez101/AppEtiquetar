using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Model.ViewModels
{
    public class VmArteFormaMI
    {
        public int IdConfEtiketa { get; set; }
        public int IdForma { get; set; }
        public int[] IdTipografia { get; set; }
        public int[] IdColor { get; set; }
        public string TextTop { get; set; }
        public string TextLeft { get; set; }
        public string TextAlign { get; set; }
        public int FontMinSize { get; set; }
        public int FontMaxSize { get; set; }
        public int NumeroLineas { get; set; }
        public int CaracteresLinea { get; set; }
        public string ContainerWidth { get; set; }

        //Arte
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Path { get; set; }
        public List<ArteSplit> ArteSplit { get; set; }
    }
}