using Newtonsoft.Json;

namespace Core.Extensions;

public class ErrorDetails
{
    public string Message { get; set; } = null!;
    public int StatusCode { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
