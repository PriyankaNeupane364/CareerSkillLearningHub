using System.Collections.Generic;
using CareerSkillHub.Data_Access_Layer;
using CareerSkillHub.Models;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Business logic for courses.
    /// </summary>
    public class CourseBLL
    {
        private readonly CourseDAL _courseDal = new CourseDAL();

        public Course GetById(int courseId) { return _courseDal.SelectById(courseId); }
        public List<Course> GetAll() { return _courseDal.SelectAll(); }
        public List<Course> GetByInstructor(int instructorId) { return _courseDal.SelectByInstructor(instructorId); }
        public int CountAll() { return _courseDal.CountAll(); }

        public int AddCourse(string title, string description, string category, int instructorId, string imageUrl)
        {
            Validate(title, description, category, instructorId);

            Course c = new Course
            {
                Title = title.Trim(),
                Description = description.Trim(),
                Category = category.Trim(),
                InstructorID = instructorId,
                ImageURL = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl.Trim()
            };
            return _courseDal.Insert(c);
        }

        public void UpdateCourse(int courseId, string title, string description, string category, int instructorId, string imageUrl)
        {
            Validate(title, description, category, instructorId);

            Course c = new Course
            {
                CourseID = courseId,
                Title = title.Trim(),
                Description = description.Trim(),
                Category = category.Trim(),
                InstructorID = instructorId,
                ImageURL = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl.Trim()
            };
            _courseDal.Update(c);
        }

        public void DeleteCourse(int courseId)
        {
            _courseDal.Delete(courseId);
        }

        private void Validate(string title, string description, string category, int instructorId)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new Helpers.ValidationException("Course title is required.");
            if (string.IsNullOrWhiteSpace(description)) throw new Helpers.ValidationException("Course description is required.");
            if (string.IsNullOrWhiteSpace(category)) throw new Helpers.ValidationException("Course category is required.");
            if (instructorId <= 0) throw new Helpers.ValidationException("Select an instructor.");
        }
    }
}