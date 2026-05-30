using System;
using System.Threading.Tasks;
using ModularNotificationService.Cap2.Services;
using ModularNotificationService.Cap2.UseCases;

namespace ModularNotificationService.Cap2
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // Composición de la infraestructura (Inyección manual)
            var emailService = new EmailService();
            var pushService = new PushService();
            var smsService = new SmsService();

            var registerUserUseCase = new RegisterUser(emailService, pushService, smsService);

            // Escenario 1: Usuario Web Común
            var userWeb = new RegisterUserInput("John Doe", "john@doe.com", "", "", "Web", false);
            await registerUserUseCase.ExecuteAsync(userWeb);

            // Escenario 2: Usuario Mobile VIP
            var userMobileVip = new RegisterUserInput("Anna VIP", "", "token_firebase_123", "+5411234567", "Mobile", true);
            await registerUserUseCase.ExecuteAsync(userMobileVip);
        }
    }
}