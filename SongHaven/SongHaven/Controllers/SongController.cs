using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SongHaven;

namespace SongHaven.Controllers
{

    
    public class SongController : Controller
    {
        private SongHavenEntities db = new SongHavenEntities();
        // GET: Song
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(IEnumerable<HttpPostedFileBase> files, string title, string artist, string album)
        {
            Song song = new Song()
            {
                guid_id = Guid.NewGuid(),
                dt_created_date = DateTime.Now,
                dt_last_played_date = DateTime.Now,
                int_number_of_plays = 0,
                nvc_file_type = "wav",
                nvc_album = album,
                nvc_artist = artist,
                nvc_title = title
            };

            db.Songs.Add(song);
            db.SaveChanges();

            foreach (var file in files)
            {

                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = "";
                    if (fileName.ToLower().Contains(".mp3"))
                        path = Path.Combine(@"C:\ProgramData\SongHaven\Music", song.guid_id.ToString() + ".mp3");
                    else if (fileName.ToLower().Contains(".wav"))
                        path = Path.Combine(@"C:\ProgramData\SongHaven\Music", song.guid_id.ToString() + ".wav");
                    
                    file.SaveAs(path);
                }
                
            }
            return RedirectToAction("Home","Index");
        }
    }

    
}