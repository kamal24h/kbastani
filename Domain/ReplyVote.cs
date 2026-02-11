using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReplyVote : BaseEntity
    {
        public long ReplyVoteId { get; set; }
        public Guid TopicVoteGuid { get; set; }

        public long ReplyId { get; set; }
        public ForumReply Reply { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public AppUser User { get; set; } = default!;

        public int Value { get; set; } // +1 or -1
    }

}
