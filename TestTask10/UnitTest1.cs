using Task10.Utils;

namespace TestTask10;

[TestClass]
public class TestUtilService
{
    [TestMethod]
    public void TestIsParamsFilled()
    {
        int? a = 1;
        int b = 2;
        int? c = null;
        int? d = 3;
        
        Assert.IsTrue(UtilService.IsParamsFilled(a));
        Assert.IsTrue(UtilService.IsParamsFilled(a, b));
        Assert.IsTrue(UtilService.IsParamsFilled(a, b, d));
        
        Assert.IsFalse(UtilService.IsParamsFilled(a, b, c, d));
        Assert.IsFalse(UtilService.IsParamsFilled(c));
        Assert.IsFalse(UtilService.IsParamsFilled(b, c));
        Assert.IsFalse(UtilService.IsParamsFilled());
        
    }
}