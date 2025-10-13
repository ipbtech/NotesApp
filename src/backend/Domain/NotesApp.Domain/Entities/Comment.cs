using NotesApp.Domain.Entities.Base;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Комментарий
/// </summary>
public class Comment : BaseEntity, IUserSpecific, INoteSpecific
{
#nullable disable

    /// <summary>
    /// Содержимое комментария
    /// </summary>
    public string Content { get; set; }

#nullable enable

    /// <inheritdoc />
    public Guid UserId { get; set; }

    /// <inheritdoc />
    public User? User { get; set; }

    /// <inheritdoc />
    public Guid NoteId { get; set; }

    /// <inheritdoc />
    public Note? Note { get; set; }

    /// <summary>
    /// Идентификатор родительского коммента (при репосте)
    /// </summary>
    public Guid? ParentCommentId { get; set; }

    /// <summary>
    /// Родительский коммент (при репосте)
    /// </summary>
    public Comment? ParentComment { get; set; }

    /// <summary>
    /// Репосты
    /// </summary>
    public ICollection<Comment> Replies { get; set; } = [];

    /// <summary>
    /// Лайки
    /// </summary>
    public ICollection<CommentLike> Likes { get; set; } = [];
}