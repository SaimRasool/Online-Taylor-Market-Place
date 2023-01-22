using OTM.DAL;
using OTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTM.BLL
{
    public class BLL_Product
    {
        public void Bll_Add_Product(MdlProduct mdl)
        {
            DAL_Product obj = new DAL_Product();
            obj.Dal_Add_Product(mdl);
        }

        public List<MdlProduct> BLL_Read_Product()
        {

            DAL_Product obj = new DAL_Product();
            return obj.DAL_Read_Product();

        }

        public List<MdlProduct> BLL_ReadClient_Product(int ID)
        {

            DAL_Product obj = new DAL_Product();
            return obj.DAL_ReadClient_Product(ID);

        }

        public MdlProduct BLL_ReadSingleClient_Product(int ID)
        {

            DAL_Product obj = new DAL_Product();
            return obj.DAL_ReadSingleClient_Product(ID);

        }

        public void Bll_Delete_Product(int ID)
        {
            DAL_Product obj = new DAL_Product();
            obj.DLL_Delete_Product(ID);
        }

        public void BLL_Update_Product(MdlProduct mdl)
        {

            DAL_Product obj = new DAL_Product();
            obj.Dal_Update_Product(mdl);
        }

    }
}