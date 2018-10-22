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
    public class EtiketaB : IEtiketaB
    {
        public void Create(Etiketa etiketa, NotificationMessage nm)
        {
            etiketa.State = State.Added;
            new EtiketaD().Save(etiketa, nm);
        }

        public void Delete(Etiketa etiketa, NotificationMessage nm)
        {
            etiketa.State = State.Deleted;
            new EtiketaD().Save(etiketa, nm);
        }

        public void Edit(Etiketa etiketa, NotificationMessage nm)
        {
            etiketa.State = State.Modified;
            new EtiketaD().Save(etiketa, nm);
        }

        public Etiketa Read(int idEtiketa)
        {
            return new EtiketaD().Read(idEtiketa);
        }

        public Etiketa Read(string nombre)
        {
            return new EtiketaD().Read(nombre);
        }

        public List<Etiketa> ReadList()
        {
            return new EtiketaD().ReadList();
        }
    }
}