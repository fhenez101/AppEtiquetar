using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using App.Etiketarte.Model.ViewModels;
using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace App.Etiketarte.Data.Etiketas
{
    public class OrdenD : DataBase<Orden>
    {
        public VmEtiketasML GetTipografiasColorsConfiguration(string codigo, NotificationMessage nm)
        {
            var arte = db.Artes
                .AsEnumerable()
                .Select(x => new Arte
                {
                    IdArte = x.IdArte,
                    Codigo = x.Codigo
                })
                .Where(x => x.Codigo == codigo)
                .FirstOrDefault();

            var vmEtiketasML = db.ArteFormas
                .AsEnumerable()
                .Select(x => new VmEtiketasML
                {
                    IdArteForma = x.IdArteForma,
                    IdArte = x.IdArte,
                    IdConfEtiketa = x.IdConfEtiketa,
                    TextTop = x.TextTop,
                    TextLeft = x.TextLeft,
                    TextAlign = x.TextAlign,
                    NumeroLineas = x.NumeroLineas,
                    CaracteresLinea = x.CaracteresLinea,
                    FontMinSize = x.FontMinSize,
                    FontMaxSize = x.FontMaxSize,
                    ContainerWidth = x.ContainerWidth
                })
                .Where(x => x.IdArte == arte.IdArte)
                .SingleOrDefault();

            var detalleTipografias = db.DetalleTipografias
                .Include(x => x.ArteForma)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => vmEtiketasML.IdArteForma == x.IdArteForma)
                .ToList();

            var tipografias = db.Tipografias
                .AsEnumerable()
                .Select(x => new Tipografia
                {
                    IdTipoGrafia = x.IdTipoGrafia,
                    Nombre = x.Nombre,
                })
                .Where(x => detalleTipografias.Select(y => y.IdTipografia).Contains(x.IdTipoGrafia))
                .ToList();

            var detalleColores = db.DetalleColors
                .Include(x => x.ArteForma)
                .AsEnumerable()
                .Select(x => x)
                .Where(x => vmEtiketasML.IdArteForma == x.IdArteForma)
                .ToList();

            var colores = db.Colors
                .AsEnumerable()
                .Select(x => new Color
                {
                    IdColor = x.IdColor,
                    Nombre = x.Nombre,
                    Hexadecimal = x.Hexadecimal
                })
                .Where(x => detalleColores.Select(y => y.IdColor).Contains(x.IdColor))
                .ToList();

            vmEtiketasML.Tipografias = tipografias.Select(x => new VmTipografia
            {
                Id = x.IdTipoGrafia,
                Nombre = x.Nombre
            }).ToList();
            vmEtiketasML.Colores = colores.Select(x => new VmColor
            {
                Id = x.IdColor,
                Nombre = x.Nombre,
                Hexadecimal = x.Hexadecimal
            }).ToList();

            return vmEtiketasML;
        }
    }
}