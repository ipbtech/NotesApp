using NotesApp.Domain.Entities.Base;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities;

/// <summary>
/// Аватар пользователя
/// </summary>
public class UserAvatar : BaseFile, IUserSpecific
{
    /// <inheritdoc />
    public Guid UserId { get; set; }

    /// <inheritdoc />
    public User? User { get; set; }
}