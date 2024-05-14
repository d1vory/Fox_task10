using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;

namespace Task10.Controllers;

public class CourseController: Controller
{
    private readonly ApplicationContext _db;

    public CourseController(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _db.Courses.ToListAsync();
        return View(courses);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    // public async Task<IActionResult> Edit(int? id)
    // {
    //     if (!id.HasValue)
    //     {
    //         return NotFound();
    //     }
    //
    //     var course = await _db.FindAsync(id.Value);
    //
    // }
    
    
}