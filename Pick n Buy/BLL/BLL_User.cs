using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OTM.Models;
using OTM.DAL;
namespace OTM.BLL
{
    public class BLL_User
    {
        public void bllRegisterUser(MdlAccount mdl)
        {
            DAL_User obj = new DAL_User();
            obj.DalRegisterUser(mdl);
        }

        public MdlAccount bllLoginUser(MdlAccount mdl)
        {
            DAL_User obj = new DAL_User();
           return obj.DalLoginUser(mdl);
        }

        public MdlAccount bllReadUser(int ID)
        {
            DAL_User obj = new DAL_User();
            return obj.DalReadUser(ID);
        }

        public MdlAccount BLL_IsEmailExist(string str)
        {
            try
            {
                DAL_User obj = new DAL_User();
                return obj.Dal_IsEmailExist(str);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool BLL_ChangePassword(MdlAccount user)
        {
            DAL_User obj = new DAL_User();
            if (obj.Dal_ChangePassword(user))
                return true;
            else
                return false;
        }

        public List<MdlAccount> bllReadUserChatWith(int ID)
        {
            DAL_User obj = new DAL_User();
            return obj.DAL_ReadUserChatWith(ID);
        }


    }
}