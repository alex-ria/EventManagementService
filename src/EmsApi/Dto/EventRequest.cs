using System.ComponentModel.DataAnnotations;

public record EventRequestDto : IValidatableObject
{
    [Required]
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }

    [Required]
    public DateTime StartAt { get; init; }

    [Required]
    public DateTime EndAt { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndAt <= StartAt)
        {
            yield return new ValidationResult(
                "End date must be after start date",
                new[] { nameof(EndAt) }
            );
        }
    }
}