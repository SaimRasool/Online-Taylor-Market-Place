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
    public class DAL_Chat
    {

        private string _ConnString;

        #region Query
        const String SQL_Insert_Chat = "SPW_InsertChat";
        const String SQL_Recevied_Chat = "SPW_ReceivedChat";
        const String SQL_ReadSigle_Chat = "SPW_ReadSingleChat";
        const String SQL_Delivered_Chat = "SPW_DeliveredChat";


        #endregion

        #region Parameters

        const String PARM_USER_ID = "@ID";
        const String PARM_Senderid = "@SenderID";
        const String PARM_Receiverid = "@ReceiverID";
        const String PARM_Message = "@Message";
        const String PARM_Status = "@Status";
        const String PARM_Date = "@Created_at";

        #endregion

        #region Constructor
        public DAL_Chat()
        {
            _ConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        }
        #endregion
        public void DalInsertChat(MdlChat mdl)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter(PARM_Senderid, SqlDbType.Int)
            {
                Value = mdl.sender_id
            };
            parm[1] = new SqlParameter(PARM_Receiverid, SqlDbType.Int)
            {
                Value = mdl.receiver_id
            };
            parm[2] = new SqlParameter(PARM_Message, SqlDbType.NVarChar)
            {
                Value = mdl.message
            };
            parm[3] = new SqlParameter(PARM_Status, SqlDbType.NVarChar)
            {
                Value = mdl.status
            };
            parm[4] = new SqlParameter(PARM_Date, SqlDbType.Date)
            {
                Value = DateTime.Today.Date
            };
            SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Insert_Chat, parm);

        }

        public List<MdlChat> DalReceivedChat(int receiverID, int SenderID)
        {
            try
            {
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter(PARM_Receiverid, SqlDbType.Int)
                {
                    Value = receiverID
                };
                parm[1] = new SqlParameter(PARM_Senderid, SqlDbType.Int)
                {
                    Value = SenderID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_Recevied_Chat, parm);
                List<MdlChat> Chatlist = new List<MdlChat>();
                foreach (DataRow oRow in oTable.Rows)
                {
                    MdlChat ch = new MdlChat();
                    ch.id = Model.Common.CheckIntegerNull(oRow["ID"]);
                    ch.sender_id = Model.Common.CheckIntegerNull(oRow["SenderID"]);
                    ch.receiver_id = Model.Common.CheckIntegerNull(oRow["ReceiverID"]);
                    ch.message = Model.Common.CheckStringNull(oRow["Message"]);
                    Enum.TryParse(Model.Common.CheckStringNull(oRow["Status"]), out MdlChat.messageStatus myStatus);
                    ch.status = myStatus;
                    ch.created_at = Model.Common.CheckDateTimeNull(oRow["Created_at"]);
                    Chatlist.Add(ch);
                }
                return Chatlist;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public MdlChat DalReadSingleChat(int ID)
        {
            try
            {
                MdlChat ch = new MdlChat();
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
                {
                    Value = ID
                };
                DataTable oTable = SqlHelper.ExecuteTable(this._ConnString, CommandType.StoredProcedure, SQL_ReadSigle_Chat, parm);
                foreach (DataRow oRow in oTable.Rows)
                {
                    ch.id = Model.Common.CheckIntegerNull(oRow["ID"]);
                    ch.sender_id = Model.Common.CheckIntegerNull(oRow["SenderID"]);
                    ch.receiver_id = Model.Common.CheckIntegerNull(oRow["ReceiverID"]);
                    ch.message = Model.Common.CheckStringNull(oRow["Message"]);
                    ch.status = (MdlChat.messageStatus)oRow["Status"];
                    ch.created_at = Model.Common.CheckDateTimeNull(oRow["Created_at"]);

                }

                return ch;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void DalDeliveredChat(MdlChat mdl)
        {
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter(PARM_USER_ID, SqlDbType.Int)
            {
                Value = mdl.id
            };
            parm[1] = new SqlParameter(PARM_Senderid, SqlDbType.Int)
            {
                Value = mdl.sender_id
            };
            parm[2] = new SqlParameter(PARM_Receiverid, SqlDbType.Int)
            {
                Value = mdl.receiver_id
            };
            parm[3] = new SqlParameter(PARM_Message, SqlDbType.NVarChar)
            {
                Value = mdl.message
            };
            parm[4] = new SqlParameter(PARM_Status, SqlDbType.NVarChar)
            {
                Value = mdl.status
            };
            parm[5] = new SqlParameter(PARM_Date, SqlDbType.Date)
            {
                Value = mdl.created_at
            };
            SqlHelper.SaveData(this._ConnString, CommandType.StoredProcedure, SQL_Delivered_Chat, parm);

        }
    }
}