using Microsoft.Win32;
using TestAPISystem.Models;

namespace TestAPISystem.Repository
{
    public interface IAccountRepository
    {
        Task<LoginModel> SignUp(Register model);
        Task<LoginModel> Login(Login model);
    }
}
