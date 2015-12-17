using System.Web.Mvc;
using WebUI.ServiceReferencePartie;

namespace WebUI.Controllers
{
    public class PartieController : Controller
    {
        private IGestionPartie Repository;

        public PartieController(IGestionPartie referencePartie)
        {
            Repository = referencePartie;
        }

        public ActionResult EnAttente()
        {
            if (Repository.PartieCourante().Etat == 1)
            {
                return RedirectToAction("Plateau");
            }

            return View(Repository.PartieCourante());
        }

        public ActionResult Plateau()
        {
            return View();
        }
    }
}