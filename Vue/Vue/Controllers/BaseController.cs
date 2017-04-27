using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vue.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        [NonAction]
        public JsonResult _Json(object data, JsonRequestBehavior Jbehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(data, Jbehavior);
        }

    }
}
