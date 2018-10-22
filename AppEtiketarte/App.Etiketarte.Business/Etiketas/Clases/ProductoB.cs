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
    public class ProductoB : IProductoB
    {
        public void Delete(Producto producto, NotificationMessage nm)
        {
            producto.State = State.Deleted;
            new ProductoD().Save(producto, nm);
        }

        public void Edit(Producto producto, NotificationMessage nm)
        {
            producto.State = State.Modified;
            new ProductoD().Save(producto, nm);
        }

        public Producto Read(int idProducto)
        {
            return new ProductoD().Read(idProducto);
        }

        public Producto Read(string nombre)
        {
            return new ProductoD().Read(nombre);
        }

        public List<Producto> ReadList()
        {
            return new ProductoD().ReadList();
        }

        public void Create(Producto producto, NotificationMessage nm)
        {
            producto.State = State.Added;
            new ProductoD().Save(producto, nm);
        }

    }
}