using System.ComponentModel.DataAnnotations;

namespace AbissalSystem.ViewModels
{
    public class EditorClientViewModel
    {
        [Required()]
        public string FullName { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string CallPhone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}