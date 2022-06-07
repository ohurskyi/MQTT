namespace MessagingLibrary.Core.Results
{
    public class ExecutionResult : IExecutionResult
    {
        protected ExecutionResult() { }

        private ExecutionResult(string failureReason)
        {
            FailureReason = failureReason;
        }

        public string FailureReason { get; } = string.Empty;
        
        public bool Success  => string.IsNullOrEmpty(FailureReason);

        public static ExecutionResult Ok() => new ExecutionResult();

        public static ExecutionResult Fail(string reason) => new ExecutionResult(reason);
    }
}