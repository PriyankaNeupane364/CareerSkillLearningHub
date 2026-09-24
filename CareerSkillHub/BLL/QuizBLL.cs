using System.Collections.Generic;
using CareerSkillHub.Data_Access_Layer;
using CareerSkillHub.Models;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Business logic for quizzes and their questions.
    /// </summary>
    public class QuizBLL
    {
        private readonly QuizDAL _quizDal = new QuizDAL();
        private readonly QuestionDAL _questionDal = new QuestionDAL();

        public Quiz GetById(int quizId) { return _quizDal.SelectById(quizId); }
        public List<Quiz> GetByCourse(int courseId) { return _quizDal.SelectByCourse(courseId); }
        public List<Quiz> GetAll() { return _quizDal.SelectAll(); }
        public List<Quiz> GetByInstructor(int instructorId) { return _quizDal.SelectByInstructor(instructorId); }
        public int CountAll() { return _quizDal.CountAll(); }

        public int AddQuiz(int courseId, string title, string description)
        {
            Validate(courseId, title);

            Quiz quiz = new Quiz
            {
                CourseID = courseId,
                Title = title.Trim(),
                Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim()
            };
            return _quizDal.Insert(quiz);
        }

        public void UpdateQuiz(int quizId, int courseId, string title, string description)
        {
            Validate(courseId, title);

            Quiz quiz = new Quiz
            {
                QuizID = quizId,
                CourseID = courseId,
                Title = title.Trim(),
                Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim()
            };
            _quizDal.Update(quiz);
        }

        public void DeleteQuiz(int quizId)
        {
            _quizDal.Delete(quizId);
        }

        // ============================ QUESTIONS ============================

        public Question GetQuestionById(int questionId) { return _questionDal.SelectById(questionId); }
        public List<Question> GetQuestions(int quizId) { return _questionDal.SelectByQuiz(quizId); }
        public int CountQuestions(int quizId) { return _questionDal.CountByQuiz(quizId); }

        public int AddQuestion(int quizId, string questionText,
            string optionA, string optionB, string optionC, string optionD, string correctAnswer)
        {
            ValidateQuestion(quizId, questionText, optionA, optionB, optionC, optionD, correctAnswer);

            Question q = new Question
            {
                QuizID = quizId,
                QuestionText = questionText.Trim(),
                OptionA = optionA.Trim(),
                OptionB = optionB.Trim(),
                OptionC = optionC.Trim(),
                OptionD = optionD.Trim(),
                CorrectAnswer = correctAnswer.ToUpper()
            };
            return _questionDal.Insert(q);
        }

        public void UpdateQuestion(int questionId, int quizId, string questionText,
            string optionA, string optionB, string optionC, string optionD, string correctAnswer)
        {
            ValidateQuestion(quizId, questionText, optionA, optionB, optionC, optionD, correctAnswer);

            Question q = new Question
            {
                QuestionID = questionId,
                QuizID = quizId,
                QuestionText = questionText.Trim(),
                OptionA = optionA.Trim(),
                OptionB = optionB.Trim(),
                OptionC = optionC.Trim(),
                OptionD = optionD.Trim(),
                CorrectAnswer = correctAnswer.ToUpper()
            };
            _questionDal.Update(q);
        }

        public void DeleteQuestion(int questionId)
        {
            _questionDal.Delete(questionId);
        }

        private void Validate(int courseId, string title)
        {
            if (courseId <= 0) throw new Helpers.ValidationException("Select a course.");
            if (string.IsNullOrWhiteSpace(title)) throw new Helpers.ValidationException("Quiz title is required.");
        }

        private void ValidateQuestion(int quizId, string text,
            string a, string b, string c, string d, string answer)
        {
            if (quizId <= 0) throw new Helpers.ValidationException("Select a quiz.");
            if (string.IsNullOrWhiteSpace(text)) throw new Helpers.ValidationException("Question text is required.");
            if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b) ||
                string.IsNullOrWhiteSpace(c) || string.IsNullOrWhiteSpace(d))
                throw new Helpers.ValidationException("All four options are required.");

            string norm = (answer ?? "").Trim().ToUpper();
            if (norm != "A" && norm != "B" && norm != "C" && norm != "D")
                throw new Helpers.ValidationException("Select the correct answer (A, B, C or D).");
        }
    }
}