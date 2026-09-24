namespace CareerSkillHub.Models
{
    /// Represents one row from the Questions table (a multiple choice question).
    public class Question
    {
        public int QuestionID { get; set; }
        public int QuizID { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; } // 'A', 'B', 'C' or 'D'
    }
}