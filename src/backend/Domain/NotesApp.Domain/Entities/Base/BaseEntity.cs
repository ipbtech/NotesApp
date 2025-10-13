using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities.Base;

/// <summary>
/// Базовая сущность
/// </summary>
public abstract class BaseEntity : IAuditable
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }

    /// <inheritdoc />
    public DateTimeOffset CreatedAtUtc { get; set; }

    /// <inheritdoc />
    public DateTimeOffset UpdatedAtUtc { get; set; }
}