using System;
using System.Collections.Generic;

namespace TwoNote.Core.Entities
{
    public class NotebookEntity : BaseEntity
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public IEnumerable<PageEntity> Pages { get; set; }
    }
}
