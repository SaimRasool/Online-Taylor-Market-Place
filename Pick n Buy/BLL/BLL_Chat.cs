using OTM.DAL;
using OTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTM.BLL
{
    public class BLL_Chat
    {
        public void BLLInsertChat(MdlChat mdl)
        {
            DAL_Chat obj = new DAL_Chat();
            obj.DalInsertChat(mdl);
        }

        public List<MdlChat> BllReadChat(int receiverID, int SenderID)
        {
            DAL_Chat obj = new DAL_Chat();
            return obj.DalReceivedChat(receiverID, SenderID);
        }

        public MdlChat BLLReadSingleChat(int ID)
        {
            DAL_Chat obj = new DAL_Chat();
            return obj.DalReadSingleChat(ID);
        }

        public void BLLDeliveredChat(MdlChat mdl)
        {
            DAL_Chat obj = new DAL_Chat();
            obj.DalInsertChat(mdl);
        }
    }
}