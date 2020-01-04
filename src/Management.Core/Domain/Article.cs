using System.Collections.Generic;

namespace Management.Core.Domain
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Survey> SurveyList { get; set; }
    }
}
