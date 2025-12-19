namespace Domain
{
    public class Project : BaseEntity
    {
        public long ProjectId { get; set; }
        public Guid ProjectGuid { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? RepoUrl { get; set; }
        public string? DemoUrl { get; set; }
        public bool IsPublished { get; set; } = true;
        public long UserProfileId { get; set; }
        public UserProfile Profile { get; set; } = default!;
        public ICollection<ProjectTech> Techs { get; set; } = new List<ProjectTech>();
    }

}
