using NotesApp.Domain.Entities.Base;
using NotesApp.Domain.Enums;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
public class User : BaseEntity
{
#nullable disable
    
    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Отображаемое имя пользователя
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Bio пользователя
    /// </summary>
    public string Bio { get; set; }

#nullable enable

    /// <summary>
    /// Флаг активности пользователя
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Роль пользователя
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Аватар пользователя
    /// </summary>
    public UserAvatar? Avatar { get; set; }

    /// <summary>
    /// Заметки пользователя
    /// </summary>
    public ICollection<Note> Notes { get; set; } = [];

    /// <summary>
    /// Подписки на пользователей и тэги
    /// </summary>
    public ICollection<BaseSubscription> Subscriptions { get; set; } = [];

    /// <summary>
    /// Комментарии пользователя
    /// </summary>
    public ICollection<Comment> Comments { get; set; } = [];

    /// <summary>
    /// Лайки пользователя на заметки и комментарии
    /// </summary>
    public ICollection<BaseLike> Likes { get; set; } = [];
}