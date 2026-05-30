using System;
using System.Threading.Tasks;
using ModularNotificationService.Cap2.Contracts; // Corregido: Namespace e interfaz añadidos

namespace ModularNotificationService.Cap2.Services
{
    public class SmsService : INotificationChannel
    {
        public async Task SendAsync(string recipient, string message)
        {
            await Task.Delay(50); 
            Console.WriteLine($"[SMS-Priority] Enviando a celular {recipient}: {message}");
        }
    }
}