using BookingSystemEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingSystemWeb.Controllers
{
    public class HomeController : Controller
    {
        private BookingSystemDbEntities db = new BookingSystemDbEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Email,Password")] Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                Kunde kundeFraDb = db.Kunde.Where(k => k.Email == kunde.Email).FirstOrDefault();

                if (kundeFraDb == null)
                {
                    kunde.Navn = "";
                    kunde.Adresse = "";
                    kunde.Email = kunde.Email.ToLower();

                    kunde = db.Kunde.Add(kunde); // overrider kunde
                    db.SaveChanges();

                    int id = kunde.KundeId;
                    Session["kunde"] = kunde;

                    return RedirectToAction("Edit/" + id, "Kundes");
                }
                else
                {
                    if (kundeFraDb.Password == kunde.Password)
                    {
                        int id = kundeFraDb.KundeId;
                        Session["kunde"] = kundeFraDb;
                        return RedirectToAction("Edit/" + id, "Kundes");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Forkert password til email");
                        return View(kunde);
                    }
                }
            }

            return View();
        }
    }
}