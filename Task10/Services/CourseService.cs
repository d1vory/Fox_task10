using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;

namespace Task10.Services;

public class CourseService
{
    private readonly ApplicationContext _db;

    public CourseService(ApplicationContext db)
    {
        _db = db;
    }
    
    public async Task<List<Course>> List()
    {
        return await _db.Courses.ToListAsync();
    }
    
    public async Task<Course?> Retrieve(int id)
    {
        var course = await _db.Courses.FindAsync(id);
        return course;
    }
    
    public Task<Course> Create(string name, string description)
    {
        var course = new Course() { Name = name, Description = description };
        return Create(course);
    }

    public async Task<Course> Create(Course course)
    {
        await ValidateName(course.Name);
        await _db.Courses.AddAsync(course);
        await _db.SaveChangesAsync();
        return course;
    }

    public async Task<Course> Update(Course course, int? id = null)
    {
        await ValidateName(course.Name);
        if (id.HasValue)
        {
            course.Id = id.Value;
        }
        _db.Courses.Update(course);
        await _db.SaveChangesAsync();
        return course;
    }

    public async Task Delete(int id)
    {
        var course = await _db.Courses.FindAsync(id);
        if (course == null)
        {
            return;
        }

        _db.Courses.Remove(course);
        await _db.SaveChangesAsync();
    }

    public async Task ValidateName(string name, int? id = null)
    {
        if (await _db.Courses.AnyAsync(c => c.Name == name && c.Id != id))
        {
            throw new ApplicationException("This name already exists");
        }
    }
}