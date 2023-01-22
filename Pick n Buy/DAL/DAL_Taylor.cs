using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using OTM.Models;

namespace OTM.DAL
{
    public class DAL_Taylor
    {
        private string _ConnString;

        #region Constructor
        public DAL_Taylor()
        {
            _ConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        }
        #endregion

        #region Parameters

        const String PARM_Taylor_About = "@About";
        const String PARM_Taylor_Image = "@Image";
        const String PARM_Taylor_Name = "@Name";
        const String PARM_Taylor_ID = "@ID";
        const String PARM_USER_ID = "@ID";
        const String PARM_Taylor_Skill1 = "@Skill1";
        const String PARM_Taylor_Skill2 = "@Skill2";
        const String PARM_Taylor_Skill3 = "@Skill3";
        const String PARM_Taylor_S1Des = "@S1Des";
        const String PARM_Taylor_S2Des = "@S2Des";
        const String PARM_Taylor_S3Des = "@S3Des";
        const String PARM_Taylor_S1Image = "@S1Image ";
        const String PARM_Taylor_S2Image = "@S2Image ";
        const String PARM_Taylor_S3Image = "@S3Image ";

        #endregion

        #region Query 

        const String SQL_Add_Taylor = "SPW_AddTaylor";
        const String SQL_Add_Portfolio = "SPW_AddPortfolio";
        const String SQL_Read_Taylor = "SPW_ReadTaylor";
        const String SQL_ReadAll_Taylor = "SPW_ReadAllTaylor";
        const String SQL_ReadSingleAll_Taylor = "SPW_ReadSingleTaylor";
        const String SQL_Read_Portfolio = "SPW_ReadPortfolio";

        #endregion

        public List<MdlTaylor> Dal_ReadTaylor(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = ID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Read_Taylor, parm);
                List<MdlTaylor> TaylorList = new List<MdlTaylor>();
                foreach (DataRow oRow in oTable.Rows)
                {
                    MdlTaylor tl = new MdlTaylor();
                    tl.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    tl.About = Model.Common.CheckStringNull(oRow["About"]);
                    tl.Name = Model.Common.CheckStringNull(oRow["Name"]);
                    tl.Image = Model.Common.CheckStringNull(oRow["ProfileImage"]);
                    TaylorList.Add(tl);
                }
                return TaylorList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Dal_AddTaylor(MdlTaylor mdl)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[13];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = mdl.ID
                };
                parm[1] = new SqlParameter(PARM_Taylor_Name, SqlDbType.NVarChar)
                {
                    Value = mdl.Name
                };
                parm[2] = new SqlParameter(PARM_Taylor_Image, SqlDbType.NVarChar)
                {
                    Value = mdl.Image
                };
                parm[3] = new SqlParameter(PARM_Taylor_About, SqlDbType.NVarChar)
                {
                    Value = mdl.About
                };
                parm[4] = new SqlParameter(PARM_Taylor_Skill1, SqlDbType.NVarChar)
                {
                    Value = mdl.Skill1
                };
                parm[5] = new SqlParameter(PARM_Taylor_Skill2, SqlDbType.NVarChar)
                {
                    Value = mdl.Skill2
                };
                parm[6] = new SqlParameter(PARM_Taylor_Skill3, SqlDbType.NVarChar)
                {
                    Value = mdl.Skill3
                };
                parm[7] = new SqlParameter(PARM_Taylor_S1Image, SqlDbType.NVarChar)
                {
                    Value = mdl.S1Image
                };
                parm[8] = new SqlParameter(PARM_Taylor_S2Image, SqlDbType.NVarChar)
                {
                    Value = mdl.S2Image
                };
                parm[9] = new SqlParameter(PARM_Taylor_S3Image, SqlDbType.NVarChar)
                {
                    Value = mdl.S3Image
                };
                parm[10] = new SqlParameter(PARM_Taylor_S1Des, SqlDbType.NVarChar)
                {
                    Value = mdl.S1Des
                };
                parm[11] = new SqlParameter(PARM_Taylor_S2Des, SqlDbType.NVarChar)
                {
                    Value = mdl.S2Des
                };
                parm[12] = new SqlParameter(PARM_Taylor_S3Des, SqlDbType.NVarChar)
                {
                    Value = mdl.S3Des
                };
                SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Add_Taylor, parm);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<MdlTaylor> Dal_ReadAllTaylor()
        {
            try
            {
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_ReadAll_Taylor, null);
                List<MdlTaylor> TaylorList = new List<MdlTaylor>();
                foreach (DataRow oRow in oTable.Rows)
                {
                    MdlTaylor tl = new MdlTaylor();
                    tl.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    tl.About = Model.Common.CheckStringNull(oRow["About"]);
                    tl.Name = Model.Common.CheckStringNull(oRow["Name"]);
                    tl.Image = Model.Common.CheckStringNull(oRow["ProfileImage"]);
                    tl.Skill1 = Model.Common.CheckStringNull(oRow["Skill1"]);
                    TaylorList.Add(tl);
                }
                return TaylorList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public MdlTaylor Dal_ReadSingleTaylor(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = ID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_ReadSingleAll_Taylor, parm);
                MdlTaylor tl = new MdlTaylor(); foreach (DataRow oRow in oTable.Rows)
                {

                    tl.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    tl.About = Model.Common.CheckStringNull(oRow["About"]);
                    tl.Name = Model.Common.CheckStringNull(oRow["Name"]);
                    tl.Image = Model.Common.CheckStringNull(oRow["ProfileImage"]);
                    tl.Skill1 = Model.Common.CheckStringNull(oRow["Skill1"]);
                    tl.Skill2 = Model.Common.CheckStringNull(oRow["Skill2"]);
                    tl.Skill3 = Model.Common.CheckStringNull(oRow["Skill3"]);
                    tl.S1Des = Model.Common.CheckStringNull(oRow["S1Des"]);
                    tl.S2Des = Model.Common.CheckStringNull(oRow["S2Des"]);
                    tl.S3Des = Model.Common.CheckStringNull(oRow["S3Des"]);
                    tl.S1Image = Model.Common.CheckStringNull(oRow["S1Image"]);
                    tl.S2Image = Model.Common.CheckStringNull(oRow["S2Image"]);
                    tl.S3Image = Model.Common.CheckStringNull(oRow["S3Image"]);

                }
                return tl;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Dal_AddPortfolio(Mdl_Portfolio mdl)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = mdl.ID
                };
                parm[1] = new SqlParameter(PARM_Taylor_Image, SqlDbType.NVarChar)
                {
                    Value = mdl.Image
                };
                parm[2] = new SqlParameter(PARM_Taylor_About, SqlDbType.NVarChar)
                {
                    Value = mdl.About
                };              
                SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Add_Portfolio, parm);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Mdl_Portfolio> Dal_ReadPortfolio(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = ID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Read_Portfolio, parm);
                List<Mdl_Portfolio> TaylorList = new List<Mdl_Portfolio>();
                foreach (DataRow oRow in oTable.Rows)
                {
                    Mdl_Portfolio tl = new Mdl_Portfolio();
                    tl.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    tl.About = Model.Common.CheckStringNull(oRow["About"]);
                    tl.Image = Model.Common.CheckStringNull(oRow["Image"]);
                    TaylorList.Add(tl);
                }
                return TaylorList;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}