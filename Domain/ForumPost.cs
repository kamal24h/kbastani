namespace Domain
{
    public class ForumPost : BaseEntity
    {
        public long ForumPostId { get; set; }
        public Guid ForumPostGuid { get; set; }
        public string Body { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
        public AppUser Author { get; set; } = default!;
        public long ThreadId { get; set; }
        public ForumThread Thread { get; set; } = default!;
    }

}
