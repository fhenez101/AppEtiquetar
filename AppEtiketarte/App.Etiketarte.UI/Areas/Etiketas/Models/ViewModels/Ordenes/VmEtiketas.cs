using App.Etiketarte.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels.Ordenes
{
    public class VmEtiketas
    {
        #region Listar

        public List<VmImagen> LFormas { get; set; }
        public List<VmImagen> LArtes { get; set; }
        public List<VmTipografia> LTipografias { get; set; }
        public List<VmColor> LColores { get; set; }
        public string LTextTop { get; set; }
        public string LTextLeft { get; set; }
        public string LTextAlign { get; set; }
        public int LNumeroLineas { get; set; }
        public int LCaracteresLinea { get; set; }
        public int LFontMinSize { get; set; }
        public int LFontMaxSize { get; set; }
        public string LContainerWidth { get; set; }

        #endregion

        #region Ordenar

        public string OCodeForma { get; set; }
        public string OCodeArte { get; set; }
        public string OTexto { get; set; }
        public int OIdTipografia { get; set; }
        public int OIdColor { get; set; }
        public int Cantidad { get; set; }

        #endregion
    }

    //Clases auxiliares
    public class VmUrlImage
    {
        public string Url { get; set; }
    }

    public class VmImagen
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int ImageParts { get; set; }
        public List<VmUrlImage> Img { get; set; }
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