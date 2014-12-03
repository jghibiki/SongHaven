using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SongHaven;
using Microsoft.AspNet.Identity;

namespace SongHaven.Controllers
{
    public class HomeController : Controller
    {
        private SongHavenEntities db = new SongHavenEntities();

        public ActionResult Index()
        {
            var singleOrDefault = db.Requests.SingleOrDefault(x => x.dt_created_date == db.Requests.Min(y => y.dt_created_date));
            if (singleOrDefault != null)
            {
                var firstOrDefault = singleOrDefault.Song;
                if (firstOrDefault != null)
                    ViewBag.NowPlaying = firstOrDefault.nvc_title.ToString() + " by " + firstOrDefault.nvc_artist.ToString() + " on " + firstOrDefault.nvc_album.ToString();
                else
                    ViewBag.nowplaying = "no song playing";
            }
            else ViewBag.nowplaying = "no song playing";

            ViewBag.Requests = (from r in db.Requests
                                select r).OrderBy(r => r.dt_created_date);

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
                dt_created_date = DateTime.UtcNow,
                fk_song = (Guid)id,
                fk_user = userGuid,
                i_vote_to_skip = 0.ToString()
            };
            db.Requests.Add(newRequest);

            //increment number of plays
            song.int_number_of_plays++;
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult AdminSkip()
        {
            db.Commands.Add(new Command()
                {
                    int_command = (int)Resources.RemoteCommand.NEXT_SONG,
                }
            );

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult AdminVUp()
        {
            db.Commands.Add(new Command()
                {
                    int_command = (int)Resources.RemoteCommand.VOLUME_UP,
                }
            );

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult AdminVDown()
        {
            db.Commands.Add(new Command()
                {
                    int_command = (int)Resources.RemoteCommand.VOLUME_DOWN,
                }
            );

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        
        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult PlayPause()
        {
            db.Commands.Add(new Command()
                {
                    int_command = (int)Resources.RemoteCommand.PLAY_PAUSE,
                }
            );

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Users = AuthorizedUsers.Users)]
        public ActionResult Restart()
        {
            db.Commands.Add(new Command()
                {
                    int_command = (int)Resources.RemoteCommand.RESTART,
                }
            );

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult VoteToSkipSong()
        {
            var request = db.Requests.SingleOrDefault(x => x.dt_created_date == db.Requests.Min(y => y.dt_created_date));
            if (request != null)
            {
                string userid = HttpContext.User.Identity.GetUserId();
                bool alreadyVoted = false;
                var voterList = from vl in db.RequestToVoters select vl;
                foreach (RequestToVoter r2v in voterList)
                {
                    var user = db.Users.SingleOrDefault(x => x.guid_id == r2v.pk_guid_User);
                    if (user != null)
                    {
                        if (user.nvc_mvc_id == userid)
                        {
                            alreadyVoted = true;
                            break;
                        }
                    }

                }
                if (!alreadyVoted)
                {
                    User user = db.Users.SingleOrDefault(x => x.nvc_mvc_id == userid);
                    if (user != null)
                    {
                        db.RequestToVoters.Add(new RequestToVoter()
                        {
                            pk_guid_Request = request.guid_id,
                            pk_guid_User = user.guid_id
                        });

                        request.i_vote_to_skip = (1 + int.Parse(request.i_vote_to_skip)).ToString();


                        if (request.i_vote_to_skip == "3")
                        {
                            db.Commands.Add(new Command()
                            {
                                int_command = (int) Resources.RemoteCommand.NEXT_SONG,
                            }
                                );
                        }

                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public class Resources
        {
            public enum RemoteCommand
            {
                DO_NOTHING = 0,
                PLAY_PAUSE = 1,
                NEXT_SONG = 2,
                VOLUME_UP = 3,
                VOLUME_DOWN = 4,
                RESTART = 5
            }
        }
    }
}
