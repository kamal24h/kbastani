using AutoMapper;
using Domain;

namespace DataAccess.Dtos
{
    public class BlogCategoryDto : BaseDto
    {
        public long? BlogCategoryId { get; set; }
        public Guid? BlogCategoryGuid { get; set; }
        public string NameFa { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public IList<BlogPostDto> Posts { get; set; } = [];

        public override bool IsValid()
        {
            var baseValid = base.IsValid();           
            var result = _validationMessage.ToString() == string.Empty && baseValid;
            return result;
        }

        public override void PrepareDto(Guid currentUserId)
        {
            base.PrepareDto(currentUserId);
            if (BlogCategoryId.GetValueOrDefault() == 0) // Create
            {
                BlogCategoryGuid = Guid.NewGuid();
                CreatedAt = DateTime.Now;               
            }
            else // Update
            {
                ModifiedAt = DateTime.Now;
            }
        }

        public static void ConfigureMapper(Profile mProfile)
        {
            mProfile.CreateMap<BlogCategoryDto, BlogCategory>();
            //.ForMember(d => d.Images, opt => opt.Ignore())
            //.AfterMap(UpdateImages);
        }
    }
}
