using System;
using System.ComponentModel.DataAnnotations;
using Timeap.Common.Validation;

namespace Timeap.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.TitleMaxLength,
            MinimumLength =ValidationConstants.TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.DescritionMaxLength,
            MinimumLength = ValidationConstants.DescritionMinLength)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ClientId { get; set; }

        public User Client { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
