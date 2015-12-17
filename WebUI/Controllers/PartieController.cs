using System.Web.Mvc;
using WebUI.Models;
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

        public ActionResult Plateau(Session session)
        {
            if (Repository.PartieCourante().JoueurCourant.Id != session.Id)
            {
                return RedirectToAction("PlateauEnAttente");
            }
            return View();
        }

        public ActionResult PlateauEnAttente(Session session)
        {
            if (Repository.PartieCourante().JoueurCourant.Id == session.Id)
            {
                return RedirectToAction("Plateau");
            }
            return View();
        }
    }
}