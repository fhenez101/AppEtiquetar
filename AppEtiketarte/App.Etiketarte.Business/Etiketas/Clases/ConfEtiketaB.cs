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
    public class ConfEtiketaB : IConfEtiketaB
    {
        public void Create(ConfEtiketa confEtiketa, NotificationMessage nm)
        {
            confEtiketa.State = State.Added;
            new ConfEtiketaD().Save(confEtiketa, nm);
        }

        public void Delete(ConfEtiketa confEtiketa, NotificationMessage nm)
        {
            confEtiketa.State = State.Deleted;
            new ConfEtiketaD().Save(confEtiketa, nm);
        }

        public void Edit(ConfEtiketa confEtiketa, NotificationMessage nm)
        {
            confEtiketa.State = State.Modified;
            new ConfEtiketaD().Save(confEtiketa, nm);
        }

        public List<ConfEtiketa> GetConfEtiketaOferta(int idEtiketa)
        {
            return new ConfEtiketaD().GetConfEtiketaOferta(idEtiketa);
        }

        public ConfEtiketa GetConfEtiketaOrden(int idConfEtiketa)
        {
            return new ConfEtiketaD().GetConfEtiketaOrden(idConfEtiketa);
        }

        public ConfEtiketa Read(int idconfEtiketa)
        {
            return new ConfEtiketaD().Read(idconfEtiketa);
        }

        public ConfEtiketa Read(string nombre)
        {
            return new ConfEtiketaD().Read(nombre);
        }

        public List<ConfEtiketa> ReadList()
        {
            return new ConfEtiketaD().ReadList();
        }
    }
}