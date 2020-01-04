using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.Core;
using Management.Core.Domain;
using Management.UI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Management.UI.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        readonly ApplicationDbContext _context;
        private readonly Scrapper _scrapper;

        public ArticleController(ApplicationDbContext context, Scrapper scrapper)
        {
            _context = context;
            _scrapper = scrapper;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.Article
                .ToListAsync();
            return View(model);
        }

        public void ImportData()
        {
           var data = _scrapper.GetTitles();

           foreach (Dictionary<string, string> dict in data)
           {
               Article article = new Article
               {
                   Title = dict["title"],
                   Description = dict["desc"]
               };
               _context.Article.Add(article);
               _context.SaveChanges();
           }
        }
    }
}