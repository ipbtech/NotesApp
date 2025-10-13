using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Тэг заметки
/// </summary>
public class Tag : BaseEntity
{
#nullable disable

    /// <summary>
    /// Имя тэга
    /// </summary>
    public string Name { get; set; }

#nullable enable

    /// <summary>
    /// Счетчик использования
    /// </summary>
    public int UsageCount { get; set; }

    /// <summary>
    /// Заметки, где используется этот тэг
    /// </summary>
    public ICollection<Note> Notes { get; set; } = [];
}