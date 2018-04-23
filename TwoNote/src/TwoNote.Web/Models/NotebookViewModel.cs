using System;
using System.Collections.Generic;

namespace TwoNote.Web.Models
{
    public class NotebookViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? SelectedPage { get; set; }
        public IEnumerable<PageViewModel> Pages { get; set; }
    }
}
