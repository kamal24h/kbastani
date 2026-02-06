using AutoMapper;
using Domain;
namespace DataAccess.Vms
{
    public class TagVm : BaseVm
    {
        public long TagId { get; set; }
        public Guid TagGuid { get; set; }
        public string NameFa { get; set; }
        public string NameEn { get; set; }
        public string Slug { get; set; }

        public static void ConfigureMapper(Profile mProfile)
        {
            mProfile.CreateMap<Tag, TagVm>();
        }
    }
}
