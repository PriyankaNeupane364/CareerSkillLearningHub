using System.Collections.Generic;
using CareerSkillHub.Data_Access_Layer;
using CareerSkillHub.Models;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Business logic for editable website content (homepage / about / featured).
    /// </summary>
    public class WebsiteContentBLL
    {
        private readonly WebsiteContentDAL _contentDal = new WebsiteContentDAL();

        public List<WebsiteContent> GetAll() { return _contentDal.SelectAll(); }
        public List<WebsiteContent> GetByType(string contentType) { return _contentDal.SelectByType(contentType); }
        public WebsiteContent GetByTypeAndTitle(string contentType, string title) { return _contentDal.SelectByTypeAndTitle(contentType, title); }

        public int AddContent(string contentType, string title, string content)
        {
            Validate(contentType, title, content);

            WebsiteContent wc = new WebsiteContent
            {
                ContentType = contentType,
                Title = title.Trim(),
                Content = content.Trim()
            };
            return _contentDal.Insert(wc);
        }

        public void UpdateContent(int contentId, string contentType, string title, string content)
        {
            Validate(contentType, title, content);

            WebsiteContent wc = new WebsiteContent
            {
                ContentID = contentId,
                ContentType = contentType,
                Title = title.Trim(),
                Content = content.Trim()
            };
            _contentDal.Update(wc);
        }

        public void DeleteContent(int contentId)
        {
            _contentDal.Delete(contentId);
        }

        private void Validate(string contentType, string title, string content)
        {
            if (string.IsNullOrWhiteSpace(contentType))
                throw new Helpers.ValidationException("Content type is required.");
            if (string.IsNullOrWhiteSpace(title))
                throw new Helpers.ValidationException("Title is required.");
            if (string.IsNullOrWhiteSpace(content))
                throw new Helpers.ValidationException("Content is required.");
        }
    }
}