using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SongHaven;

namespace SongHaven.Controllers
{
    public class SongsController : Controller
    {
        private SongHavenEntities db = new SongHavenEntities();

        // GET: Songs
        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult Index()
        {
            return View(db.Songs.ToList());
        }

        [HttpPost]
        public ActionResult Index(string searchMode, string searchString)
        {
            var songs = from m in db.Songs select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                if (searchMode == "title")
                {
                    songs = songs.Where(s => s.nvc_title.Contains(searchString));
                }
                else if (searchMode == "album")
                {
                    songs = songs.Where(s => s.nvc_album.Contains(searchString));
                }
                else if (searchMode == "artist")
                {
                    songs = songs.Where(s => s.nvc_artist.Contains(searchString));
                }
            }

            return View(songs.ToList());
        }

        // GET: Songs/Details/5
        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }


            return View(song);
        }

        // GET: Songs/Create
        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult Create([Bind(Include = "guid_id,nvc_title,nvc_album,nvc_artist,int_number_of_plays,dt_created_date,dt_last_played_date,nvc_file_type")] Song song)
        {
            if (ModelState.IsValid)
            {
                song.guid_id = Guid.NewGuid();
                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(song);
        }

        // GET: Songs/Edit/5
        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult Edit([Bind(Include = "guid_id,nvc_title,nvc_album,nvc_artist,int_number_of_plays,dt_created_date,dt_last_played_date,nvc_file_type")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Song song = db.Songs.Find(id);

            System.IO.File.Delete(Path.Combine(@"C:\ProgramData\SongHaven\music", string.Format("{0}.{1}", song.guid_id, song.nvc_file_type)));

            db.Requests.RemoveRange(from r in db.Requests
                where r.Song.guid_id == song.guid_id
                select r);

            db.Songs.Remove(song);
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
