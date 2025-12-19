using AutoMapper;
using Domain;
namespace DataAccess.Vms
{
    public class UserProfileVm : BaseVm
    {
        public long UserProfileId { get; set; }
        public Guid UserProfileGuid { get; set; }
        


        public static void ConfigureMapper(Profile mProfile)
        {
            mProfile.CreateMap<UserProfile, UserProfileVm>()
            //.ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.ItemCategory.Title))
            //.ForMember(dest => dest.BrandTitle, opt => opt.MapFrom(src => src.Brand.Title))
            //.ForMember(dest => dest.MyMainImage, opt => opt.Ignore())
            //.ForMember(dest => dest.ImagePaths, opt => opt.Ignore())
            //.ForMember(d => d.ImagePaths, opt => opt.MapFrom<ShowEstateImageResolver>());
            ;
        }
    }
}
