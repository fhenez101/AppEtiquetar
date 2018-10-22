using App.Etiketarte.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels
{
    public class VmArteForma
    {
        public int IdArteForma { get; set; }
        public int IdConfEtiketa { get; set; }
        public int[] IdTipografia { get; set; }
        public int[] IdColor { get; set; }
        public string[][] InputText { get; set; }
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

        //EditMode
        public int[] IdTipografiaEdit { get; set; }
        public int[] IdColorEdit { get; set; }

        //Arte
        public int IdArte { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string ArtePath { get; set; }
        public List<ArteSplit> ArteSplit { get; set; }

        //Forma
        public int IdForma { get; set; }
        public string FormaCodigo { get; set; }
        public string FormaNombre { get; set; }
        public string FormaPath { get; set; }
        public List<FormaSplit> FormaSplit { get; set; }
        public List<Forma> Forma { get; set; }

        public List<Tipografia> Tipografias { get; set; }
        public List<Color> Colors { get; set; }
    }
}