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
                                  select s.Song.nvc_title).FirstOrDefault();
          

            ViewBag.Requests = (from r in db.Requests
                                select r);

            ViewBag.Messages = (from m in db.Messages
                                select m).Take(10);

            return View(db.Songs.ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var a = form[0];
            return View();
        }
        [HttpPost]
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
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}