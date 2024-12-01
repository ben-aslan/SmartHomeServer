using System.Reflection;

namespace Core.Utilities.Results;

public class ResultAssembly
{
    public Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
