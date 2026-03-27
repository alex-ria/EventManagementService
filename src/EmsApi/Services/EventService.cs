using EventManagementService.EmsApi.Models;

namespace EventManagementService.EmsApi.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _repository;

    public EventService(IEventRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Event>> GetEventsAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Event?> GetEventById(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<Event?> CreateEvent(Event evt)
    {
        if (evt == null)
            return Task.FromResult<Event?>(null);

        return _repository.AddAsync(evt);
    }

    public Task<Event?> UpdateEvent(int id, Event evt)
    {
        if (evt == null)
            return Task.FromResult<Event?>(null);

        return _repository.UpdateAsync(id, evt);
    }

    public Task<bool> DeleteEvent(int id)
    {
        return _repository.DeleteAsync(id);
    }
}

