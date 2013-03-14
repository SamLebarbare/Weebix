using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weebix.Models;

namespace Weebix.Controllers
{
    public class PartyController : Controller
    {
        //
        // GET: /Party/

        public ActionResult Index(int partyId)
        {

			try
			{
				using (var context = new WeebixDoContext ())
				{
					var party = context.games.FirstOrDefault (p => p.partyId == partyId);
					ViewBag.partyId = party.partyId;
					ViewBag.name = party.name;
				}
			}
			catch (Exception ex)
			{
				
			}

			
            return View();
        }

    }
}
