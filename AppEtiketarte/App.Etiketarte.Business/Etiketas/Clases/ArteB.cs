using App.Etiketarte.Business.Etiketas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using App.Etiketarte.Data.Etiketas;
using App.Etiketarte.Model.Common;

namespace App.Etiketarte.Business.Etiketas.Clases
{
    public class ArteB : IArteB
    {
        public void Delete(Arte arte, NotificationMessage nm)
        {
            arte.State = State.Deleted;
            new ArteD().Save(arte, nm);
        }

        public void Edit(Arte arte, NotificationMessage nm)
        {
            arte.State = State.Modified;
            new ArteD().Save(arte, nm);
        }

        public Arte Read(int idArte)
        {
            return new ArteD().Read(idArte);
        }

        public Arte Read(string nombre)
        {
            return new ArteD().Read(nombre);
        }

        public List<Arte> ReadList()
        {
            return new ArteD().ReadList();
        }

        public void Create(Arte arte, NotificationMessage nm)
        {
            arte.State = State.Added;
            new ArteD().Save(arte, nm);
        }

        public List<Arte> GetArtesOrden(int idEtiketa, string codigo)
        {
            return new ArteD().GetArtesOrden(idEtiketa, codigo);
        }
    }
}