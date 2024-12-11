using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var model =await _context.Categories.ToListAsync();

            return Ok(model); 
        }
        [HttpPost]
        public async Task<IActionResult> AddCategories(Category category)
        {
            await _context.Categories.AddAsync(category);
            _context.SaveChanges();

            return Ok(category);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody]Category category)
        {
            var model = await _context.Categories.SingleOrDefaultAsync(c => c.Id == category.Id);

            if (model is null)
                return NotFound($"No genre is find with ID: {category.Id}");

            model.Name = category.Name;

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategoryPatch(int id, [FromBody] JsonPatchDocument<Category> category)
        {
            var model = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if (model is null)
                return NotFound($"No genre is find with ID: {id}");

           // model.ApplyTo(category);
            category.ApplyTo(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }
    }
}
