using System.Web.Mvc;
using System.Web.Routing;
using UI.Models;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }

        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    return Redirect(returnUrl);
                }
                else
                    ModelState.AddModelError("", "The_user_name_or_password_provided_is_incorrect");
            }
            return View(model);
        }

        public ActionResult LogOut(string returnUrl)
        {
            FormsService.SignOut();
            return Redirect(returnUrl ?? Url.Action("LogOn", "Account"));
        }
    }
}