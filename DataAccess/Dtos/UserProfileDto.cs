using AutoMapper;
using Domain;

namespace DataAccess.Dtos
{
    public class UserProfileDto : BaseDto
    {
        public long? UserProfileId { get; set; }
        public Guid? UserProfileGuid { get; set; }
        

        public override bool IsValid()
        {
            var baseValid = base.IsValid();
            //if (string.IsNullOrEmpty(Family))
            //    _validationMessage.AppendLine("نام فامیلی ساکن باید وارد شود.");
            //if (string.IsNullOrEmpty(UserName))
            //    _validationMessage.AppendLine("نام کاربری باید وارد شود.");
            var result = _validationMessage.ToString() == string.Empty && baseValid;
            return result;
        }

        public override void PrepareDto(Guid currentUserId)
        {
            base.PrepareDto(currentUserId);
            if (UserProfileId.GetValueOrDefault() == 0) // Create
            {
                UserProfileGuid = Guid.NewGuid();
                CreatedAt = DateTime.Now;
            }
            else // Update
            {
                ModifiedAt = DateTime.Now;
            }
        }

        public static void ConfigureMapper(Profile mProfile)
        {
            mProfile.CreateMap<UserProfileDto, UserProfile>();
            //.ForMember(d => d.Images, opt => opt.Ignore())
            //.AfterMap(UpdateImages);
        }
    }
}
