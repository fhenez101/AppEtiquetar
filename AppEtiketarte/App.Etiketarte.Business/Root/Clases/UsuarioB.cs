using App.Etiketarte.Business.Root.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using App.Etiketarte.Model.Common;
using App.Etiketarte.Data.Root;

namespace App.Etiketarte.Business.Root.Clases
{
    public class UsuarioB : IUsuarioB
    {
        public void Create(Usuario usuario, NotificationMessage nm)
        {
            usuario.State = State.Added;
            new UsuarioD().Save(usuario, nm);
        }

        public void Delete(Usuario usuario, NotificationMessage nm)
        {
            usuario.State = State.Deleted;
            new UsuarioD().Save(usuario, nm);
        }

        public void Edit(Usuario usuario, NotificationMessage nm)
        {
            usuario.State = State.Modified;
            new UsuarioD().Save(usuario, nm);
        }

        public Usuario Read(string facebookId)
        {
            return new UsuarioD().Read(facebookId);
        }
    }
}