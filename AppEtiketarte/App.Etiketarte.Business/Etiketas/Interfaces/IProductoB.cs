using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IProductoB
    {
        void Create(Producto producto, NotificationMessage nm);
        void Edit(Producto producto, NotificationMessage nm);
        void Delete(Producto producto, NotificationMessage nm);
        Producto Read(int idProducto);
        Producto Read(string nombre);
        List<Producto> ReadList();
    }
}
