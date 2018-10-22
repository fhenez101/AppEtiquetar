using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using App.Etiketarte.Model.ViewModels;
using App.Etiketarte.Notification;

namespace App.Etiketarte.Data.Etiketas
{
    public class OfertaD : DataBase<Oferta>
    {
        public Oferta Read(int idOferta)
        {
            return db.Ofertas
                .Include(x => x.DetalleOfertas)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => x.IdOferta == idOferta)
                .SingleOrDefault();
        }

        public Oferta Read(string nombre)
        {
            return db.Ofertas
                .Include(x => x.DetalleOfertas)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => x.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        public List<Oferta> ReadList()
        {
            return db.Ofertas
                .Include(x => x.DetalleOfertas)
                .AsEnumerable()
                .Select(x => x)
                .ToList();
        }

        public void Delete(int idOferta, NotificationMessage nm)
        {
            using (var context = db)
            {
                try
                {
                    var oferta = context.Ofertas
                        .Include(x => x.DetalleOfertas)
                        .AsEnumerable()
                        .Select(x => x)
                        .Where(x => x.IdOferta == idOferta)
                        .SingleOrDefault();

                    context.DetalleOfertas.RemoveRange(oferta.DetalleOfertas);
                    context.Ofertas.Remove(oferta);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ManageCatch(ex, nm);
                }
            }
        }

        #region DetalleOferta

        public VmOfertaML ReadDetalleOferta(int idDetalleOferta)
        {
            return db.DetalleOfertas
                .Include(x => x.ConfEtiketa)
                .AsEnumerable()
                .Select(x => new VmOfertaML
                {
                    IdDetalleOferta = x.IdDetalleOferta,
                    IdOferta = x.IdOferta,
                    IdConfEtiketa = x.IdConfEtiketa,
                    IdEtiketa = x.ConfEtiketa.IdEtiketa,
                    IdPresentacion = x.IdPresentacion,
                    UnidadPorPaquete = db.Presentacions
                        .Where(y => y.IdPresentacion == x.IdPresentacion)
                        .Select(z => z.UnidadPorPaquete)
                        .FirstOrDefault(),
                    NombreEtiketa = db.Etiketas
                        .Where(y => y.IdEtiketa == x.ConfEtiketa.IdEtiketa)
                        .Select(z => z.Nombre)
                        .FirstOrDefault(),
                    NombreConfEtiketa = x.ConfEtiketa.Nombre,
                    Cantidad = x.Cantidad,
                    Estado = x.Estado
                })
                .Where(x => x.IdDetalleOferta == idDetalleOferta)
                .FirstOrDefault();
        }

        public List<VmOfertaML> GetDetalleOferta(int idOferta)
        {
            return db.DetalleOfertas
                .Include(x => x.ConfEtiketa)
                .AsEnumerable()
                .Select(x => new VmOfertaML
                {
                    IdDetalleOferta = x.IdDetalleOferta,
                    IdOferta = x.IdOferta,
                    IdConfEtiketa = x.IdConfEtiketa,
                    IdEtiketa = x.ConfEtiketa.IdEtiketa,
                    IdPresentacion = x.IdPresentacion,
                    NombreEtiketa = db.Etiketas
                        .Where(y => y.IdEtiketa == x.ConfEtiketa.IdEtiketa)
                        .Select(z => z.Nombre)
                        .FirstOrDefault(),
                    NombreConfEtiketa = x.ConfEtiketa.Nombre,
                    Cantidad = x.Cantidad,
                    Estado = x.Estado
                })
                .Where(x => x.IdOferta == idOferta)
                .ToList();
        }

        public void CreateDetalleOferta(DetalleOferta detalleOferta, NotificationMessage nm)
        {
            using (var context = db)
            {
                try
                {
                    context.DetalleOfertas.Add(detalleOferta);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ManageCatch(ex, nm);
                }
            }
        }

        public void EditDetalleOferta(DetalleOferta detalleOferta, NotificationMessage nm)
        {
            using (var context = db)
            {
                try
                {
                    context.Entry(detalleOferta).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ManageCatch(ex, nm);
                }
            }
        }

        public void DeleteDetalleOferta(int idDetalleOferta, NotificationMessage nm)
        {
            using (var context = db)
            {
                try
                {
                    var detalleOferta = context.DetalleOfertas
                        .Select(x => x)
                        .Where(x => x.IdDetalleOferta == idDetalleOferta)
                        .SingleOrDefault();

                    context.DetalleOfertas.Remove(detalleOferta);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ManageCatch(ex, nm);
                }
            }
        }

        #endregion
    }
}