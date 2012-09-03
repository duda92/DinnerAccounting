using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace UI.Models
{
    public class LogOnModel
    {
        [Required(ErrorMessage = "Введите ваш логин (Имя_домена\\имя_пользователя)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);

        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) { /*throw new ArgumentException("Value cannot be null or empty.", "userName");*/}
            if (String.IsNullOrEmpty(password)) { /*throw new ArgumentException("Value cannot be null or empty.", "password");*/}

            return _provider.ValidateUser(userName, password);
        }
    }
}