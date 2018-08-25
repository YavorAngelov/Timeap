using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Timeap.Common.Validation;

namespace Timeap.Models
{
    public class StatusType
    {
        public StatusType()
        {
            this.Statuses = new List<Status>();
        }

        public StatusType(string name) 
            : this()
        {
            this.Name = name;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.NameMaxLength,
            MinimumLength = ValidationConstants.NameMinLength)]
        public string Name { get; set; }

        public ICollection<Status> Statuses { get; set; }
    }
}
