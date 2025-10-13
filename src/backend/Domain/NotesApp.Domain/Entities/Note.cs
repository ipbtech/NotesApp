using NotesApp.Domain.Entities.Base;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Заметка
/// </summary>
public class Note : BaseEntity, IUserSpecific
{
#nullable disable

    /// <summary>
    /// Заголовок заметки
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Содержание заметки
    /// </summary>
    public string Content { get; set; }

#nullable enable

    /// <summary>
    /// Статус публичности заметки
    /// </summary>
    public PrivacyStatus PrivacyStatus { get; set; }

    /// <summary>
    /// Количество лайков
    /// </summary>
    public int LikesCount { get; set; }

    /// <inheritdoc />
    public Guid UserId { get; set; }

    /// <inheritdoc />
    public User? User { get; set; }

    /// <summary>
    /// Используемые тэги
    /// </summary>
    public ICollection<Tag> Tags { get; set; } = [];

    /// <summary>
    /// Комментарии
    /// </summary>
    public ICollection<Comment> Comments { get; set; } = [];

    /// <summary>
    /// Лайки
    /// </summary>
    public ICollection<NoteLike> Likes { get; set; } = [];

    /// <summary>
    /// Изображения-вложения к заметке
    /// </summary>
    public ICollection<NoteImage> Images { get; set; } = [];
}