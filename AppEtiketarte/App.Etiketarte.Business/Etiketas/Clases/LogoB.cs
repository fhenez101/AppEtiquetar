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
    public class LogoB : ILogoB
    {
        public void Delete(Logo logo, NotificationMessage nm)
        {
            logo.State = State.Deleted;
            new LogoD().Save(logo, nm);
        }

        public void Edit(Logo logo, NotificationMessage nm)
        {
            logo.State = State.Modified;
            new LogoD().Save(logo, nm);
        }

        public Logo Read(int idLogo)
        {
            return new LogoD().Read(idLogo);
        }

        public Logo Read(string nombre)
        {
            return new LogoD().Read(nombre);
        }

        public List<Logo> ReadList()
        {
            return new LogoD().ReadList();
        }

        public void Create(Logo logo, NotificationMessage nm)
        {
            logo.State = State.Added;
            new LogoD().Save(logo, nm);
        }

        public void EditLogoSplit(ICollection<LogoSplit> logoSplit, string[] splitName, NotificationMessage nm)
        {
            int counter = 0;

            logoSplit.ToList().ForEach(x =>
            {
                x.Logo = null;
                x.Nombre = splitName[counter];
                x.State = State.Modified;
                x.ModifiedProperties = new ObservableCollection<string>() { (nameof(x.Nombre)) };
                new LogoSplitD().Save(x, nm);
                counter++;
            });
        }

        public void DeleteLogoSplit(ICollection<LogoSplit> logoSplit, NotificationMessage nm)
        {
            logoSplit.ToList().ForEach(x =>
            {
                x.Logo = null;
                x.State = State.Deleted;
                new LogoSplitD().Save(x, nm);
            });
        }
    }
}
