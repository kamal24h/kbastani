using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Education
    {
        public long EducationId { get; set; }
        public Guid EducationGuid { get; set; }

        public string DegreeFa { get; set; } = default!;
        public string DegreeEn { get; set; } = default!;

        public string UniversityFa { get; set; } = default!;
        public string UniversityEn { get; set; } = default!;

        public string DescriptionFa { get; set; } = default!;
        public string DescriptionEn { get; set; } = default!;

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
