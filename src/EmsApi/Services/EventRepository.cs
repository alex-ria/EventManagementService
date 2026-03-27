using EventManagementService.EmsApi.Models;

namespace EventManagementService.EmsApi.Services;

public class EventRepository : IEventRepository
{
    private readonly List<Event> _events = new();
    // По идее потокобезопасность тут не нужна, т.к. потом будет использоваться база данных, 
    // но раз рассказали про Lock, то пусть будет
    private readonly Lock _lock = new();
    private int _nextId = 0;

    public EventRepository()
    {
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        var now = DateTime.Now;
        var sampleEvents = new[]
        {
            new Event
            {
                Id = ++_nextId,
                Title = "Конференция №1",
                Description = "Какое-то описание конференции",
                StartAt = now.AddDays(7),
                EndAt = now.AddDays(7).AddHours(8)
            },
            new Event
            {
                Id = ++_nextId,
                Title = "Вебинар №1",
                Description = "Какое-то описание вебинара",
                StartAt = now.AddDays(1),
                EndAt = now.AddDays(1).AddHours(2)
            },
            new Event
            {
                Id = ++_nextId,
                Title = "Вебинар №2",
                Description = "Еще одно описание вебинара",
                StartAt = now.AddDays(3),
                EndAt = now.AddDays(3).AddHours(3)
            },
            new Event
            {
                Id = ++_nextId,
                Title = "Конференция №2",
                Description = "Еще какая-то конференция",
                StartAt = now.AddDays(5),
                EndAt = now.AddDays(5).AddHours(8)
            },
            new Event
            {
                Id = ++_nextId,
                Title = "Просто встреча",
                Description = "Просто какая-то встреча не пойми с кем",
                StartAt = now.AddDays(14),
                EndAt = now.AddDays(14).AddHours(2)
            }
        };

        _events.AddRange(sampleEvents);
    }

    public Task<IEnumerable<Event>> GetAllAsync()
    {
        using (_lock.EnterScope())
        {
            return Task.FromResult((IEnumerable<Event>)_events.ToList());
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
