using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using OTM.Models;
using System.Data.SqlClient;
using System.Data;

namespace OTM.DAL
{
    public class DAL_User
    {
        private string _ConnString;

        #region Query
        
        const String SQL_Register_User = "SPW_SignUP";
        const String SQL_Login_User = "SPW_IsUserExist";
        const String SQL_Read_User = "SPW_ReadUser";
        const String SQL_Email_Exist = "SPW_IsEmailExist";
        const String SQL_User_ChangePassword = "SPW_ChangePassword";
        const String SQL_User_ThatChat = "SPW_ReadUserChatWith";


        #endregion

        #region Parameters

        const String PARM_USER_ID = "@ID";
        const String PARM_USER_Fname = "@Fname";
        const String PARM_USER_Lname = "@Lname";
        const String PARM_USER_Email = "@Email";
        const String PARM_USER_Password = "@Password";

        const String PARM_USER_City = "@City";
        const String PARM_USER_Country = "@Country";
        const String PARM_USER_Phone = "@Phone";
        const String PARM_IsAdmin = "@IsTaylor";
        #endregion

        #region Constructor
        public DAL_User()
        {
            _ConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        }
        #endregion

        public void DalRegisterUser(MdlAccount mdl)
        {
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter(PARM_USER_Fname, SqlDbType.NVarChar)
            {
                Value = mdl.FName
            };
            parm[1] = new SqlParameter(PARM_USER_Lname, SqlDbType.NVarChar)
            {
                Value = mdl.Lname
            };
            parm[2] = new SqlParameter(PARM_USER_Email, SqlDbType.NVarChar)
            {
                Value = mdl.Email
            };
            parm[3] = new SqlParameter(PARM_USER_Password, SqlDbType.NVarChar)
            {
                Value = mdl.Password
            };
            parm[4] = new SqlParameter(PARM_USER_City, SqlDbType.NVarChar)
            {
                Value = mdl.City
            };
            parm[5] = new SqlParameter(PARM_USER_Phone, SqlDbType.BigInt)
            {
                Value = mdl.Phone
            };
            parm[6] = new SqlParameter(PARM_IsAdmin, SqlDbType.Int)
            {
                Value = mdl.IsTaylor
            };
            SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Register_User, parm);

        }

        public MdlAccount DalLoginUser(MdlAccount mdl)
        {
            try
            {
                MdlAccount RtrnVal = new MdlAccount();
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter(PARM_USER_Email, SqlDbType.NVarChar)
                {
                    Value = mdl.Email
                };
                parm[1] = new SqlParameter(PARM_USER_Password, SqlDbType.NVarChar)
                {
                    Value = mdl.Password
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Login_User, parm);
                foreach (DataRow oRow in oTable.Rows)
                {
                    RtrnVal.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    RtrnVal.Email = Model.Common.CheckStringNull(oRow["Email"]);
                    RtrnVal.Password = Model.Common.CheckStringNull(oRow["Password"]);
                    RtrnVal.IsTaylor = Model.Common.CheckIntegerNull(oRow["IsTaylor"]);
                    RtrnVal.FName = Model.Common.CheckStringNull(oRow["Fname"]);

                }

                return RtrnVal;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public MdlAccount DalReadUser(int ID)
        {
            try
            {
                MdlAccount RtrnVal = new MdlAccount();
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = ID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Read_User, parm);
                foreach (DataRow oRow in oTable.Rows)
                {
                    RtrnVal.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    RtrnVal.Email = Model.Common.CheckStringNull(oRow["Email"]);
                    RtrnVal.FName = Model.Common.CheckStringNull(oRow["Fname"]);
                    RtrnVal.Lname = Model.Common.CheckStringNull(oRow["Lname"]);
                    RtrnVal.City = Model.Common.CheckStringNull(oRow["City"]);
                    RtrnVal.Phone = Model.Common.CheckLongNull(oRow["Phone"]);

                }

                return RtrnVal;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public MdlAccount Dal_IsEmailExist(string str)
        {
            MdlAccount RtrnVal = new MdlAccount();
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_USER_Email, SqlDbType.NVarChar)
                {
                    Value = str
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Email_Exist, parm);
                foreach (DataRow oRow in oTable.Rows)
                {
                    RtrnVal.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    RtrnVal.Email = Model.Common.CheckStringNull(oRow["Email"]);
                    RtrnVal.Password = Model.Common.CheckStringNull(oRow["Password"]);
                    RtrnVal.IsTaylor = Model.Common.CheckIntegerNull(oRow["IsTaylor"]);
                }
                return RtrnVal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Dal_ChangePassword(MdlAccount obj)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = obj.ID
                };
                parm[1] = new SqlParameter(PARM_USER_Password, SqlDbType.NVarChar)
                {
                    Value = obj.Password
                };
                SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_User_ChangePassword, parm);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MdlAccount> DAL_ReadUserChatWith(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = ID
                };
                List<MdlAccount> RtrnVal = new List<MdlAccount>();
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_User_ThatChat, parm);
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