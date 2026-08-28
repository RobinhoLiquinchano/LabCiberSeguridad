using LabCiberSeguridad.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LabCiberSeguridad.Controllers
{
    public class HomeController : Controller
    {

        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inicio()
        {
            return View();
        }

        public IActionResult ConfirmaCuenta()
        {
            return View();
        }
        public IActionResult Password()
        {
            return View();
        }
        public IActionResult BuscarCuenta()
        {
            // Aquí puedes agregar lógica si lo necesitas antes de mostrar la vista
            return View();
        }
        public IActionResult ConfirmaSMS()
        {
            // Aquí puedes agregar lógica si lo necesitas antes de mostrar la vista
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // ... tus otros métodos ...

        [HttpPost]
        public IActionResult VerifyCode(string codigo)
        {
            if (codigo == "123456")
            {
                return RedirectToAction("CambiarContrasenia");
            }
            else
            {
                ViewBag.Error = "Código incorrecto, intenta de nuevo.";
                return View("ConfirmaCuenta");
            }
        }

        // AGREGA ESTO:
        public IActionResult CambiarContrasenia()
        {
            return View();
        }
        public IActionResult Initiate()
        {
            return View();
        }
    }
}
