namespace Domain
{
    public class Tag : BaseEntity
    {
        public long TagId { get; set; }
        public Guid TagGuid { get; set; }
        public string NameFa { get; set; }
        public string NameEn { get; set; }
        public string Slug { get; set; }
    }

}
