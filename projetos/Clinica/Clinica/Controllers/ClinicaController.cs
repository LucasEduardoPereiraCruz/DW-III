using Microsoft.AspNetCore.Mvc;
using Clinica.Data;
using Clinica.Models;
using MongoDB.Driver;

namespace Clinica.Controllers
{
    public class ClinicaController : Controller
    {
        private readonly MongoDbContext _context = new MongoDbContext();

        // LISTAR
        public IActionResult Index()
        {
            var lista = _context.Clinicas.Find(_ => true).ToList();
            return View(lista);
        }

        // FORM
        public IActionResult Create()
        {
            return View();
        }

        // SALVAR
        [HttpPost]
        public IActionResult Create(ClinicaModel clinica)
        {
            clinica.Alarme = false;
            _context.Clinicas.InsertOne(clinica);
            return RedirectToAction("Index");
        }

        // LIGAR / DESLIGAR
        public IActionResult ToggleAlarme(string id)
        {
            var clinica = _context.Clinicas.Find(c => c.Id == id).FirstOrDefault();

            if (clinica != null)
            {
                clinica.Alarme = !clinica.Alarme;
                _context.Clinicas.ReplaceOne(c => c.Id == id, clinica);
            }

            return RedirectToAction("Index");
        }
    }
}