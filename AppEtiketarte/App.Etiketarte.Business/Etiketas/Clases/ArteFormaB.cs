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
using App.Etiketarte.Model.ViewModels;

namespace App.Etiketarte.Business.Etiketas.Clases
{
    public class ArteFormaB : IArteFormaB
    {
        public void Create(VmArteFormaMI vmArteForma, NotificationMessage nm)
        {
            (new ArteFormaD()).CreateArteForma(vmArteForma, nm);
        }

        public void Delete(int idArteForma, NotificationMessage nm)
        {
            (new ArteFormaD()).DeleteArteForma(idArteForma, nm);
        }

        public void Edit(VmArteFormaMU vmArteForma, NotificationMessage nm)
        {
            (new ArteFormaD()).EditArteForma(vmArteForma, nm);
        }

        public ArteForma GetArteFormaOrden(string codigo)
        {
            return new ArteFormaD().GetArteFormaOrden(codigo);
        }

        public VmArteFormaML Read(int idArteForma)
        {
            return (new ArteFormaD()).Read(idArteForma);
        }

        public List<VmArteFormaML> ReadList(int idConfEtiketa)
        {
            return (new ArteFormaD()).ReadList(idConfEtiketa);
        }
    }
}