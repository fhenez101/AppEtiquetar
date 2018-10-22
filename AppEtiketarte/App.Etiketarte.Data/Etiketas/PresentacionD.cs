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
    public class PresentacionD : DataBase<Presentacion>
    {
        public Presentacion Read(int idPresentacion)
        {
            return db.Presentacions
                .Include(x => x.Etiketa)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => x.IdPresentacion == idPresentacion)
                .SingleOrDefault();
        }

        public List<Presentacion> ReadList(int idEtiketa)
        {
            return db.Presentacions
                .Include(x => x.Etiketa)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => x.IdEtiketa == idEtiketa)
                .ToList();
        }

        public List<Presentacion> GetPresentacionOferta(int idEtiketa)
        {
            return db.Presentacions
                .AsEnumerable()
                .Select(x => new Presentacion
                {
                    IdPresentacion = x.IdPresentacion,
                    IdEtiketa = x.IdEtiketa,
                    UnidadPorPaquete = x.UnidadPorPaquete
                })
                .Where(x => x.IdEtiketa == idEtiketa)
                .ToList();
        }
    }
}