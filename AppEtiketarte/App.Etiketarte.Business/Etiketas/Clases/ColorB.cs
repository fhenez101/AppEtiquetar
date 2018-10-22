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

namespace App.Etiketarte.Business.Etiketas.Clases
{
    public class ColorB : IColorB
    {
        public void Delete(Color color, NotificationMessage nm)
        {
            color.State = State.Deleted;
            new ColorD().Save(color, nm);
        }

        public void Edit(Color color, NotificationMessage nm)
        {
            color.State = State.Modified;
            new ColorD().Save(color, nm);
        }

        public Color Read(int idColor)
        {
            return new ColorD().Read(idColor);
        }

        public Color Read(string nombre)
        {
            return new ColorD().Read(nombre);
        }

        public List<Color> ReadList()
        {
            return new ColorD().ReadList();
        }

        public void Create(Color color, NotificationMessage nm)
        {
            color.State = State.Added;
            new ColorD().Save(color, nm);
        }

        public List<Color> GetColoresOrden(string codigo)
        {
            return new ColorD().GetColoresOrden(codigo);
        }
    }
}