namespace MoviesApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string? Nots { get; set; }
        public byte[]? Image { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
