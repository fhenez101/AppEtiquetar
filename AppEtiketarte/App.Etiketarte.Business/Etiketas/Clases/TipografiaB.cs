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
    public class TipografiaB : ITipografiaB
    {
        public void Delete(Tipografia tipografia, NotificationMessage nm)
        {
            tipografia.State = State.Deleted;
            new TipografiaD().Save(tipografia, nm);
        }

        public void Edit(Tipografia tipografia, NotificationMessage nm)
        {
            tipografia.State = State.Modified;
            new TipografiaD().Save(tipografia, nm);
        }

        public Tipografia Read(int idTipografia)
        {
            return new TipografiaD().Read(idTipografia);
        }

        public Tipografia Read(string nombre)
        {
            return new TipografiaD().Read(nombre);
        }

        public List<Tipografia> ReadList()
        {
            return new TipografiaD().ReadList();
        }

        public void Create(Tipografia tipografia, NotificationMessage nm)
        {
            tipografia.State = State.Added;
            new TipografiaD().Save(tipografia, nm);
        }

        public List<Tipografia> GetTipografiasOrden(string codigo)
        {
            return new TipografiaD().GetTipografiasOrden(codigo);
        }
    }
}