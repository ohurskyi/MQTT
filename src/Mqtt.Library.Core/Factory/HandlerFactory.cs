namespace Mqtt.Library.Core.Factory;

public delegate object HandlerFactory(Type handlerType);

public static class HandlerFactoryExtensions
{
    public static T GetInstance<T>(this HandlerFactory handlerFactory, Type type) => (T)handlerFactory(type);
    public static IEnumerable<T> GetInstances<T>(this HandlerFactory handlerFactory) => (IEnumerable<T>)handlerFactory(typeof(IEnumerable<T>));

}