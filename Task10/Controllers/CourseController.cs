using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;
using Task10.Services;
using Task10.Utils;

namespace Task10.Controllers;

public class CourseController : Controller
{
    private readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.List();
        return View(courses);
    }


    [Route("courses/create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Route("courses/create")]
    public async Task<IActionResult> Create(Course course)
    {
        try
        {
            await _courseService.Create(course);
        }
        catch (ApplicationException ex)
        {
            ModelState.AddModelError("Name", ex.Message);
            return View();
        }
        return RedirectToAction("Index");
    }

    [Route("courses/{courseId}/edit")]
    public async Task<IActionResult> Edit(int? courseId)
    {
        if (!UtilService.IsParamsFilled(courseId))
        {
            return NotFound();
        }

        var course = await _courseService.Retrieve(courseId.Value);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }


    [HttpPost]
    [Route("courses/{courseId}/edit")]
    public async Task<IActionResult> Edit(int? courseId, Course course)
    {
        if (!UtilService.IsParamsFilled(courseId))
        {
            return NotFound();
        }

        await _courseService.Update(course, courseId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("courses/{courseId?}/delete")]
    public async Task<IActionResult> Delete(int? courseId)
    {
        if (!UtilService.IsParamsFilled(courseId))
        {
            return NotFound();
        }

        await _courseService.Delete(courseId.Value);
        return RedirectToAction("Index");
    }
}