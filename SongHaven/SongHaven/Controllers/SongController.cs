using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SongHaven;
using System.Net;
using Microsoft.AspNet.Identity;

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

            foreach (var file in files)
            {

                if (file.ContentLength > 0)
                {

                    string fileType = null;
                    var fileName = Path.GetFileName(file.FileName);
                    var path = "";

                    if (fileName.ToLower().Contains(".mp3"))
                        fileType = "mp3";
                    else if (fileName.ToLower().Contains(".wav"))
                        fileType = "wav";

                    Song song = new Song()
                    {
                        guid_id = Guid.NewGuid(),
                        dt_created_date = DateTime.Now,
                        dt_last_played_date = DateTime.Now,
                        int_number_of_plays = 0,
                        nvc_file_type = fileType,
                        nvc_album = album,
                        nvc_artist = artist,
                        nvc_title = title
                    };

                    if (fileName.ToLower().Contains(".mp3"))
                        path = Path.Combine(@"C:\ProgramData\SongHaven\Music", song.guid_id.ToString() + ".mp3");
                    else if (fileName.ToLower().Contains(".wav"))
                        path = Path.Combine(@"C:\ProgramData\SongHaven\Music", song.guid_id.ToString() + ".wav");

                    file.SaveAs(path);

                    db.Songs.Add(song);
                    db.SaveChanges();
                }
                
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult SearchResults(string searchString, string searchMode)
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
      
    }

    
}