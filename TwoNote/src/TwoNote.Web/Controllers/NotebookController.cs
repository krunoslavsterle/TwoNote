using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwoNote.Core.Entities;
using TwoNote.Core.Interfaces;

namespace TwoNote.Web.Controllers
{
    public class NotebookController : Controller
    {
        private readonly IRepository<NotebookEntity> notebookRepository;
        private readonly IRepository<PageEntity> pageRepository;

        public NotebookController(IRepository<NotebookEntity> notebookRepository, IRepository<PageEntity> pageRepository)
        {
            this.notebookRepository = notebookRepository;
            this.pageRepository = pageRepository;
        }

        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            var notebooks = await notebookRepository.ListAsync();
            return View("Index", notebooks);
        }

        [HttpGet]
        [ActionName("Pages")]
        public async Task<IActionResult> PagesAsync(Guid notebookId)
        {
            var pages = await pageRepository.ListAsync();
            return View("Pages", pages);
        }
    }
}
