using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OTM.BLL;
using OTM.Models;
using PusherServer;

namespace OTM.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private Pusher pusher;

        //class constructor
        public ChatController()
        {

            var options = new PusherOptions
            {
                Cluster = "ap2",
                Encrypted = true
            };

            pusher = new Pusher(
               "930576",
               "9461ef37ed9224dfb8b3",
               "3b6cc5cbbca1e19e861b",
               options
           );
        }

        public JsonResult AuthForChannel(string channel_name, string socket_id)
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            var currentUser = (Models.MdlAccount)Session["user"];

            if (channel_name.IndexOf("presence") >= 0)
            {

                var channelData = new PresenceChannelData()
                {
                    user_id = currentUser.ID.ToString(),
                    user_info = new
                    {
                        id = currentUser.ID,
                        name = currentUser.FName
                    },
                };

                var presenceAuth = pusher.Authenticate(channel_name, socket_id, channelData);

                return Json(presenceAuth);

            }

            if (channel_name.IndexOf(currentUser.ID.ToString()) == -1)
            {
                return Json(new { status = "error", message = "User cannot join channel" });
            }

            var auth = pusher.Authenticate(channel_name, socket_id);

            return Json(auth);


        }
        // GET: Chat

        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return Redirect("/");
            }
            var currentUser = (MdlAccount)Session["user"];
            BLL_Seller obj = new BLL_Seller();
            ViewBag.allUsers = obj.BLL_ReadAllSeller();
            ViewBag.currentUser = currentUser;
            return View();
        }

        [HttpGet]
        public JsonResult ConversationWithContact(int contact)
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }
            var currentUser = (Models.MdlAccount)Session["user"];
            var conversations = new List<Models.MdlChat>();
            BLL_Chat obj = new BLL_Chat();
            conversations= obj.BllReadChat(currentUser.ID,contact);
            return Json(new { status = "success", data = conversations }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SendMessage()
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }
            var currentUser = (MdlAccount)Session["user"];
            var contact = Convert.ToInt32(Request.Form["contact"]);
            string socket_id = Request.Form["socket_id"];
            MdlChat convo = new MdlChat()
            {
                sender_id = currentUser.ID,
                message = Request.Form["message"],
                receiver_id = contact
            };
            BLL_Chat obj = new BLL_Chat();
            obj.BLLInsertChat(convo);
            var conversationChannel = getConvoChannel(currentUser.ID, contact);
            pusher.TriggerAsync(
              conversationChannel,
              "new_message",
              convo,
              new TriggerOptions() { SocketId = socket_id });
            return Json(convo);
        }
        [HttpPost]
        public JsonResult MessageDelivered(int message_id)
        {
            MdlChat convo = null;
            BLL_Chat obj = new BLL_Chat();
            convo = obj.BLLReadSingleChat(message_id);         
                if (convo != null)
                {
                    convo.status = MdlChat.messageStatus.Delivered;
                    obj.BLLDeliveredChat(convo);
                }
            string socket_id = Request.Form["socket_id"];
            var conversationChannel = getConvoChannel(convo.sender_id, convo.receiver_id);
            pusher.TriggerAsync(
              conversationChannel,
              "message_delivered",
              convo,
              new TriggerOptions() { SocketId = socket_id });
            return Json(convo);
        }

        private String getConvoChannel(int user_id, int contact_id)
        {
            if (user_id > contact_id)
            {
                return "private-chat-" + contact_id + "-" + user_id;
            }
            return "private-chat-" + user_id + "-" + contact_id;
        }
    }
}