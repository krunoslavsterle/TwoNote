using System;
using System.Collections.Generic;

namespace TwoNote.Web.Models
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Notebooks = new List<NotebookViewModel>();
        }

        public Guid? SelectedNotebookId { get; set; }
        public IEnumerable<NotebookViewModel> Notebooks { get; set; }
    }
}
