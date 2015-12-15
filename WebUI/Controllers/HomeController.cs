using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevTrends.WCFDataAnnotations;
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
        public ActionResult Connexion(JoueurClient login)
        {

            //TODO verifier les champs
            if (ModelState.IsValid)
            {
                JoueurClient personne = Repository.Connexion(login);
                if (personne == null)
                {
                    ModelState.AddModelError("Fail", "Vos données sont incorrectes");
                    return View(login);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
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
        public ActionResult Inscription(JoueurClient personne)
        {
            //TODO verifier les champs
            if (ModelState.IsValid)
            {
                Repository.Inscription(personne);

                return (RedirectToAction("Index"));
            }
            else
            {
                return View("Inscription", personne);
            }
        }
    }
}
