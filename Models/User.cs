using System.Collections.Generic;

namespace AbissalSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<Role> Roles { get; set; }        
    }
}