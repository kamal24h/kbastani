using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TopicVote : BaseEntity
    {
        public long TopicVoteId { get; set; }
        public Guid TopicVoteGuid { get; set; }

        public long TopicId { get; set; }
        public ForumTopic Topic { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public AppUser User { get; set; } = default!;

        // +1 = Upvote, -1 = Downvote
        public int Value { get; set; }
    }

}
