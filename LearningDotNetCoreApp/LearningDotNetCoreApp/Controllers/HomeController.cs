using required.Modals;
using required.Repository;
using required.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace required.Controllers
{
    public class HomeController : Controller
    {
        public readonly NewBookAlertConfig _newBookAlertConfig;
        public readonly NewBookAlertConfig _thirdPartyBookConfig;
        private readonly IMessageRepository _messageRepository;
        private readonly IEmailService _emailService;

        public HomeController(IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfig,
            IMessageRepository messageRepository, IEmailService emailService)
        {
            _newBookAlertConfig = newBookAlertConfig.Get("InternalBook");
            _thirdPartyBookConfig = newBookAlertConfig.Get("ThirdPrtyBook");
            _messageRepository = messageRepository;
            _emailService = emailService;
        }
        public async Task<IActionResult> Index()
        {
            //var UserEmailOptions = new UserEmailOptions
            //{
            //    ToEmails = new List<string> { "test@gmil.com", "test1@gmil.com", "test3@gmil.com" },
            //    PlaceHolder= new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("{{UserName}}", "Anand Shukla")}
            //};
            //await _emailService.SendEmailAsync(UserEmailOptions);
            // var value = _messageRepository.GetName();
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
