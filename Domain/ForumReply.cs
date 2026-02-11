using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ForumReply : BaseEntity
    {
        public long ForumReplyId { get; set; }
        public Guid ForumReplyGuid{ get; set; }
        public string Content { get; set; } = default!;

        public bool IsApproved { get; set; } = false;

        public string UserId { get; set; } = default!;
        public AppUser User { get; set; } = default!;
        public long TopicId { get; set; }
        public ForumTopic Topic { get; set; } = default!;

        // Threading
        public long? ParentId { get; set; }
        public ForumReply? Parent { get; set; }
        public ICollection<ForumReply> Children { get; set; } = [];
        public ICollection<ReplyVote> Votes { get; set; } = [];

    }

}
