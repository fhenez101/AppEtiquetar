using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Model.Common
{
    /// <summary>
    /// Interface que define el estado de un objeto y de las propiedades cuyos valores han sido modificados.
    /// </summary>
    public interface IObjectWithState
    {
        /// <summary>
        /// Estado del objeto modelo.
        /// </summary>
        State State { get; set; }

        /// <summary>
        /// Propiedades cuyos valores han sido modificados.
        /// </summary>
        ObservableCollection<string> ModifiedProperties { get; set; }
    }
}