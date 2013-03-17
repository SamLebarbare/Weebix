using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weebix.Models;

namespace Weebix.Controllers
{
    public class DeckController : Controller
    {
        //
        // GET: /deck/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null)
            {
                // extract only the fielname             
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/images/User-Image folder             
                var path = Path.Combine(Server.MapPath("~/images/deck/"), fileName);

                string filepathToSave = "/images/deck/" + fileName;
                file.SaveAs(path);

                using (var context = new WeebixDoContext())
                {
                    var card = context.deck.Create();

                    card.path = filepathToSave;
                    context.deck.Add(card);
                    context.SaveChanges();


                }
            }
            
            return View();
        }
    }
}
