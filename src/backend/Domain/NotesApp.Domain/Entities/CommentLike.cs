using NotesApp.Domain.Entities.Base;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Лайк коммента
/// </summary>
public class CommentLike : BaseLike, ICommentSpecific
{
    /// <inheritdoc />
    public Guid CommentId { get; set; }

    /// <inheritdoc />
    public Comment? Comment { get; set; }
}