using System;
using System.Threading.Tasks;
using ModularNotificationService.Cap2.Contracts; // Corregido: apunta al contrato actual

namespace ModularNotificationService.Cap2.UseCases
{
    public record RegisterUserInput(string Name, string Email, string DeviceToken, string PhoneNumber, string Origin, bool IsVip);

    public class RegisterUser
    {
        private readonly INotificationChannel _emailChannel;
        private readonly INotificationChannel _pushChannel;
        private readonly INotificationChannel _smsChannel;

        public RegisterUser(
            INotificationChannel emailChannel, 
            INotificationChannel pushChannel, 
            INotificationChannel smsChannel)
        {
            _emailChannel = emailChannel;
            _pushChannel = pushChannel;
            _smsChannel = smsChannel;
        }

        public async Task ExecuteAsync(RegisterUserInput input)
        {
            Console.WriteLine($"\n[UseCase] Procesando registro de {input.Name}...");

            // 1. Regla por canal de Origen
            if (input.Origin.Equals("Web", StringComparison.OrdinalIgnoreCase))
            {
                // Pasamos el mail como destino y el mensaje construido por el caso de uso
                await _emailChannel.SendAsync(input.Email, $"Bienvenido a la plataforma web, {input.Name}!");
            }
            else if (input.Origin.Equals("Mobile", StringComparison.OrdinalIgnoreCase))
            {
                await _pushChannel.SendAsync(input.DeviceToken, $"¡Tu cuenta ha sido creada desde la App, {input.Name}!");
            }

            // 2. Regla Transversal de Negocio: Condición VIP
            if (input.IsVip)
            {
                await _smsChannel.SendAsync(input.PhoneNumber, $"[ALERTA VIP] {input.Name}, tu registro prioritario fue confirmado.");
            }
        }
    }
}