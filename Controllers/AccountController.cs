
using AspNetCoreHero.ToastNotification.Abstractions;
using HRMS.Models;
using HRMS_project.Models;
using HRMS_project.Repository;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AppDbContext _context;
        private readonly INotyfService _notyf;
        private readonly ILogger<AccountController> _logger;
      
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
       
      
        public AccountController(IAccountRepository accountRepository, AppDbContext context, ILogger<AccountController> logger, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, INotyfService notyf, SignInManager<ApplicationUser> signInManager)
        {
            _accountRepository = accountRepository;
            _context = context;
            _notyf = notyf;
            _logger = logger;
            _roleManager = roleManager; 
            _userManager = userManager;
            _signInManager = signInManager;
          
        }
        
        [Authorize(Roles = "Admin , HR")]

        [HttpGet]
        public IActionResult CreateRole()
        {
            var users = _userManager.Users.ToList();
            ViewBag.users = new SelectList(users);

            var roles = _roleManager.Roles.ToList();
            ViewBag.roles = new SelectList(roles);
            return View();
       
        }
        [Authorize(Roles = "Admin , HR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRole roleModel)
        {

           
            var RoleExist = await _roleManager.RoleExistsAsync(roleModel.RoleName);
            if (RoleExist)
            {

                var currentUser = _userManager.FindByNameAsync(roleModel.UserName).Result;

                await _userManager.AddToRoleAsync(currentUser, roleModel.RoleName);
                _context.createRoles.Add(roleModel);
                _context.SaveChanges();
                if (await _userManager.IsInRoleAsync(currentUser, "Admin") ||
                           await _userManager.IsInRoleAsync(currentUser, "HR"))
                {
                    _notyf.Success("succesfully entered");
                    return RedirectToAction("Index", "Admin");
                }
               
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View(roleModel);

            }

        }
        public IActionResult AdminAccess()
        {
            return View();
        }

        public IActionResult Register()
        {
            var countries = _context.country.ToList();
            ViewBag.countries = new SelectList(countries, "CountryId", "Name");
            return View();
           
        }
        [HttpGet]
        public JsonResult GetStates(int CountryId)
        {
            var states = _context.states.Where(x => x.CountryId == CountryId).ToList();


            return Json(new SelectList(states, "StateId", "Name"));

        }

        public JsonResult GetCities(int StateId)
        {
            var cities = _context.cities.Where(x => x.StateId == StateId).ToList();


            return Json(new SelectList(cities, "cityId", "Name"));

        }
        public JsonResult GetPostalCode(int cityId)
        {
            var codes = _context.postalCode.Where(x => x.cityId == cityId).ToList();


            return Json(new SelectList(codes, "ZipId", "zip"));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                
                
                var result = await _accountRepository.CreateUserAsync(register);
                if (!result.Succeeded)
                {
                    _notyf.Error("opps! data is already exist");
                    return RedirectToAction("Index","Home");
                }
               
                ModelState.Clear();
                _notyf.Success("Data added");
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(login);

                if (result.Succeeded)
                {
                    //return RedirectToAction("CreateRole");

                    var currentuser = _userManager.FindByNameAsync(login.Email).Result;
                    if(await _userManager.IsInRoleAsync(currentuser, "Admin") ||
                            await _userManager.IsInRoleAsync(currentuser , "HR"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        HttpContext.Session.SetString("UserName", login.Email);
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                  
              
            }
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword(ForgotPassword model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Find the user by email
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        // If the user is found AND Email is confirmed
        //        if (user != null && await _userManager.IsEmailConfirmedAsync(user))
        //        {
        //            // Generate the reset password token
        //            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //            // Build the password reset link
        //            var passwordResetLink = Url.Action("ResetPassword", "Account",
        //                    new { email = model.Email, token = token }, Request.Scheme);

        //            // Log the password reset link
        //            _logger.Log(LogLevel.Warning, passwordResetLink);

        //            // Send the user to Forgot Password Confirmation view
        //            return View("ForgotPasswordConfirmation");
        //        }


        //        return View("ForgotPasswordConfirmation");
        //    }

        //    return View(model);
        //}
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {

            if (!ModelState.IsValid)
                return View(email);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            if (emailResponse)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            else
            {

            }
            return View(email);

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {
              
                var user = await _userManager.FindByEmailAsync(resetPassword.Email);

                if (user != null)
                {
                   
                    var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                   
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(resetPassword);
                }

               
                return View("ResetPasswordConfirmation");
            }
          
            return View(resetPassword);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
       
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePassword change)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
       
        public IActionResult OnBoardRequest()
        {
            var bunit = _context.bunits.ToList().Where(m => m.Bunit_status == true);
            foreach(var item in bunit)
            {
                ViewBag.bunit = new SelectList(bunit, "BUnitID", "Businessunit");
            }

            var Department = _context.dept.ToList().Where(n =>n.Status == true);
            foreach(var item in Department)
            {
                ViewBag.depts = new SelectList(Department, "DeptId", "DeptName");

            }

            var RM = _context.rMs.ToList();
            ViewBag.rMs = new SelectList(RM, "RMID", "ReportingManager");

            return View();
           
        }
        [HttpGet]
        public JsonResult GetSDepts(int DeptID)
            
        {
            var sdepts = _context.sDepts.Where(x => x.DeptID == DeptID).ToList();
           
            return Json(new SelectList(sdepts, "SDeptID", "SubDepartment"));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnBoardRequest(RequestModel requestModels)
        {
            if (_context.requestModels.Count((m) => m.EmpID == requestModels.EmpID) == 0)
            {
                _context.requestModels.Add(requestModels);
                _notyf.Success("Employee added");
            }
            else
            {
                _notyf.Error("Employee already exist");
            }
         
            _context.SaveChanges();
       
            return RedirectToAction("OnDashboard", "Account");
        }
        [HttpGet]
        public IActionResult OnDashboard(string search)
        {
            var units = from data in _context.requestModels

                        join bunit in _context.bunits on
                        data.BUnitID equals bunit.BUnitID

                        join Dept in _context.dept on
                        data.DeptId equals Dept.DeptId

                        join sDept in _context.sDepts on
                        data.SDeptID equals sDept.SDeptID

                        join RM in _context.rMs on
                        data.RMID equals RM.RMID

                        select new RequestModel
                        {
                            EmpID = data.EmpID,
                            Firstname = data.Firstname,
                            Lastname = data.Lastname,
                            Email = data.Email,
                            Mobile = data.Mobile,
                            Doj = data.Doj,
                            Designation = data.Designation,
                            Businessunit = bunit.Businessunit,
                            DeptName = Dept.DeptName,
                            SubDepartment = sDept.SubDepartment,
                            ReportingManager = RM.ReportingManager,

                        };
            var details = from v in units select v;
            if (!string.IsNullOrEmpty(search))
            {
                details = details.Where(m => m.Firstname.Contains(search));
                if (details.Count((m) => m.Firstname == search) == 0)
                {
                    _notyf.Information("Searched data not valid");
                }
                
            }
            return View(details.AsNoTracking().ToList());

        }
        [HttpGet]
        public IActionResult DashEdit(int id)
        {
            var bunit = _context.bunits.ToList();
            ViewBag.bunit = new SelectList(bunit, "BUnitID", "Businessunit");


            var Department = _context.dept.ToList();
            ViewBag.depts = new SelectList(Department, "DeptId", "DeptName");
            
            var sdept = _context.sDepts.ToList();
            ViewBag.sdept = new SelectList(sdept, "SDeptID", "SubDepartment");

            var rm = _context.rMs.ToList();
            ViewBag.rm = new SelectList(rm, "RMID", "ReportingManager");

            var details= _context.requestModels.Find(id);
            return View(details);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DashEdit(RequestModel requestModel, int id)
        {

            bool IsEmployeeExist = false;

            RequestModel nedit = _context.requestModels.Find(id);

            if (nedit != null)
            {
                IsEmployeeExist = true;
            }
            else
            {
                nedit = new RequestModel();
            }

            if (ModelState.IsValid)
            {

                nedit.Firstname = requestModel.Firstname;
                nedit.Lastname = requestModel.Lastname;
                nedit.Email = requestModel.Email;
                nedit.Mobile = requestModel.Mobile;
                nedit.Doj = requestModel.Doj;
                nedit.Designation = requestModel.Designation;
                nedit.BUnitID = requestModel.BUnitID;
                nedit.DeptId = requestModel.DeptId;
                nedit.SubDepartment = requestModel.SubDepartment;
                nedit.ReportingManager = requestModel.ReportingManager;
               

                if (IsEmployeeExist)
                {
                    _context.Update(nedit);
                }
                else
                {
                    _context.Add(nedit);
                }
                _context.SaveChanges();


                return RedirectToAction("OnDashboard");
            }
            return View(requestModel);
        }
        public IActionResult DashDelete(int id)
        {
            var unit = _context.requestModels.FirstOrDefault(m => m.EmpID == id);
            return View(unit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DashDelete(RequestModel requestModel, int id)
        {
            var nEdit = _context.requestModels.Find(id);
            _context.requestModels.Remove(nEdit);
            _context.SaveChanges();

            return RedirectToAction("OnDashboard");
        }
    }

}
