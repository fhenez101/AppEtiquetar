using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Notification
{
    public static class Messages
    {
        public static class General
        {
            // TODO Cambiar para hacer uso de recursos
            public static Message READY = new Message()
            {
                Id = 1,
                Level = MessageLevel.Information,
                Title = "La acción se ejecutó satisfactoriamente."
            };

            public static Message EXCEPTION = new Message()
            {
                Id = 2,
                Level = MessageLevel.Warning,
                Title = "Se ha presentado una excepción."
            };

            public static MessageConcurrency CONCURRENCY_DELETE = new MessageConcurrency()
            {
                Id = 3,
                Level = MessageLevel.Warning,
                Title = "No fué posible aplicar la eliminación. Los datos fueron eliminados por otro usuario.",
                Description = "La acción fué cancelada debido a que los datos que se intenta eliminar ya no existen en la base de datos y fueron eliminados por otro usuario."
            };

            public static MessageConcurrency CONCURRENCY_UPDATE = new MessageConcurrency()
            {
                Id = 4,
                Level = MessageLevel.Warning,
                Title = "No fué posible aplicar la modificación. Los datos fueron modificados por otro usuario.",
                Description = "La acción fué cancelada debido a que los datos que se intenta modificar, fueron previamente por otro usuario."
            };

            public static Message NO_EXISTS = new Message()
            {
                Id = 5,
                Level = MessageLevel.Warning,
                Title = "El dato solicitado no existe.",
                Description = "La acción intentó afectar datos que no existen."
            };
        }
    }
}