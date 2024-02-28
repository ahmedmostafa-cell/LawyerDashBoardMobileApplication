using Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<ApplicationUser> Users { get; }
        //IBaseRepository<ApplicationRole> Roles { get; }

        //-----------------------------------------------------------------------------------
        IBaseRepository<Notification> Notifications { get; }
        IBaseRepository<NotificationConfirmed> NotificationsConfirmed { get; }


        //-----------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------





        //-----------------------------------------------------------------------------------
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
