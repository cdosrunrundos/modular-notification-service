
using System;
using System.Threading.Tasks;
using ModularNotificationService.Cap2.Contracts;

namespace ModularNotificationService.Cap2.Services
{
    public class PushService : INotificationChannel
    {
        public Task SendAsync(string recipient, string message)
        {
            // Simulación pura para cumplir con el contrato
            Console.WriteLine($"[Push-Firebase] Notificación enviada al dispositivo {recipient}: {message}");
            return Task.CompletedTask;
        }
    }
}