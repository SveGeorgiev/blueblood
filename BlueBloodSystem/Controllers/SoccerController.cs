using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlueBloodSystem.Models;

namespace BlueBloodSystem.Controllers
{
    public class SoccerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Soccers
        public async Task<ActionResult> Index()
        {
            List<Soccer> soccerList = await db.Soccers.OrderByDescending(s => s.Date).ToListAsync();
            return View(new SoccerViewModel(soccerList));
        }


        // GET: Soccers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Soccers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Opponents,Result,Date,Score")] Soccer soccer)
        {
            if (ModelState.IsValid)
            {
                db.Soccers.Add(soccer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(soccer);
        }

        // GET: Soccers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soccer soccer = await db.Soccers.FindAsync(id);
            if (soccer == null)
            {
                return HttpNotFound();
            }
            return View(soccer);
        }

        // POST: Soccers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Opponents,Result,Date,Score")] Soccer soccer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soccer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(soccer);
        }

        // GET: Soccers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soccer soccer = await db.Soccers.FindAsync(id);
            if (soccer == null)
            {
                return HttpNotFound();
            }
            return View(soccer);
        }

        // POST: Soccers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Soccer soccer = await db.Soccers.FindAsync(id);
            db.Soccers.Remove(soccer);
            await db.SaveChangesAsync();
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
