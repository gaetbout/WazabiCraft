using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.ServiceReferencePartie;

namespace WebUI.Controllers
{
    public class AccuelController : Controller
    {
        private IGestionPartie RepositoryP;

        public AccuelController(IGestionPartie gestionPartie)
        {
            RepositoryP = gestionPartie;
            RepositoryP.init();
        }

        public ActionResult Index() 
        {
            return RedirectToAction("Index","Home");
        }
    }
}