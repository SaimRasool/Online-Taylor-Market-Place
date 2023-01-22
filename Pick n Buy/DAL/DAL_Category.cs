using OTM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OTM.DAL
{
    public class DAL_Category
    {
        private string _ConnString;

        #region Constructor
        public DAL_Category()
        {
            _ConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        }
        #endregion

        #region Parameters

        const String PARM_CATEGORY_name = "@Name";
        const String PARM_CATEGORY_Description = "@Description";
        const String PARM_CATEGORY_Thumbnail = "@Thumbnail";
        const String PARM_CATEGORY_ID = "@ID";

        #endregion

        #region Query 

        const String SQL_Add_Category = "SPW_Category";
        const String SQL_Read_Category = "SPW_Read_All_Category";
        const String SQL_Update_Category = "SPW_Update_Category";
        const String SQL_Get_Category = "SPW_GetCategory";
        const String SQL_Delete_Category = "SPW_DeleteCategory";


        #endregion

        public void Dal_Add_Category(MdlCategory mdl)
        {
            try
            {
                
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter(PARM_CATEGORY_name, SqlDbType.NVarChar)
            {
                Value = mdl.Name
            };
            parm[1] = new SqlParameter(PARM_CATEGORY_Description, SqlDbType.NVarChar)
            {
                Value = mdl.Description
            };
            parm[2] = new SqlParameter(PARM_CATEGORY_Thumbnail, SqlDbType.NVarChar)
            {
                Value = mdl.Thumbnail
            };

            SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Add_Category, parm);

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<MdlCategory> DAL_ReadCategory()
        {
            try
            {
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Read_Category, null);
                List<MdlCategory> CategoriesList = new List<MdlCategory>();
                foreach (DataRow oRow in oTable.Rows)
                {
                    MdlCategory ctg = new MdlCategory();
                    ctg.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    ctg.Description = Model.Common.CheckStringNull(oRow["Description"]);
                    ctg.Name = Model.Common.CheckStringNull(oRow["Name"]);
                    ctg.Thumbnail = Model.Common.CheckStringNull(oRow["Thumbnail"]);
                    CategoriesList.Add(ctg);
                }
                return CategoriesList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Dal_Update_Category(MdlCategory mdl)
        {
            try
            {

                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter(PARM_CATEGORY_name, SqlDbType.NVarChar)
                {
                    Value = mdl.Name
                };
                parm[1] = new SqlParameter(PARM_CATEGORY_Description, SqlDbType.NVarChar)
                {
                    Value = mdl.Description
                };
                parm[2] = new SqlParameter(PARM_CATEGORY_Thumbnail, SqlDbType.NVarChar)
                {
                    Value = mdl.Thumbnail
                };
                parm[3] = new SqlParameter(PARM_CATEGORY_ID, SqlDbType.Int)
                {
                    Value = mdl.ID
                };
                SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Update_Category, parm);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public MdlCategory DAL_GetCategory(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_CATEGORY_ID, SqlDbType.NVarChar)
                {
                    Value = ID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Get_Category, parm);
                MdlCategory ctg = new MdlCategory();
                foreach (DataRow oRow in oTable.Rows)
                {
                    ctg.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    ctg.Description = Model.Common.CheckStringNull(oRow["Description"]);
                    ctg.Name = Model.Common.CheckStringNull(oRow["Name"]);
                    ctg.Thumbnail = Model.Common.CheckStringNull(oRow["Thumbnail"]);
                }
                return ctg;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DLL_Delete_Category(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@ID", SqlDbType.Int)
                {
                    Value = ID
                };
                SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Delete_Category, parm);
                
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}