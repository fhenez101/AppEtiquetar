using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Model.ViewModels
{
    public class VmArteFormaML
    {
        public int IdArteForma { get; set; }
        public int IdConfEtiketa { get; set; }
        public string NombreConfEtiketa { get; set; }
        public string NombreEtiketa { get; set; }
        public string TextTop { get; set; }
        public string TextLeft { get; set; }
        public string TextAlign { get; set; }
        public int FontMinSize { get; set; }
        public int FontMaxSize { get; set; }
        public int NumeroLineas { get; set; }
        public int CaracteresLinea { get; set; }
        public string ContainerWidth { get; set; } 

        //Arte
        public int IdArte { get; set; }
        public string ArteCodigo { get; set; }
        public string ArteNombre { get; set; }
        public string ArtePath { get; set; }
        public List<ArteSplit> ArteSplit { get; set; }

        //Forma
        public int IdForma { get; set; }
        public string FormaCodigo { get; set; }
        public string FormaNombre { get; set; }
        public string FormaPath { get; set; }
        public List<FormaSplit> FormaSplit { get; set; }

        //Colores
        public List<Color> Colores { get; set; }

        //Tipografias
        public List<Tipografia> Tipografias { get; set; }
    }
}
