using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeManagementSystem.Data;
using RecipeManagementSystem.Models;

namespace RecipeManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly RecipeDbContext _context;

    public HomeController(RecipeDbContext context)
    {
        _context = context;
    }

    // GET: Home/Index
    public async Task<IActionResult> Index(string searchString)
    {
        var recipes = from r in _context.Recipes
                      select r;

        if (!string.IsNullOrEmpty(searchString))
        {
            recipes = recipes.Where(r => r.Name.Contains(searchString));
        }

        return View(await recipes.ToListAsync());
    }

    // GET: Home/Details/5

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (recipe == null)
        {
            return NotFound();
        }

        return PartialView("_RecipeDetails", recipe);
    }
        // GET: Home/Create
    public IActionResult Create()
    {
        return View(new Recipe()); // Fixed: Pass new Recipe object
    }

    // POST: Home/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Ingredients,Instructions,PreparationTime,Category")] Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            _context.Add(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(recipe);
    }

    // GET: Home/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        return View(recipe);
    }

    // POST: Home/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ingredients,Instructions,PreparationTime,Category")] Recipe recipe)
    {
        if (id != recipe.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(recipe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(recipe);
    }

    private bool RecipeExists(int id)
    {
        return _context.Recipes.Any(e => e.Id == id);
    }
    
}