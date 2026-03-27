using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IEventService eventService, ILogger<EventsController> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventResponseDto>>> GetEvents()
    {
        try
        {
            var events = await _eventService.GetEventsAsync();

            if (events == null || !events.Any())
                return NotFound("No events found");
            return Ok(events.ToEventResponses());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting events");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventResponseDto>> GetEventById(int id)
    {
        try
        {
            var evt = await _eventService.GetEventById(id);
            if (evt == null)
                return NotFound($"Event with ID {id} not found");
            return Ok(evt.ToEventResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting event with ID {id}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<EventResponseDto>> CreateEvent([FromBody] EventRequestDto request)
    {
        try
        {
            if (request == null)
                return BadRequest("Event data is required");

            var evt = await _eventService.CreateEvent(request.ToEvent());
            if (evt == null)
                return BadRequest("Failed to create event");
            return CreatedAtAction(nameof(GetEventById), new { id = evt.Id }, evt.ToEventResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating event");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EventResponseDto>> UpdateEvent(int id, [FromBody] EventRequestDto request)
    {
        try
        {
            if (request == null)
                return BadRequest("Event data is required");

            var evt = await _eventService.UpdateEvent(id, request.ToEvent());
            if (evt == null)
                return NotFound($"Event with ID {id} not found");
            return Ok(evt.ToEventResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating event with ID {id}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(int id)
    {
        try
        {
            var result = await _eventService.DeleteEvent(id);
            if (!result)
                return NotFound($"Event with ID {id} not found");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting event with ID {id}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}