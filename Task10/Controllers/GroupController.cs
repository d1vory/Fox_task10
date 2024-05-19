using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;
using Task10.Services;
using Task10.Utils;

namespace Task10.Controllers;

[Route("courses/{courseId}/groups")]
public class GroupController : Controller
{
    private readonly GroupService _groupService;
    private readonly ApplicationContext _db;

    public GroupController(GroupService groupService)
    {
        _db = new ApplicationContext();
        _groupService = groupService;
    }

    [Route("")]
    public async Task<IActionResult> Index(int? courseId)
    {
        if (!UtilService.IsParamsFilled(courseId))
        {
            return NotFound();
        }

        if (TempData.ContainsKey("deleteError"))
        {
            var splittedError = TempData["deleteError"].ToString().Split(",");
            ModelState.AddModelError(splittedError[0], splittedError[1]);
        }

        var groups = await _groupService.List(courseId.Value);
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
        if (!UtilService.IsParamsFilled(courseId))
        {
            return NotFound();
        }

        try
        {
            await _groupService.Create(group);
        }
        catch (ApplicationException ex)
        {
            ModelState.AddModelError("Name", ex.Message);
            return View();
        }
        return RedirectToAction("Index", new { courseId });
    }

    [Route("{groupId}/edit")]
    public async Task<IActionResult> Edit(int? courseId, int? groupId)
    {
        if (!UtilService.IsParamsFilled(courseId, groupId))
        {
            return NotFound();
        }

        var group = await _groupService.Retrieve(groupId.Value);
        if (group == null)
        {
            return NotFound();
        }

        ViewBag.CourseId = courseId.Value;
        return View(group);
    }


    [HttpPost]
    [Route("{groupId}/edit")]
    public async Task<IActionResult> Edit(int? courseId, int? groupId, Group group)
    {
        if (!UtilService.IsParamsFilled(courseId, groupId))
        {
            return NotFound();
        }
        try
        {
            await _groupService.Update(group, groupId.Value);
        }
        catch (ApplicationException ex)
        {
            ModelState.AddModelError("Name", ex.Message);
            return View();
        }
        
        return RedirectToAction("Index", new { courseId });
    }

    [HttpPost]
    [Route("{groupId}/delete")]
    public async Task<IActionResult> Delete(int? courseId, int? groupId)
    {
        if (!UtilService.IsParamsFilled(courseId, groupId))
        {
            return NotFound();
        }

        try
        {
            await _groupService.Delete(groupId.Value);
        }
        catch(ApplicationException ex)
        {
            TempData["deleteError"] = $"{groupId.Value}, There are students in this group";
            return RedirectToAction(nameof(Index), new { courseId });
        }
        
        return RedirectToAction("Index", new { courseId });
    }
}