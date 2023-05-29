using DLtaMail_Webapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace DLtaMail_Webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public void connectToSMTPServer(string email)
        {
            using(var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                smtpClient.Authenticate(email, "");
                //smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inbox(string email)
        {
            connectToSMTPServer(email);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}