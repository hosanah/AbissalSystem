
using System.Collections.Generic;

namespace AbissalSystem.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<User> Users { get; set; }
    }
}