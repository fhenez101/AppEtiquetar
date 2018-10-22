using App.Etiketarte.Model;
using App.Etiketarte.Model.ViewModels;
using App.Etiketarte.Notification;
using System.Collections.Generic;

namespace App.Etiketarte.Business.Etiketas.Interfaces
{
    public interface IArteFormaB
    {
        void Create(VmArteFormaMI vmArteForma, NotificationMessage nm);
        void Edit(VmArteFormaMU vmArteForma, NotificationMessage nm);
        void Delete(int idArteForma, NotificationMessage nm);
        VmArteFormaML Read(int idArteForma);
        List<VmArteFormaML> ReadList(int idConfEtiketa);
        ArteForma GetArteFormaOrden(string codigo);
    }
}