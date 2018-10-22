using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IColorB
    {
        void Create(Color color, NotificationMessage nm);
        void Edit(Color color, NotificationMessage nm);
        void Delete(Color color, NotificationMessage nm);
        Color Read(int idColor);
        Color Read(string nombre);
        List<Color> ReadList();
        List<Color> GetColoresOrden(string codigo);
    }
}
