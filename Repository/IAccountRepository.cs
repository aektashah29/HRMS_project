using HRMS.Models;
using HRMS_project.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HRMS_project.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(Register userModel);
        Task<SignInResult> PasswordSignInAsync(Login login);

        Task SignOutAsync();

       
    }
}