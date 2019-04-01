using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Plugin.Messaging;
using smartCubes.Models;

namespace smartCubes.Utils
{
    public class Mail
    {
        public Mail(String filePath, UserModel user)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //string filePath = Path.Combine(path, fileName);
            var text = File.ReadAllText(filePath);
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                //emailMessenger.SendEmail("alvaro.barroso16@gmail.com", "Xamarin Messaging Plugin", "Well hello there from Xam.Messaging.Plugin");

                // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc. 
                var email = new EmailMessageBuilder()
                .To(user.Email)
                  //.Cc("cc.plugins@xamarin.com")
                  //.Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
                .Subject("Exportar sesion")
                .Body("Se adjunta la sesion seleccionada")
                .WithAttachment(filePath, "application/msexcel")
                .Build();

                emailMessenger.SendEmail(email);

            }   
        }

    }
}
