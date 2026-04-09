using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using moomug_project.Data;
using moomug_project.Models;
using Microsoft.EntityFrameworkCore;

namespace moomug_project.Controllers
{
    public class MyCollectionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;

        // Dependency injected database and user manager for use in actions
        public MyCollectionController(ApplicationDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
        }

        // GET: Index - Displays the user's collection
        public async Task<IActionResult> Index(string searchQuery)
        {
            // Get the current logged-in user
            var user = await _um.GetUserAsync(User);
            if (user == null)
            {
                return View(); // The view will handle the logic for not logged-in users
            }

            // Retrieve the user's collection from the database, including related GlobalMugCollection data
            var userId = user.Id;
            var userCollection = await _db.MyCollections
                .Where(c => c.ApplicationUserId == userId)
                .Include(c => c.GlobalMugCollection)  // Include GlobalMugCollection data
                .ToListAsync();
            
            // If there's a search query, filter the results
            if (!string.IsNullOrEmpty(searchQuery))
            {
                string searchQueryLower = searchQuery.ToLower();

                userCollection = userCollection.Where(c =>
                        c.GlobalMugCollection.MugName.ToLower().Contains(searchQueryLower) ||
                        c.GlobalMugCollection.StartYear.ToString().Contains(searchQueryLower) ||
                        c.GlobalMugCollection.EndYear.ToString().Contains(searchQueryLower)
                ).ToList();
            }

            return View(userCollection);
        }

        // POST: Add - Adds a mug to the user's collection
        [HttpPost]
        public async Task<IActionResult> Add(int globalMugId)
        {
            // Get the current logged-in user
            var user = await _um.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
            }

            // Check if the mug is already in the user's collection
           
            // If not, create a new entry and add it to the user's collection
            var newCollectionItem = new MyCollection
            {
                ApplicationUserId = user.Id,
                GlobalMugCollectionId = globalMugId,
                MyDescription = "New mug from global collection",
                MyMugDetails = "",
                MyHasLabel = false,
                MyIsUsed = false
            };
                
            _db.MyCollections.Add(newCollectionItem);
            await _db.SaveChangesAsync(); // Save changes to the database

            return RedirectToAction("Index", "GlobalCollection"); // Redirect back to the global collection view
        }
        
        [HttpPost]
        public async Task<IActionResult> Save(int id, string description, string details, bool isUsed, bool hasLabel)
        {
            var user = await _um.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
            }

            // Fetch the mug from the database
            var mug = await _db.MyCollections.FirstOrDefaultAsync(m => m.Id == id && m.ApplicationUserId == user.Id);
            if (mug == null)
            {
                return NotFound();
            }

            // Update the mug properties
            mug.MyDescription = description ?? string.Empty; // Handle nulls gracefully
            mug.MyMugDetails = details ?? string.Empty;      // Handle nulls gracefully
            mug.MyIsUsed = isUsed;                           // No special handling needed for bool
            mug.MyHasLabel = hasLabel;                       // No special handling needed for bool

            // Save changes to the database
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "MyCollection");
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int myMugId)
        {
            // Get the current logged-in user
            var user = await _um.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
            }

            // Find the mug in the user's collection
            var mugToRemove = _db.MyCollections.FirstOrDefault(m => m.Id == myMugId && m.ApplicationUserId == user.Id);
            if (mugToRemove != null)
            {
                // Remove the mug from the user's collection
                _db.MyCollections.Remove(mugToRemove);
                await _db.SaveChangesAsync(); // Save changes to the database
            }

            return RedirectToAction("Index"); // Redirect back to the user's collection view
        }
    }
}