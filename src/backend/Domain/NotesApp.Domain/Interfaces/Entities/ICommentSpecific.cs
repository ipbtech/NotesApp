using NotesApp.Domain.Entities;

namespace NotesApp.Domain.Interfaces.Entities;

/// <summary>
/// Интерфейс указания принадлежности к комментарию
/// </summary>
public interface ICommentSpecific
{
    /// <summary>
    /// Идентификатор комментария
    /// </summary>
    public Guid CommentId { get; set; }

    /// <summary>
    /// Комментарий
    /// </summary>
    public Comment? Comment { get; set; }
}