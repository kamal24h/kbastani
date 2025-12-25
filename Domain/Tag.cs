namespace Domain
{
    public class Tag : BaseEntity
    {
        public long TagId { get; set; }
        public Guid TagGuid { get; set; }
        public string NameFa { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public string Slug { get; set; } = default!;
    }

}
