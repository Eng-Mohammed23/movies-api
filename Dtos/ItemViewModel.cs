namespace MoviesApi.Dtos
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string? Nots { get; set; }
        public IFormFile? Image { get; set; }
        public int CategoryId { get; set; }
    }
}
