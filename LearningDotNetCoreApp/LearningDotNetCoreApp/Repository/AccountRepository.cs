using required.Modals;
using required.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace required.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IUserService userService,
            IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task<IdentityResult> CreateUserAsync(SigUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                Email = userModel.Email,
                UserName = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                DateOfBirth = userModel.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //if (!string.IsNullOrEmpty(token))
                //{
                //    await SendEmailConfirmationEmail(user, token);
                //}
                await GenerateEmailConfirmationTokeAsync(user);
            }

            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
            return result;
        }

        public async Task SigOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePassword(ChangePassword changePassword)
        {
            var userid = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userid);
            var result = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
            return result;
        }


        public async Task<IdentityResult> ResetPasswordAsync(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByIdAsync(resetPassword.UserId);
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            return result;
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user, string token)
        {
            var appDomain = _configuration.GetValue<string>("Application:AppDomain");
            var confirmationLink = _configuration.GetValue<string>("Application:EmailConfirmation");
            var UserEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                PlaceHolder = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName+ " "+ user.LastName) ,
                    new KeyValuePair<string, string>("{{Link}}", string.Format( appDomain+confirmationLink,user.Id,token))
                }
            };
            await _emailService.SendEmailForEmailConfirmationAsync(UserEmailOptions);
        }


        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            var appDomain = _configuration.GetValue<string>("Application:AppDomain");
            var confirmationLink = _configuration.GetValue<string>("Application:ForgotPassword");
            var UserEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                PlaceHolder = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName+ " "+ user.LastName) ,
                    new KeyValuePair<string, string>("{{Link}}", string.Format( appDomain+confirmationLink,user.Id,token))
                }
            };
            await _emailService.SendEmailForForgotpasswordAsync(UserEmailOptions);
        }


        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task GenerateEmailConfirmationTokeAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }

        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
