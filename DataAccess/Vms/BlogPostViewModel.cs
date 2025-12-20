using Microsoft.AspNetCore.Http;

namespace DataAccess.Vms
{
    public class BlogPostViewModel
    {
        public int Id { get; set; }

        public string TitleFa { get; set; } = default!;
        public string TitleEn { get; set; } = default!;
        public string SummaryFa { get; set; } = default!;
        public string SummaryEn { get; set; } = default!;
        public string ContentFa { get; set; } = default!;
        public string ContentEn { get; set; } = default!;

        public bool IsPublished { get; set; }

        public IFormFile? Thumbnail { get; set; }
    }

}
