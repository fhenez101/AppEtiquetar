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

namespace App.Etiketarte.Business.Etiketas.Clases
{
    public class PresentacionB : IPresentacionB
    {
        public void Create(Presentacion presentacion, NotificationMessage nm)
        {
            presentacion.State = State.Added;
            new PresentacionD().Save(presentacion, nm);
        }

        public void Delete(Presentacion presentacion, NotificationMessage nm)
        {
            presentacion.State = State.Deleted;
            new PresentacionD().Save(presentacion, nm);
        }

        public void Edit(Presentacion presentacion, NotificationMessage nm)
        {
            presentacion.State = State.Modified;
            new PresentacionD().Save(presentacion, nm);
        }

        public List<Presentacion> GetPresentacionOferta(int idEtiketa)
        {
            return new PresentacionD().GetPresentacionOferta(idEtiketa);
        }

        public Presentacion Read(int idPresentacion)
        {
            return new PresentacionD().Read(idPresentacion);
        }

        public List<Presentacion> ReadList(int idEtiketa)
        {
            return new PresentacionD().ReadList(idEtiketa);
        }
    }
}