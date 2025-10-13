namespace NotesApp.Domain.Enums;

/// <summary>
/// Статус публичности заметки
/// </summary>
public enum PrivacyStatus
{
    /// <summary>
    /// Черновик
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Приватная
    /// </summary>
    Private = 1,

    /// <summary>
    /// Только по ссылке
    /// </summary>
    LinkOnly = 2,

    /// <summary>
    /// Только для подписчиков
    /// </summary>
    FriendsOnly = 3,

    /// <summary>
    /// Публичная
    /// </summary>
    Public = 4
}