using System;

namespace TwoNote.Core.Entities
{
    public class PageEntity : BaseEntity
    {
        public Guid NotebookId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public NotebookEntity Notebook { get; set; }
    }
}
