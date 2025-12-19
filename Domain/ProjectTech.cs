namespace Domain
{
    public class ProjectTech : BaseEntity
    {
        public long ProjectTechId { get; set; }
        public Guid ProjectTechGuid { get; set; }
        public string Name { get; set; } = default!;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = default!;
    }

}
