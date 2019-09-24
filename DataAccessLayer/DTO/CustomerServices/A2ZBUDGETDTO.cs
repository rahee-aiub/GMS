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
    public class A2ZBUDGETDTO
    {
        #region Propertise

        public Int32 ID { set; get; }

        public int BranchNo { set; get; }
        public Int16 BudgetYear { set; get; }
        public int GLACCNO { set; get; }
        public Int16 DrCrFlag { set; get; }
        public decimal Amount1 { set; get; }
        public decimal Amount2 { set; get; }
        public decimal Amount3 { set; get; }
        public decimal Amount4 { set; get; }
        public decimal Amount5 { set; get; }
        public decimal Amount6 { set; get; }
        public decimal Amount7 { set; get; }
        public decimal Amount8 { set; get; }
        public decimal Amount9 { set; get; }
        public decimal Amount10 { set; get; }
        public decimal Amount11 { set; get; }
        public decimal Amount12 { set; get; }
        public decimal BudgetAmount { set; get; }


        #endregion


        public static A2ZBUDGETDTO GetInformation(int BranchNo, int BudgetYear, int GLACCNO, Int16 DrCrFlag)
        {
            var prm = new object[4];

            prm[0] = BranchNo;
            prm[1] = BudgetYear;
            prm[2] = GLACCNO;
            prm[3] = DrCrFlag;

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetBudgetAmt", prm, "A2ZACGMS");


            var p = new A2ZBUDGETDTO();
            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetInteger(dt.Rows[0]["Id"]);
                p.BranchNo = Converter.GetInteger(dt.Rows[0]["BranchNo"]);
                p.BudgetYear = Converter.GetSmallInteger(dt.Rows[0]["BudgetYear"]);
                p.GLACCNO = Converter.GetInteger(dt.Rows[0]["GLACCNO"]);
                p.DrCrFlag = Converter.GetSmallInteger(dt.Rows[0]["DrCrFlag"]);
                p.Amount1 = Converter.GetInteger(dt.Rows[0]["Amount1"]);
                p.Amount2 = Converter.GetInteger(dt.Rows[0]["Amount2"]);
                p.Amount3 = Converter.GetInteger(dt.Rows[0]["Amount3"]);
                p.Amount4 = Converter.GetInteger(dt.Rows[0]["Amount4"]);
                p.Amount5 = Converter.GetInteger(dt.Rows[0]["Amount5"]);
                p.Amount6 = Converter.GetInteger(dt.Rows[0]["Amount6"]);
                p.Amount7 = Converter.GetInteger(dt.Rows[0]["Amount7"]);
                p.Amount8 = Converter.GetInteger(dt.Rows[0]["Amount8"]);
                p.Amount9 = Converter.GetInteger(dt.Rows[0]["Amount9"]);
                p.Amount10 = Converter.GetInteger(dt.Rows[0]["Amount10"]);
                p.Amount11 = Converter.GetInteger(dt.Rows[0]["Amount11"]);
                p.Amount12 = Converter.GetInteger(dt.Rows[0]["Amount12"]);
                p.BudgetAmount = Converter.GetInteger(dt.Rows[0]["BudgetAmount"]);

                return p;

            }

            return p;

        }



        public static int InsertInformation(A2ZBUDGETDTO dto)
        {

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACGMS"), "Sp_CSInsertBudgetAmt", new object[] { dto.BranchNo, dto.BudgetYear, dto.GLACCNO, dto.DrCrFlag, dto.Amount1, dto.Amount2, dto.Amount3, dto.Amount4, dto.Amount5, dto.Amount6, dto.Amount7, dto.Amount8, dto.Amount9, dto.Amount10, dto.Amount11, dto.Amount12, dto.BudgetAmount });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }


        public static int UpdateInformation(A2ZBUDGETDTO dto)
        {

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACGMS"), "Sp_CSUpdateBudgetAmt", new object[] { dto.BranchNo, dto.BudgetYear, dto.GLACCNO, dto.DrCrFlag, dto.Amount1, dto.Amount2, dto.Amount3, dto.Amount4, dto.Amount5, dto.Amount6, dto.Amount7, dto.Amount8, dto.Amount9, dto.Amount10, dto.Amount11, dto.Amount12, dto.BudgetAmount });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

            
            return 0;
        }

    }
}
