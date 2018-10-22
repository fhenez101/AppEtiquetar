using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IPresentacionB
    {
        void Create(Presentacion presentacion, NotificationMessage nm);
        void Edit(Presentacion presentacion, NotificationMessage nm);
        void Delete(Presentacion presentacion, NotificationMessage nm);
        Presentacion Read(int idPresentacion);
        List<Presentacion> ReadList(int idEtiketa);

        List<Presentacion> GetPresentacionOferta(int idEtiketa);
    }
}