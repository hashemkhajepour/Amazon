using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazon
{
    public class ManageEmailsController : Controller
    {
        // GET: ManageEmails
        public ActionResult ActivationEmail()
        {
            return PartialView();
        }
    }
}