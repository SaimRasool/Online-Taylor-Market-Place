using OTM.DAL;
using OTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTM.BLL
{
    public class BLL_Category
    {
        public void Bll_Add_Category(MdlCategory mdl)
        {
            DAL_Category obj = new DAL_Category();
            obj.Dal_Add_Category(mdl);
        }

        public List<MdlCategory> BLL_ReadCategory()
        {

                DAL_Category obj = new DAL_Category();
                return obj.DAL_ReadCategory();

        }

        public void BLL_UpdateCategory(MdlCategory mdl)
        {

            DAL_Category obj = new DAL_Category();
            obj.Dal_Update_Category(mdl);
        }

        public MdlCategory BLL_GetCategory(int ID)
        {

            DAL_Category obj = new DAL_Category();
            return obj.DAL_GetCategory(ID);

        }

        public void Bll_Delete_Category(int ID)
        {
            DAL_Category obj = new DAL_Category();
            obj.DLL_Delete_Category(ID);
        }

    }
}