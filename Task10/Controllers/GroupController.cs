using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;

namespace Task10.Controllers;

[Route("courses/{courseId}/groups")]
public class GroupController : Controller
{
    private readonly ApplicationContext _db;

    public GroupController(ApplicationContext db)
    {
        _db = db;
    }

    [Route("")]
    public async Task<IActionResult> Index(int? courseId)
    {
        if (!courseId.HasValue)
        {
            return NotFound();
        }

        var groups = await _db.Groups.Where(g => g.CourseId == courseId.Value).ToListAsync();
        ViewBag.CourseId = courseId;
        return View(groups);
    }

    [Route("create")]
    public IActionResult Create(int? courseId)
    {
        return View();
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(int? courseId, Group group)
    {
        await _db.Groups.AddAsync(group);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index", new { courseId });
    }

    [Route("{groupId}/edit")]
    public async Task<IActionResult> Edit(int? courseId, int? groupId)
    {
        if (!courseId.HasValue || !groupId.HasValue)
        {
            return NotFound();
        }

        var group = await _db.Groups.FindAsync(groupId.Value);
        if (group == null)
        {
            return NotFound();
        }

        ViewBag.CourseId = courseId;
        return View(group);
    }


    [HttpPost]
    [Route("{groupId}/edit")]
    public async Task<IActionResult> Edit(int? courseId, int? groupId, Group group)
    {
        if (!courseId.HasValue || !groupId.HasValue)
        {
            return NotFound();
        }

        group.Id = groupId.Value;
        _db.Groups.Update(group);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index", new { courseId });
    }

    [HttpPost]
    [Route("{groupId}/delete")]
    public async Task<IActionResult> Delete(int? courseId, int? groupId)
    {
        if (!courseId.HasValue || !groupId.HasValue)
        {
            return NotFound();
        }

        var group = await _db.Groups.FindAsync(groupId.Value);
        if (group == null)
        {
            return NotFound();
        }

        _db.Groups.Remove(group);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index", new { courseId });
    }
}