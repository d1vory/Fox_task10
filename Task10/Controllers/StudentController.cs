using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;

namespace Task10.Controllers;

[Route("courses/{courseId}/groups/{GroupId}/students")]
public class StudentController: Controller
{
    private readonly ApplicationContext _db;

    public StudentController(ApplicationContext db)
    {
        _db = db;
    }
    
    
    [Route("")]
    public async Task<IActionResult> Index(int? courseId, int? groupId)
    {
        if (!courseId.HasValue || !groupId.HasValue)
        {
            return NotFound();
        }

        var students = await _db.Students.Where(g => g.GroupId == groupId.Value).ToListAsync();
        ViewBag.CourseId = courseId;
        ViewBag.GroupId = groupId;
        return View(students);
    }
    
    [Route("create")]
    public IActionResult Create(int? courseId, int? groupId)
    {
        if (!courseId.HasValue || !groupId.HasValue)
        {
            return NotFound();
        }
        return View();
    }
    
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(int? courseId, int? groupId, Student student)
    {
        await _db.Students.AddAsync(student);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index", new { courseId, groupId });
    }
}