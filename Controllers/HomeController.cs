using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        List<Dish> AllDishes = _context.Dishs.OrderBy(c=>c.Name).ToList();
        ViewBag.AllDishes = AllDishes;
        return View();
    }

    public IActionResult NewDish()
    {
        return View();
    }
    [HttpGet("dishes/{DishId}/edit")]
    public IActionResult EditDish(int DishId)
    {
        Dish? dish = _context.Dishs.FirstOrDefault(c=>c.DishId == DishId);
        return View(dish);

    }
    [HttpPost("dishes/{DishId}/update")]
    public IActionResult UpdateDish(int DishId, Dish NewDish)
    {
        if(ModelState.IsValid)
        {
            Dish? oldDish = _context.Dishs.FirstOrDefault(c=>c.DishId == DishId);
            oldDish.Chef = NewDish.Chef;
            oldDish.Name = NewDish.Name;
            oldDish.Calories = NewDish.Calories;
            oldDish.Tastiness = NewDish.Tastiness;
            oldDish.Description = NewDish.Description;
            oldDish.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("ShowDish", new{DishId = oldDish.DishId});
        } 
        else 
        {
            return View("EditDish", NewDish);
        }
        
    }
    [HttpPost("dishes/create")]
    public IActionResult AddDish(Dish NewDish)
    {
        if(ModelState.IsValid)
        {
            _context.Add(NewDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        return View("NewDish");
    }

    [HttpGet("dishes/{DishId}")]
    public IActionResult ShowDish(int DishId)
    {
        Dish? dish = _context.Dishs.FirstOrDefault(c=>c.DishId == DishId);
        return View(dish);
    }
    [HttpPost("dishes/{DishId}/Destroy")]
    public IActionResult DestroyDish(int DishId)
    {
        Dish? dishToDelete = _context.Dishs.SingleOrDefault(c=>c.DishId == DishId);
        _context.Dishs.Remove(dishToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
