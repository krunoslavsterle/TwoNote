using System.Collections.Generic;

namespace TwoNote.Web.Models
{
    public class IndexViewModel
    {
        IEnumerable<NotebookViewModel> Notebooks { get; set; }
    }
}
