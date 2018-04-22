using System.Collections.Generic;

namespace TwoNote.Web.Models
{
    public class NotebookViewModel
    {
        IEnumerable<PageViewModel> Pages { get; set; }
    }
}
