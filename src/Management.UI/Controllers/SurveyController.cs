using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.Core.Domain;
using Management.UI.Data;
using Management.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Management.UI.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurveyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await SurveyModelFactory();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SurveyCreateModel model)
        {
            foreach (var questionModel in model.QuestionModelList)
            {
                var survey = new Survey
                {
                    ArticleId = model.ArticleId,
                    Question = questionModel.Question,
                    CorrectAnswer = (int)questionModel.CorrectAnswer,
                    AnswerList = new List<Answer>()
                };

                foreach (var answerModel in questionModel.AnswerModelList)
                {
                    var answer = new Answer
                    {
                        Question = answerModel.Answer,
                        DisplaySign = (int)answerModel.DisplaySign
                    };
                    survey.AnswerList.Add(answer);
                }

                _context.Survey.Add(survey);
                _context.SaveChanges();
            }

            return View(model);
        }

        public async Task<SurveyCreateModel> SurveyModelFactory()
        {
            SurveyCreateModel model = new SurveyCreateModel();

            var articleList = await _context.Article.AsQueryable()
                .ToListAsync();

            model.ArticleModelList = articleList.Select(s => new ArticleModel
            { Id = s.Id, Description = s.Description, Title = s.Title }).ToList();

            return model;
        }

        public IActionResult GetSurveyList()
        {
            var result = _context.Survey.Include(a => a.Article).ToList();

            return View(result);

        }
    }
}