using EventManagementService.EmsApi.Models;

public static class EventExtensions
{
    public static EventResponseDto ToEventResponse(this Event evt)
    {
        return new EventResponseDto(
            evt.Id,
            evt.Title,
            evt.Description,
            evt.StartAt,
            evt.EndAt
        );
    }

    public static IEnumerable<EventResponseDto> ToEventResponses(this IEnumerable<Event> events)
    {
        return events.Select(e => e.ToEventResponse());
    }

    public static Event ToEvent(this EventRequestDto request)
    {
        return new Event
        {
            Id = 0, // Id will be set by repository
            Title = request.Title,
            Description = request.Description,
            StartAt = request.StartAt,
            EndAt = request.EndAt,
        };
    }
}