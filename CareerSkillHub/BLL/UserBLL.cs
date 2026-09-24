using System.Collections.Generic;
using CareerSkillHub.Data_Access_Layer;
using CareerSkillHub.Models;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Business logic for user accounts (profile management + admin user CRUD).
    /// </summary>
    public class UserBLL
    {
        private readonly UserDAL _userDal = new UserDAL();

        public User GetById(int userId) { return _userDal.SelectById(userId); }
        public User GetByEmail(string email) { return _userDal.SelectByEmail(email); }
        public List<User> GetAll() { return _userDal.SelectAll(); }
        public int CountAll() { return _userDal.CountAll(); }
        public int CountByRole(string role) { return _userDal.CountByRole(role); }

        /// <summary>Returns a list of instructors (for course dropdowns).</summary>
        public List<User> GetInstructors() { return _userDal.SelectAll().FindAll(u => u.Role == "Instructor"); }

        /// <summary>Updates a user's profile details (name + email).</summary>
        public void UpdateProfile(int userId, string fullName, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new Helpers.ValidationException("Full name is required.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new Helpers.ValidationException("A valid email address is required.");

            if (_userDal.EmailExists(email.Trim(), userId))
                throw new Helpers.ValidationException("That email address is already used by another account.");

            _userDal.UpdateProfile(userId, fullName.Trim(), email.Trim().ToLowerInvariant());

            if (AuthBLL.CurrentUserID == userId)
                AuthBLL.RefreshSessionName(fullName.Trim());
        }

        /// <summary>Changes the password for the given user.</summary>
        public void ChangePassword(int userId, string currentPassword, string newPassword)
        {
            User u = _userDal.SelectById(userId);
            if (u == null) throw new Helpers.ValidationException("User not found.");

            if (!Helpers.PasswordHelper.Verify(currentPassword, u.PasswordSalt, u.PasswordHash))
                throw new Helpers.ValidationException("Current password is incorrect.");

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
                throw new Helpers.ValidationException("New password must be at least 6 characters.");

            string salt = Helpers.PasswordHelper.GenerateSalt();
            string hash = Helpers.PasswordHelper.Hash(newPassword, salt);
            _userDal.UpdatePassword(userId, hash, salt);
        }

        // ---------- Admin user CRUD ----------

        /// <summary>Adds a new user (admin function, role can be Student / Instructor / Admin).</summary>
        public int AddUser(string fullName, string email, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new Helpers.ValidationException("Full name is required.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new Helpers.ValidationException("A valid email address is required.");
            if (string.IsNullOrEmpty(password) || password.Length < 6)
                throw new Helpers.ValidationException("Password must be at least 6 characters.");
            if (role != "Student" && role != "Instructor" && role != "Admin")
                throw new Helpers.ValidationException("Select a valid role.");

            if (_userDal.EmailExists(email.Trim()))
                throw new Helpers.ValidationException("That email address is already registered.");

            string salt = Helpers.PasswordHelper.GenerateSalt();
            string hash = Helpers.PasswordHelper.Hash(password, salt);

            User u = new User
            {
                FullName = fullName.Trim(),
                Email = email.Trim().ToLowerInvariant(),
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = role
            };
            return _userDal.Insert(u);
        }

        /// <summary>Updates user details (admin function).</summary>
        public void UpdateUser(int userId, string fullName, string email, string role)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new Helpers.ValidationException("Full name is required.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new Helpers.ValidationException("A valid email address is required.");
            if (role != "Student" && role != "Instructor" && role != "Admin")
                throw new Helpers.ValidationException("Select a valid role.");

            if (_userDal.EmailExists(email.Trim(), userId))
                throw new Helpers.ValidationException("That email address is already used by another account.");

            User u = new User
            {
                UserID = userId,
                FullName = fullName.Trim(),
                Email = email.Trim().ToLowerInvariant(),
                Role = role
            };
            _userDal.Update(u);
        }

        /// <summary>Deletes a user (admin function).</summary>
        public void DeleteUser(int userId)
        {
            if (AuthBLL.CurrentUserID == userId)
                throw new Helpers.ValidationException("You cannot delete your own account.");

            User target = _userDal.SelectById(userId);
            if (target == null)
                throw new Helpers.ValidationException("User not found.");

            // An instructor who already owns courses cannot be removed until
            // those courses are deleted (they are linked by a foreign key).
            if (target.Role == "Instructor" && new CourseDAL().CountByInstructor(userId) > 0)
                throw new Helpers.ValidationException(
                    "This instructor owns courses. Delete their courses before removing the account.");

            if (_userDal.Delete(userId) == false)
                throw new Helpers.ValidationException("Unable to delete this user.");
        }
    }
}