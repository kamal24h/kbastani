using AutoMapper;
using DataAccess.Dtos;
using DataAccess.Vms;
using Domain;
namespace DataAccess.Dtos
{
    public class TagDto : BaseDto
    {
        public long? TagId { get; set; }
        public Guid? TagGuid { get; set; }
        public string NameFa { get; set; }
        public string NameEn { get; set; }
        public string Slug { get; set; }

        public override bool IsValid()
        {            
            var result = _validationMessage.ToString() == string.Empty;
            return result;
        }

        public override void PrepareDto(Guid currentUserId)
        {
            base.PrepareDto(currentUserId);
            if (TagId.GetValueOrDefault() == 0) // Create
            {
                TagGuid = Guid.NewGuid();
                CreatedAt = DateTime.Now;                
            }
            else // Update
            {
                ModifiedAt = DateTime.Now;
                //ModifiedBy = currentUserId;                
            }
        }

        public static void ConfigureMapper(Profile mProfile)
        {
            mProfile.CreateMap<TagDto, Tag>();
        }
    }
}
