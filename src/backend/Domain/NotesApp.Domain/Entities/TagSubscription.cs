using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Подписка на тэг
/// </summary>
public class TagSubscription : BaseSubscription
{
    /// <summary>
    /// Идентификатор тэга
    /// </summary>
    public Guid? TagId { get; set; }

    /// <summary>
    /// Тэг, на который подписались
    /// </summary>
    public Tag? Tag { get; set; }
}