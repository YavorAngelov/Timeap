using System;
using System.ComponentModel.DataAnnotations;
using Timeap.Common.Validation;

namespace Timeap.Models
{
    public class Solution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.TitleMaxLength,
            MinimumLength = ValidationConstants.TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.DescritionMaxLength,
            MinimumLength = ValidationConstants.DescritionMinLength)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
