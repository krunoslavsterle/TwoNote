﻿using System;
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
        [ActionName("Pages")]
        public async Task<IActionResult> PagesAsync(Guid notebookId)
        {
            if (notebookId == Guid.Empty) throw new FormatException("Invalid format exception for [notebookId]");

            var notebook = await notebookRepository.GetByIdAsync(notebookId, nameof(NotebookEntity.Pages));
            var vm = MapNotebookEntityToNotebookViewModel(notebook);

            return PartialView("_PageSection", vm);
        }

        private IndexViewModel CreateIndexViewModel(IEnumerable<NotebookEntity> notebookEntities)
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
                SelectedNotebookId = notebooks.FirstOrDefault()?.Id
            };

            return vm;
        }

        private NotebookViewModel MapNotebookEntityToNotebookViewModel(NotebookEntity entity)
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
                SelectedPage = pages.FirstOrDefault()?.Id
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
