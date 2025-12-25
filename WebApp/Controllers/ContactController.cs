using DataAccess;
using DataAccess.Vms;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly EmailService _email;
        private readonly AppDbContext _db;

        public ContactController(EmailService email, AppDbContext db)
        {
            _email = email;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(new ContactFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // امنیت (Anti-Spam + Honeypot)
            if (!string.IsNullOrEmpty(Request.Form["website"]))
            {
                return RedirectToAction("Index"); // Bot detected
            }


            // ذخیره در دیتابیس (اختیاری)
            var msg = new ContactMessage
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message
            };

            _db.ContactMessages.Add(msg);
            await _db.SaveChangesAsync();

            // ارسال ایمیل
            string body = $@"
            <h3>New Contact Message</h3>
            <p><strong>Name:</strong> {model.Name}</p>
            <p><strong>Email:</strong> {model.Email}</p>
            <p><strong>Subject:</strong> {model.Subject}</p>
            <p><strong>Message:</strong><br>{model.Message}</p>
        ";

            await _email.SendEmailAsync("your-email@gmail.com", "New Contact Message", body);

            TempData["Success"] = "Your message has been sent successfully.";

            return RedirectToAction("Index");
        }
    }

}
