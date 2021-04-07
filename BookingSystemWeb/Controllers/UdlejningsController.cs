using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingSystemEF;

namespace BookingSystemWeb.Controllers
{
    public class UdlejningsController : Controller
    {
        private BookingSystemDbEntities db = new BookingSystemDbEntities();

        // GET: Udlejnings
        public ActionResult Index(string id)
        {
            if (id == null)
                return View(db.Udlejning.Include(u => u.Kunde).Include(u => u.Værktøj).ToList());
            else
                return View(db.Udlejning.ToList().FindAll(u => u.KundeId.ToString() == id));
        }

        // GET: Udlejnings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Udlejning udlejning = db.Udlejning.Find(id);
            if (udlejning == null)
            {
                return HttpNotFound();
            }
            return View(udlejning);
        }

        // GET: Udlejnings/Create
        public ActionResult Create()
        {
            ViewBag.KundeId = new SelectList(db.Kunde, "KundeId", "Navn");
            ViewBag.VærktøjId = new SelectList(db.Værktøj, "VærktøjId", "Værktøjsnavn");
            return View();
        }

        // POST: Udlejnings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UdlejningsId,KundeId,VærktøjId,FraDato,TilDato")] Udlejning udlejning)
        {
            if (ModelState.IsValid)
            {
                udlejning.Status = "reserveret";
                db.Udlejning.Add(udlejning);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KundeId = new SelectList(db.Kunde, "KundeId", "Navn", udlejning.KundeId);
            ViewBag.VærktøjId = new SelectList(db.Værktøj, "VærktøjId", "Værktøjsnavn", udlejning.VærktøjId);
            return View(udlejning);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}