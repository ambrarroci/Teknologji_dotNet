﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt_Teknologji_dotNet.Models;

namespace Projekt_Teknologji_dotNet.Controllers
{
    public class RezervimetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rezervimet
        [Authorize]
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            var rezervimet = db.Rezervimet.Include(r => r.Klient).Include(r => r.Makinat).Where(r => r.Klient.Username == user);
            /*if (!string.IsNullOrEmpty(user))
            {
                rezervimet = rezervimet.Where(r => r.Klient.Username == user);
            }*/
                
            return View(rezervimet.ToList());
        }

        // GET: Rezervimet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervimet rezervimet = db.Rezervimet.Find(id);
            if (rezervimet == null)
            {
                return HttpNotFound();
            }
            return View(rezervimet);
        }

        // GET: Rezervimet/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.KlientID = new SelectList(db.Klient, "ID", "Username");
            ViewBag.MakinatID = new SelectList(db.Makinat, "ID", "Modeli");
            return View();
        }

        // POST: Rezervimet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rezervimet rezervimet, int? makineId)
        {
            if (ModelState.IsValid)
            {
                var user = System.Web.HttpContext.Current.User.Identity.Name;
                var klient = db.Klient.Where(m => m.Username == user).ToList();
                var makine = db.Makinat.Where(m => m.ID == makineId).ToList();
                decimal pagesDite = 0;
                foreach(var item in makine)
                {
                    if(item.ERezervuar == true)
                    {
                        ViewBag.msg = "Error";
                    }

                    pagesDite = item.Kosto1Dite;
                }
                DateTime dt1 = rezervimet.Date_Rezervimi;
                DateTime dt2 = rezervimet.Date_kthimi;
                TimeSpan span = dt2.Subtract(dt1);
                decimal spanSecs = (span.Hours * 3600) + (span.Minutes * 60) + span.Seconds;
                decimal spanPart = spanSecs / 86400M;
                decimal result2 = span.Days + spanPart;
                decimal result3 = result2 * pagesDite;
                var klientId = 0;
                foreach (var item in klient)
                {
                    klientId = item.ID;   
                }
                db.Rezervimet.Add(new Rezervimet { Date_Rezervimi = rezervimet.Date_Rezervimi, Date_kthimi = rezervimet.Date_kthimi, Pagesa_totale = result3, KlientID = klientId, MakinatID = makineId });
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.KlientID = new SelectList(db.Klient, "ID", "Username", rezervimet.KlientID);
            ViewBag.MakinatID = new SelectList(db.Makinat, "ID", "Modeli", rezervimet.MakinatID);
            return View(rezervimet);
        }

        // GET: Rezervimet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervimet rezervimet = db.Rezervimet.Find(id);
            if (rezervimet == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlientID = new SelectList(db.Klient, "ID", "Username", rezervimet.KlientID);
            ViewBag.MakinatID = new SelectList(db.Makinat, "ID", "Modeli", rezervimet.MakinatID);
            return View(rezervimet);
        }

        // POST: Rezervimet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date_Rezervimi,Date_kthimi,Pagesa_totale,KlientID,MakinatID")] Rezervimet rezervimet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervimet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlientID = new SelectList(db.Klient, "ID", "Username", rezervimet.KlientID);
            ViewBag.MakinatID = new SelectList(db.Makinat, "ID", "Modeli", rezervimet.MakinatID);
            return View(rezervimet);
        }

        // GET: Rezervimet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervimet rezervimet = db.Rezervimet.Find(id);
            if (rezervimet == null)
            {
                return HttpNotFound();
            }
            return View(rezervimet);
        }

        // POST: Rezervimet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervimet rezervimet = db.Rezervimet.Find(id);
            db.Rezervimet.Remove(rezervimet);
            db.SaveChanges();
            return RedirectToAction("Index");
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
