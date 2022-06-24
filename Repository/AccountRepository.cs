using HRMS.Models;
using HRMS_project.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
       
        private readonly AppDbContext _context;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
         
            _context = context; 
          
        }

        public async Task<IdentityResult> CreateUserAsync(Register register)
        {
            var user = new ApplicationUser()
            {
                Name = register.Name,
                Gender = register.Gender,
                Address = register.Address,
                CountryId = register.CountryId,
                StateId = register.StateId,
                cityId = register.cityId,
                ZipId = register.ZipId,
                Email = register.Email,
                UserName = register.Email
            };
              var result = await _userManager.CreateAsync(user, register.Password);
              return result;
        }

      

        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                
            }
        }
        public async Task<SignInResult> PasswordSignInAsync(Login login)
        {
           var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
            return result;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

       
    }
}
