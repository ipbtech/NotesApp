namespace NotesApp.Domain.Interfaces.Entities;

/// <summary>
/// Интерфейс для аудита времени создания и обновления сущности
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Дата создания сущности
    /// </summary>
    public DateTimeOffset CreatedAtUtc { get; set; }

    /// <summary>
    /// Дата обновления сущности
    /// </summary>
    public DateTimeOffset UpdatedAtUtc { get; set; }
}