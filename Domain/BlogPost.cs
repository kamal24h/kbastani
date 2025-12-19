namespace Domain

{
    public class BlogPost : BaseEntity
    {
        public long BlogPostId { get; set; }
        public Guid BlogPostGuid { get; set; }        
        public string Title { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Summary { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; }
        public string AuthorId { get; set; } = default!;
        public AppUser Author { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<BlogPostTag> Tags { get; set; } = [];
        public long? BlogCategoryId { get; set; }
        public BlogCategory? Category { get; set; }
    }

}
