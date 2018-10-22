using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface ITipografiaB
    {
        void Create(Tipografia producto, NotificationMessage nm);
        void Edit(Tipografia producto, NotificationMessage nm);
        void Delete(Tipografia producto, NotificationMessage nm);
        Tipografia Read(int idProducto);
        Tipografia Read(string nombre);
        List<Tipografia> ReadList();
        List<Tipografia> GetTipografiasOrden(string codigo);
    }
}