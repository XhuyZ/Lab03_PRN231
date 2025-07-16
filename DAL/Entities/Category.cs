namespace DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<Orchid> Orchids { get; set; } = new List<Orchid>();
    }
}

