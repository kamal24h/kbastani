namespace Domain
{
    public class ForumThread : BaseEntity
    {
        public long ForumThreadId { get; set; }
        public Guid ForumThreadGuid { get; set; }
        public string Title { get; set; } = default!;        
        public string AuthorId { get; set; } = default!;
        public AppUser Author { get; set; } = default!;
        public long CategoryId { get; set; }
        public ForumCategory Category { get; set; } = default!;
        public bool IsLocked { get; set; }
        public ICollection<ForumPost> Posts { get; set; } = new List<ForumPost>();
    }

}
