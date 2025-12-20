namespace Domain
{
    public class Project : BaseEntity
    {
        public long ProjectId { get; set; }
        public Guid ProjectGuid { get; set; }

        public string TitleFa { get; set; } = default!;
        public string TitleEn { get; set; } = default!;

        public string DescriptionFa { get; set; } = default!;
        public string DescriptionEn { get; set; } = default!;

        public string? ImagePath { get; set; }
        public string? ProjectUrl { get; set; }
        public string? GithubUrl { get; set; }

        public string? RepoUrl { get; set; }
        public string? DemoUrl { get; set; }
        public bool IsPublished { get; set; } = true;
        public long UserProfileId { get; set; }
        public UserProfile Profile { get; set; } = default!;
        public ICollection<ProjectTech> Techs { get; set; } = [];
    }

}
