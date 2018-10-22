using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using App.Etiketarte.Model.ViewModels;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace App.Etiketarte.Data.Etiketas
{
    public class ArteFormaD : DataBase<ArteForma>
    {
        public VmArteFormaML Read(int idArteForma)
        {
            try
            {
                var nombreEtiketa = db.ConfEtiketas
                    .Include(x => x.ArteFormas)
                    .Include(x => x.Etiketa)
                    .AsEnumerable()
                    .Select(x => new
                    {
                        Nombre = x.Etiketa.Nombre,
                        ArteFormas = x.ArteFormas
                    })
                    .Where(x => x.ArteFormas.Select(y => y.IdArteForma).Contains(idArteForma))
                    .FirstOrDefault();

                //ArteForma
                var arteForma = db.ArteFormas
                    .Include(x => x.ConfEtiketa)
                    .AsEnumerable()
                    .Select(x => new ArteForma
                    {
                        ConfEtiketa = x.ConfEtiketa,
                        IdArteForma = x.IdArteForma,
                        IdConfEtiketa = x.IdConfEtiketa,
                        IdArte = x.IdArte,
                        IdForma = x.IdForma,
                        TextTop = x.TextTop,
                        TextLeft = x.TextLeft,
                        TextAlign = x.TextAlign,
                        FontMinSize = x.FontMinSize,
                        FontMaxSize = x.FontMaxSize,
                        NumeroLineas = x.NumeroLineas,
                        CaracteresLinea = x.CaracteresLinea,
                        ContainerWidth = x.ContainerWidth
                    })
                    .Where(x => x.IdArteForma == idArteForma)
                    .FirstOrDefault();

                //Arte
                var arte = db.Artes
                    .Include(x => x.ArteSplits)
                    .AsEnumerable()
                    .Select(x => new Arte
                    {
                        IdArte = x.IdArte,
                        Codigo = x.Codigo,
                        Nombre = x.Nombre,
                        Path = x.Path,
                        ArteSplits = x.ArteSplits
                    })
                    .Where(x => x.IdArte == arteForma.IdArte)
                    .FirstOrDefault();

                //Forma
                var forma = db.Formas
                    .Include(x => x.FormaSplits)
                    .AsEnumerable()
                    .Select(x => new Forma
                    {
                        IdForma = x.IdForma,
                        Codigo = x.Codigo,
                        Nombre = x.Nombre,
                        Path = x.Path,
                        FormaSplits = x.FormaSplits
                    })
                    .Where(x => x.IdForma == arteForma.IdForma)
                    .FirstOrDefault();

                //Colores
                var colores = db.Colors
                    .Include(x => x.DetalleColors)
                    .AsEnumerable()
                    .Select(x => new Color
                    {
                        IdColor = x.IdColor,
                        Hexadecimal = x.Hexadecimal,
                        DetalleColors = x.DetalleColors
                    })
                    .Where(x => x.DetalleColors.Select(y => y.IdArteForma).Contains(idArteForma))
                    .ToList();


                //Tipografias
                var tipografias = db.Tipografias
                    .Include(x => x.DetalleTipografias)
                    .AsEnumerable()
                    .Select(x => new Tipografia
                    {
                        IdTipoGrafia = x.IdTipoGrafia,
                        Nombre = x.Nombre,
                        DetalleTipografias = x.DetalleTipografias
                    })
                    .Where(x => x.DetalleTipografias.Select(y => y.IdArteForma).Contains(idArteForma))
                    .ToList();

                return new VmArteFormaML
                {
                    IdArteForma = arteForma.IdArteForma,
                    IdConfEtiketa = arteForma.IdConfEtiketa,
                    NombreConfEtiketa = arteForma.ConfEtiketa.Nombre,
                    NombreEtiketa = nombreEtiketa.Nombre,
                    TextTop = arteForma.TextTop,
                    TextLeft = arteForma.TextLeft,
                    TextAlign = arteForma.TextAlign,
                    FontMinSize = arteForma.FontMinSize,
                    FontMaxSize = arteForma.FontMaxSize,
                    NumeroLineas = arteForma.NumeroLineas,
                    CaracteresLinea = arteForma.CaracteresLinea,
                    ContainerWidth = arteForma.ContainerWidth,

                    //Arte
                    IdArte = arte.IdArte,
                    ArteCodigo = arte.Codigo,
                    ArteNombre = arte.Nombre,
                    ArtePath = arte.Path,
                    ArteSplit = arte.ArteSplits.ToList(),

                    //Forma
                    IdForma = forma.IdForma,
                    FormaCodigo = forma.Codigo,
                    FormaNombre = forma.Nombre,
                    FormaPath = forma.Path,
                    FormaSplit = forma.FormaSplits.ToList(),

                    //Colores
                    Colores = colores,

                    //Tipografias
                    Tipografias = tipografias
                };
            }
            catch
            {
                throw;
            }
        }

        public List<VmArteFormaML> ReadList(int idConfEtiketa)
        {
            try
            {
                var result = new List<VmArteFormaML>();

                db.ArteFormas
                    .AsEnumerable()
                    .Select(x => new ArteForma
                    {
                        IdConfEtiketa = x.IdConfEtiketa,
                        IdArteForma = x.IdArteForma
                    })
                    .Where(x => x.IdConfEtiketa == idConfEtiketa)
                    .ToList()
                    .ForEach(x =>
                    {
                        result.Add(Read(x.IdArteForma));
                    });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateArteForma(VmArteFormaMI vmArteFormaMI, NotificationMessage nm)
        {
            using (var context = db)
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Insert Arte & ArteSplit
                        var arte = new Arte
                        {
                            Codigo = vmArteFormaMI.Codigo,
                            Nombre = vmArteFormaMI.Nombre,
                            Path = vmArteFormaMI.Path,
                            ArteSplits = vmArteFormaMI.ArteSplit,
                            Estado = true
                        };
                        context.Artes.Add(arte);

                        int idArte = arte.IdArte;

                        //InsertArteForma
                        var arteForma = new ArteForma
                        {
                            IdConfEtiketa = vmArteFormaMI.IdConfEtiketa,
                            IdArte = idArte,
                            IdForma = vmArteFormaMI.IdForma,
                            TextTop = vmArteFormaMI.TextTop,
                            TextLeft = vmArteFormaMI.TextLeft,
                            TextAlign = vmArteFormaMI.TextAlign,
                            FontMinSize = vmArteFormaMI.FontMinSize,
                            FontMaxSize = vmArteFormaMI.FontMaxSize,
                            NumeroLineas = vmArteFormaMI.NumeroLineas,
                            CaracteresLinea = vmArteFormaMI.CaracteresLinea,
                            ContainerWidth = vmArteFormaMI.ContainerWidth
                        };
                        context.ArteFormas.Add(arteForma);

                        int idArteForma = arteForma.IdArteForma;

                        //Insert DetalleColor
                        var detalleColor = vmArteFormaMI.IdColor.Select(x => new DetalleColor
                        {
                            IdColor = x,
                            IdArteForma = idArteForma,
                            Estado = true
                        });
                        context.DetalleColors.AddRange(detalleColor);

                        //Insert DetalleTipografria
                        var detalleTipografia = vmArteFormaMI.IdTipografia.Select(x => new DetalleTipografia
                        {
                            IdTipografia = x,
                            IdArteForma = idArteForma,
                            Estado = true
                        });
                        context.DetalleTipografias.AddRange(detalleTipografia);

                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        ManageCatch(ex, nm);
                    }
                }
            }
        }

        public void EditArteForma(VmArteFormaMU vmArteFormaMU, NotificationMessage nm)
        {
            using (var context = db)
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        //ArteForma
                        if (vmArteFormaMU.IdArteForma != 0)
                        {
                            var arteForma = new ArteForma
                            {
                                IdArteForma = vmArteFormaMU.IdArteForma,
                                IdForma = vmArteFormaMU.IdForma,
                                TextTop = vmArteFormaMU.TextTop,
                                TextLeft = vmArteFormaMU.TextLeft,
                                TextAlign = vmArteFormaMU.TextAlign,
                                NumeroLineas = vmArteFormaMU.NumeroLineas,
                                CaracteresLinea = vmArteFormaMU.CaracteresLinea,
                                FontMinSize = vmArteFormaMU.FontMinSize,
                                FontMaxSize = vmArteFormaMU.FontMaxSize,
                                ContainerWidth = vmArteFormaMU.ContainerWidth
                            };
                            context.ArteFormas.Attach(arteForma);
                            context.Entry(arteForma).Property(x => x.IdForma).IsModified = true;
                            context.Entry(arteForma).Property(x => x.TextTop).IsModified = true;
                            context.Entry(arteForma).Property(x => x.TextLeft).IsModified = true;
                            context.Entry(arteForma).Property(x => x.TextAlign).IsModified = true;
                            context.Entry(arteForma).Property(x => x.NumeroLineas).IsModified = true;
                            context.Entry(arteForma).Property(x => x.CaracteresLinea).IsModified = true;
                            context.Entry(arteForma).Property(x => x.FontMinSize).IsModified = true;
                            context.Entry(arteForma).Property(x => x.FontMaxSize).IsModified = true;
                            context.Entry(arteForma).Property(x => x.ContainerWidth).IsModified = true;
                        }

                        //ArteSplit
                        if (vmArteFormaMU.ArteSplit.Count() > 0)
                        {
                            int counter = 0;
                            var idArray = context.ArteSplits
                                .Select(x => new
                                {
                                    x.IdArteSplit,
                                    x.IdArte
                                })
                                .AsEnumerable()
                                .Where(x => x.IdArte == vmArteFormaMU.IdArte)
                                .ToArray();

                            vmArteFormaMU.ArteSplit.ForEach(x =>
                            {
                                var arteSplit = new ArteSplit
                                {
                                    IdArteSplit = idArray[counter].IdArteSplit,
                                    Nombre = x.Nombre
                                };
                                context.ArteSplits.Attach(arteSplit);
                                context.Entry(arteSplit).Property(y => y.Nombre).IsModified = true;
                                counter++;
                            });
                        }

                        //Arte
                        var arte = db.Artes
                            .Select(x => x)
                            .AsQueryable()
                            .Where(x => x.IdArte == vmArteFormaMU.IdArte)
                            .FirstOrDefault();

                        if (!arte.Codigo.Equals(vmArteFormaMU.Codigo) || !arte.Nombre.Equals(vmArteFormaMU.Nombre))
                        {
                            arte.Codigo = vmArteFormaMU.Codigo;
                            arte.Nombre = vmArteFormaMU.Nombre;

                            context.Artes.Attach(arte);
                            context.Entry(arte).Property(x => x.Codigo).IsModified = true;
                            context.Entry(arte).Property(x => x.Nombre).IsModified = true;
                        }

                        //Colores
                        var colores = db.DetalleColors
                            .Select(x => x)
                            .Where(x => x.IdArteForma == vmArteFormaMU.IdArteForma)
                            .ToList();

                        var colorInsert = vmArteFormaMU?.IdColor?
                            .ToList()
                            .FindAll(x => !colores.Select(y => y.IdColor).Contains(x));
                        var colorDelete = (vmArteFormaMU.IdColor != null)
                            ? colores.FindAll(x => !(vmArteFormaMU.IdColor.Contains(x.IdColor)))
                            : colores;

                        if (colorInsert != null || colorDelete != null)
                        {
                            if (colorInsert != null)
                            {
                                var detalleColor = colorInsert.Select(x => new DetalleColor
                                {
                                    IdColor = x,
                                    IdArteForma = vmArteFormaMU.IdArteForma,
                                    Estado = true
                                });
                                context.DetalleColors.AddRange(detalleColor);
                            }
                            else if (colorDelete != null)
                            {
                                var detalleColor = colores
                                    .Where(x => colorDelete.Select(y => y.IdColor).Contains(x.IdColor));
                                context.DetalleColors.RemoveRange(detalleColor);
                            }
                        }

                        //Tipografias
                        var tipografias = db.DetalleTipografias
                            .Select(x => x)
                            .Where(x => x.IdArteForma == vmArteFormaMU.IdArteForma)
                            .ToList();

                        var tipografiaInsert = vmArteFormaMU?.IdTipografia?
                            .ToList()
                            .FindAll(x => !tipografias.Select(y => y.IdTipografia).Contains(x));
                        var tipografiaDelete = (vmArteFormaMU.IdTipografia != null)
                            ? tipografias.FindAll(x => !(vmArteFormaMU.IdTipografia.Contains(x.IdTipografia)))
                            : tipografias;

                        if (tipografiaInsert != null || tipografiaDelete != null)
                        {
                            if (tipografiaInsert != null)
                            {
                                var detalleTipografia = tipografiaInsert.Select(x => new DetalleTipografia
                                {
                                    IdTipografia = x,
                                    IdArteForma = vmArteFormaMU.IdArteForma,
                                    Estado = true
                                });
                                context.DetalleTipografias.AddRange(detalleTipografia);
                            }
                            else if (tipografiaDelete != null)
                            {
                                var detalleTipografia = tipografias
                                    .Where(x => tipografiaDelete.Select(y => y.IdTipografia).Contains(x.IdTipografia));
                                context.DetalleTipografias.RemoveRange(detalleTipografia);
                            }
                        }

                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        ManageCatch(ex, nm);
                    }
                }
            }
        }

        public void DeleteArteForma(int idArteForma, NotificationMessage nm)
        {
            using (var context = db)
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var arteForma = context.ArteFormas
                            .Include(x => x.Arte)
                            .Include(x => x.Arte.ArteSplits)
                            .Include(x => x.DetalleColors)
                            .Include(x => x.DetalleTipografias)
                            .AsEnumerable()
                            .Select(x => x)
                            .Where(x => x.IdArteForma == idArteForma)
                            .SingleOrDefault();

                        context.ArteSplits.RemoveRange(arteForma.Arte.ArteSplits);
                        context.Artes.Remove(arteForma.Arte);
                        context.DetalleColors.RemoveRange(arteForma.DetalleColors);
                        context.DetalleTipografias.RemoveRange(arteForma.DetalleTipografias);
                        context.ArteFormas.Remove(arteForma);

                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        ManageCatch(ex, nm);
                    }
                }
            }
        }

        public ArteForma GetArteFormaOrden(string codigo)
        {
            var arte = db.Artes
               .AsEnumerable()
               .Select(x => new Arte
               {
                   IdArte = x.IdArte,
                   Codigo = x.Codigo
               })
               .Where(x => x.Codigo == codigo)
               .FirstOrDefault();

            var arteForma = db.ArteFormas
               .AsEnumerable()
               .Select(x => new ArteForma
               {
                   IdArteForma = x.IdArteForma,
                   IdConfEtiketa = x.IdConfEtiketa,
                   IdArte = x.IdArte,
                   TextTop = x.TextTop,
                   TextLeft = x.TextLeft,
                   TextAlign = x.TextAlign,
                   NumeroLineas = x.NumeroLineas,
                   CaracteresLinea = x.CaracteresLinea,
                   FontMinSize = x.FontMinSize,
                   FontMaxSize = x.FontMaxSize,
                   ContainerWidth = x.ContainerWidth
               })
               .Where(x => x.IdArte == arte.IdArte)
               .FirstOrDefault();

            return arteForma;
        }
    }
}