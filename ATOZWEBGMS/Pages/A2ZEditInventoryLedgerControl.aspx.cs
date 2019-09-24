using System;
using System.Web;
using System.Web.UI;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using System.Data;

namespace ATOZWEBGMS.Pages
{
    public partial class A2ZEditInventoryLedgerControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CurrencyDropdown();
                CurrencyDropdown1();
                CurrencyDropdown2();
                CurrencyDropdown3();
                CurrencyDropdown4();
            }
        }



        private void MetalDropdown()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode = 21 AND PartyCurrencyCode = " + ddlCurrency1.SelectedValue + "";
            ddlMetalLedger = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlMetalLedger, "A2ZACGMS");
        }

        private void CarryingDropdown()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode = 21 AND PartyCurrencyCode = " + ddlCurrency2.SelectedValue + "";
            ddlCarryingLedger = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCarryingLedger, "A2ZACGMS");
        }

        private void MakingDropdown()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode = 21 AND PartyCurrencyCode = " + ddlCurrency3.SelectedValue + "";
            ddlMakingLedger = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlMakingLedger, "A2ZACGMS");
        }

        private void StoneDropdown()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode = 21 AND PartyCurrencyCode = " + ddlCurrency4.SelectedValue + "";
            ddlStoneLedger = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlStoneLedger, "A2ZACGMS");
        }

        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");

        }
        private void CurrencyDropdown1()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode = 99";
            ddlCurrency1 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency1, "A2ZACGMS");

        }

        private void CurrencyDropdown2()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode = 1";
            ddlCurrency2 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency2, "A2ZACGMS");

        }

        private void CurrencyDropdown3()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode > 1 AND CurrencyCode != 99";
            ddlCurrency3 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency3, "A2ZACGMS");

        }

        private void CurrencyDropdown4()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode > 1 AND CurrencyCode != 99";
            ddlCurrency4 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency4, "A2ZACGMS");

        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }





        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var prm = new object[6];
                prm[0] = ddlLocation.SelectedValue;
                prm[1] = ddlCurrency.SelectedValue;


                if (ddlMetalLedger.SelectedIndex == -1 || ddlMetalLedger.SelectedIndex == 0)
                {
                    prm[2] = "0";
                }
                else
                {
                    prm[2] = ddlMetalLedger.SelectedValue;
                }


                if (ddlMakingLedger.SelectedIndex == -1 || ddlMakingLedger.SelectedIndex == 0)
                {
                    prm[3] = "0";
                }
                else
                {
                    prm[3] = ddlMakingLedger.SelectedValue;
                }

                if (ddlStoneLedger.SelectedIndex == -1 || ddlStoneLedger.SelectedIndex == 0)
                {
                    prm[4] = "0";
                }
                else
                {
                    prm[4] = ddlStoneLedger.SelectedValue;
                }

                if (ddlCarryingLedger.SelectedIndex == -1 || ddlCarryingLedger.SelectedIndex == 0)
                {
                    prm[5] = "0";
                }
                else
                {
                    prm[5] = ddlCarryingLedger.SelectedValue;
                }



                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_UpdateInventoryLdg]", prm, "A2ZACGMS"));


                if (result == 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data not inserted');", true);
                return;
            }

        }

        protected void ddlCurrency1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrency1.SelectedValue != "-Select-")
            {
                MetalDropdown();
            }

        }

        protected void ddlCurrency2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrency2.SelectedValue != "-Select-")
            {
                CarryingDropdown();
            }

        }

        protected void ddlCurrency3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrency3.SelectedValue != "-Select-")
            {
                MakingDropdown();
            }

        }

        protected void ddlCurrency4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrency4.SelectedValue != "-Select-")
            {
                StoneDropdown();
            }

        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {


            int location = Converter.GetInteger(ddlLocation.SelectedValue);
            int currency = Converter.GetInteger(ddlCurrency.SelectedValue);
            A2ZINVCTRLDTO getDTO = (A2ZINVCTRLDTO.GetInformation(location, currency));

            if (getDTO.Location > 0)
            {
                Int64 accno1 = Converter.GetLong(getDTO.GoldLedger);
                A2ZPARTYDTO get1DTO = (A2ZPARTYDTO.GetAccNoInformation(accno1));

                if (get1DTO.PartyCode > 0)
                {
                    ddlCurrency1.SelectedValue = Converter.GetString(get1DTO.PartyCurrencyCode);
                    MetalDropdown();
                    ddlMetalLedger.SelectedValue = Converter.GetString(getDTO.GoldLedger);
                }

                Int64 accno2 = Converter.GetLong(getDTO.CarryingLedger);
                if (accno2 != 0)
                {
                    A2ZPARTYDTO get2DTO = (A2ZPARTYDTO.GetAccNoInformation(accno2));

                    if (get2DTO.PartyCode > 0)
                    {
                        ddlCurrency2.SelectedValue = Converter.GetString(get2DTO.PartyCurrencyCode);
                        CarryingDropdown();
                        ddlCarryingLedger.SelectedValue = Converter.GetString(getDTO.CarryingLedger);
                    }
                }



                Int64 accno3 = Converter.GetLong(getDTO.MakingLedger);
                A2ZPARTYDTO get3DTO = (A2ZPARTYDTO.GetAccNoInformation(accno3));

                if (get3DTO.PartyCode > 0)
                {
                    ddlCurrency3.SelectedValue = Converter.GetString(get3DTO.PartyCurrencyCode);
                    MakingDropdown();
                    ddlMakingLedger.SelectedValue = Converter.GetString(getDTO.MakingLedger);
                }



                Int64 accno4 = Converter.GetLong(getDTO.StoneLedger);
                A2ZPARTYDTO get4DTO = (A2ZPARTYDTO.GetAccNoInformation(accno4));

                if (get4DTO.PartyCode > 0)
                {
                    ddlCurrency4.SelectedValue = Converter.GetString(get4DTO.PartyCurrencyCode);
                    StoneDropdown();
                    ddlStoneLedger.SelectedValue = Converter.GetString(getDTO.StoneLedger);
                }

            }
            else
            {
                ddlCurrency.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Not in File');", true);
                return;
            }
        }



    }
}
