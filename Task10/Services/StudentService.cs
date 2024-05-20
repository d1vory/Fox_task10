using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;

namespace Task10.Services;

public class StudentService
{
    private readonly BaseApplicationContext _db;

    public StudentService(BaseApplicationContext db)
    {
        _db = db;
    }
    
    public async Task<List<Student>> List(int groupId)
    {
        return await _db.Students.Where(s => s.GroupId == groupId).ToListAsync();
    }
    
    public async Task<Student?> Retrieve(int id)
    {
        var student = await _db.Students.FindAsync(id);
        return student;
    }
    
    public async Task<Student> Create(string firstName, string lastName, int groupId)
    {
        var student = new Student() { FirstName = firstName, LastName = lastName, GroupId = groupId};
        return await Create(student);
    }

    public async Task<Student> Create(Student student)
    {
        await _db.Students.AddAsync(student);
        await _db.SaveChangesAsync();
        return student;
    }

    public async Task<Student> Update(Student student, int? id = null)
    {
        if (id.HasValue)
        {
            student.Id = id.Value;
        }
        _db.Students.Update(student);
        await _db.SaveChangesAsync();
        return student;
    }

    public async Task Delete(int id)
    {
        var student = await _db.Students.FindAsync(id);
        if (student == null)
        {
            return;
        }

        _db.Students.Remove(student);
        await _db.SaveChangesAsync();
    }
}