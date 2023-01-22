using OTM.DAL;
using OTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTM.BLL
{
    public class Bll_Order
    {
        public void Bll_PlaceOrder(List<MdlOrder> mdlList,int ID,MdlBillingAddress mdl)
        {
            DAL_Order obj = new DAL_Order();
            obj.Dal_PlaceOrder(mdlList, ID,mdl);
        }

    }
}