using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Common.Dispatchers
{
    public class EventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TEvent>(TEvent @event)
        {
            var eventType = @event.GetType();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var handlers = _serviceProvider.GetServices(handlerType);

            if (handlers == null || !handlers.Any())
            {
                throw new InvalidOperationException($"No handlers registered for event type {eventType.Name}");
            }

            var method = handlerType.GetMethod("HandleAsync")
                ?? throw new InvalidOperationException($"No HandleAsync method found for handler type {handlerType.Name}");


            foreach (var handler in handlers)
            {
                if (method != null)
                {
                    await (Task)method.Invoke(handler, [@event]);
                }
            }
        }

    }
}
