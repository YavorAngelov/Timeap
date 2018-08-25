using System.ComponentModel.DataAnnotations;

namespace Timeap.Web.Models
{
    public class ContactBindingModel
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
