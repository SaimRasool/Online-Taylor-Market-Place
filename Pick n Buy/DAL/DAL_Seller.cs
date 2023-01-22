using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OTM.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OTM.DAL
{
    public class DAL_Seller
    {
        private string _ConnString;

        #region Constructor
        public DAL_Seller()
        {
            _ConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        }
        #endregion

        #region Parameters

        const String PARM_User_About = "@Description";
        const String PARM_User_Logo = "@Logo";
        const String PARM_USER_Company = "@Company";
        const String PARM_USER_Address = "@Address";
        const String PARM_USER_ID = "@ID";

        #endregion

        #region Query 

        const String SQL_Is_Profile = "SPW_IsProfile";
        const String SQL_Seller_ProfileComlete = "SPW_CompleteSellerProfile";
        const String SQL_Update_Category = "SPW_Update_Category";
        const String SQL_Get_Category = "SPW_GetCategory";
        const String SQL_Delete_Category = "SPW_DeleteCategory";
        const String SQL_Read_AllSeller = "SPW_ReadAllSellerUser";


        #endregion

        public MdlSeller DAL_IsProfileComplete(int ID)
        {
            try
            {
                MdlSeller RtrnVal = new MdlSeller();
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = ID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Is_Profile, parm);
                foreach (DataRow oRow in oTable.Rows)
                {
                    RtrnVal.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    RtrnVal.Comapny = Model.Common.CheckStringNull(oRow["CompanyName"]);
                    RtrnVal.Address = Model.Common.CheckStringNull(oRow["Address"]);
                    RtrnVal.About = Model.Common.CheckStringNull(oRow["About"]);
                    RtrnVal.Logo = Model.Common.CheckStringNull(oRow["Logo"]);
                }

                return RtrnVal;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Dal_CompleteProfile(MdlSeller mdl)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
            {
                Value = mdl.ID
            };
            parm[1] = new SqlParameter(PARM_USER_Address, SqlDbType.NVarChar)
            {
                Value = mdl.Address
            };
            parm[2] = new SqlParameter(PARM_USER_Company, SqlDbType.NVarChar)
            {
                Value = mdl.Comapny
            };
            parm[3] = new SqlParameter(PARM_User_Logo, SqlDbType.NVarChar)
            {
                Value = mdl.Logo
            };
            parm[4] = new SqlParameter(PARM_User_About, SqlDbType.NVarChar)
            {
                Value = mdl.About
            };
            SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Seller_ProfileComlete, parm);

        }

        public List<MdlAccount> DAL_ReadAllSeller()
        {
            try
            {
                List<MdlAccount> RtrnVal = new List<MdlAccount>();
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Read_AllSeller, null);
                foreach (DataRow oRow in oTable.Rows)
                {
                    MdlAccount sel = new MdlAccount();
                    sel.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    sel.Email = Model.Common.CheckStringNull(oRow["Email"]);
                    sel.FName = Model.Common.CheckStringNull(oRow["Fname"]);
                    sel.Lname = Model.Common.CheckStringNull(oRow["Lname"]);
                    sel.City = Model.Common.CheckStringNull(oRow["City"]);
                    sel.Phone = Model.Common.CheckLongNull(oRow["Phone"]);
                    RtrnVal.Add(sel);
                }

                return RtrnVal;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}