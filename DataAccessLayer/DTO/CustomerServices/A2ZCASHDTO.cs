using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
//using DataAccessLayer.Conn;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZCASHDTO
    {
        #region Propertise
        public Int32 Id { set; get; }
        public int CashCode { set; get; }
        public string CashName { set; get; }
        public int CashCurrencyCode { set; get; }
        public decimal CashBalance { set; get; }
        public decimal CashTodaysBalance { set; get; }
        public Int16 a { set; get; }
          
        #endregion




        public static A2ZCASHDTO GetInformation(int AccKeyNo, int Currency)
        {

            A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
            string date1 = dt2.ToString("dd/MM/yyyy");

            var prm1 = new object[4];
            prm1[0] = AccKeyNo;
            prm1[1] = Converter.GetDateToYYYYMMDD(date1);
            prm1[2] = Currency;
            prm1[3] = 0;

            DataTable dt3 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_GenerateSingleCashBalance", prm1, "A2ZACGMS");

            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCASH WHERE CashCode = " + AccKeyNo + " AND CashCurrencyCode = " + Currency + "", "A2ZACGMS");

            //var prm = new object[1];    
            //prm[0] = AccountNo;
                       
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetInfoAccountNo", prm, "A2ZACGMS");


            var p = new A2ZCASHDTO();
            if (dt.Rows.Count > 0)
            {
                p.Id = Converter.GetInteger(dt.Rows[0]["Id"]);
                p.CashCode = Converter.GetInteger(dt.Rows[0]["CashCode"]);
                p.CashName = Converter.GetString(dt.Rows[0]["CashName"]);
                p.CashCurrencyCode = Converter.GetInteger(dt.Rows[0]["CashCurrencyCode"]);
                p.CashBalance = Converter.GetDecimal(dt.Rows[0]["CashBalance"]);

                return p;
            }
            else
            {
                p.CashCode = 0;    
            }


            return p;

        }


     
    }
}
