using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ForumTopic : BaseEntity
    {
        public long ForumTopicId { get; set; }
        public Guid ForumTopicGuid { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string Slug { get; set; } = default!;

        public bool IsApproved { get; set; } = false;
        public bool IsLocked { get; set; } = false;
        public bool IsPinned { get; set; } = false;

        public string UserId { get; set; } = default!;
        public AppUser User { get; set; } = default!;

        public long CategoryId { get; set; }
        public ForumCategory Category { get; set; } = default!;

        public ICollection<ForumReply> Replies { get; set; } = [];
        public ICollection<TopicVote> Votes { get; set; } = [];
    }
}
