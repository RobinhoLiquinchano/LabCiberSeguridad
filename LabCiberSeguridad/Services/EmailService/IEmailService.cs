using LabCiberSeguridad.Models;

namespace LabCiberSeguridad.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto request);
    }
}
