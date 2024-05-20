using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;
using Task10.Services;
using Task10.Utils;

namespace Task10.Controllers;

[Route("courses/{courseId}/groups/{GroupId}/students")]
public class StudentController : Controller
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }


    [Route("")]
    public async Task<IActionResult> Index(int? courseId, int? groupId)
    {
        if (!UtilService.IsParamsFilled(courseId, groupId))
        {
            return NotFound();
        }

        var students = await _studentService.List(groupId.Value);
        ViewBag.CourseId = courseId;
        ViewBag.GroupId = groupId;
        return View(students);
    }

    [Route("create")]
    public IActionResult Create(int? courseId, int? groupId)
    {
        if (!UtilService.IsParamsFilled(courseId, groupId))
        {
            return NotFound();
        }

        return View();
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(int? courseId, int? groupId, Student student)
    {
        await _studentService.Create(student);
        return RedirectToAction("Index", new { courseId, groupId });
    }

    [Route("{studentId}/edit")]
    public async Task<IActionResult> Edit(int? courseId, int? groupId, int? studentId)
    {
        if (!UtilService.IsParamsFilled(courseId, groupId, studentId))
        {
            return NotFound();
        }

        var student = await _studentService.Retrieve(studentId.Value);
        if (student == null)
        {
            return NotFound();
        }

        ViewBag.CourseId = courseId;
        ViewBag.groupId = groupId;
        return View(student);
    }


    [HttpPost]
    [Route("{studentId}/edit")]
    public async Task<IActionResult> Edit(int? courseId, int? groupId, int? studentId, Student student)
    {
        if (!UtilService.IsParamsFilled(courseId, groupId, studentId))
        {
            return NotFound();
        }

        await _studentService.Update(student, studentId);
        return RedirectToAction("Index", new { courseId, groupId });
    }

    [HttpPost]
    [Route("{studentId}/delete")]
    public async Task<IActionResult> Delete(int? courseId, int? groupId, int? studentId)
    {
        if (!UtilService.IsParamsFilled(courseId, groupId, studentId))
        {
            return NotFound();
        }

        await _studentService.Delete(studentId.Value);
        return RedirectToAction("Index", new { courseId, groupId });
    }
}