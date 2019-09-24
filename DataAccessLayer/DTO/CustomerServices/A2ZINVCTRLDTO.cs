using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZINVCTRLDTO
    {
        #region Propertise

        public int Location { set; get; }
        public Int64 GoldLedger { set; get; }

        public Int64 MakingLedger { set; get; }

        public Int64 StoneLedger { set; get; }

        public Int64 CarryingLedger { set; get; }

        

        #endregion

        
        public static A2ZINVCTRLDTO GetInformation(int Location, int Currency)
        {

            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZINVSTOCKCTRL WHERE  Location='" + Location + "' AND Currency='" + Currency + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZACGMS");

            var p = new A2ZINVCTRLDTO();
            if (dt.Rows.Count > 0)
            {
                p.Location = Converter.GetInteger(dt.Rows[0]["Location"]);
                p.GoldLedger = Converter.GetLong(dt.Rows[0]["GoldLedger"]);
                p.MakingLedger = Converter.GetLong(dt.Rows[0]["MakingLedger"]);
                p.StoneLedger = Converter.GetLong(dt.Rows[0]["StoneLedger"]);
                p.CarryingLedger = Converter.GetLong(dt.Rows[0]["CarryingLedger"]);


                return p;
            }
            p.Location = 0;
            return p;

        }
       
    }
}
