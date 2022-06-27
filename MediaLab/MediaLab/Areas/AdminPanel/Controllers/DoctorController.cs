using MediaLab.DAL;
using MediaLab.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLab.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class DoctorController : Controller
    {
        private AppDbContext _context { get;  }
        private readonly IWebHostEnvironment _env;
        public IEnumerable<Doctor> doctors;
        public DoctorController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            doctors = _context.Doctors.Where(d => !d.IsDeleted).ToList();

        }
        public IActionResult Index()
        {
            return View(doctors);
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id==null)
            {
                return BadRequest();

            }
            var doctorDb = _context.Doctors.Where(d => !d.IsDeleted).FirstOrDefault(d => d.Id == Id);
            if (doctorDb==null)
            {
                return NotFound();
            }
            doctorDb.IsDeleted = true;
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (doctor.Photo!=null)
            {
                string folder = "assets/img/doctors/";
                folder += Guid.NewGuid().ToString()+doctor.Photo.FileName ;
                string ServerFolder = Path.Combine(_env.WebRootPath, folder);
              await doctor.Photo.CopyToAsync(new FileStream(ServerFolder, FileMode.Create));

            }
            var IsExist = _context.Doctors.Where(d => !d.IsDeleted).Any(d => d.Job.ToLower() == doctor.Job.ToLower());
            if (IsExist)
            {
                ModelState.AddModelError("Job", $"{doctor.Job} is already exist!");
                return View();
            }
            if (doctor.Description.Length<10)
            {
                ModelState.AddModelError("Description", "Your decsription must be at least 10 character");
                return View();
            }
           
            await  _context.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Update(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();

            }
            Doctor doctorDb = _context.Doctors.Where(d => !d.IsDeleted).FirstOrDefault(d => d.Id == Id);
            if (doctorDb == null)
            {
                return NotFound();
            }
            return View(doctorDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Doctor doctor)
        {
            if (Id == null)
            {
                return BadRequest();

            }
            var doctorDb = _context.Doctors.Where(d => !d.IsDeleted).FirstOrDefault(d => d.Id == Id);
            if (doctorDb == null)
            {
                return NotFound();
            }
            var IsExist = _context.Doctors.Where(d => !d.IsDeleted).Any(d => d.Job.ToLower() == doctor.Job.ToLower() && doctor.Job!=doctorDb.Job);
            if (IsExist)
            {
                ModelState.AddModelError("Job", $"{doctor.Job} is already exist!");
                return View();
            }
            if (doctor.Description.Length < 10)
            {
                ModelState.AddModelError("Description", "Your decsription must be at least 10 character");
                return View();
            }
           
            doctorDb.Name = doctor.Name;
            doctorDb.Job = doctor.Job;
            doctorDb.Description = doctor.Description;
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        
    }
}
