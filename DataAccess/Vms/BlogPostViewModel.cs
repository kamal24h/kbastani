using Domain;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Vms
{
    public class BlogPostViewModel
    {
        public long BlogPostId { get; set; }

        public string TitleFa { get; set; } = default!;
        public string TitleEn { get; set; } = default!;
        public string SummaryFa { get; set; } = default!;
        public string SummaryEn { get; set; } = default!;
        public string ContentFa { get; set; } = default!;
        public string ContentEn { get; set; } = default!;

        public bool IsPublished { get; set; }

        public IFormFile? Thumbnail { get; set; }

        public long CategoryId { get; set; }
        public List<long> SelectedTags { get; set; } = [];

        public List<BlogCategory>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }

    }

}
