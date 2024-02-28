using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using System.Linq;
using System.Threading.Tasks;

namespace AlMohamyProject.Hubs
{
    public class BasicChatHub : Hub
    {
        private readonly AlMohamyDbContext _db;
        public BasicChatHub(AlMohamyDbContext db)
        {
            _db = db;
        }

        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("MessageReceived", user, message);
        }
        [Authorize]
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            var userId = _db.Users.FirstOrDefault(u => u.Email.ToLower() == receiver.ToLower()).Id;

            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.User(userId).SendAsync("MessageReceived", sender, message);
            }

        }

    } 
}
