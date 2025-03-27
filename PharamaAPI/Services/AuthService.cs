using PharmaAPI.Interface;
using PharmaAPI.DTO;
using System.Threading.Tasks;

namespace PharmaAPI.Services
{
    public class AuthService : IAuthService  
    {
        private readonly IAuthService _authRepository;  

        public AuthService(IAuthService authRepository)  
        {
            _authRepository = authRepository;
        }

        public Task<string> RegisterAsync(RegisterDTO model)
        {
            return _authRepository.RegisterAsync(model);
        }

        public Task<string> LoginAsync(LoginDTO model)
        {
            return _authRepository.LoginAsync(model);
        }
    }
}
