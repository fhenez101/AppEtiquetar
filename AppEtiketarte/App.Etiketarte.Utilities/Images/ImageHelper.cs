using App.Etiketarte.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace App.Etiketarte.Utilities.Images
{
    public class ImageHelper<T> where T : new()
    {
        public string NanoTime
        {
            get { return ((long)(Stopwatch.GetTimestamp() / (Stopwatch.Frequency / 1000000000.0))).ToString(); }
        }

        public MvcHtmlString ShowImage<Y>(string imagePath, int imageParts, Y[] obj)
        {
            int counter = 0;
            string path = string.Empty;
            var containerDiv = new TagBuilder("div");
            var divImage = new StringBuilder();

            if (obj != null)
            {
                for (int q = 0; q < imageParts; q++)
                {
                    var fatherDiv = new TagBuilder("div");
                    fatherDiv.MergeAttribute("class", "fatherDiv");

                    for (int j = 0; j < imageParts; j++)
                    {
                        path = Path.Combine(imagePath, GetImageName(obj[counter]));

                        if (File.Exists(path))
                        {
                            string base64ImageRepresentation = Convert.ToBase64String(File.ReadAllBytes(path));
                            var div = new TagBuilder("div");
                            var img = new TagBuilder("img");

                            img.MergeAttribute("src", $"data:image/png;base64,{base64ImageRepresentation}");
                            div.InnerHtml = img.ToString();
                            fatherDiv.InnerHtml += div.ToString();
                        }

                        counter++;
                    }

                    divImage.Append(fatherDiv);
                }
            }

            containerDiv.MergeAttribute("class", "containerImage");
            containerDiv.InnerHtml = divImage.ToString();

            return MvcHtmlString.Create(containerDiv.ToString(TagRenderMode.Normal));
        }

        public List<T> SplitImageWaterMark(SafeImage safeImg)
        {
            int counter = 0;
            string baseDir = Path.GetFullPath(safeImg.FullPath);
            var bmpStamp = new Bitmap(Path.Combine(baseDir, safeImg.WatermarkImg));
            var bmps = new Bitmap[safeImg.ImageParts, safeImg.ImageParts];
            var img = Image.FromStream(safeImg.HttpPostedFileBaseImg.InputStream, false);
            int width = (int)((double)img.Width / safeImg.ImageParts + 0.5);
            int height = (int)((double)img.Height / safeImg.ImageParts + 0.5);
            var result = new List<T>();

            try
            {
                for (int i = 0; i < safeImg.ImageParts; i++)
                {
                    for (int j = 0; j < safeImg.ImageParts; j++)
                    {
                        using (var stream = new MemoryStream())
                        {
                            using (bmps[i, j] = new Bitmap(width, height))
                            {
                                using (var graphics = Graphics.FromImage(bmps[i, j]))
                                {
                                    graphics.DrawImage(img, new Rectangle(0, 0, width, height),
                                        new Rectangle(j * width, i * height, width, height),
                                        GraphicsUnit.Pixel);
                                }

                                bmps[i, j].Save(stream, safeImg.ImgFormat);
                            }

                            using (var bmpUpload = new Bitmap(stream, false))
                            {
                                using (var graphicsObj = Graphics.FromImage(bmpUpload))
                                {
                                    var positionWaterMark = new Point((bmpUpload.Width / 100), (bmpUpload.Height / 50));

                                    safeImg.ImgCode = NanoTime;

                                    graphicsObj.RotateTransform(safeImg.Angle);
                                    graphicsObj.DrawImage(bmpStamp, positionWaterMark);

                                    using (var stremaUpload = new MemoryStream())
                                    {
                                        var path = Path.Combine(baseDir, $"{safeImg.ImgCode}-{counter}.{safeImg.ImgFormat}");
                                        bmpUpload.Save(stremaUpload, bmpUpload.RawFormat);

                                        File.WriteAllBytes(path, stremaUpload.ToArray());
                                    }
                                }
                            }
                        }
                       
                        var obj = new T();
                        var prop = obj.GetType().GetProperty("Nombre", BindingFlags.Public | BindingFlags.Instance);
                        if (prop != null && prop.CanWrite)
                        {
                            prop.SetValue(obj, $"{safeImg.ImgCode}-{counter}.{safeImg.ImgFormat}", null);
                        }

                        result.Add(obj);
                        counter++;
                    }
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public void DeleteFile(List<string> fileList, string dir)
        {
            try
            {
                string baseDir = Path.GetFullPath(dir);

                fileList.ForEach(x =>
                {
                    string fileName = Path.Combine(baseDir, x);

                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                });
            }
            catch
            {
                throw;
            }
        }

        private static string GetImageName<Y>(Y obj)
        {
            string imageName = string.Empty;
            var prop = obj.GetType().GetProperty("Nombre", BindingFlags.Public | BindingFlags.Instance);

            if (prop != null && prop.CanRead)
            {
                imageName = prop.GetValue(obj, null).ToString();
            }

            return imageName;
        }
    }
}