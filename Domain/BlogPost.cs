namespace Domain

{
    public class BlogPost : BaseEntity
    {
        public long BlogPostId { get; set; }
        public Guid BlogPostGuid { get; set; }
        
        // Titles
        public string TitleFa { get; set; } = default!;
        public string TitleEn { get; set; } = default!;

        // Summaries
        public string SummaryFa { get; set; } = default!;
        public string SummaryEn { get; set; } = default!;

        // Content
        public string ContentFa { get; set; } = default!;
        public string ContentEn { get; set; } = default!;

        // SEO Slug
        public string Slug { get; set; } = default!;

        // Image
        public string? ThumbnailPath { get; set; }

        // Meta
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
        public bool IsPublished { get; set; } = false;
                
        public string AuthorId { get; set; } = default!;
        public AppUser Author { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<BlogPostTag> Tags { get; set; } = [];
        public long? CategoryId { get; set; }
        public BlogCategory? Category { get; set; }
    }

}
