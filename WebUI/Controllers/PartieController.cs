using System;
using System.Collections.Generic;
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
                return RedirectToAction("PlateauEnAttente");
            }

            return View(Repository.PartieCourante());
        }

        public ActionResult PlateauEnAttente(Session session)
        {
            if (Repository.PartieCourante() == null)
            {
                return RedirectToAction("VoirParties", "Principal");
            }
            if (Repository.PartieCourante().JoueurCourant.IdJoueur == session.Id)
            {
                return RedirectToAction("Plateau");
            }
            ViewBag.Id = session.Id;
            return View(Repository.PartieCourante());
        }

        public ActionResult Plateau(Session session)
        {
            //Cas ou partie est finie
            if (Repository.PartieCourante() == null)
            {
                return RedirectToAction("VoirParties", "Principal");
            }
            //On est pas le joueur courant
            if (Repository.PartieCourante().JoueurCourant.IdJoueur != session.Id)
            {
                return RedirectToAction("PlateauEnAttente");
            }
            return View(Repository.PartieCourante());
        }

        [HttpPost]
        public ActionResult Plateau(Session session, Dictionary<string, string> myDictionary)
        {
            if (Repository.PartieCourante() == null)
            {
                return Json(@Url.Action("VoirParties", "Principal"));
            }
            if (Repository.PartieCourante().JoueurCourant.IdJoueur != session.Id)
            {
                return Json(@Url.Action("PlateauEnAttente"));
            }

            if (!Repository.ActionDes(myDictionary))
            {
                return Json(@Url.Action("Plateau"));
            }

            return Json(Url.Action("PlateauCarte"));
        }

        public ActionResult PlateauCarte(Session session)
        {
            if (Repository.PartieCourante() == null)
            {
                return RedirectToAction("VoirParties", "Principal");
            }
            if (Repository.PartieCourante().JoueurCourant.IdJoueur != session.Id)
            {
                return RedirectToAction("PlateauEnAttente");
            }
            return View(Repository.PartieCourante());
        }

        [HttpPost]
        public ActionResult PlateauCarte(Session session, string effetCarte)
        {
            if (Repository.PartieCourante() == null)
            {
                return Json(@Url.Action("VoirParties", "Principal"));
            }
            string[] rienAFaire = new[] {"1", "3", "7", "8", "10"};
            string[] choisirAdversaire = new[] {"4", "5", "6", "9"};

            if (Array.IndexOf(rienAFaire, effetCarte) > -1)
            {
                return Json(@Url.Action("CarteRienAFaire", new
                {
                    id = effetCarte
                }));
            }
            else if (Array.IndexOf(choisirAdversaire, effetCarte) > -1)
            {
                return Json(@Url.Action("CarteAdversaire", new
                {
                    idEffet = effetCarte
                }));
            }
            else
            {
                return Json(@Url.Action("CarteSens", new
                {
                    idEffet = effetCarte
                }));
            }
        }

        public ActionResult AucuneCarte(Session session)
        {
            if (Repository.PartieCourante() == null)
            {
                return RedirectToAction("VoirParties", "Principal");
            }
            if (Repository.PartieCourante().JoueurCourant.IdJoueur != session.Id)
            {
                return RedirectToAction("PlateauEnAttente");
            }
            Repository.TourSuivant();
            return RedirectToAction("PlateauEnAttente");
        }

        [HttpGet]
        public ActionResult CarteSens(Session session, string idEffet)
        {
            ViewBag.IdSession = session.Id;
            ViewBag.IdEffet = idEffet;
            return View(Repository.PartieCourante());
        }

        [HttpGet]
        public ActionResult CarteAdversaire(Session session, string idEffet)
        {
            ViewBag.IdSession = session.Id;
            ViewBag.IdEffet = idEffet;
            return View(Repository.PartieCourante());
        }

        public ActionResult QuitterPartie(Session session)
        {
            JoueurClient joueur = new JoueurClient();
            joueur.Id = session.Id;

            Repository.QuitterPartie(joueur);
            return RedirectToAction("Index", "Principal");
        }

        [HttpGet]
        public ActionResult CarteRienAFaire(Session session, string id)
        {
            if (Repository.PartieCourante() == null)
            {
                return Json(@Url.Action("VoirParties", "Principal"));
            }
            if (!Repository.ActionCartes(id, "0", "0"))
            {
                return RedirectToAction("PlateauCarte");
            }
            if (Repository.PartieCourante() == null)
            {
                return RedirectToAction("VoirParties", "Principal");
            }
            Repository.TourSuivant();
            return RedirectToAction("PlateauEnAttente");
        }

        [HttpPost]
        public ActionResult JoueurCarte(Session session, string idEffet, string idJoueur, string value)
        {
            if (Repository.PartieCourante() == null)
            {
                return Json(@Url.Action("VoirParties", "Principal"));
            }
            if (!Repository.ActionCartes(idEffet, idJoueur, value))
            {
                return Json(@Url.Action("PlateauCarte"));
            }
            if (Repository.PartieCourante() == null)
            {
                return Json(@Url.Action("VoirParties", "Principal"));
            }
            Repository.TourSuivant();
            return Json(@Url.Action("PlateauEnAttente"));
        }
    }
}