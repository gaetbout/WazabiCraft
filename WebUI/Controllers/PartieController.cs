using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class PartieController : Controller
    {
        // GET: Partie
        public ActionResult EnAttente()
        {
            return View();
        }
    }
}