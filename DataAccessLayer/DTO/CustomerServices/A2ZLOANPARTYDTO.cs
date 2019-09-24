using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.BLL;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZLOANPARTYDTO
    {
        #region Propertise
        public int ID { get; set; }
        public int LPartyCode { set; get; }
        public string LPartyName { set; get; }
        public string LPartyAddresssLine1 { set; get; }
        public string LPartyAddresssLine2 { set; get; }
        public string LPartyAddresssLine3 { set; get; }
        public string LPartyMobileNo { set; get; }
        public string LPartyEmail { set; get; }

        #endregion


        public static int InsertInformation(A2ZLOANPARTYDTO dto)
        {
            
            int rowEffect = 0;

            var prm = new object[7];

            prm[0] = dto.LPartyCode;
            prm[1] = dto.LPartyName;
            prm[2] = dto.LPartyAddresssLine1;
            prm[3] = dto.LPartyAddresssLine2;
            prm[4] = dto.LPartyAddresssLine3;
            prm[5] = dto.LPartyMobileNo;
            prm[6] = dto.LPartyEmail;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_InsertLoanParty", prm, "A2ZACGMS"));
            if (result == 0)
            {
                rowEffect = 1;
            }
            else
            {
                rowEffect = 0;
            }

            return rowEffect;
        }

        public static A2ZLOANPARTYDTO GetAllLoanParty()
        {

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySp("Sp_GetLoanPartyInfo", "A2ZACGMS");

            var p = new A2ZLOANPARTYDTO();
            if (dt.Rows.Count > 0)
            {
               
                p.LPartyName = Converter.GetString(dt.Rows[0]["LPartyName"]);
                p.LPartyAddresssLine1 = Converter.GetString(dt.Rows[0]["LPartyAddresssLine1"]);
                p.LPartyAddresssLine2 = Converter.GetString(dt.Rows[0]["LPartyAddresssLine2"]);
                p.LPartyAddresssLine3 = Converter.GetString(dt.Rows[0]["LPartyAddresssLine3"]);
                p.LPartyMobileNo = Converter.GetString(dt.Rows[0]["LPartyMobileNo"]);
                p.LPartyEmail = Converter.GetString(dt.Rows[0]["LPartyEmail"]);

                return p;
            }

            return p;

        }


        public static A2ZLOANPARTYDTO GetLoanPartyByPartyCode(int PartyCode)
        {
            var prm = new object[1];

            prm[0] = PartyCode;

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetLoanPartyByPartyCode", prm, "A2ZACGMS");

            var p = new A2ZLOANPARTYDTO();
            if (dt.Rows.Count > 0)
            {
                p.LPartyCode = Converter.GetInteger(dt.Rows[0]["LPartyCode"]);
                p.LPartyName = Converter.GetString(dt.Rows[0]["LPartyName"]);
                p.LPartyAddresssLine1 = Converter.GetString(dt.Rows[0]["LPartyAddresssLine1"]);
                p.LPartyAddresssLine2 = Converter.GetString(dt.Rows[0]["LPartyAddresssLine2"]);
                p.LPartyAddresssLine3 = Converter.GetString(dt.Rows[0]["LPartyAddresssLine3"]);
                p.LPartyMobileNo = Converter.GetString(dt.Rows[0]["LPartyMobileNo"]);
                p.LPartyEmail = Converter.GetString(dt.Rows[0]["LPartyEmail"]);

                return p;
            }

            return p;

        }
        public static int UpdateLoanParty(A2ZLOANPARTYDTO dto)
        {

            int rowEffect = 0;

            var prm = new object[7];

            prm[0] = dto.LPartyCode;
            prm[1] = dto.LPartyName;
            prm[2] = dto.LPartyAddresssLine1;
            prm[3] = dto.LPartyAddresssLine2;
            prm[4] = dto.LPartyAddresssLine3;
            prm[5] = dto.LPartyMobileNo;
            prm[6] = dto.LPartyEmail;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateLoanPartyInfo", prm, "A2ZACGMS"));
            if (result == 0)
            {
                rowEffect = 1;
            }
            else
            {
                rowEffect = 0;
            }

            return rowEffect;
        }

    

    }
}
