using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weebix.Models;

namespace Weebix.Controllers
{
    public class LobbyController : Controller
    {
        //
        // GET: /Lobby/

        public ActionResult Index()
        {
            return View();
        }

    }
}
