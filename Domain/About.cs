using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class About
    {
        public int AboutId { get; set; }
        public Guid AboutGuid { get; set; }

        public string TitleFa { get; set; } = default!;
        public string TitleEn { get; set; } = default!;

        public string BioFa { get; set; } = default!;
        public string BioEn { get; set; } = default!;

        public string? ProfileImagePath { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? LocationFa { get; set; }
        public string? LocationEn { get; set; }

        public string? LinkedinUrl { get; set; }
        public string? GithubUrl { get; set; }
        public string? WebsiteUrl { get; set; }
    }
}
