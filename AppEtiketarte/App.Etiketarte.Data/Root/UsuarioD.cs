using App.Etiketarte.Data.Common;
using App.Etiketarte.Model;
using System.Data.Entity;
using System.Linq;

namespace App.Etiketarte.Data.Root
{
    public class UsuarioD : DataBase<Usuario>
    {
        public Usuario Read(string facebookId)
        {
            return db.Usuarios
                .Include(x => x.Perfil)
                .Select(x => x)
                .Where(x => x.FacebookId.Equals(facebookId) && x.Estado == true)
                .SingleOrDefault();
        }
    }
}