namespace Timeap.Models
{
    public class UsersTeams
    {
        public string MemberId { get; set; }

        public User Member { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
