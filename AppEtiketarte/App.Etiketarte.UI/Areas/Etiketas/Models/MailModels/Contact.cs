using System.ComponentModel.DataAnnotations;

namespace App.Etiketarte.UI.Areas.Etiketas.Models.MailModels
{
    public class Contact
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "El E-Mail es requerido")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El E-Mail no es válido")]
        public string Correo { get; set; }
        public string Telefono { get; set; }
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }
        public string ReCaptcha { get; set; }
    }
}