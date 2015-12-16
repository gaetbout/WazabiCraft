using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Infrastructure.Binders
{
    public class SessionModelBinder : IModelBinder
    {
        private const string sessionKey = "JoueurClient";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Session login = null;

            if (controllerContext.HttpContext.Session != null)
            {
                login = (Session) controllerContext.HttpContext.Session[sessionKey];
            }

            if (login == null)
            {
                login = new Session();

                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = login;
                }
            }
            return login;
        }
    }
}