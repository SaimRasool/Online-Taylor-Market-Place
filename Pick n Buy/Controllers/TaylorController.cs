using OTM.BLL;
using OTM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OTM.Controllers
{
    [Authorize]
    public class TaylorController : Controller
    {
        // GET: Taylor
        public ActionResult Taylor()
        {
            try
            {
                BLL_Taylor obj = new BLL_Taylor();
                List<MdlTaylor> mdlist = obj.BLL_ReadTaylor(Convert.ToInt32(Session["UserID"]));
                return View(mdlist);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult AddTaylor()
        {
                return View();
        }
        [HttpPost]
        public ActionResult AddTaylor(MdlTaylor mdl, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                var allowedextention = new[] { ".jpg", ".Jpg", ".jpeg", ".png" };
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var ext = Path.GetExtension(file.FileName);
                        if (allowedextention.Contains(ext))
                        {
                            var filename = Path.GetFileName(file.FileName);
                            var path = "~/images/Taylor_Images/" + filename;
                            file.SaveAs(Server.MapPath(path));
                            if (i == 0)
                                mdl.Image = path.Replace("~/", "/../");
                            else if (i == 1)
                                mdl.S1Image = path.Replace("~/", "/../");
                            else if (i == 2)
                                mdl.S2Image = path.Replace("~/", "/../");
                            else 
                                mdl.S3Image = path.Replace("~/", "/../");
                        }
                    }
                }
               
                if (ModelState.IsValid)
                {
                    mdl.ID = Convert.ToInt32(Session["UserID"]);
                    BLL_Taylor obj = new BLL_Taylor();
                    obj.BLL_AddTaylor(mdl);
                    ViewBag.Message = "Taylor Added Successfully";
                    return View();
                }
                else
                    return View();
                
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult AddPortfolio()
        {

            try
            {
                BLL_Taylor obj = new BLL_Taylor();
                List<MdlTaylor> mdlist = obj.BLL_ReadTaylor(Convert.ToInt32(Session["UserID"]));
                Mdl_Portfolio mdl = new Mdl_Portfolio();
                mdl.TaylorList = mdlist;
                return View(mdl);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public ActionResult AddPortfolio(Mdl_Portfolio mdl , HttpPostedFileBase file)
        {
            var allowedextention = new[] { ".jpg", ".Jpg", ".jpeg", ".png" };
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName);
                if (allowedextention.Contains(ext))
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = "~/Images/Portfolio/" + filename + ext;
                    file.SaveAs(Server.MapPath(path));
                    mdl.Image = path.Replace("~/", "/../");
                }
               
            }
            if (ModelState.IsValid)
            {
                BLL_Taylor obj = new BLL_Taylor();
                obj.BLL_AddPortfolio(mdl);
                ViewBag.Message = "Portfolio Save Successfully";
                List<MdlTaylor> mdlist = obj.BLL_ReadTaylor(Convert.ToInt32(Session["UserID"]));
                mdl.TaylorList = mdlist;
                return View(mdl);
            }
            else
                return RedirectToAction("AddPortfolio");
        }

        public ActionResult Portfolio(int ID)
        {
            Session["TaylorID"] = ID;
            BLL_Taylor obj = new BLL_Taylor();
            MdlTaylor mdl = obj.BLL_ReadSingleTaylor(ID);
            return View(mdl);
        }

        public ActionResult PortfolioPartial()
        {
            BLL_Taylor obj = new BLL_Taylor();
            List<Mdl_Portfolio> mdl = obj.BLL_ReadPortfolio(Convert.ToInt32(Session["TaylorID"]));
            return PartialView(mdl);
        }
    }
}