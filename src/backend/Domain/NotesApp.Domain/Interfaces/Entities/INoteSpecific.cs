using NotesApp.Domain.Entities;

namespace NotesApp.Domain.Interfaces.Entities;

/// <summary>
/// Интерфейс указания принадлежности к заметке
/// </summary>
public interface INoteSpecific
{
    /// <summary>
    /// Идентификатор заметки
    /// </summary>
    public Guid NoteId { get; set; }

    /// <summary>
    /// Заметка
    /// </summary>
    public Note? Note { get; set; }
}