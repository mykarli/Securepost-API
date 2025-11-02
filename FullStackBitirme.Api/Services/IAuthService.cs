using System.Threading.Tasks;
using FullStackBitirme.Api.Models;

namespace FullStackBitirme.Api.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string fullName, string email, string password);
        Task<User?> LoginAsync(string email, string password);
    }
}
