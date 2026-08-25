using LabCiberSeguridad.Models;
using LabCiberSeguridad.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LabCiberSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto request)
        {
            try
            {
                await _emailService.SendEmail(request);
                return Ok(new { message = "Correo enviado exitosamente." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}