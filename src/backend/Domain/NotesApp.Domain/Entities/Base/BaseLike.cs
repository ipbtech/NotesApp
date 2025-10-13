using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities.Base;

/// <summary>
/// Базовая сущность лайка
/// </summary>
public abstract class BaseLike : BaseEntity, IUserSpecific
{
    /// <inheritdoc />
    public Guid UserId { get; set; }

    /// <inheritdoc />
    public User? User { get; set; }
}