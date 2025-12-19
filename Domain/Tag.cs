namespace Domain
{
    public class Tag : BaseEntity
    {
        public long TagId { get; set; }
        public Guid TagGuid { get; set; }
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
    }

}
