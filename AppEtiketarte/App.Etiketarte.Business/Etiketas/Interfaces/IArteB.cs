using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IArteB
    {
        void Create(Arte arte, NotificationMessage nm);
        void Edit(Arte arte, NotificationMessage nm);
        void Delete(Arte arte, NotificationMessage nm);
        Arte Read(int idArte);
        Arte Read(string nombre);
        List<Arte> ReadList();
        List<Arte> GetArtesOrden(int idEtiketa, string codigo);
    }
}