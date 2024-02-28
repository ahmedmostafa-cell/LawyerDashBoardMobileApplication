
using AlMohamyProject.Dtos;
using BL.Dtos;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}
