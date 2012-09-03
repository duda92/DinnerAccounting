using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public RedirectToRouteResult Index()
        {
            return User.Identity.IsAuthenticated ? RedirectToAction("User", "Propositions") : RedirectToAction("LogOn", "Account", new { returnUrl = Request.Url });
        }

        [Authorize]
        public ActionResult GetMyInfo()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminPart()
        {
            return View();
        }
    }
}