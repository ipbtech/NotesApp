using NotesApp.Domain.Entities;

namespace NotesApp.Domain.Interfaces.Entities;

/// <summary>
/// Интерфейс указания принадлежности к пользователю
/// </summary>
public interface IUserSpecific
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User? User { get; set; }
}