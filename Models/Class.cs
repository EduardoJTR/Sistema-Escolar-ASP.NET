namespace SchoolSystem.Models
{
    public class Class
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        
        // Codigo legível para um humano, Ex: 3ºB
        public string ClassCode { get; set; }
    }
}
