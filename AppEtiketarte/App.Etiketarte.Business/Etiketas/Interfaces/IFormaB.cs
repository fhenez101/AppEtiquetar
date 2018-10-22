using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IFormaB
    {
        //Forma
        void Create(Forma forma, NotificationMessage nm);
        void Edit(Forma forma, NotificationMessage nm);
        void Delete(Forma forma, NotificationMessage nm);
        Forma Read(int idForma);
        Forma Read(string nombre);
        List<Forma> ReadList();
        List<Forma> GetFormasOrden(int idEtiketa);

        //FormaSplit
        void EditFormaSplit(ICollection<FormaSplit> formaSplit, string[] splitName, NotificationMessage nm);
    }
}
