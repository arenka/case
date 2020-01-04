using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Management.UI.Models
{
    public class SurveyCreateModel
    {
        public SurveyCreateModel()
        {
            QuestionModelList = new List<QuestionModel>();

            for (int i = 0; i < 4; i++)
            {
                QuestionModelList.Add(new QuestionModel());
            }
        }

        public List<ArticleModel> ArticleModelList { get; set; }
        public int ArticleId { get; set; }
        public List<QuestionModel> QuestionModelList { get; set; }
    }

    public class ArticleModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public SelectList DisplaySings { get; set; }


    }

    public class QuestionModel
    {
        public QuestionModel()
        {
            AnswerModelList = new List<AnswerModel>
            {
                new AnswerModel {DisplaySign = DisplaySign.A},
                new AnswerModel {DisplaySign = DisplaySign.B},
                new AnswerModel {DisplaySign = DisplaySign.C},
                new AnswerModel {DisplaySign = DisplaySign.D}
            };
        }

        public string Question { get; set; }
        public List<AnswerModel> AnswerModelList { get; set; }
        public DisplaySign CorrectAnswer { get; set; }
    }

    public class AnswerModel
    {
        public string Answer { get; set; }
        public DisplaySign DisplaySign { get; set; }
    }

    public enum DisplaySign
    {
        A  = 1,
        B,
        C,
        D
    }
}
