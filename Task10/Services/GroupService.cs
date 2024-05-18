using System.Text.RegularExpressions;
using Task10.Data;

namespace Task10.Services;

public class GroupService: ICrudService<Group>
{
    private readonly ApplicationContext _db;

    public GroupService(ApplicationContext db)
    {
        _db = db;
    }


    public Task<List<Group>> List()
    {
        throw new NotImplementedException();
    }

    public Task<Group> Create()
    {
        throw new NotImplementedException();
    }

    public Task<Group> Update()
    {
        throw new NotImplementedException();
    }

    public Task Delete()
    {
        throw new NotImplementedException();
    }
}