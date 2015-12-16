using System.Web.Mvc;
using System.Web.WebPages;
using DevTrends.WCFDataAnnotations;
using WebUI.Models;
using WebUI.ServiceReferenceJoueur;

namespace WebUI.Controllers
{
    [ValidateDataAnnotationsBehavior]
    public class HomeController : Controller
    {
        private IGestionJoueur Repository;

        public HomeController(IGestionJoueur personneRepository)
        {
            Repository = personneRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Resume()
        {
            return PartialView();
        }

        public ActionResult Connexion()
        {
            return View(new JoueurClient());
        }

        [HttpPost]
        public ActionResult Connexion(Session session, JoueurClient login)
        {
            if (PseudoEstValid(login) && MdpEstValid(login))
            {
                JoueurClient joueur = Repository.Connexion(login);
                if (joueur == null)
                {
                    ModelState.AddModelError("Fail", "Vos données sont incorrectes");
                    return View(login);
                }
                else
                {
                    session.Id = joueur.Id;
                    session.Pseudo = joueur.Pseudo;
                    return RedirectToAction("Index", "Principal");
                }
            }
            else
            {
                return View(login);
            }
        }

        public ActionResult Inscription()
        {
            return View("Inscription", new JoueurClient());
        }

        [HttpPost]
        public ActionResult Inscription(JoueurClient login)
        {
            if (PseudoEstValid(login) && MdpEstValid(login) && ConfirmMdpEstValid(login))
            {
                if (Repository.Inscription(login))
                {
                    return (RedirectToAction("Index"));
                }
                else
                {
                    ModelState.AddModelError("Fail", "Ce pseudo est déjà utilisé");
                    return View("Inscription", login);
                }
            }
            else
            {
                return View("Inscription", login);
            }
        }

        private bool PseudoEstValid(JoueurClient joueur)
        {
            return joueur.Pseudo != null && !joueur.Pseudo.Trim().IsEmpty();
        }

        private bool MdpEstValid(JoueurClient joueur)
        {
            return joueur.Mdp != null && !joueur.Mdp.Trim().IsEmpty();
        }

        private bool ConfirmMdpEstValid(JoueurClient joueur)
        {
            return joueur.ConfirmPassword != null && !joueur.Mdp.Trim().IsEmpty() &&
                   joueur.ConfirmPassword.Equals(joueur.Mdp);
        }
    }
}