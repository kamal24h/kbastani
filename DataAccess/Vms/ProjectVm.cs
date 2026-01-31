using AutoMapper;
using Domain;

namespace DataAccess.Vms
{
    public class ProjectVm : BaseVm
    {
        public long ProjectId { get; set; }
        public Guid ProjectGuid { get; set; }

        public string TitleFa { get; set; } = default!;
        public string TitleEn { get; set; } = default!;

        public string DescriptionFa { get; set; } = default!;
        public string DescriptionEn { get; set; } = default!;

        public string? ImagePath { get; set; }
        public string? ProjectUrl { get; set; }
        public string? GithubUrl { get; set; }

        public string? RepoUrl { get; set; }
        public string? DemoUrl { get; set; }
        public bool IsPublished { get; set; } = true;
        public UserProfileVm Profile { get; set; } = default!;
        public IList<ProjectTechVm> Techs { get; set; } = [];


        public static void ConfigureMapper(Profile mProfile)
        {
            mProfile.CreateMap<Project, ProjectVm>()
            //.ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.ItemCategory.Title))
            //.ForMember(dest => dest.BrandTitle, opt => opt.MapFrom(src => src.Brand.Title))
            //.ForMember(dest => dest.MyMainImage, opt => opt.Ignore())
            //.ForMember(dest => dest.ImagePaths, opt => opt.Ignore())
            //.ForMember(d => d.ImagePaths, opt => opt.MapFrom<ShowEstateImageResolver>());
            ;
        }
    }
}
