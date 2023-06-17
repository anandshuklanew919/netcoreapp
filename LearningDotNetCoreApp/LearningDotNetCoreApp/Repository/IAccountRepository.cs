using required.Modals;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace required.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SigUpUserModel userModel);

        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);

        Task SigOutAsync();

        Task<IdentityResult> ChangePassword(ChangePassword changePassword);

        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);

        Task GenerateEmailConfirmationTokeAsync(ApplicationUser user);

        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);

        Task<IdentityResult> ResetPasswordAsync(ResetPassword resetPassword);
    }
}