using System.Collections.Generic;
using CareerSkillHub.Data_Access_Layer;
using CareerSkillHub.Models;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Business logic for learning progress tracking.
    /// </summary>
    public class ProgressBLL
    {
        private readonly ProgressDAL _progressDal = new ProgressDAL();

        public List<Progress> GetByUser(int userId) { return _progressDal.SelectByUser(userId); }
        public int AverageForUser(int userId) { return _progressDal.AverageForUser(userId); }

        /// <summary>
        /// Upserts progress: inserts a new row if none exists for this
        /// student + course, otherwise updates the existing row.
        /// </summary>
        public void SetProgress(int userId, int courseId, int percentage)
        {
            if (percentage < 0) percentage = 0;
            if (percentage > 100) percentage = 100;

            if (_progressDal.Exists(userId, courseId))
                _progressDal.Update(userId, courseId, percentage);
            else
                _progressDal.Insert(userId, courseId, percentage);
        }
    }
}