using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace App.Etiketarte.Data.Etiketas
{
    public class ConfEtiketaD : DataBase<ConfEtiketa>
    {
        public ConfEtiketa Read(int idConfEtiketa)
        {
            return db.ConfEtiketas
                .Include(x => x.Etiketa)
                .Select(x => x)
                .Where(x => x.IdConfEtiketa == idConfEtiketa)
                .SingleOrDefault();
        }

        public ConfEtiketa Read(string nombre)
        {
            return db.ConfEtiketas
                .Include(x => x.Etiketa)
                .Select(x => x)
                .Where(c => c.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        public List<ConfEtiketa> ReadList()
        {
            return db.ConfEtiketas
                .Include(x => x.Etiketa)
                .Select(x => x)
                .ToList();
        }

        public List<ConfEtiketa> ReadList(int idEtiketa)
        {
            var confEtiketa = db.ConfEtiketas
                .Include(x => x.Etiketa)
                .AsEnumerable()
                .Select(x => new ConfEtiketa
                {
                    IdConfEtiketa = x.IdConfEtiketa,
                    Estado = x.Estado,
                    Etiketa = x.Etiketa
                })
                .Where(x => x.Etiketa.IdEtiketa == idEtiketa && x.Estado == true && x.Etiketa.Estado == true)
                .ToList();

            return confEtiketa;
        }

        public ConfEtiketa GetConfEtiketaOrden(int idConfEtiketa)
        {
            var confEtiketa = db.ConfEtiketas
                .AsEnumerable()
                .Select(x => new ConfEtiketa
                {
                    IdConfEtiketa = x.IdConfEtiketa
                })
                .Where(x => x.IdConfEtiketa == idConfEtiketa)
                .FirstOrDefault();

            return confEtiketa;
        }

        public List<ConfEtiketa> GetConfEtiketaOferta(int idEtiketa)
        {
            var confEtiketa = db.ConfEtiketas
                .AsEnumerable()
                .Select(x => new ConfEtiketa
                {
                    IdConfEtiketa = x.IdConfEtiketa,
                    IdEtiketa = x.IdEtiketa,
                    Nombre = x.Nombre
                })
                .Where(x => x.IdEtiketa == idEtiketa)
                .ToList();

            return confEtiketa;
        }
    }
}