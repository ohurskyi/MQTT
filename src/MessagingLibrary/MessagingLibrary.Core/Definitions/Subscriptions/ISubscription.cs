namespace MessagingLibrary.Core.Definitions.Subscriptions;

public interface ISubscription
{
    Type HandlerType { get; }
    string Topic { get;}
}