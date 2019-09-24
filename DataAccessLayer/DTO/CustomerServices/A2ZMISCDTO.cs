using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public  class A2ZMISCDTO
    {
        #region Propertise
        public int ID { set; get; }
       
        public int MiscCode { set; get; }
        public String MiscName { set; get; }
        #endregion


        public static int InsertInformation(A2ZMISCDTO dto)
        {
            dto.MiscName = (dto.MiscName != null) ? dto.MiscName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZMISC(DataCode,DataName)values('" + dto.MiscCode + "','" + dto.MiscName + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACGMS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static A2ZMISCDTO GetInformation(int misccode)
        {
            //var prm = new object[1];

            //prm[0] = bankcode;
           


            //DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_HRGetInfoBank", prm, "A2ZHRMCUS");

            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZMISC WHERE DataCode = " + misccode, "A2ZACGMS");


            var p = new A2ZMISCDTO();
            if (dt.Rows.Count > 0)
            {

                p.MiscCode = Converter.GetInteger(dt.Rows[0]["DataCode"]);
                p.MiscName = Converter.GetString(dt.Rows[0]["DataName"]);
                return p;
            }
            p.MiscCode = 0;

            return p;

        }

        public static DropDownList GetDropDownList(DropDownList ddlMiscCode)
        {
            return BLL.CommonManager.Instance.FillDropDownList("SELECT DataCode,DataName FROM A2ZMISC ORDER BY DataName", ddlMiscCode, "A2ZACGMS");
        }
        public static int UpdateInformation(A2ZMISCDTO dto)
        {
            dto.MiscName = (dto.MiscName != null) ? dto.MiscName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZMISC set DataCode='" + dto.MiscCode + "',DataName='" + dto.MiscName + "' where DataCode='" + dto.MiscCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACGMS"));
            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
