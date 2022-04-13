namespace Mqtt.Library.Processing.Factory;

public delegate object HandlerFactory(Type handlerType);

public static class HandlerFactoryExtensions
{
    public static T GetHandler<T>(this HandlerFactory handlerFactory) => (T)handlerFactory(typeof(T));
}