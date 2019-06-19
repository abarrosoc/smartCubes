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
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //string filePath = Path.Combine(path, fileName);
            var text = File.ReadAllText(filePath);
            if (emailMessenger.CanSendEmail)
            {
                var email = new EmailMessageBuilder()
                .To(user.Email)
                  //.Cc("cc.plugins@xamarin.com")
                  //.Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
                .Subject("Smart Games - Exportar sesión")
                .Body("Se adjunta la sesión seleccionada")
                .WithAttachment(filePath, "application/msexcel")
                .Build();

                emailMessenger.SendEmail(email);

            }   
        }

    }
}
