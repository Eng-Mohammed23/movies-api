using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private int _maxAllowedPosterSize = 108488;
        private List<string> _allowedExtentions = new() { ".jpg",".png"};

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MovieDto dto)
        {
            if (_allowedExtentions.Contains(dto.Poster.FileName))
                return BadRequest("no alowed extinsions");

            if(dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max than allowed");

            var isValidGenre = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);

            if (!isValidGenre)
                return BadRequest("isValidGenre");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                Storeline = dto.Storeline,
                GenreId = dto.GenreId,
                Poster = dataStream.ToArray(),
            };

            await _context.AddAsync(movie);
            _context.SaveChanges();

            return Ok(movie);
        }
    }
}
