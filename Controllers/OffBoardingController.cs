using AspNetCoreHero.ToastNotification.Abstractions;
using HRMS_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HRMS_project.Controllers
{
    [Authorize]
    public class OffBoardingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly INotyfService _notyf;

        public OffBoardingController(AppDbContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult OffBoardingcheckpoint()
        {
            var bunit = _context.bunits.ToList().Where(m => m.Bunit_status == true);
            foreach (var item in bunit)
            {
                ViewBag.bunit = new SelectList(bunit, "BUnitID", "Businessunit");
            }

            var Department = _context.dept.ToList().Where(m => m.Status == true);
            foreach (var item in Department)
            {
                ViewBag.depts = new SelectList(Department, "DeptId", "DeptName");
            }

            var asigne = _context.assigneess.ToList();
            ViewBag.asigne = new SelectList(asigne, "AssigneeId", "AssigneeName");


            return View();
        }
        [HttpPost]
        public IActionResult OffBoardingcheckpoint(offcheckpoint offcheckpoint)
        {
            if(_context.offCheckpoint.Count((m) => m.offcheckpointName == offcheckpoint.offcheckpointName) == 0)
            {
                _context.offCheckpoint.Add(offcheckpoint);
                _notyf.Success("Checkpointname added");
            }
            else
            {
                _notyf.Error("checkpointname already exist");
            }
            _context.SaveChanges();

            return RedirectToAction("OffBoardingcheckpointUnits");
        }
        [HttpGet]
        public IActionResult OffBoardingcheckpointUnits(string search)
        {
            var units = from data in _context.offCheckpoint

                        join bunit in _context.bunits on
                        data.BUnitID equals bunit.BUnitID

                        join Dept in _context.dept on
                        data.DeptId equals Dept.DeptId

                        join assignee in _context.assigneess on
                        data.AssigneeId equals assignee.AssigneeId


                        select new offcheckpoint
                        {
                            offcheckpointId = data.offcheckpointId,
                            offcheckpointName = data.offcheckpointName,
                            Businessunit = bunit.Businessunit,
                            DeptName = Dept.DeptName,
                            AssigneeName = assignee.AssigneeName,
                            Description = data.Description,

                        };

            var details = from v in units select v;
            if (!string.IsNullOrEmpty(search))
            {
                details = details.Where(m => m.offcheckpointName.Contains(search));
                if (details.Count((m) => m.offcheckpointName != search) == 0)
                {
                    _notyf.Information("Searched data not valid");
                }

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
            var details = _context.offCheckpoint.Find(id);
            return View(details);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ONEdit(offcheckpoint offcheckpoint, int id)
        {

            bool IsEmployeeExist = false;

            offcheckpoint nedit = _context.offCheckpoint.Find(id);

            if (nedit != null)
            {
                IsEmployeeExist = true;
            }
            else
            {
                nedit = new offcheckpoint();
            }

            if (ModelState.IsValid)
            {


                nedit.offcheckpointName = offcheckpoint.offcheckpointName;
                nedit.BUnitID = offcheckpoint.BUnitID;
                nedit.DeptId = offcheckpoint.DeptId;
                nedit.AssigneeId = offcheckpoint.AssigneeId;
                nedit.Description = offcheckpoint.Description;


                if (IsEmployeeExist)
                {
                    _context.Update(nedit);
                }
                else
                {
                    _context.Add(nedit);
                }
                _context.SaveChanges();


                return RedirectToAction("OffBoardingcheckpointUnits");
            }
            return View(offcheckpoint);
        }

        public IActionResult ONDelete(int id)
        {
            var unit = _context.offCheckpoint.FirstOrDefault(m => m.offcheckpointId == id);

            return View(unit);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ONDelete(offcheckpoint offcheckpoint, int id)
        {
            var nEdit = _context.offCheckpoint.Find(id);
            _context.offCheckpoint.Remove(nEdit);
            _context.SaveChanges();

            return RedirectToAction("OffBoardingcheckpointUnits");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Copy(int[] id)
        {


            string strtoint = string.Empty;
            if (id != null)
            {
                strtoint = id.Select(a => a.ToString()).Aggregate((i, j) => i + "," + j);
            }
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Internsip_db; Integrated Security=true");
            SqlCommand cmd = new SqlCommand("Sp_copyOffBoarding ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("strtoint", strtoint);

            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                
            }
            con.Close();


            return RedirectToAction("OffBoardingcheckpointUnits");

        }
    }
}