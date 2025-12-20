namespace Domain
{
    public class ForumCategory : BaseEntity
    {
        public long ForumCategoryId { get; set; }
        public Guid ForumCategoryGuid { get; set; }
        public string NameFa { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public ICollection<ForumThread> Threads { get; set; } = new List<ForumThread>();
    }

}
