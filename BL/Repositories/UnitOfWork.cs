using BL.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlMohamyDbContext _context;

        public IBaseRepository<ApplicationUser> Users { get; private set; }
        public IBaseRepository<Notification> Notifications { get; private set; }
        public IBaseRepository<NotificationConfirmed> NotificationsConfirmed { get; }












        public UnitOfWork(AlMohamyDbContext context)
        {
            _context = context;
            NotificationsConfirmed = new BaseRepository<NotificationConfirmed>(_context);
            Notifications = new BaseRepository<Notification>(_context);


        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
