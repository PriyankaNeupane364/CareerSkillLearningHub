using System;
using System.Web;

namespace CareerSkillHub.BLL
{
    /// <summary>
    /// Handles registration, login, logout and all session-related logic.
    /// Pages should call these helpers instead of touching Session directly.
    /// </summary>
    public class AuthBLL
    {
        private readonly Data_Access_Layer.UserDAL _userDal = new Data_Access_Layer.UserDAL();

        // ==================================================================
        // REGISTER
        // ==================================================================
        /// <summary>
        /// Validates the fields, checks the email is not already registered,
        /// hashes the password and inserts a new Student. Returns the new UserID.
        /// </summary>
        public int Register(string fullName, string email, string password)
        {
            ValidateRegistration(fullName, email, password);

            if (_userDal.EmailExists(email.Trim()))
                throw new Helpers.ValidationException("That email address is already registered.");

            string salt = Helpers.PasswordHelper.GenerateSalt();
            string hash = Helpers.PasswordHelper.Hash(password, salt);

            Models.User u = new Models.User
            {
                FullName = fullName.Trim(),
                Email = email.Trim().ToLowerInvariant(),
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "Student"
            };

            return _userDal.Insert(u);
        }

        private void ValidateRegistration(string fullName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new Helpers.ValidationException("Full name is required.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
                throw new Helpers.ValidationException("A valid email address is required.");
            if (string.IsNullOrEmpty(password) || password.Length < 6)
                throw new Helpers.ValidationException("Password must be at least 6 characters.");
        }

        // ==================================================================
        // LOGIN
        // ==================================================================
        /// <summary>
        /// Looks up the user by email and verifies the password.
        /// On success it stores the user details in Session.
        /// Returns the Role so the calling page knows where to redirect.
        /// </summary>
        public string Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(password))
                throw new Helpers.ValidationException("Email and password are required.");

            Models.User u = _userDal.SelectByEmail(email.Trim().ToLowerInvariant());

            if (u == null || !Helpers.PasswordHelper.Verify(password, u.PasswordSalt, u.PasswordHash))
                throw new Helpers.ValidationException("Invalid email or password.");

            SetSession(u);
            return u.Role;
        }

        private void SetSession(Models.User u)
        {
            HttpContext.Current.Session["UserID"] = u.UserID;
            HttpContext.Current.Session["FullName"] = u.FullName;
            HttpContext.Current.Session["Email"] = u.Email;
            HttpContext.Current.Session["Role"] = u.Role;
        }

        public static void RefreshSessionName(string fullName)
        {
            if (IsLoggedIn) HttpContext.Current.Session["FullName"] = fullName;
        }

        // ==================================================================
        // LOGOUT
        // ==================================================================
        public static void Logout()
        {
            if (HttpContext.Current == null) return;
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        // ==================================================================
        // SESSION READ HELPERS
        // ==================================================================
        public static bool IsLoggedIn
        {
            get { return HttpContext.Current != null && HttpContext.Current.Session["UserID"] != null; }
        }

        public static int CurrentUserID
        {
            get { return IsLoggedIn ? Convert.ToInt32(HttpContext.Current.Session["UserID"]) : 0; }
        }

        public static string CurrentUserName
        {
            get { return IsLoggedIn ? HttpContext.Current.Session["FullName"] as string : null; }
        }

        public static string CurrentRole
        {
            get { return IsLoggedIn ? HttpContext.Current.Session["Role"] as string : null; }
        }

        public static bool IsAdmin { get { return IsLoggedIn && CurrentRole == "Admin"; } }
        public static bool IsInstructor { get { return IsLoggedIn && CurrentRole == "Instructor"; } }
        public static bool IsStudent { get { return IsLoggedIn && CurrentRole == "Student"; } }

        /// <summary>Redirects to the login page when the visitor is not logged in.</summary>
        public static void RequireLogin()
        {
            if (!IsLoggedIn)
                HttpContext.Current.Response.Redirect("~/Account/Login.aspx");
        }

        /// <summary>Redirects to the access-denied page when the visitor lacks a required role.</summary>
        public static void RequireRole(string requiredRole)
        {
            if (!IsLoggedIn)
                HttpContext.Current.Response.Redirect("~/Account/Login.aspx");
            if (CurrentRole != requiredRole)
                HttpContext.Current.Response.Redirect("~/Pages/AccessDenied.aspx");
        }
    }
}