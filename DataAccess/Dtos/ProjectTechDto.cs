using AutoMapper;
using Domain;

namespace DataAccess.Dtos
{
    public class ProjectTechDto : BaseDto
    {
        public long? ProjectTechId { get; set; }
        public Guid? ProjectTechGuid { get; set; }
        public string Name { get; set; } = default!;
        public long ProjectId { get; set; }

        public override bool IsValid()
        {
            var baseValid = base.IsValid();
            if (ProjectId == 0)
                _validationMessage.AppendLine("پروژه باید مشخص شود.");            
            var result = _validationMessage.ToString() == string.Empty && baseValid;
            return result;
        }

        public override void PrepareDto(Guid currentUserId)
        {
            base.PrepareDto(currentUserId);
            if (ProjectTechId.GetValueOrDefault() == 0) // Create
            {
                ProjectTechGuid = Guid.NewGuid();
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
            mProfile.CreateMap<ProjectTechDto, ProjectTech>();
            //.ForMember(d => d.Images, opt => opt.Ignore())
            //.AfterMap(UpdateImages);
        }
    }

}
