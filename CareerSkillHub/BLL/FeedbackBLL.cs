using System.Collections.Generic;
using CareerSkillHub.Data_Access_Layer;
using CareerSkillHub.Models;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Business logic for student feedback.
    /// </summary>
    public class FeedbackBLL
    {
        private readonly FeedbackDAL _feedbackDal = new FeedbackDAL();

        public List<Feedback> GetAll() { return _feedbackDal.SelectAll(); }
        public int CountAll() { return _feedbackDal.CountAll(); }

        public void AddFeedback(int userId, int rating, string message)
        {
            if (rating < 1 || rating > 5)
                throw new Helpers.ValidationException("Rating must be between 1 and 5.");
            if (string.IsNullOrWhiteSpace(message))
                throw new Helpers.ValidationException("Please write a short message.");

            Feedback f = new Feedback
            {
                UserID = userId,
                Rating = rating,
                Message = message.Trim()
            };
            _feedbackDal.Insert(f);
        }

        public void DeleteFeedback(int feedbackId)
        {
            _feedbackDal.Delete(feedbackId);
        }
    }
}