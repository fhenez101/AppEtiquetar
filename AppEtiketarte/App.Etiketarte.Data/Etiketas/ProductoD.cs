using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace App.Etiketarte.Data.Etiketas
{
    public class ProductoD : DataBase<Producto>
    {
        public Producto Read(int idProducto)
        {
            return db.Productoes
                .Include(x => x.Etiketas)
                .Select(x => x)
                .Where(x => x.IdProducto == idProducto)
                .SingleOrDefault();
        }

        public Producto Read(string nombre)
        {
            return db.Productoes
                .Include(x => x.Etiketas)
                .Select(x => x)
                .Where(x => x.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        public List<Producto> ReadList()
        {
            return db.Productoes.Include(x => x.Etiketas).Select(x => x).ToList();
        }
    }
}