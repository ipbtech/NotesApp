namespace NotesApp.Domain.Entities.Base;

/// <summary>
/// Базовая сущность файла
/// </summary>
public abstract class BaseFile : BaseEntity
{
#nullable disable

    /// <summary>
    /// Имя файла
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Оригинальное имя файла
    /// </summary>
    public string OriginalFileName { get; set; }

    /// <summary>
    /// Размер файла
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// Имя бакета
    /// </summary>
    public string BucketName { get; set; }

    /// <summary>
    /// Путь в S3 хранилище
    /// </summary>
    public string ObjectName { get; set; }

#nullable enable

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{BucketName}/{ObjectName}";
    }
}