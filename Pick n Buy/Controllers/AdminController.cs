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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category()
        {
            try
            {
                BLL_Category obj = new BLL_Category();
                List<MdlCategory> CategoriesList = obj.BLL_ReadCategory();
                return View(CategoriesList);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public ActionResult UpdateCategory(int ID)
        {
            try
            {
                BLL_Category obj = new BLL_Category();
                MdlCategory Category = obj.BLL_GetCategory(ID);
                return View(Category);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult UpdateCategory(MdlCategory mdl, HttpPostedFileBase file)
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
                        var path = "~/Images/Category_Image/" + filename+ext;
                        file.SaveAs(Server.MapPath(path));
                        mdl.Thumbnail = path.Replace("~/", "/../");
                    }
                }
                if (ModelState.IsValid)
                {
                    BLL_Category obj = new BLL_Category();
                    obj.BLL_UpdateCategory(mdl);
                    return RedirectToAction("Category", "Admin");
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(MdlCategory mdl, HttpPostedFileBase file)
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
                            var path = "~/Images/Category_Image/" + filename+ext;
                            file.SaveAs(Server.MapPath(path));
                            mdl.Thumbnail = path.Replace("~/", "/../");
                        }
                    }
                    if (ModelState.IsValid)
                    {
                        BLL_Category obj = new BLL_Category();
                        obj.Bll_Add_Category(mdl);
                        return RedirectToAction("Category", "Admin");
                    }
                    return View();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public ActionResult DeleteCategory(int ID)
        {
            try
            {
                BLL_Category obj = new BLL_Category();
                obj.Bll_Delete_Category(ID);
                return RedirectToAction("Category", "Admin");
            }
            catch (Exception)
            {
                
                throw;
            }
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
                MdlProduct mdl=new MdlProduct();
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
                var allowedextention = new[] { ".jpg", ".Jpg", ".jpeg", ".png" };
                if (file != null)
                {
                    var ext = Path.GetExtension(file.FileName);
                    if (allowedextention.Contains(ext))
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = "~/Images/Product_Image/" + filename+ext;
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
    }
}