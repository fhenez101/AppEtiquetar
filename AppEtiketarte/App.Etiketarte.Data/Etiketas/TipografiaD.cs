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
    public class TipografiaD : DataBase<Tipografia>
    {
        public Tipografia Read(int idTipografia)
        {
            return db.Tipografias
                .Include(x => x.DetalleTipografias)
                .Select(x => x)
                .Where(x => x.IdTipoGrafia == idTipografia)
                .SingleOrDefault();
        }

        public Tipografia Read(string nombre)
        {
            return db.Tipografias
                .Include(x => x.DetalleTipografias)
                .Select(x => x)
                .Where(x => x.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        public List<Tipografia> ReadList()
        {
            return db.Tipografias.Include(x => x.DetalleTipografias).Select(x => x).ToList();
        }

        public List<Tipografia> GetTipografiasOrden(string codigo)
        {
            var arte = db.Artes
                .AsEnumerable()
                .Select(x => new Arte
                {
                    IdArte = x.IdArte,
                    Codigo = x.Codigo
                })
                .Where(x => x.Codigo == codigo)
                .FirstOrDefault();

            var arteForma = db.ArteFormas
                .AsEnumerable()
                .Select(x => new ArteForma
                {
                    IdConfEtiketa = x.IdConfEtiketa,
                    IdArteForma = x.IdArteForma,
                    IdArte = x.IdArte
                })
                .Where(x => x.IdArte == arte.IdArte)
                .ToList();

            var detalleTipografias = db.DetalleTipografias
                .Include(x => x.ArteForma)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => arteForma.Select(y => y.IdArteForma).Contains(x.IdArteForma))
                .ToList();

            var tipografias = db.Tipografias
                .AsEnumerable()
                .Select(x => new Tipografia
                {
                    IdTipoGrafia = x.IdTipoGrafia,
                    Nombre = x.Nombre,
                })
                .Where(x => detalleTipografias.Select(y => y.IdTipografia).Contains(x.IdTipoGrafia))
                .ToList();

            return tipografias;
        }
    }
}