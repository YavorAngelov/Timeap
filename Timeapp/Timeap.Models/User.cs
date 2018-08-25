using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Timeap.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Projects = new List<Project>();
            this.Teams = new List<UsersTeams>();
        }

        public ICollection<Project> Projects { get; set; }

        public ICollection<UsersTeams> Teams { get; set; }
    }
}
