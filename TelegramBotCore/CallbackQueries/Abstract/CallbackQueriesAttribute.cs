namespace TelegramBotCore.CallbackQueries.Abstract;

[AttributeUsage(AttributeTargets.Class)]
public class CallbackQueriesAttribute : Attribute
{
    public string FunctionCode { get; set; } = "no_func";
}
