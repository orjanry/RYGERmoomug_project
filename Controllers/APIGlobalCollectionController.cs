using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moomug_project.Data;

namespace moomug_project.Controllers;

[ApiController]
[Route("api/[controller]")]

public class APIGlobalCollectionController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public APIGlobalCollectionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchMugs(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Search query cannot be empty");
        }

        var results = await _context.GlobalMugCollections
            .Where(m => m.MugName.Contains(query))
            .ToListAsync();

        return Ok(results);
    }
}