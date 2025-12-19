namespace Domain

{
    public class BlogPostTag : BaseEntity
    {
        public long BlogPostTagId { get; set; }
        public Guid BlogPostTagGuid { get; set; }
        public long PostId { get; set; }
        public BlogPost Post { get; set; } = default!;
        public long TagId { get; set; }
        public Tag Tag { get; set; } = default!;
    }

}
