using System.ComponentModel.DataAnnotations;

namespace Timeap.Common.Models.BindingModels
{
    public class UserContactBindingModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        [MaxLength(5000)]
        public string Message { get; set; }
    }
}
