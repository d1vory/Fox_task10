namespace Task10.Utils;

public class UtilService
{
    public static bool IsParamsFilled(params int?[] nums)
    {
        return nums.All(p => p.HasValue);
    }
}