using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IConfEtiketaB
    {
        void Create(ConfEtiketa confEtiketa, NotificationMessage nm);
        void Edit(ConfEtiketa confEtiketa, NotificationMessage nm);
        void Delete(ConfEtiketa confEtiketa, NotificationMessage nm);
        ConfEtiketa Read(int idconfEtiketa);
        ConfEtiketa Read(string nombre);
        List<ConfEtiketa> ReadList();
        ConfEtiketa GetConfEtiketaOrden(int idConfEtiketa);
        List<ConfEtiketa> GetConfEtiketaOferta(int idEtiketa);
    }
}