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
    public class DAL_Order
    {
        private string _ConnString;
        private int Orderid;
        #region Constructor
        public DAL_Order()
        {
            _ConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        }
        #endregion

        #region Parameters

        const String PARM_User_ID = "@UserID";
        const String PARM_OrderID = "@OrderID";
        const String PARM_Product_name = "@name";
        const String PARM_Order_Quantity = "@Quantity";
        const String PARM_UnitPrice = "@UnitPrice";
        const String PARM_Total = "@Total";
        const String PARM_IsBilling = "@IsBilling";
        const String PARM_USER_Fname = "@Fname";
        const String PARM_USER_Lname = "@Lname";
        const String PARM_USER_Email = "@Email";
        const String PARM_USER_Address = "@Address";
        const String PARM_USER_City = "@City";
        const String PARM_USER_Country = "@Country";
        const String PARM_USER_Phone = "@Phone";

        #endregion

        #region Query
        
        const String SQL_Place_Order = "SPW_Place_N_oREDER";
        const String SQL_Place_OrderDtail = "SPW_PlaceOrder_Detail";
        const String SQL_Place_ShipAndBillAddress = "SPW_AddShipAndBillAddress";

        #endregion

        public void Dal_PlaceOrder(List<MdlOrder> mdl,int ID,MdlBillingAddress md)
        {
            try
             {
                 SqlParameter[] parm = new SqlParameter[2];
                 parm[0] = new SqlParameter(PARM_User_ID, SqlDbType.Int)
                 {
                     Value = ID
                 };
                 parm[1] = new SqlParameter(PARM_Total, SqlDbType.Float)
                 {
                     Value = mdl.Sum(i => i.Total)
                 };
                 Orderid = Convert.ToInt32(SqlHelper.ExecuteScalar(this._ConnString, CommandType.StoredProcedure, SQL_Place_Order, parm));

                 foreach (var item in mdl)
                 {
                     SqlParameter[] param = new SqlParameter[5];
                     param[0] = new SqlParameter(PARM_OrderID, SqlDbType.Int)
                     {
                         Value = this.Orderid
                     };
                     param[1] = new SqlParameter(PARM_Product_name, SqlDbType.NVarChar)
                     {
                         Value = item.name
                     };
                     param[2] = new SqlParameter(PARM_Order_Quantity, SqlDbType.Int)
                     {
                         Value = item.Quantity
                     };
                    
                     param[3] = new SqlParameter(PARM_UnitPrice, SqlDbType.Float)
                     {
                         Value = item.UnitPrice
                     };
                     param[4] = new SqlParameter(PARM_Total, SqlDbType.Float)
                     {
                         Value = item.Total
                     };
                     SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Place_OrderDtail, param);
                 }
                 Dal_SaveBillAndShipAdd(md);
             }
             catch (Exception)
             {

                 throw;
             }

        }

        public void Dal_SaveBillAndShipAdd(MdlBillingAddress mdl)
        {
            for (int i = 0; i < 2;i++ )
            {

                try
                {
                    SqlParameter[] parm = new SqlParameter[7];
                    if(i == 0 || mdl.IsBilling)
                    {
                        parm[0] = new SqlParameter(PARM_USER_Fname, SqlDbType.NVarChar)
                        {
                            Value = mdl.User.FName
                        };
                        parm[1] = new SqlParameter(PARM_USER_Lname, SqlDbType.NVarChar)
                        {
                            Value = mdl.User.Lname
                        };
                        parm[2] = new SqlParameter(PARM_USER_Email, SqlDbType.NVarChar)
                        {
                            Value = mdl.User.Email
                        };
                        parm[3] = new SqlParameter(PARM_USER_City, SqlDbType.NVarChar)
                        {
                            Value = mdl.User.City
                        };
                    
                        parm[4] = new SqlParameter(PARM_USER_Phone, SqlDbType.BigInt)
                        {
                            Value = mdl.User.Phone
                        };
                        parm[5] = new SqlParameter(PARM_OrderID, SqlDbType.Int)
                        {
                            Value = this.Orderid
                        };
                        parm[6] = new SqlParameter(PARM_IsBilling, SqlDbType.Int)
                        {
                            Value = i
                        };
                    }
                    else
                        if (i == 1 && !mdl.IsBilling)
                        {
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
                            parm[3] = new SqlParameter(PARM_USER_City, SqlDbType.NVarChar)
                            {
                                Value = mdl.City
                            };
                            
                            parm[5] = new SqlParameter(PARM_USER_Address, SqlDbType.NVarChar)
                            {
                                Value = mdl.Address
                            };
                            parm[6] = new SqlParameter(PARM_USER_Phone, SqlDbType.BigInt)
                            {
                                Value = mdl.Phone
                            };
                            parm[7] = new SqlParameter(PARM_OrderID, SqlDbType.Int)
                            {
                                Value = this.Orderid
                            };
                            parm[8] = new SqlParameter(PARM_IsBilling, SqlDbType.Int)
                            {
                                Value = i
                            };
                        }
                    SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Place_ShipAndBillAddress, parm);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


    }
}