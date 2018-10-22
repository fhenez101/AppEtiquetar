using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using App.Etiketarte.Notification;

namespace App.Etiketarte.Data.Etiketas
{
    public class FormaD : DataBase<Forma>
    {
        public Forma Read(int idForma)
        {
            return db.Formas
                .Include(x => x.FormaSplits)
                .Select(x => x)
                .Where(x => x.IdForma == idForma)
                .SingleOrDefault();
        }

        public Forma Read(string nombre)
        {
            return db.Formas
                .Include(x => x.FormaSplits)
                .Select(x => x)
                .Where(x => x.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        public List<Forma> ReadList()
        {
            return db.Formas.Include(x => x.FormaSplits).Select(x => x).ToList();
        }

        public List<Forma> GetFormasOrden(int idEtiketa)
        {
            var arteForma = db.ArteFormas
                .AsEnumerable()
                .Select(x => new ArteForma
                {
                    IdConfEtiketa = x.IdConfEtiketa,
                    IdForma = x.IdForma
                })
                .Where(x => new ConfEtiketaD().ReadList(idEtiketa).Select(y => y.IdConfEtiketa).Contains(x.IdConfEtiketa))
                .ToList();

            return db.Formas
                .Include(x => x.FormaSplits)
                .AsEnumerable()
                .Select(x => new Forma
                {
                    IdForma = x.IdForma,
                    Codigo = x.Codigo,
                    Nombre = x.Nombre,
                    FormaSplits = x.FormaSplits
                })
                .Where(x => arteForma.Select(y => y.IdForma).Contains(x.IdForma))
                .ToList();
        }

        public void Delete(Forma forma, NotificationMessage nm)
        {
            using (var context = db)
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var split = db.FormaSplits
                             .Select(x => x)
                             .Where(x => x.IdForma == forma.IdForma)
                             .ToList();

                        var foma = db.Formas
                            .Select(x => x)
                            .Where(x => x.IdForma == forma.IdForma)
                            .FirstOrDefault();

                        context.FormaSplits.RemoveRange(split);
                        context.Formas.Remove(foma);

                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        ManageCatch(ex, nm);
                    }
                }
            }
        }
    }
}