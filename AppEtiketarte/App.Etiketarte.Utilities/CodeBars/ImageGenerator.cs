using App.Etiketarte.Utilities.CodeBars.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Utilities.CodeBars
{
    public class ImageGenerator
    {
        public bool GenerarImagenEan13(string codigo, string ubicacionImagen, System.Drawing.Imaging.ImageFormat formato, string titulo = default(string), Ean13Parametros config = null)
        {
            int nErrores = 0;
            string mensajeErrores = string.Empty;
            string bcTemp = string.Empty;
            string digitoTemp = string.Empty;
            string digitoControl = string.Empty;
            string codigoFinal = string.Empty;
            Ean13 generadorEan13 = null;
            Ean13Parametros configFinal = null;
            Image imagenBc = null;
            try
            {
                //valido el codigo de barras
                if (!string.IsNullOrEmpty((codigo)))
                {

                    if (codigo.Length == 12)
                    {
                        //genero el codigo de control
                        digitoControl = Ean13Utilidades.CalculateChecksum(codigo).ToString();
                        codigoFinal = codigo + digitoControl;
                    }
                    else if (codigo.Length == 13)
                    {
                        codigoFinal = codigo;
                    }
                    else
                    {
                        nErrores++;
                        mensajeErrores = "La longitud del código de barras es incorrecta. Longitud = " + codigo.Length.ToString();
                    }
                }
                else
                {
                    nErrores++;
                    mensajeErrores = "No se ha especificado el codigo de barras";
                }

                if (string.IsNullOrEmpty(ubicacionImagen))
                {
                    mensajeErrores = "No ha especificado la ruta de la imagen";
                    nErrores++;
                }

                if (nErrores == 0)
                {
                    if (config != null)
                    {
                        configFinal = config;
                    }
                    else
                    {
                        configFinal = new Ean13Parametros();
                    }
                    try
                    {
                        generadorEan13 = new Ean13(codigoFinal, titulo, configFinal);
                    }
                    catch (Exception ex)
                    {
                        mensajeErrores = "No ha podido inicializar el objeto generador de EAN13." + ex.Message;
                        nErrores++;
                    }

                    if ((nErrores == 0) && (generadorEan13 != null))
                    {
                        try
                        {
                            imagenBc = generadorEan13.Paint(Color.White);
                        }
                        catch (Exception ex)
                        {
                            mensajeErrores = "No ha podido generar el objeto Image del codigo de barras " + codigo + ". " + ex.Message;
                            nErrores++;
                        }

                        if ((nErrores == 0) && (imagenBc != null))
                        {
                            imagenBc.Save(ubicacionImagen, formato);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                mensajeErrores = ex.Message;
                nErrores++;
            }
            finally
            {
                generadorEan13 = null;
            }

            if (nErrores == 0)
            {
                return true;
            }
            else
            {
                throw new Exception(mensajeErrores);
            }
        }

        public bool GenerarImagenCode128(string codigo, string ubicacionImagen, System.Drawing.Imaging.ImageFormat formato, int x = 0, int y = 0)
        {
            int nErrores = 0;
            string mensajeErrores = string.Empty;
            Image imagenBc = null;
            CodeBarsGenerator generadorCode128 = null;
            Graphics gra = null;
            Bitmap bmp = null;

            try
            {
                //valido el codigo de barras
                if (string.IsNullOrEmpty((codigo)))
                {
                    nErrores++;
                    mensajeErrores = "No se ha especificado el codigo de barras";
                }

                if (string.IsNullOrEmpty(ubicacionImagen))
                {
                    mensajeErrores = "No ha especificado la ruta de la imagen";
                    nErrores++;
                }

                if (nErrores == 0)
                {
                    try
                    {
                        generadorCode128 = new CodeBarsGenerator();
                    }
                    catch (Exception ex)
                    {
                        mensajeErrores = "No ha podido inicializar el objeto generador de CODE128." + ex.Message;
                        nErrores++;
                    }

                    if ((nErrores == 0) && (generadorCode128 != null))
                    {
                        try
                        {
                            bmp = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
                            gra = Graphics.FromImage(bmp);

                            imagenBc = generadorCode128.DrawCode128(gra, codigo, x, y, Color.White);
                        }
                        catch (Exception ex)
                        {
                            mensajeErrores = "No ha podido generar el objeto Image del codigo de barras " + codigo + ". " + ex.Message;
                            nErrores++;
                        }

                        if ((nErrores == 0) && (imagenBc != null))
                        {
                            imagenBc.Save(ubicacionImagen, formato);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                mensajeErrores = ex.Message;
                nErrores++;
            }
            finally
            {
                generadorCode128 = null;
                gra = null;
                bmp = null;
            }

            if (nErrores == 0)
            {
                return true;
            }
            else
            {
                throw new Exception(mensajeErrores);
            }
        }

        //Este Usaremos
        public byte[] GenerarImagenCode39(string codigo, Code39Parametros config = null)
        {
            try
            {
                return (byte[])new ImageConverter().ConvertTo(
                    new Code39(codigo, (config ?? new Code39Parametros())).Paint(Color.White), typeof(byte[])
                );

                //Ejemplo de uso
                //string rutaBase = @"c:\temp\bc";
                //string code = "PROGRAMMINGAPPS";
                //var rutaImagen = Path.Combine(rutaBase, code + "_code39");

                //using (var mStream = new MemoryStream(new ImageGenerator().GenerarImagenCode39(codigo)))
                //{
                //    Image.FromStream(mStream).Save($"{ rutaImagen}.{ImageFormat.Png}");
                //}
            }
            catch
            {
                throw;
            }
        }
    }
}
