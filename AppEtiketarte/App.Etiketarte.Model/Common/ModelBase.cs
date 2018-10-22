using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Model.Common
{
    /// <summary>
    /// Clase base para las clases modelo básicas. Define la gestión del estado del objeto.
    /// </summary>
    public class ModelBase : IObjectWithState
    {
        /// <summary>
        /// Propiedad utilizada para la validación de la Concurrenci Optimista.
        /// </summary>
        //[Timestamp]
        //public byte[] RowVersion { get; set; }

        /// <summary>
        /// Estado actual del objeto modelo.
        /// </summary>                 
        [ScaffoldColumn(false)]
        [NotMapped]
        public State State { get; set; }

        /// <summary>
        /// Propiedades cuyos valores han sido modificados.
        /// </summary>
        [ScaffoldColumn(false)]
        [NotMapped]
        public ObservableCollection<string> ModifiedProperties { get; set; }

        public ModelBase()
        {
            ModifiedProperties = new ObservableCollection<string>();

            // En caso que se conozcan cambios a la colección de propiedades modificadas se debe considerar a objeto del modelo como modificado
            ModifiedProperties.CollectionChanged += (sender, e) =>
            {
                State = State.Modified;
            };
        }
    }
}