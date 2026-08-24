using LabCiberSeguridad.Models;
using LabCiberSeguridad.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LabCiberSeguridad.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;

        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public IActionResult ProcesarMetodo(string method)
        {
            if (method == "correo")
            {
                // 1. Generar código aleatorio de 6 dígitos
                string codigo = new Random().Next(100000, 999999).ToString();

                // 2. Guardar el código temporalmente en la Sesión
                HttpContext.Session.SetString("CodigoVerificacion", codigo);

                // 3. Definir el correo estático de destino
                string emailDestino = "smartiodiversityutn@gmail.com";

                // 4. Diseñar la plantilla HTML personalizada del correo estilo Facebook
                string cuerpoHtml = $@"
        <!DOCTYPE html>
        <html lang='es'>
        <head>
            <meta charset='UTF-8'>
            <style>
                body {{
                    font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
                    background-color: #f0f2f5;
                    color: #1c1e21;
                    margin: 0;
                    padding: 20px;
                }}
                .email-container {{
                    max-width: 600px;
                    margin: 0 auto;
                    background-color: #ffffff;
                    padding: 30px;
                    border-radius: 8px;
                    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    display: flex;
                    align-items: center;
                    margin-bottom: 25px;
                }}
                .fb-logo {{
                    width: 36px;
                    height: 36px;
                    background-color: #1877f2;
                    border-radius: 50%;
                    text-align: center;
                    line-height: 36px;
                    color: white;
                    font-weight: bold;
                    font-size: 20px;
                }}
                h1 {{
                    font-size: 20px;
                    font-weight: 700;
                    margin-bottom: 20px;
                    color: #1c1e21;
                }}
                p {{
                    font-size: 15px;
                    line-height: 1.5;
                    margin: 14px 0;
                }}
                .code-box {{
                    background-color: #e7f3ff;
                    border: 1px solid #b8daff;
                    border-radius: 6px;
                    text-align: center;
                    font-size: 28px;
                    font-weight: 600;
                    letter-spacing: 6px;
                    padding: 16px;
                    margin: 24px 0 6px 0;
                    color: #050505;
                }}
                .code-footer {{
                    text-align: center;
                    font-size: 13px;
                    color: #65676b;
                    margin-bottom: 25px;
                }}
                .section-title {{
                    font-weight: 600;
                    margin-top: 22px;
                    margin-bottom: 6px;
                    font-size: 15px;
                }}
                a {{
                    color: #1877f2;
                    text-decoration: none;
                }}
                a:hover {{
                    text-decoration: underline;
                }}
                .footer-text {{
                    margin-top: 30px;
                    font-size: 14px;
                }}
            </style>
        </head>
        <body>
            <div class='email-container'>
                <!-- Cabecera simple con logo de Facebook -->
                <div class='header'>
                    <div class='fb-logo'>f</div>
                </div>

                <!-- Título principal -->
                <h1>Solo te queda un paso para confirmar tu cuenta</h1>
                
                <p>Hola, Angel:</p>
                
                <p>Hemos recibido una solicitud para confirmar tu cuenta de alguien que está usando Facebook. Introduce este código:</p>
                
                <!-- Caja con el código generado dinámicamente -->
                <div class='code-box'>{codigo}</div>
                <div class='code-footer'>No compartas este código con nadie.</div>

                <!-- Secciones de seguridad adicionales -->
                <div class='section-title'>Si alguien solicita este código</div>
                <p>No compartas este código con nadie, especialmente si te dice que trabaja para Facebook o Meta. Es posible que esté intentando hackear tu cuenta.</p>

                <div class='section-title'>¿No has solicitado esto?</div>
                <p>Si has recibido este correo electrónico pero no estás intentando realizar cambios, <a href='#'>infórmanos</a>. Mientras no compartas este código con nadie, no es necesario que adoptes ninguna otra medida.</p>

                <p class='footer-text'>
                    Gracias,<br>
                    <strong>Seguridad de Facebook</strong>
                </p>
            </div>
        </body>
        </html>";

                var emailDto = new EmailDto
                {
                    To = emailDestino,
                    Subject = "Tu código de confirmación de Facebook",
                    Body = cuerpoHtml // <--- Aquí le inyectamos la plantilla HTML personalizada con el código dinámico
                };

                // 5. Enviar el correo usando tu servicio existente
                _emailService.SendEmail(emailDto);

                // 6. Redirigir a la vista de confirmación
                return RedirectToAction("ConfirmaCuenta");
            }
            else if (method == "password")
            {
                return RedirectToAction("CambiarContrasenia");
            }

            return RedirectToAction("BuscarCuenta");
        }

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
    }
}
