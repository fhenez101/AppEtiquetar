using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using System.Collections.Generic;
using System.Linq;

namespace App.Etiketarte.Data.Root
{
    public class CantonD : DataBase<CANTON_CR>
    {
        public List<CANTON_CR> Read(int codigo_provincia)
        {
            return db.CANTON_CR
                .AsEnumerable()
                .Select(x => new CANTON_CR
                {
                    codigo_canton = x.codigo_canton,
                    codigo_provincia = x.codigo_provincia,
                    nombre_canton = x.nombre_canton
                })
                .Where(x => x.codigo_provincia == codigo_provincia)
                .ToList();
        }

        public List<CANTON_CR> ReadList()
        {
            return db.CANTON_CR
                .AsEnumerable()
                .Select(x => new CANTON_CR
                {
                    codigo_canton = x.codigo_canton,
                    codigo_provincia = x.codigo_provincia,
                    nombre_canton = x.nombre_canton
                })
                .ToList();
        }
    }
}