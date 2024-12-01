using System.Reflection;

namespace Business;

public class BusinessAssembly
{
    public static Assembly GetAssembly => Assembly.GetExecutingAssembly();
}
