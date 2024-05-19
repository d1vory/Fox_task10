using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;

namespace Task10.Services;

public class GroupService
{
    private readonly ApplicationContext _db;

    public GroupService(ApplicationContext db)
    {
        _db = db;
    }
    
    public async Task<List<Group>> List(int courseId)
    {
        var groups = await _db.Groups.Where(g => g.CourseId == courseId).ToListAsync();
        return groups;
    }
    
    public async Task<Group?> Retrieve(int id)
    {
        return await _db.Groups.FindAsync(id);
    }

    public async Task<Group> Create(string name, Course course)
    {
        return await Create(new Group() { Name = name, Course = course });
    }

    public async Task<Group> Create(Group group)
    {
        await ValidateName(group.Name);
        await _db.Groups.AddAsync(group);
        await _db.SaveChangesAsync();
        return group;
    }

    public async Task<Group> Update(Group group, int? id = null)
    {
        await ValidateName(group.Name);
        if (id.HasValue)
        {
            group.Id = id.Value;
        }
        _db.Groups.Update(group);
        await _db.SaveChangesAsync();
        return group;
    }

    public async Task Delete(int id)
    {
        if (await _db.Students.AnyAsync(s => s.GroupId == id))
        {
            throw new ApplicationException("There are students in this group");
        }

        var group = await _db.Groups.FindAsync(id);
        if (group == null)
        {
            return;
        }
        _db.Groups.Remove(group);
        await _db.SaveChangesAsync();
    }
    
    public async Task ValidateName(string name, int? id = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ApplicationException("Name should be filled");
        }
        if (await _db.Groups.AnyAsync(g => g.Name == name && g.Id != id))
        {
            throw new ApplicationException("This name already exists");
        }
    }
}