namespace Domain
{
    public class Skill : BaseEntity
    {
        public long SkillId { get; set; }
        public Guid SkillGuid { get; set; }
        public string Name { get; set; } = default!;
        public int Level { get; set; }  // 1..100
        public long UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; } = default!;
    }

}
