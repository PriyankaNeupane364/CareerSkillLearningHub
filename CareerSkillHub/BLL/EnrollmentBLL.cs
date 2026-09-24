using System.Collections.Generic;
using CareerSkillHub.Data_Access_Layer;
using CareerSkillHub.Models;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Business logic for course enrollments.
    /// </summary>
    public class EnrollmentBLL
    {
        private readonly EnrollmentDAL _enrollmentDal = new EnrollmentDAL();

        public bool IsEnrolled(int userId, int courseId) { return _enrollmentDal.IsEnrolled(userId, courseId); }
        public List<Enrollment> GetByUser(int userId) { return _enrollmentDal.SelectByUser(userId); }
        public int CountByUser(int userId) { return _enrollmentDal.CountByUser(userId); }
        public int CountByCourse(int courseId) { return _enrollmentDal.CountByCourse(courseId); }
        public int CountAll() { return _enrollmentDal.CountAll(); }

        /// <summary>
        /// Enrolls a student in a course. Throws a friendly error if the student
        /// is already enrolled (this prevents duplicate enrollments).
        /// </summary>
        public void Enroll(int userId, int courseId)
        {
            if (_enrollmentDal.IsEnrolled(userId, courseId))
                throw new Helpers.ValidationException("You are already enrolled in this course.");

            _enrollmentDal.Insert(userId, courseId);
        }

        public void Unenroll(int userId, int courseId)
        {
            _enrollmentDal.Delete(userId, courseId);
        }
    }
}