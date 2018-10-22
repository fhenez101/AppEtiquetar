using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using System.Collections.Generic;
using System.Linq;

namespace App.Etiketarte.Data.Root
{
    public class DistritoD : DataBase<DISTRITO_CR>
    {
        public List<DISTRITO_CR> Read(int codigo_canton)
        {
            return db.DISTRITO_CR
                .AsEnumerable()
                .Select(x => new DISTRITO_CR
                {
                    codigo_distrito = x.codigo_distrito,
                    codigo_canton = x.codigo_canton,
                    nombre_distrito = x.nombre_distrito
                })
                .Where(x => x.codigo_canton == codigo_canton)
                .ToList();
        }

        public List<DISTRITO_CR> ReadList()
        {
            return db.DISTRITO_CR
                .AsEnumerable()
                .Select(x => new DISTRITO_CR
                {
                    codigo_distrito = x.codigo_distrito,
                    codigo_canton = x.codigo_canton,
                    nombre_distrito = x.nombre_distrito
                })
                .ToList();
        }
    }
}