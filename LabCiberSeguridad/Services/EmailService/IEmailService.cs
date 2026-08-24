using LabCiberSeguridad.Models;

namespace LabCiberSeguridad.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
