using System.Threading.Tasks;
using FitCenter.Models.Model;


namespace FitCenter.Data.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}