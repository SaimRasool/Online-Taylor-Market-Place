using OTM.BLL;
using OTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pick_n_Buy.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Product(int ID)
        {
            try
            {
                BLL_Product obj = new BLL_Product();
                List<MdlProduct> ProductList = obj.BLL_ReadClient_Product(ID);
                return View(ProductList);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Category()
        {
            try
            {
                BLL_Category obj = new BLL_Category();
                List<MdlCategory> CategoriesList = obj.BLL_ReadCategory();
                return PartialView(CategoriesList);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult PrdouctDiscription(int ID)
        {
            try
            {
                ViewBag.Message = null;
                BLL_Product obj = new BLL_Product();
                MdlProduct Product = obj.BLL_ReadSingleClient_Product(ID);
                return View(Product);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult PrdouctDiscription(MdlProduct mdl)
        {
            try
            {
               if (User.Identity.IsAuthenticated)
               {
                   MdlOrder ord = new MdlOrder();
                   List<MdlOrder> ordList = new List<MdlOrder>();
                   ord.name = mdl.Name;
                   ord.Quantity = mdl.Quantity;
                   ord.UnitPrice = mdl.UnitPrice;
                   ord.Total = ord.UnitPrice * ord.Quantity;
                   if(Session["Order"]!=null)
                   {
                       ordList = (List<MdlOrder>)Session["Order"];
                       ordList.Add(ord);
                       Session["Order"] = ordList;
                   }
                   else
                   {
                       ordList.Add(ord);
                       Session["Order"] = ordList;
                   }         
                   return RedirectToAction("Cart", "Client");
               }
               else
               {     
                   ViewBag.Message="Please Login First!!!!";
                   return View(mdl);               }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Cart()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Order"] != null)
                {
                    List<MdlOrder> ordList = new List<MdlOrder>();
                    ordList = (List<MdlOrder>)Session["Order"];
                    return View(ordList);
                }
                else
                {
                    ViewBag.Message = "Cart is empty.....Nothing to be Added in Cart";
                    return View();
                }
            }
            else
            {
                ViewData["Message"] = "Please Login First!!!!";
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult GetAddresess()
        {
            BLL_User obj = new BLL_User();
            MdlBillingAddress mdl = new MdlBillingAddress();
            mdl.User = obj.bllReadUser((int)Session["UserID"]);
            return View(mdl);
        }
        [HttpPost]
        public ActionResult GetAddresess(MdlBillingAddress mdl)
        {
            List<MdlOrder> ordList = new List<MdlOrder>();
            ordList = (List<MdlOrder>)Session["Order"];
            Bll_Order obj = new Bll_Order();
            obj.Bll_PlaceOrder(ordList, (int)Session["UserID"],mdl);
            Session["Order"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Taylor()
        {
            BLL_Taylor obj = new BLL_Taylor();
            List<MdlTaylor> mdl = obj.BLL_ReadAllTaylor();
            return PartialView(mdl);
        }


    }
}