using OTM.BLL;
using OTM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pick_n_Buy.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        // GET: Taylor
        public ActionResult Index()
        {
            int ID = Convert.ToInt32(Session["UserID"]);
            BLL_Seller obj = new BLL_Seller();
            MdlSeller mdl=obj.BLL_IsProfileComplete(ID);
            Session["SID"] = mdl.ID;
            return View();
        }
        [HttpPost]
        public ActionResult Index(MdlSeller mdl, HttpPostedFileBase file)
        {
            var allowedextention = new[] { ".jpg", ".Jpg", ".jpeg", ".png" };
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName);
                if (allowedextention.Contains(ext))
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = "~/Images/SellerLogo/" + filename + ext;
                    file.SaveAs(Server.MapPath(path));
                    mdl.Logo = path.Replace("~/", "/../");
                }
            }
            if (ModelState.IsValid)
            {
                mdl.ID = Convert.ToInt32(Session["UserID"]);
                BLL_Seller obj = new BLL_Seller();
                obj.BLL_CompleteProfile(mdl);
                return View();
            }
            else
                return View();
        }

        public ActionResult Product()
        {
            try
            {
                BLL_Product obj = new BLL_Product();
                List<MdlProduct> ProductList = obj.BLL_Read_Product();
                return View(ProductList);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult AddProduct()
        {

            try
            {
                BLL_Category obj = new BLL_Category();
                MdlProduct mdl = new MdlProduct();
                mdl.CategoryList = obj.BLL_ReadCategory();
                return View(mdl);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult AddProduct(MdlProduct mdl, HttpPostedFileBase file)
        {

            try
            {
                mdl.ID = Convert.ToInt32(Session["UserID"]);
                var allowedextention = new[] { ".jpg", ".Jpg", ".jpeg", ".png" };
                if (file != null)
                {
                    var ext = Path.GetExtension(file.FileName);
                    if (allowedextention.Contains(ext))
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = "~/Images/Product_Image/" + filename + ext;
                        file.SaveAs(Server.MapPath(path));
                        mdl.Thumbnail = path.Replace("~/", "/../");
                    }
                }
                if (ModelState.IsValid)
                {
                    BLL_Product obj = new BLL_Product();
                    obj.Bll_Add_Product(mdl);
                    return RedirectToAction("Product", "Admin");
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult DeleteProduct(int ID)
        {
            try
            {
                BLL_Product obj = new BLL_Product();
                obj.Bll_Delete_Product(ID);
                return RedirectToAction("Product", "Seller");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult UpdateProduct(int ID)
        {
            try
            {
                BLL_Category obj1 = new BLL_Category();
                BLL_Product obj = new BLL_Product();
                MdlProduct product = obj.BLL_ReadSingleClient_Product(ID);
                product.CategoryList = obj1.BLL_ReadCategory();
                return View(product);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult UpdateProduct(MdlProduct mdl, HttpPostedFileBase file)
        {
            try
            {
                var allowedextention = new[] { ".jpg", ".Jpg", ".jpeg", ".png" };
                if (file != null)
                {
                    var ext = Path.GetExtension(file.FileName);
                    if (allowedextention.Contains(ext))
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = "~/Images/Category_Image/" + filename + ext;
                        file.SaveAs(Server.MapPath(path));
                        mdl.Thumbnail = path.Replace("~/", "/../");
                    }
                }
                if (ModelState.IsValid)
                {
                    BLL_Product obj = new BLL_Product();
                    obj.BLL_Update_Product(mdl);
                    return RedirectToAction("Product", "Seller");
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult ChatWithCoustomer()
        {
            if (Session["user"] == null)
            {
                return Redirect("/");
            }
            var currentUser = (MdlAccount)Session["user"];
            BLL_User obj = new BLL_User();
            ViewBag.allUsers = obj.bllReadUserChatWith(currentUser.ID);
            ViewBag.currentUser = currentUser;
            return View();
        }


    }
}