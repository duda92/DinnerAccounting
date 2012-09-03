using System.Web.Mvc;
using DA.Dinners.Domain.Abstract;

namespace UI.Controllers
{
    [Authorize]
    public class PropositionsController : Controller
    {
        //
        // GET: /Proposition/

        private readonly IContinuousPropositionRepository continuousPropositionRepository;

        public PropositionsController(IPropositionRepository propositionRepository, IContinuousPropositionRepository continuousPropositionRepository)
        {
            this.continuousPropositionRepository = continuousPropositionRepository;
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult User()
        {
            return View();
        }
    }
}