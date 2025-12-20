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

        // Skills
        public List<Skill> Skills { get; set; } = new();

        // Projects
        public List<Project> Projects { get; set; } = new();

        // Experience
        public List<Experience> Experiences { get; set; } = new();

        // Education
        public List<Education> Educations { get; set; } = new();
    }
}
