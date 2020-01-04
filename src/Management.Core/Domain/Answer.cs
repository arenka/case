namespace Management.Core.Domain
{
    public class Answer
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string Question { get; set; }
        public int DisplaySign { get; set; }

        public Survey Survey { get; set; }

    }
}