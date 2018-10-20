using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class Following
    {
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }

        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FolloweeId { get; set; }

        protected Following()
        {
        }

        public Following(string followerId, string followeeId)
        {
            FollowerId = followerId;
            FolloweeId = followeeId;
        }
    }
}