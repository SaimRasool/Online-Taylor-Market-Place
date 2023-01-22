using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OTM.DAL;
using OTM.Models;

namespace OTM.BLL
{
    public class BLL_Seller
    {
        public MdlSeller BLL_IsProfileComplete(int ID)
        {
            DAL_Seller obj = new DAL_Seller();
            return obj.DAL_IsProfileComplete(ID);              
        }
        public void BLL_CompleteProfile(MdlSeller mdl)
        {
            DAL_Seller obj = new DAL_Seller();
             obj.Dal_CompleteProfile(mdl);

        }
        public List<MdlAccount> BLL_ReadAllSeller()
        {
            DAL_Seller obj = new DAL_Seller();
            return obj.DAL_ReadAllSeller();
        }
    }
}