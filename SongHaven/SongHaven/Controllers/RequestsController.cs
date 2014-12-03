using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SongHaven;

namespace SongHaven.Controllers
{
    [Authorize(Users = AuthorizedUsers.Users)]
    public class RequestsController : Controller
    {
        private SongHavenEntities db = new SongHavenEntities();

        // GET: Requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.Song).Include(r => r.User);
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.fk_song = new SelectList(db.Songs, "guid_id", "nvc_title");
            ViewBag.fk_user = new SelectList(db.Users, "guid_id", "nvc_username");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guid_id,fk_song,fk_user,dt_created_date")] Request request)
        {
            if (ModelState.IsValid)
            {
                request.guid_id = Guid.NewGuid();
                request.i_vote_to_skip = 0.ToString();
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_song = new SelectList(db.Songs, "guid_id", "nvc_title", request.fk_song);
            ViewBag.fk_user = new SelectList(db.Users, "guid_id", "nvc_username", request.fk_user);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_song = new SelectList(db.Songs, "guid_id", "nvc_title", request.fk_song);
            ViewBag.fk_user = new SelectList(db.Users, "guid_id", "nvc_username", request.fk_user);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guid_id,fk_song,fk_user,dt_created_date")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_song = new SelectList(db.Songs, "guid_id", "nvc_title", request.fk_song);
            ViewBag.fk_user = new SelectList(db.Users, "guid_id", "nvc_username", request.fk_user);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
