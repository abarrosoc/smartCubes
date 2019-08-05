using Plugin.Messaging;
using smartCubes.Models;

namespace smartCubes.Utils
{
    public class Mail
    {
        public Mail(string filePath, UserModel user)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                var email = new EmailMessageBuilder()
                .To(user.Email)
                .Subject("Smart Games - Exportar sesión")
                .Body("Se adjunta la sesión seleccionada")
                .WithAttachment(filePath, "application/msexcel")
                .Build();

                emailMessenger.SendEmail(email);

            }   
        }

    }
}
