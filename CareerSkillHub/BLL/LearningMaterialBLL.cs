using System.Collections.Generic;
using CareerSkillHub.Data_Access_Layer;
using CareerSkillHub.Models;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Business logic for learning materials.
    /// </summary>
    public class LearningMaterialBLL
    {
        private readonly LearningMaterialDAL _materialDal = new LearningMaterialDAL();

        public LearningMaterial GetById(int materialId) { return _materialDal.SelectById(materialId); }
        public List<LearningMaterial> GetByCourse(int courseId) { return _materialDal.SelectByCourse(courseId); }
        public List<LearningMaterial> GetAll() { return _materialDal.SelectAll(); }
        public List<LearningMaterial> GetByInstructor(int instructorId) { return _materialDal.SelectByInstructor(instructorId); }
        public int CountAll() { return _materialDal.CountAll(); }

        public int AddMaterial(int courseId, string title, string description, string content, string videoUrl, string resourceUrl)
        {
            Validate(courseId, title);

            LearningMaterial m = new LearningMaterial
            {
                CourseID = courseId,
                Title = title.Trim(),
                Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
                Content = string.IsNullOrWhiteSpace(content) ? null : content.Trim(),
                VideoURL = string.IsNullOrWhiteSpace(videoUrl) ? null : videoUrl.Trim(),
                ResourceURL = string.IsNullOrWhiteSpace(resourceUrl) ? null : resourceUrl.Trim()
            };
            return _materialDal.Insert(m);
        }

        public void UpdateMaterial(int materialId, int courseId, string title, string description, string content, string videoUrl, string resourceUrl)
        {
            Validate(courseId, title);

            LearningMaterial m = new LearningMaterial
            {
                MaterialID = materialId,
                CourseID = courseId,
                Title = title.Trim(),
                Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
                Content = string.IsNullOrWhiteSpace(content) ? null : content.Trim(),
                VideoURL = string.IsNullOrWhiteSpace(videoUrl) ? null : videoUrl.Trim(),
                ResourceURL = string.IsNullOrWhiteSpace(resourceUrl) ? null : resourceUrl.Trim()
            };
            _materialDal.Update(m);
        }

        public void DeleteMaterial(int materialId)
        {
            _materialDal.Delete(materialId);
        }

        private void Validate(int courseId, string title)
        {
            if (courseId <= 0) throw new Helpers.ValidationException("Select a course.");
            if (string.IsNullOrWhiteSpace(title)) throw new Helpers.ValidationException("Material title is required.");
        }
    }
}