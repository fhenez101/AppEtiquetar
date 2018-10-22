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
    public class LogoD : DataBase<Logo>
    {
        public Logo Read(int idLogo)
        {
            return db.Logoes
                .Include(x => x.LogoEtiketas)
                .Select(x => x)
                .Where(x => x.IdLogo == idLogo)
                .SingleOrDefault();
        }

        public Logo Read(string nombre)
        {
            return db.Logoes
                .Include(x => x.LogoEtiketas)
                .Select(x => x)
                .Where(x => x.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        public List<Logo> ReadList()
        {
            return db.Logoes.Include(x => x.LogoEtiketas).Select(x => x).ToList();
        }
    }
}