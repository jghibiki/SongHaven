using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SongHaven;
using Microsoft.AspNet.Identity;

namespace SongHaven.Controllers
{
    public class HomeController : Controller
    {
        private SongHavenEntities db = new SongHavenEntities();

        public ActionResult Index()
        {

            string firstOrDefault = (from s in db.Requests
                select s.Song.nvc_title).FirstOrDefault();
            if (firstOrDefault != null)
                ViewBag.NowPlaying = firstOrDefault.ToString();
            else
                ViewBag.NowPlaying = "No Song Playing";

            ViewBag.Requests = (from r in db.Requests
                                select r);

            ViewBag.Messages = (from m in db.Messages
                                select m).Take(10);

            return View(db.Songs.ToList());
        }

        [HttpPost]
        public ActionResult newContent(FormCollection form, string newContent)
        {
            var currentUserID = User.Identity.GetUserId();

            var userGuid = (from u in db.Users
                            where u.nvc_mvc_id == currentUserID
                            select u.guid_id).FirstOrDefault();

            Message newMessage = new Message
            {
                date_created = DateTime.Now,
                content = newContent,
                fk_user = userGuid,
                guid_id = Guid.NewGuid(),
            };

            db.Messages.Add(newMessage);

            db.SaveChanges();
            return RedirectToAction("Index");
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

            var currentUserID = User.Identity.GetUserId();

            var userGuid = (from u in db.Users
                            where u.nvc_mvc_id == currentUserID
                            select u.guid_id).FirstOrDefault();

            if ((from r in db.Requests
                 where r.fk_song == (Guid)id
                 select r).FirstOrDefault() != null)
            {
                Request newRequest = new Request()
                {
                    guid_id = Guid.NewGuid(),
                    dt_created_date = DateTime.Now,
                    fk_song = (Guid)id,
                    fk_user = userGuid
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
