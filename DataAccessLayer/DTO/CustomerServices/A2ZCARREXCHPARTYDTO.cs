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
    public class A2ZCARREXCHPARTYDTO
    {
        #region Propertise
        public int ID { get; set; }
        public int CPartyCode { set; get; }
        public string CPartyName { set; get; }
        public string CPartyAddresssLine1 { set; get; }
        public string CPartyAddresssLine2 { set; get; }
        public string CPartyAddresssLine3 { set; get; }
        public string CPartyMobileNo { set; get; }
        public string CPartyEmail { set; get; }

        #endregion


        public static int InsertInformation(A2ZCARREXCHPARTYDTO dto)
        {
            
            int rowEffect = 0;

            var prm = new object[7];

            prm[0] = dto.CPartyCode;
            prm[1] = dto.CPartyName;
            prm[2] = dto.CPartyAddresssLine1;
            prm[3] = dto.CPartyAddresssLine2;
            prm[4] = dto.CPartyAddresssLine3;
            prm[5] = dto.CPartyMobileNo;
            prm[6] = dto.CPartyEmail;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_InsertCarrExchParty", prm, "A2ZACGMS"));
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

        public static A2ZCARREXCHPARTYDTO GetAllCarrExchParty()
        {

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySp("Sp_GetCarrExchPartyInfo", "A2ZACGMS");

            var p = new A2ZCARREXCHPARTYDTO();
            if (dt.Rows.Count > 0)
            {
               
                p.CPartyName = Converter.GetString(dt.Rows[0]["CPartyName"]);
                p.CPartyAddresssLine1 = Converter.GetString(dt.Rows[0]["CPartyAddresssLine1"]);
                p.CPartyAddresssLine2 = Converter.GetString(dt.Rows[0]["CPartyAddresssLine2"]);
                p.CPartyAddresssLine3 = Converter.GetString(dt.Rows[0]["CPartyAddresssLine3"]);
                p.CPartyMobileNo = Converter.GetString(dt.Rows[0]["CPartyMobileNo"]);
                p.CPartyEmail = Converter.GetString(dt.Rows[0]["CPartyEmail"]);

                return p;
            }

            return p;

        }


        public static A2ZCARREXCHPARTYDTO GetCarrExchPartyByPartyCode(int PartyCode)
        {
            var prm = new object[1];

            prm[0] = PartyCode;

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetCarrExchPartyByPartyCode", prm, "A2ZACGMS");

            var p = new A2ZCARREXCHPARTYDTO();
            if (dt.Rows.Count > 0)
            {
                p.CPartyCode = Converter.GetInteger(dt.Rows[0]["CPartyCode"]);
                p.CPartyName = Converter.GetString(dt.Rows[0]["CPartyName"]);
                p.CPartyAddresssLine1 = Converter.GetString(dt.Rows[0]["CPartyAddresssLine1"]);
                p.CPartyAddresssLine2 = Converter.GetString(dt.Rows[0]["CPartyAddresssLine2"]);
                p.CPartyAddresssLine3 = Converter.GetString(dt.Rows[0]["CPartyAddresssLine3"]);
                p.CPartyMobileNo = Converter.GetString(dt.Rows[0]["CPartyMobileNo"]);
                p.CPartyEmail = Converter.GetString(dt.Rows[0]["CPartyEmail"]);

                return p;
            }

            return p;

        }
        public static int UpdateCarrExchParty(A2ZCARREXCHPARTYDTO dto)
        {

            int rowEffect = 0;

            var prm = new object[7];

            prm[0] = dto.CPartyCode;
            prm[1] = dto.CPartyName;
            prm[2] = dto.CPartyAddresssLine1;
            prm[3] = dto.CPartyAddresssLine2;
            prm[4] = dto.CPartyAddresssLine3;
            prm[5] = dto.CPartyMobileNo;
            prm[6] = dto.CPartyEmail;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateCarrExchPartyInfo", prm, "A2ZACGMS"));
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
