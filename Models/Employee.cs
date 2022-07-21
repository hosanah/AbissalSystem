namespace AbissalSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public DocumentType DocumentId { get; set; }

        public string DocumentNumber { get; set; }
        public Double Salary { get; set; }
        
    }
}