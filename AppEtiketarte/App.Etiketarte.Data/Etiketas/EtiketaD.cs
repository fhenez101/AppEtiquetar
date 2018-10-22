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
    public class EtiketaD : DataBase<Etiketa>
    {
        public Etiketa Read(int idEtiketa)
        {
            return db.Etiketas
                .Include(x => x.Producto)
                .Select(x => x)
                .Where(x => x.IdEtiketa == idEtiketa)
                .SingleOrDefault();
        }

        public Etiketa Read(string nombre)
        {
            return db.Etiketas
                .Include(x => x.Producto)
                .Select(x => x)
                .Where(x => x.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        public List<Etiketa> ReadList()
        {
            return db.Etiketas
                .Include(x => x.Producto)
                .Select(x => x)
                .ToList();
        }
    }
}