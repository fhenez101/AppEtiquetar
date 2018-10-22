using App.Etiketarte.Business.Root.Interfaces;
using System.Collections.Generic;
using App.Etiketarte.Model;
using App.Etiketarte.Data.Root;
using System;

namespace App.Etiketarte.Business.Root.Clases
{
    public class ProvinciaCantonDistritoB : IProvinciaCantonDistritoB
    {
        public List<CANTON_CR> CantonRead(int codigo_provincia)
        {
            return new CantonD().Read(codigo_provincia);
        }

        public List<CANTON_CR> CantonReadList()
        {
            return new CantonD().ReadList();
        }

        public List<DISTRITO_CR> DistritoRead(int codigo_canton)
        {
            return new DistritoD().Read(codigo_canton);
        }

        public List<DISTRITO_CR> DistritoReadList()
        {
            return new DistritoD().ReadList();
        }

        public List<PROVINCIA_CR> ProvinciaReadList()
        {
            return new ProvinciaD().ReadList();
        }
    }
}