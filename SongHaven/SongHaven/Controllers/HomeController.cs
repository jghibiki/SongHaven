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

            var firstOrDefault = (from s in db.Requests
                select s.Song).FirstOrDefault();
            if (firstOrDefault != null)
                ViewBag.NowPlaying = firstOrDefault.nvc_title.ToString() + " by " + firstOrDefault.nvc_artist.ToString() + " on " + firstOrDefault.nvc_album.ToString();
            else
                ViewBag.NowPlaying = "No Song Playing";

            ViewBag.Requests = (from r in db.Requests
                                select r).OrderByDescending(r => r.dt_created_date);

            ViewBag.Messages = (from m in db.Messages
                                select m).Take(10).OrderByDescending(m => m.date_created);

            return View(db.Songs.ToList());
        }

        [HttpPost]
        public ActionResult newContent(string newContent)
        {
            var currentUserID = User.Identity.GetUserId();

            var userGuid = (from u in db.Users
                            where u.nvc_mvc_id == currentUserID
                            select u.guid_id).FirstOrDefault();

            Message newMessage = new Message
            {
                guid_id = Guid.NewGuid(),
                date_created = DateTime.Now,
                content = newContent,
                fk_user = userGuid,
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

            Request newRequest = new Request()
            {
                guid_id = Guid.NewGuid(),
                dt_created_date = DateTime.Now,
                fk_song = (Guid)id,
                fk_user = userGuid
            };
            db.Requests.Add(newRequest);

            //increment number of plays
            song.int_number_of_plays++;
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
