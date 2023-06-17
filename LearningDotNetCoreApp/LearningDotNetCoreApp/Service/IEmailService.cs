using required.Modals;
using System.Threading.Tasks;

namespace required.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(UserEmailOptions emailOptions);

        Task SendEmailForEmailConfirmationAsync(UserEmailOptions emailOptions);

        Task SendEmailForForgotpasswordAsync(UserEmailOptions emailOptions);
    }
}