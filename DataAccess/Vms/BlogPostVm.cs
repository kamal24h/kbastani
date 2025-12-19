using AutoMapper;
using Domain;

namespace DataAccess.Vms
{
    public class BlogPostVm : BaseVm
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
        public int? CategoryId { get; set; }
        public BlogCategory? Category { get; set; }


        public static void ConfigureMapper(Profile mProfile)
        {
            mProfile.CreateMap<BlogPost, BlogPostVm>()
            //.ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.ItemCategory.Title))
            //.ForMember(dest => dest.BrandTitle, opt => opt.MapFrom(src => src.Brand.Title))
            //.ForMember(dest => dest.MyMainImage, opt => opt.Ignore())
            //.ForMember(dest => dest.ImagePaths, opt => opt.Ignore())
            //.ForMember(d => d.ImagePaths, opt => opt.MapFrom<ShowEstateImageResolver>());
            ;
        }
    }
}
