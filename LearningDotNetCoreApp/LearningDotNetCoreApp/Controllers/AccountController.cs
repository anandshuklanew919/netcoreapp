using required.Modals;
using required.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace required.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SigUpUserModel sigUpUserModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(sigUpUserModel);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = sigUpUserModel.Email });
            }
            else
            {

            }
            return View();
        }


        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInModel signInModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(signInModel);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Email is not verified");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user name and password");
                }
                ModelState.Clear();
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SigOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePassword(changePassword);

                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirModel model = new EmailConfirModel { Email = email };
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }
            return View(model);

        }


        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirModel emailConfirModel)
        {
            var user = await _accountRepository.GetUserByEmailAsync(emailConfirModel.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    emailConfirModel.IsConfirmed = true;
                    return View(emailConfirModel);
                }
                var result = _accountRepository.GenerateEmailConfirmationTokeAsync(user);
                emailConfirModel.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Somethig went wrong");
            }
            return View(emailConfirModel);
        }

        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.GetUserByEmailAsync(forgotPasswordModel.Email);
                if (user != null)
                {
                    await _accountRepository.GenerateForgotPasswordTokenAsync(user);
                    forgotPasswordModel.IsEmailSent = true;
                }
                else
                {

                }
                ModelState.Clear();

            }
            return View(forgotPasswordModel);
        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPassword resetPassword = new ResetPassword { UserId = uid,Token =token};
            return View(resetPassword);
        }

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ResetPasswordAsync(resetPassword);
                if(result.Succeeded)
                {
                    resetPassword.IsSuccess = true;
                    ModelState.Clear();
                }
                
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(resetPassword);
        }
    }
}
