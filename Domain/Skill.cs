namespace Domain
{
    public class Skill : BaseEntity
    {
        public long SkillId { get; set; }
        public Guid SkillGuid { get; set; }
        public string Name { get; set; } = default!;
        public int Level { get; set; }  // 1..100
        public int ProfileId { get; set; }
        public UserProfile Profile { get; set; } = default!;
    }

}
