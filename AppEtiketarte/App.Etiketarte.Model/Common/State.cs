using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Model.Common
{
    /// <summary>
    /// Estado de la entidad.
    /// </summary>
    public enum State : short
    {
        Added = 0,
        Unchanged = 1,
        Modified = 2,
        Deleted = 3
    }
}