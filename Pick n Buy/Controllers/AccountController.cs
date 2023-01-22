using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OTM.Models;
using OTM.BLL;
using System.Net;
using System.Web.Security;
using System.Web.Helpers;
using System.Net.Mail;

namespace Pick_n_Buy.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult Register(MdlAccount mdl)
        {
            try
            {
                
                string message ;
                bool Status = false;
                if (mdl.Email != null && mdl.Password != null)
                {
                    BLL_User obj = new BLL_User();
                    MdlAccount IsExist = obj.BLL_IsEmailExist(mdl.Email);
                    if (IsExist.Email==null)
                    {
                        var hash = Crypto.HashPassword(mdl.Password);
                        mdl.Password = hash;
                        if (mdl.UserType)
                            mdl.IsTaylor = 1;
                        else
                            mdl.IsTaylor = 0;
                            Session["Sign_Up"] = mdl;
                        mdl.ActivationCode = Guid.NewGuid();
                        Session["Code"] = mdl.ActivationCode;
                        SendVerificationLinkEmail(mdl.Email, mdl.ActivationCode.ToString());
                        message = "We Have Send Verification Email to your email id:" + mdl.Email;
                        ViewBag.Message = message;
                        Status = true;
                        ViewBag.status = Status;
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "This Email Address is Already Exist Please Enter New");
                        return View(mdl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The User Name or Password is Invalid");
                    return View(mdl);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MdlAccount mdl)
        {
            try
            {
                if (mdl.Email.ToLower() == "saimrasoolkambo@gmail.com" && mdl.Password == "123456")
                {
                    return RedirectToAction("Index", "Admin");
                }
                if (mdl.Email != null && mdl.Password != null)
                {
                    BLL_User obj = new BLL_User();
                    MdlAccount Rtn = obj.BLL_IsEmailExist(mdl.Email);
                    if (Rtn.Email == mdl.Email && Crypto.VerifyHashedPassword(Rtn.Password, mdl.Password))
                    {
                        FormsAuthentication.SetAuthCookie(mdl.Email, true);
                        Session["UserID"] = Rtn.ID;
                        Session["user"] = Rtn;
                        if (Rtn.IsTaylor == 1)
                            return RedirectToAction("Index", "Seller");
                        else
                        {
                            Session["Order"] = null;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The User Name or Password is Invalid");
                        return View(mdl);
                    }
                }
                else
                    return View();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult VerifyUser(string id)
        {
            if (Session["Code"].ToString() == new Guid(id).ToString())
            {
                return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost]
        public ActionResult VerifyUser(MdlAccount mdl)
        {

            MdlAccount mdlAcc = new MdlAccount();
            mdlAcc = (MdlAccount)Session["Sign_Up"];
            mdl.Email = mdlAcc.Email;
            mdl.Password = mdlAcc.Password;
            mdl.IsTaylor = mdlAcc.IsTaylor;
            BLL_User obj = new BLL_User();
            obj.bllRegisterUser(mdl);
            return RedirectToAction("Login", "Account");

        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(MdlAccount user)
        {
            bool Status = false;
            string message = "";
            BLL_User obj = new BLL_User();
            MdlAccount IsExist = obj.BLL_IsEmailExist(user.Email);
            if (IsExist.Email == user.Email)
            {
                IsExist.ActivationCode = Guid.NewGuid();
                Session["Activation"] = IsExist.ActivationCode;
                Session["id"] = IsExist.ID; //Store the Admin Database ID in session storage then get when user reset ther password
                SendPasswordResetEmail(IsExist.Email, IsExist.ActivationCode.ToString());
                message = "Account Reset Password link " +
                        " has been Successfully sent to your email id:" + user.Email;
                Status = true;
            }
            else
            {
                message = "This Email Address is Not Register";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Account/VerifyUser/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("tourismadvertisement@gmail.com", "OTM Team");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "takpk123"; // Replace with actual password
            string subject = "Verify Email Address!";

            string body = "<br/><br/>Thankyou for Registering" +
                "Please click on the below Button to confirm you Account" +
                "<br/><br/><a class='btn btn-primary'href='" + link + "'>Confirm</a>";

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

        }

        [NonAction]
        public void SendPasswordResetEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Account/Resetpassword/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("tourismadvertisement@gmail.com", "OTM Team");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "takpk123"; // Replace with actual password
            string subject = "Reset Password Link!";

            string body = "<br/><br/>This is Your Account Reset Password Link Please click on the below link to Change your account Password <br/><br/><a href='" + link + "'>" + link + "</a>";
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

        }

        public ActionResult Resetpassword(string id)
        {
            if (Session["Activation"].ToString() == new Guid(id).ToString())
            {
                return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost]
        public ActionResult Resetpassword(MdlAccount usr)
        {
            BLL_User obj = new BLL_User();
            usr.ID = Convert.ToInt32(Session["id"]);
            var hash = Crypto.HashPassword(usr.Password);
            usr.Password = hash;
            obj.BLL_ChangePassword(usr);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }





    }
}