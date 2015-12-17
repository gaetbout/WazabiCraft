using System.Web.Mvc;
using System.Web.WebPages;
using WebUI.Models;
using WebUI.ServiceReferencePartie;

namespace WebUI.Controllers
{
    public class PrincipalController : Controller
    {
        private IGestionPartie Repository;

        public PrincipalController(IGestionPartie referencePartie)
        {
            Repository = referencePartie;
            Repository.Init();
        }

        public ActionResult Index(Session session)
        {
            return View(Repository.PartieCourante());
        }

        public ActionResult CreerPartie()
        {
            return View(new NomPartieModel());
        }

        [HttpPost]
        public ActionResult CreerPartie(Session session, NomPartieModel model)
        {
            if (model.Nom != null && !model.Nom.Trim().IsEmpty())
            {
                JoueurClient createur = new JoueurClient();
                createur.Id = session.Id;
                createur.Pseudo = session.Pseudo;

                if (Repository.CreerPartie(createur, model.Nom))
                {
                    return RedirectToAction("EnAttente", "Partie");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            
            return View(model);
        }

        public ActionResult RejoindrePartie(Session session)
        {
            JoueurClient joueur = new JoueurClient();
            joueur.Id = session.Id;
            joueur.Pseudo = session.Pseudo;

            if (Repository.RejoindrePartie(joueur))
            {
                return RedirectToAction("EnAttente", "Partie");
            }
            else
            {
                return View("Index");
            }
        }

        public PartialViewResult Pseudo(Session session)
        {
            return PartialView(session);
        }
    }

    public class NomPartieModel
    {
        public string Nom { get; set; }
    }
}