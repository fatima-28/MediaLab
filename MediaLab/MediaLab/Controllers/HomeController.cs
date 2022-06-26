using MediaLab.DAL;
using MediaLab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLab.Controllers
{
    public class HomeController : Controller
    {

        private AppDbContext _context { get;  }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            HomeViewModel home = new HomeViewModel
            {

                Doctors=_context.Doctors.Where(d=>!d.IsDeleted).ToList()
            };
            return View(home);
        }
    }
}
