using System.Collections.Generic;

namespace TwoNote.Core.Entities
{
    public class NotebookEntity : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<PageEntity> Pages { get; set; }
    }
}
