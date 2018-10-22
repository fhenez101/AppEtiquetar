using App.Etiketarte.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Business.Root.Interfaces
{
    public interface IProvinciaCantonDistritoB
    {
        List<PROVINCIA_CR> ProvinciaReadList();
        List<CANTON_CR> CantonReadList();
        List<DISTRITO_CR> DistritoReadList();

        List<CANTON_CR> CantonRead(int codigo_provincia);
        List<DISTRITO_CR> DistritoRead(int codigo_canton);
    }
}
