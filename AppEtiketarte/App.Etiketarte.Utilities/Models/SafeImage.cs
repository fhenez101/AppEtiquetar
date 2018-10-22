using System.Drawing.Imaging;
using System.Web;

namespace App.Etiketarte.Utilities.Models
{
    public class SafeImage
    {
        public HttpPostedFileBase HttpPostedFileBaseImg { get; set; }
        public string ImgCode { get; set; }
        public string FullPath { get; set; }
        public string WatermarkImg { get; set; }
        public int ImageParts { get; set; }
        public float Angle { get; set; }
        public ImageFormat ImgFormat { get; set; }

        public SafeImage()
        {
            ImgFormat = ImageFormat.Png;
        }
    }
}