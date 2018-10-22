using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface ILogoB
    {
        // Logo
        void Create(Logo logo, NotificationMessage nm);
        void Edit(Logo logo, NotificationMessage nm);
        void Delete(Logo logo, NotificationMessage nm);
        Logo Read(int idLogo);
        Logo Read(string nombre);
        List<Logo> ReadList();

        //LogoSplit
        //FormaSplit
        void EditLogoSplit(ICollection<LogoSplit> logoSplit, string[] splitName, NotificationMessage nm);
        void DeleteLogoSplit(ICollection<LogoSplit> logoSplit, NotificationMessage nm);
    }
}
