using Task10.Data;
using Task10.Models;
using Task10.Services;

namespace TestTask10;

[TestClass]
public class TestCourseService
{
    public InMemoryAppContext db;
    private CourseService _courseService;

    [TestInitialize]
    public void TestInitialize()
    {
        db = new InMemoryAppContext();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        _courseService = new CourseService(db);
    }

    [TestMethod]
    public async Task TestList()
    {
        var items = await _courseService.List();
        Assert.AreEqual(ModelBuilderExtensions.DefaultCourses.Length, items.Count);
    }

    [TestMethod]
    public async Task TestRetrieve()
    {
        var item = await _courseService.Retrieve(1);
        Assert.IsNotNull(item);
        
        var item2 = await _courseService.Retrieve(100);
        Assert.IsNull(item2);
    }


    [TestMethod]
    public async Task TestCreate()
    {
        var item = await _courseService.Create("MyTest course", "blabalbalblablalba");
        Assert.IsNotNull(item.Id);
        
        var itemInDb = await db.Courses.FindAsync(item.Id);;
        Assert.IsNotNull(itemInDb);
    }

    [TestMethod]
    public async Task TestUpdate()
    {
        var item = await db.Courses.FindAsync(1);
        Assert.IsNotNull(item);

        var changedName = "my new changed name";
        item.Name = changedName;

        await _courseService.Update(item);
        
        var item2 = await db.Courses.FindAsync(1);
        Assert.IsNotNull(item2);
        Assert.AreEqual(changedName, item2.Name);
    }

    [TestMethod]
    public async Task TestDelete()
    {
        var item = await db.Courses.FindAsync(1);
        Assert.IsNotNull(item);
        await _courseService.Delete(item.Id);
        
        var item2 = await db.Courses.FindAsync(1);
        Assert.IsNull(item2);
    }

    [TestMethod]
    public async Task TestValidateName()
    {
        var item = await db.Courses.FindAsync(2);
        Assert.IsNotNull(item);

        await _courseService.ValidateName(item.Name, item.Id);

        await Assert.ThrowsExceptionAsync<ApplicationException>(async () => await _courseService.ValidateName(item.Name));
        await Assert.ThrowsExceptionAsync<ApplicationException>(async () => await _courseService.ValidateName(null));
        await Assert.ThrowsExceptionAsync<ApplicationException>(async () => await _courseService.ValidateName(""));

    }

}