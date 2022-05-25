namespace Mqtt.Library.Core.Results
{
    public interface IExecutionResult
    { 
        bool Success { get; }
        string FailureReason { get; }
    }
}