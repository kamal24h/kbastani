using Microsoft.AspNetCore.Http;

namespace DataAccess.Vms
{
    public class ProjectViewModel
    {
        public long ProjectId { get; set; }

        public string TitleFa { get; set; } = default!;
        public string TitleEn { get; set; } = default!;

        public string DescriptionFa { get; set; } = default!;
        public string DescriptionEn { get; set; } = default!;

        public string? ProjectUrl { get; set; }
        public string? GithubUrl { get; set; }

        public IFormFile? Image { get; set; }
    }

}
