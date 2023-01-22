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
    public class DAL_Product
    {
        private string _ConnString;

        #region Constructor
        public DAL_Product()
        {
            _ConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        }
        #endregion

        #region Parameters

        const String PARM_Product_ID = "@ID";
        const String PARM_Product_name = "@name";
        const String PARM_Product_Description = "@Description";
        const String PARM_Product_Thumbnail = "@Thumbnail";
        const String PARM_Product_UnitPrice = "@UnitPrice";
        const String PARM_Product_Category = "@Category";


        #endregion

        #region Query
        
        const String SQL_Add_Product = "SPW_AddProduct";
        const String SQL_read_Product = "SPW_Read_Product";
        const String SQL_readClient_Product = "SPW_ReadClient_Product";
        const String SQL_readSingleClient_Product = "SPW_ReadSingleClient_Product";
        const String SQL_Delete_Product = "SPW_DeleteProduct";
        const String SQL_Update_Product = "SPW_Update_Product";

        #endregion

        public void Dal_Add_Product(MdlProduct mdl)
        {
            try
            {
                
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter(PARM_Product_name, SqlDbType.NVarChar)
            {
                Value = mdl.Name
            };
            parm[1] = new SqlParameter(PARM_Product_Description, SqlDbType.NVarChar)
            {
                Value = mdl.Description
            };
            parm[2] = new SqlParameter(PARM_Product_Thumbnail, SqlDbType.NVarChar)
            {
                Value = mdl.Thumbnail
            };
            parm[3] = new SqlParameter(PARM_Product_UnitPrice, SqlDbType.Float)
            {
                Value = mdl.UnitPrice
            };
            parm[4] = new SqlParameter(PARM_Product_Category, SqlDbType.Int)
            {
                Value = mdl.Category
            };
                parm[5] = new SqlParameter(PARM_Product_ID, SqlDbType.Int)
                {
                    Value = mdl.ID
                };
                SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Add_Product, parm);

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<MdlProduct> DAL_Read_Product()
        {
            try
            {
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_read_Product, null);
                List<MdlProduct> ProductList = new List<MdlProduct>();
                foreach (DataRow oRow in oTable.Rows)
                {
                    MdlProduct pro = new MdlProduct();
                    pro.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    pro.Description = Model.Common.CheckStringNull(oRow["Description"]);
                    pro.Name = Model.Common.CheckStringNull(oRow["Name"]);
                    pro.Thumbnail = Model.Common.CheckStringNull(oRow["Thumbnail"]);
                    pro.Category = Model.Common.CheckIntegerNull(oRow["Category"]);
                    pro.UnitPrice = Model.Common.CheckDoubleNull(oRow["UnitPrice"]);
                    ProductList.Add(pro);
                }
                return ProductList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<MdlProduct> DAL_ReadClient_Product(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_Product_ID, SqlDbType.Int)
                {
                    Value = ID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_readClient_Product, parm);
                List<MdlProduct> ProductList = new List<MdlProduct>();
                foreach (DataRow oRow in oTable.Rows)
                {
                    MdlProduct pro = new MdlProduct();
                    pro.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    pro.Description = Model.Common.CheckStringNull(oRow["Description"]);
                    pro.Name = Model.Common.CheckStringNull(oRow["Name"]);
                    pro.Thumbnail = Model.Common.CheckStringNull(oRow["Thumbnail"]);
                    pro.Category = Model.Common.CheckIntegerNull(oRow["Category"]);
                    pro.UnitPrice = Model.Common.CheckDoubleNull(oRow["UnitPrice"]);
                    ProductList.Add(pro);
                }
                return ProductList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public MdlProduct DAL_ReadSingleClient_Product(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_Product_ID, SqlDbType.Int)
                {
                    Value = ID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_readSingleClient_Product, parm);
                MdlProduct Product = new MdlProduct();
                foreach (DataRow oRow in oTable.Rows)
                {
                    Product.ID = Model.Common.CheckIntegerNull(oRow["ID"]);
                    Product.Description = Model.Common.CheckStringNull(oRow["Description"]);
                    Product.Name = Model.Common.CheckStringNull(oRow["Name"]);
                    Product.Thumbnail = Model.Common.CheckStringNull(oRow["Thumbnail"]);
                    Product.Category = Model.Common.CheckIntegerNull(oRow["Category"]);
                    Product.UnitPrice = Model.Common.CheckDoubleNull(oRow["UnitPrice"]);
                }
                return Product;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public void DLL_Delete_Product(int ID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@ID", SqlDbType.Int)
                {
                    Value = ID
                };
                SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Delete_Product, parm);

            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Dal_Update_Product(MdlProduct mdl)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter(PARM_Product_name, SqlDbType.NVarChar)
                {
                    Value = mdl.Name
                };
                parm[1] = new SqlParameter(PARM_Product_Description, SqlDbType.NVarChar)
                {
                    Value = mdl.Description
                };
                parm[2] = new SqlParameter(PARM_Product_Thumbnail, SqlDbType.NVarChar)
                {
                    Value = mdl.Thumbnail
                };
                parm[3] = new SqlParameter(PARM_Product_UnitPrice, SqlDbType.Float)
                {
                    Value = mdl.UnitPrice
                };
                parm[4] = new SqlParameter(PARM_Product_Category, SqlDbType.Int)
                {
                    Value = mdl.Category
                };
                parm[5] = new SqlParameter(PARM_Product_ID, SqlDbType.Int)
                {
                    Value = mdl.ID
                };
                SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Update_Product, parm);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}