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
    public class ArteD : DataBase<Arte>
    {
        public Arte Read(int idArte)
        {
            return db.Artes
                .Include(x => x.ArteSplits)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => x.IdArte == idArte)
                .SingleOrDefault();
        }

        public Arte Read(string nombre)
        {
            return db.Artes
                .Include(x => x.ArteSplits)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => x.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        public List<Arte> ReadList()
        {
            return db.Artes
                .Include(x => x.ArteSplits)
                .AsEnumerable()
                .Select(x => x)
                .ToList();
        }

        public List<Arte> GetArtesOrden(int idEtiketa, string codigo)
        {
            var arteForma = db.ArteFormas
                .Include(x => x.Forma)
                .AsEnumerable()
                .Select(x => new ArteForma
                {
                    IdConfEtiketa = x.IdConfEtiketa,
                    IdArte = x.IdArte,
                    Forma = x.Forma
                })
                .Where(x => new ConfEtiketaD().ReadList(idEtiketa).Select(y => y.IdConfEtiketa).Contains(x.IdConfEtiketa)
                    && x.Forma.Codigo == codigo)
                .ToList();

            return db.Artes
                .Include(x => x.ArteSplits)
                .AsEnumerable()
                .Select(x => new Arte
                {
                    IdArte = x.IdArte,
                    Codigo = x.Codigo,
                    Nombre = x.Nombre,
                    ArteSplits = x.ArteSplits
                })
                .Where(x => arteForma.Select(y => y.IdArte).Contains(x.IdArte))
                .ToList();
        }
    }
}