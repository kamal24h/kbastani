using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CommonUtility.MainMenu;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;

namespace WebApp.Controllers
{
    // ForumController (نمونه)   

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using WebApp.Helpers;

    [AllowAnonymous]
    public class ForumController : Controller
    {
        private readonly AppDbContext _db;

        public ForumController(AppDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await _db.ForumCategories
                .Include(c => c.Topics)
                .ToListAsync();

            return View(categories);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Category(string slug)
        {
            var category = await _db.ForumCategories
                .Include(c => c.Topics)
                    .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(c => c.Slug == slug);

            if (category == null) return NotFound();

            return View(category);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Topic(string slug)
        {
            var topic = await _db.ForumTopics
                .Include(t => t.User)
                .Include(t => t.Replies.Where(r => r.IsApproved))
                    .ThenInclude(r => r.User)
                .Include(t => t.Replies.Where(r => r.IsApproved))
                    .ThenInclude(r => r.Children)
                .FirstOrDefaultAsync(t => t.Slug == slug);
           
            if (topic == null) return NotFound();
            if (topic.IsLocked) return BadRequest("Topic is locked");


            return View(topic);
        }

        [Authorize]
        public IActionResult CreateTopic(long categoryId)
        {
            return View(new ForumTopic { CategoryId = categoryId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTopic(ForumTopic model)
        {
            if (!ModelState.IsValid) return View(model);
            
            model.UserId = User.GetUserId();
            model.Slug = SlugHelper.GenerateSlug(model.Title);

            _db.ForumTopics.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction("Topic", new { slug = model.Slug });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Reply(long topicId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return BadRequest("Reply cannot be empty");

            var reply = new ForumReply
            {
                TopicId = topicId,
                Content = content,
                UserId = User.GetUserId(),
                CreatedAt = DateTime.UtcNow
            };

            _db.ForumReplies.Add(reply);
            await _db.SaveChangesAsync();

            var topic = await _db.ForumTopics.FindAsync(topicId);

            return RedirectToAction("Topic", new { slug = topic!.Slug });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Reply(long topicId, string content, long? parentId)
        {
            if (string.IsNullOrWhiteSpace(content))
                return BadRequest("Reply cannot be empty");

            var reply = new ForumReply
            {
                TopicId = topicId,
                Content = content,
                UserId = User.GetUserId(),
                ParentId = parentId,
                CreatedAt = DateTime.UtcNow
            };

            _db.ForumReplies.Add(reply);
            await _db.SaveChangesAsync();

            var topic = await _db.ForumTopics.FindAsync(topicId);

            return RedirectToAction("Topic", new { slug = topic!.Slug });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> VoteTopic(long topicId, int value)
        {
            if (value != 1 && value != -1)
                return BadRequest("Invalid vote");

            var userId = User.GetUserId();

            var existing = await _db.TopicVotes
                .FirstOrDefaultAsync(v => v.TopicId == topicId && v.UserId == userId);

            if (existing == null)
            {
                _db.TopicVotes.Add(new TopicVote
                {
                    TopicId = topicId,
                    UserId = userId,
                    Value = value
                });
            }
            else
            {
                if (existing.Value == value)
                {
                    // Remove vote (toggle off)
                    _db.TopicVotes.Remove(existing);
                }
                else
                {
                    // Change vote
                    existing.Value = value;
                }
            }

            await _db.SaveChangesAsync();

            var topic = await _db.ForumTopics.FindAsync(topicId);
            return RedirectToAction("Topic", new { slug = topic!.Slug });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> VoteReply(long replyId, int value)
        {
            if (value != 1 && value != -1)
                return BadRequest("Invalid vote");

            var userId = User.GetUserId();

            var existing = await _db.ReplyVotes
                .FirstOrDefaultAsync(v => v.ReplyId == replyId && v.UserId == userId);

            if (existing == null)
            {
                _db.ReplyVotes.Add(new ReplyVote
                {
                    ReplyId = replyId,
                    UserId = userId,
                    Value = value
                });
            }
            else
            {
                if (existing.Value == value)
                {
                    _db.ReplyVotes.Remove(existing);
                }
                else
                {
                    existing.Value = value;
                }
            }

            await _db.SaveChangesAsync();

            var reply = await _db.ForumReplies
                .Include(r => r.Topic)
                .FirstOrDefaultAsync(r => r.Id == replyId);

            return RedirectToAction("Topic", new { slug = reply!.Topic.Slug });
        }



    }



    //public class ForumController : Controller
    //{
    //    private readonly AppDbContext _db;
    //    public ForumController(AppDbContext db) => _db = db;

    //    public async Task<IActionResult> Index()
    //    {
    //        var cats = await _db.ForumCategories
    //            .Include(c => c.Threads)
    //            .ToListAsync();
    //        return View(cats);
    //    }

    //    [Authorize]
    //    public IActionResult CreateThread(int categoryId) => View(new ForumThread { CategoryId = categoryId });

    //    [Authorize, HttpPost, ValidateAntiForgeryToken]
    //    public async Task<IActionResult> CreateThread(ForumThread model, string body)
    //    {
    //        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(body)) return View(model);
    //        model.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    //        _db.ForumThreads.Add(model);
    //        await _db.SaveChangesAsync();

    //        var post = new ForumPost
    //        {
    //            ThreadId = model.ForumThreadId,
    //            Body = body.Trim(),
    //            AuthorId = model.AuthorId
    //        };
    //        _db.ForumPosts.Add(post);
    //        await _db.SaveChangesAsync();
    //        return RedirectToAction("Thread", new { id = model.ForumThreadId });
    //    }

    //    public async Task<IActionResult> Thread(int id)
    //    {
    //        var thread = await _db.ForumThreads
    //            .Include(t => t.Posts).ThenInclude(p => p.Author)
    //            .Include(t => t.Category)
    //            .FirstOrDefaultAsync(t => t.ForumThreadId == id);
    //        if (thread == null) return NotFound();
    //        return View(thread);
    //    }

    //    [Authorize, HttpPost, ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Reply(int threadId, string body)
    //    {
    //        var thread = await _db.ForumThreads.FindAsync(threadId);
    //        if (thread == null || thread.IsLocked) return BadRequest();
    //        var post = new ForumPost
    //        {
    //            ThreadId = threadId,
    //            Body = body.Trim(),
    //            AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier)!
    //        };
    //        _db.ForumPosts.Add(post);
    //        await _db.SaveChangesAsync();
    //        return RedirectToAction(nameof(Thread), new { id = threadId });
    //    }


    //    [HttpPost]
    //    [AllowAnonymous]
    //    public IActionResult SetLanguage(string culture, string returnUrl = "/")
    //    {
    //        Response.Cookies.Append(
    //            CookieRequestCultureProvider.DefaultCookieName,
    //            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
    //            new CookieOptions
    //            {
    //                Expires = DateTimeOffset.UtcNow.AddYears(1),
    //                IsEssential = true
    //            });
    //        return RedirectToAction("Index");
    //    }

    //}
}