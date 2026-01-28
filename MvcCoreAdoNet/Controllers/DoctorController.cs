using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Models;
using MvcCoreAdoNet.Repositories;

namespace MvcCoreAdoNet.Controllers
{
    public class DoctorController : Controller
    {
        private RepositoryHospital repo;

        public DoctorController()
        {
            this.repo = new RepositoryHospital();
        }
        public async Task<IActionResult> Index()
        {
            List<Doctor> doctores = await this.repo.GetDoctoresAsync();
            return View(doctores);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string especialidad)
        {
            List<Doctor> doctore = await this.repo.FindEspecialidadAsync(especialidad);
            return View(doctore);
        }
    }
}
