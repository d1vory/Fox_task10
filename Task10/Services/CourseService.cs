using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;

namespace Task10.Services;

public class CourseService: ICrudService<Course>
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

    public Task<Course> Create()
    {
        throw new NotImplementedException();
    }

    public Task<Course> Update()
    {
        throw new NotImplementedException();
    }

    public Task Delete()
    {
        throw new NotImplementedException();
    }
}