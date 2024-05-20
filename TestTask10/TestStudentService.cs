using Task10.Data;
using Task10.Services;

namespace TestTask10;

[TestClass]
public class TestStudentService
{
    public InMemoryAppContext db;
    private StudentService _studentService;

    [TestInitialize]
    public void TestInitialize()
    {
        db = new InMemoryAppContext();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        _studentService = new StudentService(db);
    }
    
    [TestMethod]
    public async Task TestList()
    {
        var items = await _studentService.List(1);
        var expectedItems = ModelBuilderExtensions.DefaultStudents.Where(s => s.GroupId == 1);
        Assert.AreEqual(expectedItems.Count(), items.Count);
    }

    [TestMethod]
    public async Task TestRetrieve()
    {
        var item = await _studentService.Retrieve(1);
        Assert.IsNotNull(item);
        
        var item2 = await _studentService.Retrieve(100);
        Assert.IsNull(item2);
    }


    [TestMethod]
    public async Task TestCreate()
    {
        var course = await db.Courses.FindAsync(1);
        Assert.IsNotNull(course);
        var item = await _studentService.Create("Wick", "John", 1);
        Assert.IsNotNull(item.Id);
        
        var itemInDb = await db.Students.FindAsync(item.Id);;
        Assert.IsNotNull(itemInDb);
    }

    [TestMethod]
    public async Task TestUpdate()
    {
        var item = await db.Students.FindAsync(1);
        Assert.IsNotNull(item);

        var changedFirstName = "my new changed name";
        var changedLastName = "Bbebeb";
        item.FirstName = changedFirstName;
        item.LastName = changedLastName;

        await _studentService.Update(item);
        
        var item2 = await db.Students.FindAsync(1);
        Assert.IsNotNull(item2);
        Assert.AreEqual(changedFirstName, item2.FirstName);
        Assert.AreEqual(changedLastName, item2.LastName);
    }

    [TestMethod]
    public async Task TestDelete()
    {
        var item = await db.Students.FindAsync(1);
        Assert.IsNotNull(item);
        await _studentService.Delete(item.Id);
        
        var item2 = await db.Students.FindAsync(1);
        Assert.IsNull(item2);
    }
    
}