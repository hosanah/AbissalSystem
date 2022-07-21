using System.Collections.Generic;

namespace AbissalSystem.Models
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CorporateName { get; set; }
         public List<Product> Products { get; set; }
        
    }
}