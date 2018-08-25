using System.ComponentModel.DataAnnotations;
using Timeap.Common.Validation;

namespace Timeap.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        public int StatusTypeId { get; set; }

        public StatusType StatusType { get; set; }

        [Required]
        [Range(ValidationConstants.ProgressMinValue, ValidationConstants.ProgressMaxValue)]
        public decimal Progress { get; set; }

        public int SolutionId { get; set; }

        public Solution Solution { get; set; }
    }
}
