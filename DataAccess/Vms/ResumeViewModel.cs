using Domain;

namespace DataAccess.Vms
{
    public class ResumeViewModel
    {

        // Profile Info (Optional but recommended)
        public string FullNameFa { get; set; } = default!;
        public string FullNameEn { get; set; } = default!;
        public string JobTitleFa { get; set; } = default!;
        public string JobTitleEn { get; set; } = default!;
        public string BioFa { get; set; } = default!;
        public string BioEn { get; set; } = default!;
        public string? ProfileImagePath { get; set; }

        //Contact

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }

        // Skills
        public List<Skill> Skills { get; set; } = [];

        // Projects
        public List<Project> Projects { get; set; } = [];

        // Experience
        public List<Experience> Experiences { get; set; } = [];

        // Education
        public List<Education> Educations { get; set; } = [];
    }
}
