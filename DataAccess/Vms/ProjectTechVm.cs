using AutoMapper;
using Domain;

namespace DataAccess.Vms
{
    public class ProjectTechVm : BaseVm
    {
        public long ProjectTechId { get; set; }
        public Guid ProjectTechGuid { get; set; }
        public string Name { get; set; } = default!;
        public ProjectVm Project { get; set; } = default!;

        public static void ConfigureMapper(Profile mProfile)
        {
            mProfile.CreateMap<ProjectTech, ProjectTechVm>();
        }
    }
}
