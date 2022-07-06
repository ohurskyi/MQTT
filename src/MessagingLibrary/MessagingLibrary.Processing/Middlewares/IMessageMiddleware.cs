using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;

namespace MessagingLibrary.Processing.Middlewares;

public delegate Task<HandlerResult> MessageHandlerDelegate();

public interface IMessageMiddleware
{
    Task<HandlerResult> Handle<TMessagingClientOptions>(IMessage message, MessageHandlerDelegate next) where TMessagingClientOptions : IMessagingClientOptions;
}