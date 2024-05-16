using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;

namespace Task10.Controllers;

public class GroupController: Controller
{
    private readonly ApplicationContext _db;
    private int _courseId;
    
    public GroupController(ApplicationContext db)
    {
        _db = db;
    }
    
    public async Task<IActionResult> Index(int? courseId)
    {
        if (!courseId.HasValue)
        {
            return NotFound();
        }

        _courseId = courseId.Value;
        var groups = await _db.Groups.Where(g => g.CourseId == courseId.Value).ToListAsync();
        return View(groups);
    }


    public IActionResult Create()
    {
        return View();
    }
}