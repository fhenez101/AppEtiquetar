using App.Etiketarte.Business.Etiketas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using App.Etiketarte.Model.Common;
using App.Etiketarte.Data.Etiketas;
using App.Etiketarte.Model.ViewModels;

namespace App.Etiketarte.Business.Etiketas.Clases
{
    public class OfertaB : IOfertaB
    {
        public void Create(Oferta oferta, NotificationMessage nm)
        {
            oferta.State = State.Added;
            new OfertaD().Save(oferta, nm);
        }

        public void CreateDetalleOferta(DetalleOferta detalleOferta, NotificationMessage nm)
        {
            new OfertaD().CreateDetalleOferta(detalleOferta, nm);
        }

        public void Delete(int idOferta, NotificationMessage nm)
        {
            new OfertaD().Delete(idOferta, nm);
        }

        public void DeleteDetalleOferta(int idDetalleOferta, NotificationMessage nm)
        {
            new OfertaD().DeleteDetalleOferta(idDetalleOferta, nm);
        }

        public void Edit(Oferta oferta, NotificationMessage nm)
        {
            oferta.State = State.Modified;
            new OfertaD().Save(oferta, nm);
        }

        public void EditDetalleOferta(DetalleOferta detalleOferta, NotificationMessage nm)
        {
            new OfertaD().EditDetalleOferta(detalleOferta, nm);
        }

        public List<VmOfertaML> GetDetalleOferta(int idOferta)
        {
            return new OfertaD().GetDetalleOferta(idOferta);
        }

        public Oferta Read(int idOferta)
        {
            return new OfertaD().Read(idOferta);
        }

        public VmOfertaML ReadDetalleOferta(int idDetalleOferta)
        {
            return new OfertaD().ReadDetalleOferta(idDetalleOferta);
        }

        public List<Oferta> ReadList()
        {
            return new OfertaD().ReadList();
        }
    }
}