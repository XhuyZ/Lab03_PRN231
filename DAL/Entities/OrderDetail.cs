namespace DAL.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrchidId { get; set; }
        public Orchid Orchid { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}

