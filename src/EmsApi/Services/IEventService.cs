using EventManagementService.EmsApi.Models;

public interface IEventService
{
    Task<IEnumerable<Event>> GetEventsAsync();
    Task<Event?> GetEventById(int id);
    Task<Event?> CreateEvent(Event evt);
    Task<Event?> UpdateEvent(int id, Event evt);
    Task<bool> DeleteEvent(int id);
}