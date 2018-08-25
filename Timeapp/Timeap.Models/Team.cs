using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Timeap.Common.Validation;

namespace Timeap.Models
{
    public class Team
    {
        public Team()
        {
            this.Solutions = new List<Solution>();
            this.Members = new List<UsersTeams>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.TeamNameMaxLength,
            MinimumLength = ValidationConstants.TeamNameMinLength)]
        public string Name { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public ICollection<Solution> Solutions { get; set; }

        public ICollection<UsersTeams> Members { get; set; }
    }
}
