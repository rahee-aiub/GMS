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
    public class A2ZITEMGROUPDTO
    {
        #region Propertise
        public int ID { get; set; }
        public int GroupCode { set; get; }
        public string GroupName { set; get; }
        public decimal MakingRangeFrom { set; get; }
        public decimal MakingRangeTo { set; get; }
        public decimal Purity22 { set; get; }
        public decimal Purity21 { set; get; }
        public decimal Purity18 { set; get; }

        #endregion


        public static int InsertInformation(A2ZITEMGROUPDTO dto)
        {

            int rowEffect = 0;

            var prm = new object[7];

            prm[0] = dto.GroupCode;
            prm[1] = dto.GroupName;
            prm[2] = dto.MakingRangeFrom;
            prm[3] = dto.MakingRangeTo;
            prm[4] = dto.Purity22;
            prm[5] = dto.Purity21;
            prm[6] = dto.Purity18;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_InsertItemGroup", prm, "A2ZACGMS"));
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

    

        public static A2ZITEMGROUPDTO GetInformation(int GroupCode)
        {
            var prm = new object[1];

            prm[0] = GroupCode;

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetItemGroup", prm, "A2ZACGMS");

            var p = new A2ZITEMGROUPDTO();
            if (dt.Rows.Count > 0)
            {

                p.GroupCode = Converter.GetInteger(dt.Rows[0]["GroupCode"]);
                p.GroupName = Converter.GetString(dt.Rows[0]["GroupName"]);
                p.MakingRangeFrom = Converter.GetDecimal(dt.Rows[0]["MakingRangeFrom"]);
                p.MakingRangeTo = Converter.GetDecimal(dt.Rows[0]["MakingRangeTo"]);
                p.Purity22 = Converter.GetDecimal(dt.Rows[0]["Purity22"]);
                p.Purity21 = Converter.GetDecimal(dt.Rows[0]["Purity21"]);
                p.Purity18 = Converter.GetDecimal(dt.Rows[0]["Purity18"]);


                return p;
            }
            else 
            {
                p.GroupCode = 0;
            }

            return p;

        }
        public static int UpdateInformation(A2ZITEMGROUPDTO dto)
        {

            int rowEffect = 0;

            var prm = new object[7];

            prm[0] = dto.GroupCode;
            prm[1] = dto.GroupName;
            prm[2] = dto.MakingRangeFrom;
            prm[3] = dto.MakingRangeTo;
            prm[4] = dto.Purity22;
            prm[5] = dto.Purity21;
            prm[6] = dto.Purity18;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateGroupItem", prm, "A2ZACGMS"));
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

        public static int DeleteInformation(A2ZITEMGROUPDTO dto)
        {

            int rowEffect = 0;

            var prm = new object[1];

            prm[0] = dto.GroupCode;
          

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_DeleteItemGroup", prm, "A2ZACGMS"));
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
