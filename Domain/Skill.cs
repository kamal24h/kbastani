namespace Domain
{
    public class Skill : BaseEntity
    {
        public long SkillId { get; set; }
        public Guid SkillGuid { get; set; }
        public string NameFa { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public int Level { get; set; } // 1–100
        public long UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; } = default!;
    }

}
