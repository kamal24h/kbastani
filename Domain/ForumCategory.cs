namespace Domain
{
    public class ForumCategory : BaseEntity
    {
        public long ForumCategoryId { get; set; }
        public Guid ForumCategoryGuid { get; set; }
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public ICollection<ForumThread> Threads { get; set; } = new List<ForumThread>();
    }

}
