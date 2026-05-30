using System.Threading.Tasks;

namespace ModularNotificationService.Cap2.Contracts
{
    public interface INotificationChannel
    {
        Task SendAsync(string recipient, string message);
    }
}