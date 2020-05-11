using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Configurations;
using ParkyWeb.Models;
using ParkyWeb.Repositories.IRepositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkyWeb.Controllers
{
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository nationalParkRepository;

        public NationalParksController(INationalParkRepository nationalParkRepository)
        {
            this.nationalParkRepository = nationalParkRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new NationalPark() { });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }

            var obj = await nationalParkRepository.GetAsync(SD.NationalParkAPI, id.Value);
            if (obj==null)
            {
                return NotFound();
            }

            return View(obj);
        }
        public async Task<IActionResult> GetAllNationalPark()
        {
            return Json(new { data = await nationalParkRepository.GetAllAsync(SD.NationalParkAPI) });
        }
    }
}
