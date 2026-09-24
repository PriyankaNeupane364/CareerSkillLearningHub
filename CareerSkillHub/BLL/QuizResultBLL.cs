using System.Collections.Generic;
using CareerSkillHub.Data_Access_Layer;
using CareerSkillHub.Models;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Business logic for quiz results.
    /// </summary>
    public class QuizResultBLL
    {
        private readonly QuizResultDAL _resultDal = new QuizResultDAL();
        private readonly QuizDAL _quizDal = new QuizDAL();

        public QuizResult GetById(int resultId) { return _resultDal.SelectById(resultId); }
        public List<QuizResult> GetByUser(int userId) { return _resultDal.SelectByUser(userId); }
        public int CountByUser(int userId) { return _resultDal.CountByUser(userId); }
        public decimal HighestPercentageForUser(int userId) { return _resultDal.HighestPercentageForUser(userId); }

        /// <summary>
        /// Stores a completed quiz attempt and returns the new ResultID.
        /// </summary>
        public int SaveResult(int userId, int quizId, int score, int totalQuestions)
        {
            decimal percentage = totalQuestions > 0
                ? (decimal)score * 100 / (decimal)totalQuestions
                : 0;

            QuizResult result = new QuizResult
            {
                UserID = userId,
                QuizID = quizId,
                Score = score,
                TotalQuestions = totalQuestions,
                Percentage = decimal.Round(percentage, 2)
            };
            return _resultDal.Insert(result);
        }
    }
}