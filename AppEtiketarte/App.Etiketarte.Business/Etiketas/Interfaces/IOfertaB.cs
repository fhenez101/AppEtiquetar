using App.Etiketarte.Model;
using App.Etiketarte.Model.ViewModels;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IOfertaB
    {
        void Create(Oferta oferta, NotificationMessage nm);
        void Edit(Oferta oferta, NotificationMessage nm);
        void Delete(int idOferta, NotificationMessage nm);

        void CreateDetalleOferta(DetalleOferta detalleOferta, NotificationMessage nm);
        void EditDetalleOferta(DetalleOferta detalleOferta, NotificationMessage nm);
        void DeleteDetalleOferta(int idDetalleOferta, NotificationMessage nm);
        VmOfertaML ReadDetalleOferta(int idDetalleOferta);

        Oferta Read(int idOferta);
        List<Oferta> ReadList();
        List<VmOfertaML> GetDetalleOferta(int idOferta);
    }
}