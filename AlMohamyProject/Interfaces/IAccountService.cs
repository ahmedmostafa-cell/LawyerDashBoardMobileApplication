using AlMohamyProject.Models;
using System.Threading.Tasks;

namespace AlMohamyProject.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Logout(string userName);

        Task<AuthModel> LoginAsync(LoginModel model);
    }
}
