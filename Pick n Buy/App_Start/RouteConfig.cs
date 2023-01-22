using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Pick_n_Buy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "ChatRoom",
            url: "chat",
            defaults: new { controller = "Chat", action = "Index" }
               );

            routes.MapRoute(
                name: "GetContactConversations",
                url: "contact/conversations/{contact}",
                defaults: new { controller = "Chat", action = "ConversationWithContact", contact = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PusherAuth",
                url: "Chat/AuthForChannel",
                defaults: new { controller = "Chat", action = "AuthForChannel" }
            );

            routes.MapRoute(
                name: "SendMessage",
                url: "send_message",
                defaults: new { controller = "Chat", action = "SendMessage" }
            );

            routes.MapRoute(
                name: "MessageDelivered",
                url: "MessageDelivered/{message_id}",
                defaults: new { controller = "Chat", action = "MessageDelivered", message_id = UrlParameter.Optional }
            );
        }
    }
}
