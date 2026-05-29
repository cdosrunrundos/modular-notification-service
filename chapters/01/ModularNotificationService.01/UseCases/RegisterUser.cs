
using ModularNotificationService._01.Services;

namespace ModularNotificationService._01.UseCases
{
    public record RegisterUserInput(string Name, string Email);
    public class RegisterUser
    {
        private readonly EmailService _emailService;
        public RegisterUser()
        {
            _emailService = new EmailService();
        }

        public async Task ExecuteAsync(RegisterUserInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Name))
            {
                throw new ArgumentException("Datos de registro inválidos.");
            }

            Console.WriteLine($"[UseCase] Registrando al usuario {input.Name} en la base de datos...");

            await _emailService.SendWelcomeEmailAsync(input.Email, input.Name);
        }
    }
}
