using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Root.Interfaces
{
    public interface IUsuarioB
    {
        void Create(Usuario usuario, NotificationMessage nm);
        void Edit(Usuario usuario, NotificationMessage nm);
        void Delete(Usuario usuario, NotificationMessage nm);
        Usuario Read(string facebookId);
    }
}
