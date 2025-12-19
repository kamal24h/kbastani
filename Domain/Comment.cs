namespace Domain
{
    public class Comment : BaseEntity
    {
        public long CommentId { get; set; }
        public Guid CommentGuid { get; set; }
        public string Body { get; set; } = default!;
        public bool IsApproved { get; set; } = true; // یا نیاز به تایید ادمین
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public AppUser User { get; set; } = default!;
    }

}
