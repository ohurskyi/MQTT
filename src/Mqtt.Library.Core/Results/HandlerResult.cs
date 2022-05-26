namespace Mqtt.Library.Core.Results;

public class HandlerResult
{
    public List<string> Errors { get; } = new List<string>();

    public bool Success => Errors.Count == 0;
    public void AddError(string error)
    {
        Errors.Add(error);
    }
}