using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Plugin.Messaging;

namespace smartCubes.Utils
{
    public class Mail
    {
        public Mail()
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                //emailMessenger.SendEmail("alvaro.barroso16@gmail.com", "Xamarin Messaging Plugin", "Well hello there from Xam.Messaging.Plugin");

                // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc. 
                var email = new EmailMessageBuilder()
                  .To("alvaro.barroso16@gmail.com")
                  //.Cc("cc.plugins@xamarin.com")
                  //.Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
                  .Subject("Exportar sesion")
                  .Body("Se adjunta la sesion seleccionada")
                   //.WithAttachment(SomeClass.FilePath, "pdf")
                  .Build();

                emailMessenger.SendEmail(email);

            }   
        }

    }
}
