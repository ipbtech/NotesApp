using NotesApp.Domain.Entities.Base;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Изображение-вложение к заметке
/// </summary>
public class NoteImage : BaseFile, INoteSpecific
{
    /// <summary>
    /// Порядок для сортировки
    /// </summary>
    public int Order { get; set; }

    /// <inheritdoc />
    public Guid NoteId { get; set; }

    /// <inheritdoc />
    public Note? Note { get; set; }
}