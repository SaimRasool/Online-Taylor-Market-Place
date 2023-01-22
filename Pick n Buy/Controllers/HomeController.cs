using OTM.BLL;
using OTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Pick_n_Buy.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(MdlContact obj)
        {
            try
            {
                  if (ModelState.IsValid)
                {
                    if (SendEmail(obj))
                    ViewBag.Message = "Your Message is Deliverd Successfully";
                    return View(obj);
                }
                else
                    return View();
            }
            catch (Exception)
            {
                
                throw;
            }
        
        }

        //Partial View of Cart
        public ActionResult Cartinfo()
        {
            List<MdlOrder> ordList = new List<MdlOrder>();
               if (User.Identity.IsAuthenticated)
               {
                   if (Session["Order"] != null)
                   {
                       ordList = (List<MdlOrder>)Session["Order"];
                       return PartialView(ordList);
                   }
                   else
                   {
                       return PartialView(ordList);
                   }
            }
            else
            {
                return PartialView(ordList);
            }
        }

        public bool SendEmail(MdlContact obj)
        {
            var fromEmail = new MailAddress("tourismadvertisement@gmail.com", obj.Email);
            var toEmail = new MailAddress("tourismadvertisement@gmail.com");
            var fromEmailPassword = "takpk123";
            string subject = "Vistor Message";

            string body = "<br/><br/> My Name is " + obj.Name +"<br/>"+ obj.Message + "<br/><br/><a href='mailto:" + obj.Email + "'target='_top'>Reply</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
            return true;
        }

    }
}