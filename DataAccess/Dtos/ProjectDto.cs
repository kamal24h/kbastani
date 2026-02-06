using AutoMapper;
using DataAccess.Vms;
using Domain;

namespace DataAccess.Dtos
{
    public class ProjectDto : BaseDto
    {
        public long? ProjectId { get; set; }
        public Guid? ProjectGuid { get; set; }

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
        public long? ProfileId { get; set; }
        public IList<ProjectTechDto> Techs { get; set; } = [];

        public override bool IsValid()
        {
            var baseValid = base.IsValid();
            if (ProfileId == 0)
                _validationMessage.AppendLine("پروفایل کاربر باید مشخص شود.");
            if (string.IsNullOrEmpty(TitleFa))
                _validationMessage.AppendLine("عنوان فارسی پروژه باید وارد شود.");
            if (string.IsNullOrEmpty(TitleEn))
                _validationMessage.AppendLine("عنوان انگلیسی پروژه باید وارد شود.");
            var result = _validationMessage.ToString() == string.Empty && baseValid;
            return result;
        }

        public override void PrepareDto(Guid currentUserId)
        {
            base.PrepareDto(currentUserId);
            if (ProjectId.GetValueOrDefault() == 0) // Create
            {
                ProjectGuid = Guid.NewGuid();
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
            mProfile.CreateMap<ProjectDto, Project>();
            //.ForMember(d => d.Images, opt => opt.Ignore())
            //.AfterMap(UpdateImages);
        }
    }

}
