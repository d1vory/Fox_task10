using Task10.Data;
using Task10.Models;
using Task10.Services;

namespace TestTask10;

[TestClass]
public class TestGroupService
{
    public InMemoryAppContext db;
    private GroupService _groupService;

    [TestInitialize]
    public void TestInitialize()
    {
        db = new InMemoryAppContext();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        _groupService = new GroupService(db);
    }
    
    [TestMethod]
    public async Task TestList()
    {
        var items = await _groupService.List(1);
        var expectedItems = ModelBuilderExtensions.DefaultGroups.Where(g => g.CourseId == 1);
        Assert.AreEqual(expectedItems.Count(), items.Count);
    }

    [TestMethod]
    public async Task TestRetrieve()
    {
        var item = await _groupService.Retrieve(1);
        Assert.IsNotNull(item);
        
        var item2 = await _groupService.Retrieve(100);
        Assert.IsNull(item2);
    }


    [TestMethod]
    public async Task TestCreate()
    {
        var course = await db.Courses.FindAsync(1);
        Assert.IsNotNull(course);
        var item = await _groupService.Create("MyTest group", course);
        Assert.IsNotNull(item.Id);
        
        var itemInDb = await db.Groups.FindAsync(item.Id);;
        Assert.IsNotNull(itemInDb);
    }

    [TestMethod]
    public async Task TestUpdate()
    {
        var item = await db.Groups.FindAsync(1);
        Assert.IsNotNull(item);

        var changedName = "my new changed name";
        item.Name = changedName;

        await _groupService.Update(item);
        
        var item2 = await db.Groups.FindAsync(1);
        Assert.IsNotNull(item2);
        Assert.AreEqual(changedName, item2.Name);
    }

    [TestMethod]
    public async Task TestDelete()
    {
        var groupWithStudents = await db.Groups.FindAsync(1);
        Assert.IsNotNull(groupWithStudents);
        await Assert.ThrowsExceptionAsync<ApplicationException>(async () => await _groupService.Delete(groupWithStudents.Id));

        var newGroupWithoutStudents = new Group() { Name = "qwerty", CourseId = 1 };
        await db.Groups.AddAsync(newGroupWithoutStudents);
        await db.SaveChangesAsync();
        
        await _groupService.Delete(newGroupWithoutStudents.Id);
        
        var item2 = await db.Groups.FindAsync(newGroupWithoutStudents.Id);
        Assert.IsNull(item2);
    }

    [TestMethod]
    public async Task TestValidateName()
    {
        var item = await db.Groups.FindAsync(2);
        Assert.IsNotNull(item);

        await _groupService.ValidateName(item.Name, item.Id);

        await Assert.ThrowsExceptionAsync<ApplicationException>(async () => await _groupService.ValidateName(item.Name));
        await Assert.ThrowsExceptionAsync<ApplicationException>(async () => await _groupService.ValidateName(null));
        await Assert.ThrowsExceptionAsync<ApplicationException>(async () => await _groupService.ValidateName(""));

    }
    
    
    
    
}