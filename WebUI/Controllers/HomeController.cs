using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.ServiceReferenceJoueur;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IGestionJoueur Repository;

        public HomeController(IGestionJoueur personneRepository)
        {
            this.Repository = personneRepository;
        }

        //
        // GET: /Home/

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
            ModelState.Remove("ConfirmPassword");

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
                    return RedirectToAction("Index", "Principal");
                }
            }
            else
            {
                return View(login);
            }
        }

    }
}
