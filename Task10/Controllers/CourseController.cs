using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;

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

    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        await _db.Courses.AddAsync(course);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Edit(int? courseId)
    {
        if (!courseId.HasValue)
        {
            return NotFound();
        }
    
        var course = await _db.Courses.FindAsync(courseId.Value);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Edit(Course course)
    {
        _db.Courses.Update(course);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int? courseId)
    {
        if (!courseId.HasValue)
        {
            return NotFound();
        }
        var course = await _db.Courses.FindAsync(courseId.Value);
        if (course == null)
        {
            return NotFound();
        }
        _db.Courses.Remove(course);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    
}