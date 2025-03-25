using PharmaAPI.DTO;
using System.Threading.Tasks;

namespace PharmaAPI.Interface
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDTO model);
        Task<string> LoginAsync(LoginDTO model);
    }
}
