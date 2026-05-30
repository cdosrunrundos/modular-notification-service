using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using ModularNotificationService.Cap2.Contracts; 

namespace ModularNotificationService.Cap2.Services 
{
    public class EmailService : INotificationChannel 
    {
        
        public async Task SendAsync(string recipient, string messageText)
        {
            var message = new MimeMessage();

            var fromAddress = Environment.GetEnvironmentVariable("SMTP_SENDER_EMAIL") ?? "no-reply@example.com";
            var fromName = Environment.GetEnvironmentVariable("SMTP_SENDER_NAME") ?? "No Reply";

            message.From.Add(new MailboxAddress(fromName, fromAddress));
            message.To.Add(new MailboxAddress(recipient, recipient)); 
            message.Subject = "Bienvenido a la plataforma";

            var bodyBuilder = new BodyBuilder
            {
                TextBody = messageText 
            };

            message.Body = bodyBuilder.ToMessageBody();

            var host = Environment.GetEnvironmentVariable("SMTP_HOST") ?? "localhost";
            var portStr = Environment.GetEnvironmentVariable("SMTP_PORT") ?? "25";
            var user = Environment.GetEnvironmentVariable("SMTP_USER");
            var pass = Environment.GetEnvironmentVariable("SMTP_PASS");

            if (!int.TryParse(portStr, out var port))
            {
                port = 25;
            }

            SecureSocketOptions socketOptions = port switch
            {
                465 => SecureSocketOptions.SslOnConnect,
                587 => SecureSocketOptions.StartTls,
                _ => SecureSocketOptions.None
            };

            try
            {
                using var smtp = new SmtpClient();

                Console.WriteLine($"[SMTP Client] Conectando a {host}:{port} (SSL: {socketOptions})...");

                await smtp.ConnectAsync(host, port, socketOptions);

                if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pass))
                {
                    await smtp.AuthenticateAsync(user, pass);
                }

                Console.WriteLine($"[SMTP Client] Enviando email a {recipient}...");
                await smtp.SendAsync(message);

                await smtp.DisconnectAsync(true);

                Console.WriteLine("[SMTP Client] Correo enviado con éxito.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SMTP Client] Error enviando correo: {ex.Message}");
                Console.WriteLine("[SMTP Client] Información del email (fallback):");
                Console.WriteLine($"From: {fromName} <{fromAddress}>");
                Console.WriteLine($"To: {recipient}");
                Console.WriteLine($"Subject: {message.Subject}");
                Console.WriteLine($"Body: {bodyBuilder.TextBody}");
            }
        }
    }
}