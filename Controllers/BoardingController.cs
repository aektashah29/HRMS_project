using AspNetCoreHero.ToastNotification.Abstractions;
using HRMS_project.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS_project.Controllers
{
    [Authorize]
    public class BoardingController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly AppDbContext _context;
        public BoardingController(AppDbContext context, INotyfService notyf)
        {
            _notyf = notyf;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
       [HttpGet]
        public IActionResult ONBOARDINGCHECKPOINT()
        {
            var bunit = _context.bunits.ToList().Where(i => i.Bunit_status == true);
            foreach(var item in bunit)
            {
                ViewBag.bunit = new SelectList(bunit, "BUnitID", "Businessunit");

            }


            var Department = _context.dept.ToList().Where(i => i.Status == true);
            foreach (var item in Department)
            {
                ViewBag.depts = new SelectList(Department, "DeptId", "DeptName");
            }
            var assignee = _context.assigneess.ToList();
            ViewBag.assignees = new SelectList(assignee, "AssigneeId", "AssigneeName");

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ONBOARDINGCHECKPOINT(onCheckpoint onCheckpoint)
        {
            if(_context.onCheckpoint.Count((m) => m.CheckpointName == onCheckpoint.CheckpointName) == 0)
            {
                _context.onCheckpoint.Add(onCheckpoint);
                _notyf.Success("Checkpoint added");
            }
            else
            {
                _notyf.Error("Checkpoint already exist");
            }
           
            _context.SaveChanges();

            return RedirectToAction("OnBoardingCheckpointUnits");
        }

        [HttpGet]
        public IActionResult OnBoardingCheckpointUnits(string search)
        {
           

            var units = from data in _context.onCheckpoint

                        join bunit in _context.bunits on
                        data.BUnitID equals bunit.BUnitID 
                     
                        join Dept in _context.dept on
                        data.DeptId equals Dept.DeptId 

                        join assignee in _context.assigneess on
                        data.AssigneeId equals assignee.AssigneeId 
                        

                        select new onCheckpoint
                        {
                            OnCheckpointId = data.OnCheckpointId,
                            CheckpointName = data.CheckpointName,
                            Businessunit = bunit.Businessunit,
                            DeptName = Dept.DeptName,
                            AssigneeName = assignee.AssigneeName,
                            Description = data.Description,

                        };

            var details = from v in units select v;
            if (!string.IsNullOrEmpty(search))
            {
                details = details.Where(m => m.CheckpointName.Contains(search));

            }
            return View(details.AsNoTracking().ToList());
        }

        [HttpGet]

        public IActionResult ONEdit(int id)
        {
            var bunit = _context.bunits.ToList();
            ViewBag.bunit = new SelectList(bunit, "BUnitID", "Businessunit");


            var Department = _context.dept.ToList();
            ViewBag.depts = new SelectList(Department, "DeptId", "DeptName");

            var assig = _context.assigneess.ToList();
            ViewBag.assignees = new SelectList(assig, "AssigneeId", "AssigneeName");
            var details = _context.onCheckpoint.Find(id);
            return View(details);

         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ONEdit(onCheckpoint onCheckpoint,int id)
        {

            bool IsEmployeeExist = false;

            onCheckpoint nedit = _context.onCheckpoint.Find(id);

            if (nedit != null)
            {
                IsEmployeeExist = true;
            }
            else
            {
                nedit = new onCheckpoint();
            }

            if (ModelState.IsValid)
            {

                nedit.CheckpointName = onCheckpoint.CheckpointName;
                nedit.BUnitID = onCheckpoint.BUnitID;
                nedit.DeptId = onCheckpoint.DeptId;
                nedit.AssigneeId = onCheckpoint.AssigneeId;
                nedit.Description = onCheckpoint.Description;
              

                if (IsEmployeeExist)
                {
                    _context.Update(nedit);
                }
                else
                {
                    _context.Add(nedit);
                }
                _context.SaveChanges();


                return RedirectToAction("OnBoardingCheckpointUnits");
            }
            return View(onCheckpoint);
        }

            public IActionResult ONDelete(int id)
            {
                var unit = _context.onCheckpoint.FirstOrDefault(m => m.OnCheckpointId == id);

                return View(unit);

            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult ONDelete(onCheckpoint onCheckpoint, int id)
            {
                var nEdit = _context.onCheckpoint.Find(id);
                _context.onCheckpoint.Remove(nEdit);
                _context.SaveChanges();

                return RedirectToAction("OnBoardingCheckpointUnits");
            }
        
    }
}
