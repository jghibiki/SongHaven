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
    public class HomeController : Controller
    {
        private SongHavenEntities db = new SongHavenEntities();

        public ActionResult Index()
        {
            
            ViewBag.NowPlaying = (from s in db.Requests
                                  select s.Song.nvc_title).FirstOrDefault().ToString();
            if (ViewBag.NowPlaying == null)
                ViewBag.NowPlaying = "No Song Playing";

            ViewBag.Requests = (from r in db.Requests
                                select r);

            ViewBag.Messages = (from m in db.Messages
                                select m).Take(10);

            return View(db.Songs.ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, string newContent)
        {
            var a = form["newContent"];
            return View();
        }
        public ActionResult Request(System.Guid id)
        {

            var song = (from s in db.Songs
                        where s.guid_id == id
                        select s).FirstOrDefault();

            Request newSong = new Request();
            newSong.fk_song = song.guid_id;

            db.Requests.Add(newSong);

            return View();
        }
        public ActionResult Request(Guid? id)
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

            if ((from r in db.Requests
                 where r.fk_song == (Guid)id
                 select r).FirstOrDefault() != null)
            {
                Request newRequest = new Request()
                {
                    guid_id = Guid.NewGuid(),
                    dt_created_date = DateTime.Now,
                    fk_song = (Guid)id,
                    fk_user = Guid.Parse("B0084E4D-A166-43F6-8B1B-65387C38F5D7")
                };
                db.Requests.Add(newRequest);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
