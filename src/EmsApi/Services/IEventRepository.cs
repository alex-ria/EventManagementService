using EventManagementService.EmsApi.Models;

namespace EventManagementService.EmsApi.Services;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
    Task<Event?> AddAsync(Event evt);
    Task<Event?> UpdateAsync(int id, Event evt);
    Task<bool> DeleteAsync(int id);
}
