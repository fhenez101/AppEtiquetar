using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IEtiketaB
    {
        void Create(Etiketa etiketa, NotificationMessage nm);
        void Edit(Etiketa etiketa, NotificationMessage nm);
        void Delete(Etiketa etiketa, NotificationMessage nm);
        Etiketa Read(int idEtiketa);
        Etiketa Read(string nombre);
        List<Etiketa> ReadList();
    }
}