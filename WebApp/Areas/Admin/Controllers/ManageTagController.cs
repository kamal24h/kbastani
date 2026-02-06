using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageTagController(ITagService tagService) : Controller
    {       

        public async Task<IActionResult> Index()
        {
            var tags = await tagService.GetAll();
            return View(tags.OrderBy(t => t.NameEn).ToList());
        }

        public IActionResult Create()
        {
            return View(new TagDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagDto dto)
        {
            await tagService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tag = await tagService.GetByIdAsync(id);
            if (tag == null)
                return NotFound();
            var dto = new TagDto()
            {
                TagId = tag.TagId,
                NameEn = tag.NameEn,
                NameFa = tag.NameFa,
                Slug = tag.Slug,
            };
            return View(tag);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TagDto dto)
        {
            await tagService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await tagService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string returnUrl = "/")
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true
                });
            return RedirectToAction("Index");
        }

    }
}


