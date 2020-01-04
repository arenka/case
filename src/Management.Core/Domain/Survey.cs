using System;
using System.Collections.Generic;
using System.Text;

namespace Management.Core.Domain
{
    public class Survey
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int ArticleId { get; set; }
        public int CorrectAnswer { get; set; }

        public Article Article { get; set; }
        public List<Answer> AnswerList { get; set;}
    }
}
