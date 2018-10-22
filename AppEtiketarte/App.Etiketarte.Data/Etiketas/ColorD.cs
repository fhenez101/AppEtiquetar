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
    public class ColorD : DataBase<Color>
    {
        public Color Read(int idColor)
        {
            return db.Colors
                .Include(x => x.DetalleColors)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => x.IdColor == idColor)
                .SingleOrDefault();
        }

        public Color Read(string nombre)
        {
            return db.Colors
                .Include(x => x.DetalleColors)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => x.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        public List<Color> ReadList()
        {
            return db.Colors.Include(x => x.DetalleColors).AsEnumerable().Select(x => x).ToList();
        }

        public List<Color> GetColoresOrden(string codigo)
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
                    IdArteForma = x.IdArteForma,
                    IdArte = x.IdArte
                })
                .Where(x => x.IdArte == arte.IdArte)
                .ToList();

            var detalleColores = db.DetalleColors
                .Include(x => x.ArteForma)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => arteForma.Select(y => y.IdArteForma).Contains(x.IdArteForma))
                .ToList();

            var colores = db.Colors
                .AsEnumerable()
                .Select(x => new Color
                {
                    IdColor = x.IdColor,
                    Nombre = x.Nombre,
                    Hexadecimal = x.Hexadecimal
                })
                .Where(x => detalleColores.Select(y => y.IdColor).Contains(x.IdColor))
                .ToList();

            return colores;
        }
    }
}