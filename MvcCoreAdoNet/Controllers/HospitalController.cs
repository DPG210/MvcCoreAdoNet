using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Models;
using MvcCoreAdoNet.Repositories;

namespace MvcCoreAdoNet.Controllers
{
    public class HospitalController : Controller
    {
        private RepositoryHospital repo;
        public HospitalController()
        {
            this.repo = new RepositoryHospital();
        }
        public async Task<IActionResult> Index()
        {
            List<Hospital> hospitales = await this.repo.GetHospitalesAsync();
            return View(hospitales);
        }
        public async Task<IActionResult> Details(int id)
        {
            Hospital hospital = await this.repo.FindHospitalAsync(id);
            return View(hospital);
        }
        public IActionResult Create()
        {
            return View();
        }
        //En los metodos POST de los controladores SI que recibimos
        //los objetos
        [HttpPost]
        public async Task<IActionResult> Create(Hospital hospital)
        {
            await this.repo.InsertHospitalAsync(hospital.IdHospital, hospital.Nombre, hospital.Direccion, hospital.Telefono, hospital.Camas);
            ViewData["mensaje"] = "Hospital insertado";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Hospital hospital = await this.repo.FindHospitalAsync(id);
            return View(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Hospital hospital)
        {
            await this.repo.UpdateHospitalAsync(hospital.IdHospital, hospital.Nombre, hospital.Direccion, hospital.Telefono, hospital.Camas);
            ViewData["mensaje"] = "Hospital actualizado";
            //DESPUES DE MODIFICAR NOS VAMOS A LA VISTA INDEX
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.DeleteHospitalAsync(id);
            return RedirectToAction("Index");
        }
    }
}
