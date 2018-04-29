using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwoNote.Core.Entities;
using TwoNote.Core.Interfaces;
using TwoNote.Web.Models;
using System.Linq;

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
            var notebooks = await notebookRepository.ListAsync(nameof(NotebookEntity.Pages));
            var vm = CreateIndexViewModel(notebooks);

            return View("Index", vm);
        }

        [HttpGet]
        [ActionName("Notebook")]
        public async Task<IActionResult> NotebookPartialAsync(Guid notebookId)
        {
            if (notebookId == Guid.Empty) throw new FormatException("Invalid format exception for [notebookId]");

            var notebooks = await notebookRepository.ListAsync();
            var vm = CreateIndexViewModel(notebooks, notebookId);

            return PartialView("_NotebookSection", vm);
        }

        [HttpPost]
        [ActionName("NewNotebook")]
        public async Task<JsonResult> NewNotebookAsync(string notebookName)
        {
            if (String.IsNullOrEmpty(notebookName)) throw new ArgumentNullException("[notebookName] is null or empty.");

            var entity = new NotebookEntity()
            {
                Id = Guid.NewGuid(),
                Name = notebookName,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            var result = await notebookRepository.AddAsync(entity);
            return Json(new { success = true, responseText = result.Id.ToString() });
        }

        [HttpPost]
        [ActionName("DeleteNotebook")]
        public async Task<IActionResult> DeleteNotebookAsync(Guid notebookId)
        {
            if (notebookId == Guid.Empty) throw new FormatException("Invalid format exception for [notebookId]");

            var entity = await notebookRepository.GetByIdAsync(notebookId);
            await notebookRepository.DeleteAsync(entity);

            return Ok();
        }

        [HttpGet]
        [ActionName("Pages")]
        public async Task<IActionResult> PagesPartialAsync(Guid notebookId, Guid? pageId)
        {
            if (notebookId == Guid.Empty) throw new FormatException("Invalid format exception for [notebookId]");

            var notebook = await notebookRepository.GetByIdAsync(notebookId, nameof(NotebookEntity.Pages));
            var vm = MapNotebookEntityToNotebookViewModel(notebook, pageId);

            return PartialView("_PageSection", vm);
        }

        [HttpPost]
        [ActionName("NewPage")]
        public async Task<JsonResult> NewPageAsync(Guid notebookId, string pageName)
        {
            if (Guid.Empty == notebookId) throw new ArgumentNullException("[notebookId] is empty guid.");
            if (String.IsNullOrEmpty(pageName)) throw new ArgumentNullException("[pageName] is null or empty.");

            var entity = new PageEntity()
            {
                Id = Guid.NewGuid(),
                NotebookId = notebookId,
                Name = pageName,
                Content = String.Empty,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            var result = await pageRepository.AddAsync(entity);
            return Json(new { success = true, responseText = result.Id.ToString() });
        }

        [HttpPost]
        [ActionName("DeletePage")]
        public async Task<IActionResult> DeletePageAsync(Guid pageId)
        {
            if (pageId == Guid.Empty) throw new FormatException("Invalid format exception for [pageId]");

            var entity = await pageRepository.GetByIdAsync(pageId);
            await pageRepository.DeleteAsync(entity);

            return Ok();
        }

        [HttpGet]
        [ActionName("GetPageContent")]
        public async Task<IActionResult> GetPageContentAsync(Guid pageId)
        {
            if (pageId == Guid.Empty) throw new FormatException("Invalid format exception for [pageId]");
            var entity = await pageRepository.GetByIdAsync(pageId);
            return Ok(entity.Content);
        }

        [HttpPost]
        [ActionName("UpdatePage")]
        public async Task<IActionResult> UpdatePageAsync(Guid pageId, string content)
        {
            if (pageId == Guid.Empty) throw new FormatException("Invalid format exception for [pageId]");
            if (content == null) throw new ArgumentNullException("[content] is null.");

            var entity = await pageRepository.GetByIdAsync(pageId);
            entity.Content = content;
            entity.DateUpdated = DateTime.UtcNow;
            await pageRepository.UpdateAsync(entity);

            return Json(new { success = true, responseText = "Updated" });
        }

        private IndexViewModel CreateIndexViewModel(IEnumerable<NotebookEntity> notebookEntities, Guid? notebookId = null)
        {
            var notebooks = new List<NotebookViewModel>();
            if (notebookEntities != null && notebookEntities.Count() > 0)
            {
                foreach (var notebookEntity in notebookEntities)
                    notebooks.Add(MapNotebookEntityToNotebookViewModel(notebookEntity));
            }

            var vm = new IndexViewModel
            {
                Notebooks = notebooks,
                SelectedNotebookId = notebookId ?? notebooks.FirstOrDefault()?.Id
            };

            return vm;
        }

        private NotebookViewModel MapNotebookEntityToNotebookViewModel(NotebookEntity entity, Guid? selectedPage = null)
        {
            var pages = new List<PageViewModel>();
            if (entity.Pages != null && entity.Pages.Count() > 0)
            {
                foreach (var pageEntity in entity.Pages)
                    pages.Add(MapPageEntityToPageViewModel(pageEntity));
            }

            var vm = new NotebookViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Pages = pages,
                SelectedPage = selectedPage ?? pages.FirstOrDefault()?.Id
            };

            return vm;
        }

        private PageViewModel MapPageEntityToPageViewModel(PageEntity entity)
        {
            var vm = new PageViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Content = entity.Content
            };
            return vm;
        }
    }
}
