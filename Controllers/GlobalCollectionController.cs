using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moomug_project.Data;

namespace moomug_project.Controllers;

[Route("GlobalCollection")]
public class GlobalCollectionController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public GlobalCollectionController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string searchQuery)
    {
        var mugs = await _context.GlobalMugCollections.ToListAsync();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            string searchQueryLower = searchQuery.ToLower();

            mugs = mugs.Where(m => 
                m.MugName.ToLower().Contains(searchQueryLower) ||
                m.StartYear.ToString().Contains(searchQueryLower) ||
                m.EndYear.ToString().Contains(searchQueryLower)
            ).ToList();
        }
        
        return View(mugs);
    }
    
    /*[HttpGet("search")]
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
    }*/
    
}