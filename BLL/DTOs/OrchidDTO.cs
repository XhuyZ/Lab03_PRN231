namespace BLL.DTOs
{
    public class OrchidDTO
    {
        public int Id { get; set; }
        public bool IsNatural { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty; // Include Category name in DTO
    }
}

