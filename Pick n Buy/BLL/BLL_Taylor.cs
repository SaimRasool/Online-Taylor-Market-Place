using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OTM.DAL;
using OTM.Models;

namespace OTM.BLL
{
    public class BLL_Taylor
    {
        public void BLL_AddTaylor(MdlTaylor mdl)
        {
            try
            {
                DAL_Taylor obj = new DAL_Taylor();
                obj.Dal_AddTaylor(mdl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MdlTaylor> BLL_ReadTaylor(int ID)
        {
            try
            {
                DAL_Taylor obj = new DAL_Taylor();
                return obj.Dal_ReadTaylor(ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MdlTaylor> BLL_ReadAllTaylor()
        {
            try
            {
                DAL_Taylor obj = new DAL_Taylor();
                return obj.Dal_ReadAllTaylor();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MdlTaylor BLL_ReadSingleTaylor(int ID)
        {
            try
            {
                DAL_Taylor obj = new DAL_Taylor();
                return obj.Dal_ReadSingleTaylor(ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void BLL_AddPortfolio(Mdl_Portfolio mdl)
        {
            try
            {
                DAL_Taylor obj = new DAL_Taylor();
                obj.Dal_AddPortfolio(mdl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Mdl_Portfolio> BLL_ReadPortfolio(int ID)
        {
            try
            {
                DAL_Taylor obj = new DAL_Taylor();
                return obj.Dal_ReadPortfolio(ID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}