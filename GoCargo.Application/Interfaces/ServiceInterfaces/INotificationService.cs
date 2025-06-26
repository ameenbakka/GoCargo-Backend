using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoCargo.Application.Dto;

namespace GoCargo.Application.Interfaces.ServiceInterfaces
{
    public interface INotificationService
    {
        Task SendNotification(int recipientId, string title, string message);
        Task<ICollection<NotificationDto>> GetUserNotifications(int userId);
        Task MarkAsRead(int notificationId);
    }
}
