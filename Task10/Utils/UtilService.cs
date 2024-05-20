namespace Task10.Utils;

public class UtilService
{
    public static bool IsParamsFilled(params int?[] nums)
    {
        if (nums.Length == 0)
        {
            return false;
        }
        return nums.All(p => p.HasValue);
    }
}