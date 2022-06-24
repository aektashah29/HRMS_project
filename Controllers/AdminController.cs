using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using HRMS_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Controllers
{
    [Authorize(Roles ="Admin,HR")]
    public class AdminController : Controller
    {
       
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyf1;

        public AdminController(AppDbContext context, INotyfService notyf, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _notyf1 = notyf;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
      
        public IActionResult Index()
        {
          var roles= _context.createRoles.ToList(); 

            return View(roles);

           
        }
        public IActionResult RoleEdit(int Id)
        {
            var users = _userManager.Users.ToList();
            ViewBag.users = new SelectList(users);

            var roles = _roleManager.Roles.ToList();
            ViewBag.roles = new SelectList(roles);
            var role = _context.createRoles.Find(Id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleEdit(CreateRole createRole, int id)
        {


            bool IsEmployeeExist = false;

            CreateRole nedit = _context.createRoles.Find(id);

            if (nedit != null)
            {
                IsEmployeeExist = true;
            }
            else
            {
                nedit = new CreateRole();
            }

            if (ModelState.IsValid)
            {

                nedit.UserName = createRole.UserName;
                nedit.RoleName = createRole.RoleName;
             
                if (IsEmployeeExist)
                {
                    _context.Update(nedit);
                }
                else
                {
                    _context.Add(nedit);
                }
                _context.SaveChanges();


                return RedirectToAction("Index");
            }
            return View(createRole);
        }

        public IActionResult RoleDelete(int? id)
        {
            var bunit = _context.createRoles.FirstOrDefault(m => m.Id == id);

            return View(bunit);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleDelete(int id)
        {
            var details = _context.createRoles.Find(id);
            _context.createRoles.Remove(details);
            _notyf1.Success("Record deleted successfully.");
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult BusinessUnitInfo()
        {
                        
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BusinessUnitInfo(Bunit bunit)
        {
            if(_context.bunits.Count((a) => a.Businessunit == bunit.Businessunit) == 0)
            {
                _context.bunits.Add(bunit);
                _notyf1.Success("Added");
            }
           else
            {
                _notyf1.Error("already exist");
            }
            _context.SaveChanges();
            

            return RedirectToAction("BusinessUnitInfo", "Admin");
        }
    
        [HttpGet]
        public IActionResult DEPARTMENTS()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DEPARTMENTS(Dept dept)
        {
            if(_context.dept.Count((m) => m.DeptName == dept.DeptName) == 0)
            {
                _context.dept.Add(dept);
                _notyf1.Success("Added");
            }
            else
            {
                _notyf1.Error("already exist");
            }
            _context.SaveChanges();

            return RedirectToAction("DEPARTMENTS", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard(string search)
        {
            ViewData["AllBunit"]=search;

           var bunit = from v in _context.bunits select v;
          
            
            if(!string.IsNullOrEmpty(search))
            {
                bunit = bunit.Where(m => m.Businessunit.Contains(search));

            }
          
            return View(await bunit.AsNoTracking().ToListAsync());
        }
        public IActionResult Edit(int id)
        {
            
           var bunit = _context.bunits.Find(id);
            if (bunit == null)
            {
                return NotFound();
            }
            return View(bunit);
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bunit bunit,int id)
        {

            bool IsEmployeeExist = false;

            Bunit bunits =  _context.bunits.Find(id);

            if (bunits != null)
            {
                IsEmployeeExist = true;
            }
            else
            {
                bunits = new Bunit();
            }

            if (ModelState.IsValid)
            {
               
                
                    bunits.Businessunit = bunit.Businessunit;
                    bunits.Bunit_status = bunit.Bunit_status;

                    if (IsEmployeeExist)
                    {
                        _context.Update(bunits);
                    }
                    else
                    {
                        _context.Add(bunits);
                    }
                     _context.SaveChanges();
                
                
                return RedirectToAction("Dashboard");
            }
            return View(bunit);
        }
    
        public IActionResult Delete(int? id)
        {
            var bunit =  _context.bunits.FirstOrDefault(m => m.BUnitID == id);

            return View(bunit);
           
        }
           [HttpPost]
           [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var bunit1 =  _context.bunits.Find(id);
            var details = _context.requestModels.Find(id);
            var onCheckpoint = _context.onCheckpoint.Find(id);
            var offcheckpoint = _context.offCheckpoint.Find(id);

            bool onExist = (from m in _context.requestModels
                            where m.EmpID == id
                            select m).Any();

            bool onCheckExist = (from m in _context.onCheckpoint
                            where m.OnCheckpointId == id
                            select m).Any();

            bool offCheckExist = (from m in _context.offCheckpoint
                                 where m.offcheckpointId == id
                                 select m).Any();

            if(onExist || onCheckExist || offCheckExist)
            {
                _notyf1.Information("Cannot remove the data");
                return RedirectToAction("Dashboard");
            }
            else
            {
                _context.bunits.Remove(bunit1);
                _notyf1.Success("Record deleted successfully");
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        public async Task<IActionResult> DepartmentUnits(string search)
        {
            ViewData["AllDept"] = search;
            var dept = from v in _context.dept select v;
            if (!string.IsNullOrEmpty(search))
            {
                dept = dept.Where(m => m.DeptName.Contains(search));

            }
       
            return View(await dept.AsNoTracking().ToListAsync());
        }
        public IActionResult DeptEdit(int id)
        {

            var dept = _context.dept.Find(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeptEdit(Dept dept, int id)
        {

            bool IsEmployeeExist = false;

           Dept dept1 = _context.dept.Find(id);

            if (dept1 != null)
            {
                IsEmployeeExist = true;
            }
            else
            {
                dept1 = new Dept();
            }

            if (ModelState.IsValid)
            {


                dept1.DeptName = dept.DeptName;
                dept1.Status = dept.Status;

                if (IsEmployeeExist)
                {
                    _context.Update(dept1);
                }
                else
                {
                    _context.Add(dept1);
                }
                _context.SaveChanges();


                return RedirectToAction("DepartmentUnits");
            }
            return View(dept1);
        }
        public IActionResult DeptDelete(int id)
        {
            var dept = _context.dept.FirstOrDefault(m => m.DeptId == id);

            return View(dept);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeptDelete(Dept dept, int id)
        {
            var dept1 = _context.dept.Find(id);
            _context.dept.Remove(dept1);
            _context.SaveChanges();

            return RedirectToAction("DepartmentUnits");
        }
    }
}
