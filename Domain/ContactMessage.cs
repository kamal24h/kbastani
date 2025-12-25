using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ContactMessage
    {
        public long ContactMessageId { get; set; }
        public Guid ContactMessageGuid { get; set; }

        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string Message { get; set; } = default!;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }

}
