using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Experience
    {
        public long ExperienceId { get; set; }
        public Guid ExperienceGuid { get; set; }

        public string JobTitleFa { get; set; } = default!;
        public string JobTitleEn { get; set; } = default!;

        public string CompanyFa { get; set; } = default!;
        public string CompanyEn { get; set; } = default!;

        public string DescriptionFa { get; set; } = default!;
        public string DescriptionEn { get; set; } = default!;

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // null = Present

        public bool IsCurrent => EndDate == null;
    }

}
