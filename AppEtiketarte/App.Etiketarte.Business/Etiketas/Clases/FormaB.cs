using App.Etiketarte.Business.Etiketas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using App.Etiketarte.Data.Etiketas;
using App.Etiketarte.Model.Common;
using System.Collections.ObjectModel;

namespace App.Etiketarte.Business.Etiketas.Clases
{
    public class FormaB : IFormaB
    {
        public void Delete(Forma forma, NotificationMessage nm)
        {
            new FormaD().Delete(forma, nm);
        }

        public void Edit(Forma forma, NotificationMessage nm)
        {
            forma.State = State.Modified;
            new FormaD().Save(forma, nm);
        }

        public Forma Read(int idForma)
        {
            return new FormaD().Read(idForma);
        }

        public Forma Read(string nombre)
        {
            return new FormaD().Read(nombre);
        }

        public List<Forma> ReadList()
        {
            return new FormaD().ReadList();
        }

        public void Create(Forma forma, NotificationMessage nm)
        {
            forma.State = State.Added;
            new FormaD().Save(forma, nm);
        }

        public void EditFormaSplit(ICollection<FormaSplit> formaSplit, string[] splitName, NotificationMessage nm)
        {
            int counter = 0;

            formaSplit.ToList().ForEach(x =>
            {
                x.Forma = null;
                x.Nombre = splitName[counter];
                x.State = State.Modified;
                x.ModifiedProperties = new ObservableCollection<string>() { (nameof(x.Nombre)) };
                new FormaSplitD().Save(x, nm);
                counter++;
            });
        }

        public List<Forma> GetFormasOrden(int idEtiketa)
        {
            return new FormaD().GetFormasOrden(idEtiketa);
        }
    }
}