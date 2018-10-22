using App.Etiketarte.Business.Etiketas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Etiketarte.Notification;
using App.Etiketarte.Data.Etiketas;
using App.Etiketarte.Model.ViewModels;

namespace App.Etiketarte.Business.Etiketas.Clases
{
    public class OrdenB : IOrdenB
    {
        public VmEtiketasML GetTipografiasColorsConfiguration(string codigo, NotificationMessage nm)
        {
            return new OrdenD().GetTipografiasColorsConfiguration(codigo, nm);
        }
    }
}