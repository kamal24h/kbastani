
namespace Domain
{
    public class BlogCategory : BaseEntity
    {
        public long BlogCategoryId { get; set; }
        public Guid BlogCategoryGuid { get; set; }
        public string NameFa { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public ICollection<BlogPost> Posts { get; set; } = [];
    }

}
