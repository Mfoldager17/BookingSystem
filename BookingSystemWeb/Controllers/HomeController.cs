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
        public ActionResult Index(string email, string password)
        {
            Kunde kunde = db.Kunde.ToList().Find(k => k.Email == email);

            if (kunde == null)
            {
                kunde = new Kunde
                {
                    Navn = "dit navn",
                    Adresse = "din adresse",
                    Password = password,
                    Email = email,
                };
                db.Kunde.Add(kunde);
                db.SaveChanges();
            }

            int id = kunde.KundeId;
            return RedirectToAction("Edit/" + id, "Kundes");
        }
    }
}