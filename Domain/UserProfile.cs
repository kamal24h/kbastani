namespace Domain
{
    public class UserProfile : BaseEntity
    {
        public long UserProfileId { get; set; }
        public Guid UserProfileGuid { get; set; }
        public string FullName { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Bio { get; set; } = default!;
        public ICollection<Skill> Skills { get; set; } = [];
        public ICollection<Project> Projects { get; set; } = [];
    }
}
