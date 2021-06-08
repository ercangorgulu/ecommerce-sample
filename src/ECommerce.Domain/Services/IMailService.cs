using ECommerce.Domain.Services.Mail;

namespace ECommerce.Domain.Services
{
    public interface IMailService
    {
        void SendMail(MailMessage mailMessage);
    }
}
