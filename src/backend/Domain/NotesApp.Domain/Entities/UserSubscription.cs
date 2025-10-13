using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Подписка на пользователя
/// </summary>
public class UserSubscription : BaseSubscription
{
    /// <summary>
    /// Идентификатор пользователя, на которого подписались
    /// </summary>
    public Guid? TargetUserId { get; set; }

    /// <summary>
    /// Пользователь, на которого подписались
    /// </summary>
    public User? TargetUser { get; set; }
}