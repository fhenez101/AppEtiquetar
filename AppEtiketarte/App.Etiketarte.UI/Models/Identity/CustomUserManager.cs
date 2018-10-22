using App.Etiketarte.Business.Root.Clases;
using App.Etiketarte.Business.Root.Interfaces;
using App.Etiketarte.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace App.Etiketarte.UI.Models.Identity
{
    public class CustomUserManager : UserManager<FacebookModel>
    {
        public CustomUserManager()
            : base(new CustomUserSore<FacebookModel>())
        { }

        public override Task<FacebookModel> FindAsync(string email, string facebookId)
        {
            var taskInvoke = Task<FacebookModel>.Factory.StartNew(() =>
            {
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(facebookId))
                {
                    var usuario = (new UsuarioB() as IUsuarioB).Read(facebookId);
                    string tmpFacebookId = usuario?.FacebookId ?? string.Empty;

                    if (tmpFacebookId.Equals(facebookId))
                    {
                        return new FacebookModel
                        {
                            Email = usuario.FacebookEmail,
                            FacebookId = usuario.FacebookId,
                            IsAdmin = (usuario.IdPerfil == 1) ? true : false
                        };
                    }
                }
                return null;
            });

            return taskInvoke;
        }
    }
}