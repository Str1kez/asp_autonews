using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbContext = asp_autonews.Domain.DbContext;

namespace asp_autonews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private readonly DbContext _context;

        public ArticlesApiController(DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<Domain.Entities.Article> GetArticles()
        {
            return _context.Articles;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var article = await _context.Articles.FindAsync(id);
            
            if (article == null) return NotFound();
            
            return Ok(article);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle([FromRoute] Guid id, [FromBody] Domain.Entities.Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] Domain.Entities.Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }
        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return Ok(article);
        }
        
        private bool ArticleExists(Guid id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}