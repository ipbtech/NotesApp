using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities.Base;

/// <summary>
/// Подписка на пользователя или тэг
/// </summary>
public abstract class BaseSubscription : BaseEntity, IUserSpecific
{
    /// <inheritdoc />
    public Guid UserId { get; set; }

    /// <inheritdoc />
    public User? User { get; set; }
}