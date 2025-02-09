﻿using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities
{
    public class User : IEntityId, IAuditable
    {
#nullable disable
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
#nullable enable

        public Avatar? Avatar { get; set; }
        public ICollection<Note> PersonalNotes { get; set; } = [];
        public ICollection<Note> AllowedNotes { get; set; } = [];
        public ICollection<Tag> Tags { get; set; } = [];

        //IModelId impl
        public Guid Id { get; set; }

        //IAuditable impl
        public DateTime CreatedAtUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
