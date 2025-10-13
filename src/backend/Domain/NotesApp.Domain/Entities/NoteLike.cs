using NotesApp.Domain.Entities.Base;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Лайк заметки
/// </summary>
public class NoteLike : BaseLike, INoteSpecific
{
    /// <inheritdoc />
    public Guid NoteId { get; set; }

    /// <inheritdoc />
    public Note? Note { get; set; }
}