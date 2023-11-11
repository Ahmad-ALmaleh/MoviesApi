using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _context.Genres.OrderBy(g=>g.Name).ToListAsync();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = new Genre{ Name = dto.Name, };
            await _context.AddAsync(genre);
            _context.SaveChanges();
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int Id,[FromBody] GenreDto dto)
        {
            var genre =await _context.Genres.FirstOrDefaultAsync(g=>g.Id==Id);
            if(genre == null)
                return NotFound($"this Id {Id} not exist");
            genre.Name = dto.Name;
            _context.SaveChanges();
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g=>g.Id==Id);
            if (genre == null)
                return NotFound("not found");
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return Ok(genre);
        }
    }
}
