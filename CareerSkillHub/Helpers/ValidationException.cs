using System;

namespace CareerSkillHub.Helpers
{
    /// A simple exception type used to carry user-friendly validation error messages
    /// from the BLL layer up to the code-behind pages.
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}