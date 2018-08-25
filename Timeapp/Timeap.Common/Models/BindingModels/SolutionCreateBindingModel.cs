using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Timeap.Common.Validation;

namespace Timeap.Common.Models.BindingModels
{
    public class SolutionCreateBindingModel
    {
        [Required]
        [StringLength(
            ValidationConstants.TitleMaxLength,
            MinimumLength = ValidationConstants.TitleMinLength)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.DescritionMaxLength,
            MinimumLength = ValidationConstants.DescritionMinLength)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start time")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End time")]
        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }

        public int TeamId { get; set; }
    }
}
