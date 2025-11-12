namespace SharedKernel;

public interface IEventBus
{
    //Is an abstraction for any event bus implementation (RabbitMQ, in-memory, etc)
    public interface IEventBus
    {

        //Send and event to other modules
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IntegrationEvent;


        //Register a handler that reacts when an event is received
        void Subscribe<TEvent, THandler>() where TEvent : IntegrationEvent where THandler : IIntegrationEventHandler<TEvent>;
    }

    public interface IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
    {
        Task HandleAsync(TEvent @event);

    }
}