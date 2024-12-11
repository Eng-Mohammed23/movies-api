using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AllItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItems([FromForm]ItemViewModel model)
        {
            
            using var stream = new MemoryStream();
            await model.Image!.CopyToAsync(stream);

            Item viewModel = new()
            {
                Name = model.Name,
                Nots = model.Nots,
                Price = model.Price,
                Image = stream.ToArray()
            };
            await _context.Items.AddAsync(viewModel);
            await _context.SaveChangesAsync();

            return Ok(viewModel);
        }
    }
}
