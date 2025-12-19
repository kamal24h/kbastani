namespace Domain

{
    public class BlogPostTag : BaseEntity
    {
        public int BlogPostTagId { get; set; }
        public Guid BlogPostTagGuid { get; set; }
        public BlogPost BlogPost { get; set; } = default!;
        public long TagId { get; set; }
        public Tag Tag { get; set; } = default!;
    }

}
