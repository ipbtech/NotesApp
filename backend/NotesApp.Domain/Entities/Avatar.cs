﻿using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities
{
    public class Avatar : BaseFile
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
