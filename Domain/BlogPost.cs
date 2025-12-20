namespace Domain

{
    public class BlogPost : BaseEntity
    {
        public long BlogPostId { get; set; }
        public Guid BlogPostGuid { get; set; }
        // عنوان
        public string TitleFa { get; set; } = default!;
        public string TitleEn { get; set; } = default!;
        // خلاصه
        public string SummaryFa { get; set; } = default!;
        public string SummaryEn { get; set; } = default!;
        // متن
        public string ContentFa { get; set; } = default!;
        public string ContentEn { get; set; } = default!;

        public string Slug { get; set; } = default!;
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; }
        public string AuthorId { get; set; } = default!;
        public AppUser Author { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<BlogPostTag> Tags { get; set; } = [];
        public long? CategoryId { get; set; }
        public BlogCategory? Category { get; set; }
    }

}
