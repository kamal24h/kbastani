using AutoMapper;
using Domain;

namespace DataAccess.Dtos
{
    public class BlogPostDto : BaseDto
    {
        public long? BlogPostId { get; set; }
        public Guid? BlogPostGuid { get; set; }
        public string Title { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Summary { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; }
        public string AuthorId { get; set; } = default!;
        public AppUser Author { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<BlogPostTag> Tags { get; set; } = new List<BlogPostTag>();
        public int? CategoryId { get; set; }
        public BlogCategory? Category { get; set; }

        public override bool IsValid()
        {
            var baseValid = base.IsValid();
            if (BlogPostId == 0)
                _validationMessage.AppendLine("ساختمان واحد باید مشخص شود.");
            //if (string.IsNullOrEmpty(Title))
            //    _validationMessage.AppendLine("عنوان کالا باید وارد شود.");
            var result = _validationMessage.ToString() == string.Empty && baseValid;
            return result;
        }

        public override void PrepareDto(Guid currentUserId)
        {
            base.PrepareDto(currentUserId);
            if (BlogPostId.GetValueOrDefault() == 0) // Create
            {
                BlogPostGuid = Guid.NewGuid();
                CreatedAt = DateTime.Now;
                //CreatedBy = currentUserId;
                //Version ??= 1;
                //LastAuditDate ??= DateTime.Now;
            }
            else // Update
            {
                ModifiedAt = DateTime.Now;
                //ModifiedBy = currentUserId;
                //Version ??= 1;
                //LastAuditDate ??= DateTime.Now;
            }
        }

        public static void ConfigureMapper(Profile mProfile)
        {
            mProfile.CreateMap<BlogPostDto, BlogPost>();
            //.ForMember(d => d.Images, opt => opt.Ignore())
            //.AfterMap(UpdateImages);
        }

    }
}
