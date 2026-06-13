
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Editor}")]
    [Authorize(Roles = "Admin")]
    public class ManageForumController : Controller
    {
        private readonly AppDbContext _db;

        public ManageForumController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Topics()
        {
            var topics = await _db.ForumTopics
                .Include(t => t.User)
                .Include(t => t.Category)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return View(topics);
        }

        public async Task<IActionResult> Replies()
        {
            var replies = await _db.ForumReplies
                .Include(r => r.User)
                .Include(r => r.Topic)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(replies);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveTopic(long id)
        {
            var topic = await _db.ForumTopics.FindAsync(id);
            if (topic == null) return NotFound();

            topic.IsApproved = true;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Topics));
        }

        [HttpPost]
        public async Task<IActionResult> RejectTopic(long id)
        {
            var topic = await _db.ForumTopics.FindAsync(id);
            if (topic == null) return NotFound();

            topic.IsApproved = false;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Topics));
        }

        [HttpPost]
        public async Task<IActionResult> LockTopic(long id)
        {
            var topic = await _db.ForumTopics.FindAsync(id);
            if (topic == null) return NotFound();

            topic.IsLocked = !topic.IsLocked;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Topics));
        }

        [HttpPost]
        public async Task<IActionResult> PinTopic(long id)
        {
            var topic = await _db.ForumTopics.FindAsync(id);
            if (topic == null) return NotFound();

            topic.IsPinned = !topic.IsPinned;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Topics));
        }

        [HttpPost]
        public async Task<IActionResult> ApproveReply(long id)
        {
            var reply = await _db.ForumReplies.FindAsync(id);
            if (reply == null) return NotFound();

            reply.IsApproved = true;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Replies));
        }

        [HttpPost]
        public async Task<IActionResult> RejectReply(long id)
        {
            var reply = await _db.ForumReplies.FindAsync(id);
            if (reply == null) return NotFound();

            reply.IsApproved = false;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Replies));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReply(long id)
        {
            var reply = await _db.ForumReplies
                .Include(r => r.Children)
                .FirstOrDefaultAsync(r => r.ForumReplyId == id);

            if (reply == null) return NotFound();

            DeleteReplyRecursive(reply);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Replies));
        }

        private void DeleteReplyRecursive(ForumReply reply)
        {
            foreach (var child in reply.Children.ToList())
                DeleteReplyRecursive(child);

            _db.ForumReplies.Remove(reply);
        }
    }
}


//using DataAccess;
//using Domain;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Localization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace WebApp.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Authorize(Roles = "Admin")]
//    public class ManageForumController : Controller
//    {
//        private readonly AppDbContext _db;
//        public ManageForumController(AppDbContext db) => _db = db;

//        public async Task<IActionResult> Categories()
//        {
//            var cats = await _db.ForumCategories.ToListAsync();
//            return View(cats);
//        }

//        public IActionResult EditCategory(int? id)
//        {
//            if (id == null) return View(new ForumCategory());
//            var cat = _db.ForumCategories.Find(id.Value);
//            if (cat == null) return NotFound();
//            return View(cat);
//        }

//        [HttpPost, ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditCategory(ForumCategory model)
//        {
//            if (!ModelState.IsValid) return View(model);

//            if (model.ForumCategoryId == 0)
//                _db.ForumCategories.Add(model);
//            else
//                _db.ForumCategories.Update(model);

//            await _db.SaveChangesAsync();
//            return RedirectToAction(nameof(Categories));
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        public IActionResult SetLanguage(string culture, string returnUrl = "/")
//        {
//            Response.Cookies.Append(
//                CookieRequestCultureProvider.DefaultCookieName,
//                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
//                new CookieOptions
//                {
//                    Expires = DateTimeOffset.UtcNow.AddYears(1),
//                    IsEssential = true
//                });
//            return RedirectToAction("Index");
//        }
//    }
//}
