using EventManagementService.EmsApi.Models;

namespace EventManagementService.EmsApi.Services;

public class EventRepository : IEventRepository
{
    private readonly List<Event> _events = new();
    private readonly Lock _lock = new();
    private int _nextId = 0;

    public Task<IEnumerable<Event>> GetAllAsync()
    {
        using (_lock.EnterScope())
        {
            var snapshot = _events.OrderBy(e => e.Id).ToList();
            return Task.FromResult((IEnumerable<Event>)snapshot);
        }
    }

    public Task<Event?> GetByIdAsync(int id)
    {
        using (_lock.EnterScope())
        {
            var evt = _events.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(evt);
        }
    }

    public Task<Event?> AddAsync(Event evt)
    {
        using (_lock.EnterScope())
        {
            var id = ++_nextId;
            var newEvent = new Event
            {
                Id = id,
                Title = evt.Title,
                Description = evt.Description,
                StartAt = evt.StartAt,
                EndAt = evt.EndAt
            };

            _events.Add(newEvent);
            return Task.FromResult<Event?>(newEvent);
        }
    }

    public Task<Event?> UpdateAsync(int id, Event evt)
    {
        using (_lock.EnterScope())
        {
            var existing = _events.FirstOrDefault(e => e.Id == id);
            if (existing == null)
                return Task.FromResult<Event?>(null);

            existing.Title = evt.Title;
            existing.Description = evt.Description;
            existing.StartAt = evt.StartAt;
            existing.EndAt = evt.EndAt;

            return Task.FromResult<Event?>(existing);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        using (_lock.EnterScope())
        {
            var idx = _events.FindIndex(e => e.Id == id);
            if (idx < 0)
                return Task.FromResult(false);

            _events.RemoveAt(idx);
            return Task.FromResult(true);
        }
    }
}
