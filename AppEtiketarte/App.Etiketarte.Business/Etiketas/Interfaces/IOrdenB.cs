using App.Etiketarte.Model.ViewModels;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IOrdenB
    {
        VmEtiketasML GetTipografiasColorsConfiguration(string codigo, NotificationMessage nm);
    }
}