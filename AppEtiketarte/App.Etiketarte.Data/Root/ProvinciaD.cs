using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using System.Collections.Generic;
using System.Linq;


namespace App.Etiketarte.Data.Root
{
    public class ProvinciaD : DataBase<PROVINCIA_CR>
    {
        public List<PROVINCIA_CR> ReadList()
        {
            return db.PROVINCIA_CR
                .AsEnumerable()
                .Select(x => new PROVINCIA_CR
                {
                    codigo_provincia = x.codigo_provincia,
                    nombre_provincia = x.nombre_provincia
                })
                .ToList();
        }
    }
}