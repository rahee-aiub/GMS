using System;
using System.Web;
using System.Web.UI;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using System.Data;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBGMS.Pages
{
    public partial class A2ZAverageRateReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;

                CurrencyDropdown();

                ddlCurrency.SelectedIndex = 1;

                ddlLocation.Enabled = false;

                ChkAllLocation.Visible = false;
                ddlLocation.Visible = false;

                ChkAllKarat.Visible = false;
                ddlKarat.Visible = false;
                ddlKarat.Enabled = false;


                if (CtrlProgFlag.Text != "1")
                {

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    CtrlProcDate.Text = date;
                    txtFromDate.Text = date;
                    txtToDate.Text = date;
                }
                else
                {
                    string RtxtFromDate = (string)Session["StxtFromDate"];
                    string RtxtToDate = (string)Session["StxtToDate"];

                    string RrbtAvgCurrencyRate = (string)Session["SrbtAvgCurrencyRate"];
                    string RrbtAvgMetalRate = (string)Session["SrbtAvgMetalRate"];
                    string RrbtAvgMakingRate = (string)Session["SrbtAvgMakingRate"];
                    string RrbtAvgStoneMakingRate = (string)Session["SrbtAvgStoneMakingRate"];
                    string RrbtAvgCarryingRate = (string)Session["SrbtAvgCarryingRate"];

                    string RChkAllLocation = (string)Session["SChkAllLocation"];
                    string RddlLocation = (string)Session["SddlLocation"];

                    string RChkAllKarat = (string)Session["SChkAllKarat"];
                    string RddlKarat = (string)Session["SddlKarat"];

                   
                    txtFromDate.Text = RtxtFromDate;
                    txtToDate.Text = RtxtToDate;

                    if (RrbtAvgMakingRate == "1" || RrbtAvgStoneMakingRate == "1")
                    {
                        ChkAllLocation.Visible = true;
                        ddlLocation.Visible = true;
                        ChkAllKarat.Visible = true;
                        ddlKarat.Visible = true;

                        if (RChkAllLocation == "1")
                        {
                            ChkAllLocation.Checked = true;
                            ddlLocation.SelectedIndex = 0;
                            ddlLocation.Enabled = false;
                        }
                        else 
                        {
                            ChkAllLocation.Checked = false;
                            ddlLocation.SelectedValue = RddlLocation;
                            ddlLocation.Enabled = true;
                        }


                        if (RChkAllKarat == "1")
                        {
                            ChkAllKarat.Checked = true;
                            ddlKarat.SelectedIndex = 0;
                            ddlKarat.Enabled = false;
                        }
                        else
                        {
                            ChkAllKarat.Checked = false;
                            ddlKarat.SelectedValue = RddlKarat;
                            ddlKarat.Enabled = true;
                        }

                    }
                    else 
                    {
                        ChkAllLocation.Visible = false;
                        ddlLocation.Visible = false;
                        ChkAllKarat.Visible = false;
                        ddlKarat.Visible = false;
                    }



                    if (RrbtAvgCurrencyRate == "1")
                    {
                        rbtAvgCurrencyRate.Checked = true;
                    }
                    else 
                    {
                        rbtAvgCurrencyRate.Checked = false;
                    }


                    if (RrbtAvgMetalRate == "1")
                    {
                        rbtAvgMetalRate.Checked = true;
                    }
                    else
                    {
                        rbtAvgMetalRate.Checked = false;
                    }

                    if (RrbtAvgMakingRate == "1")
                    {
                        rbtAvgMakingRate.Checked = true;
                    }
                    else
                    {
                        rbtAvgMakingRate.Checked = false;
                    }

                    if (RrbtAvgStoneMakingRate == "1")
                    {
                        rbtAvgStoneMakingRate.Checked = true;
                    }
                    else
                    {
                        rbtAvgStoneMakingRate.Checked = false;
                    }

                    if (RrbtAvgCarryingRate == "1")
                    {
                        rbtAvgCarryingRate.Checked = true;
                    }
                    else
                    {
                        rbtAvgCarryingRate.Checked = false;
                    }
                }

            }
        }

        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode > 1 AND CurrencyCode != 99";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");
        }
        protected void RemoveSession()
        {

            Session["ProgFlag"] = string.Empty;
            Session["StxtFromDate"] = string.Empty;
            Session["StxtToDate"] = string.Empty;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {


                Session["ProgFlag"] = "1";
                Session["StxtFromDate"] = txtFromDate.Text;
                Session["StxtToDate"] = txtToDate.Text;


                if (ChkAllLocation.Checked == true)
                {
                    Session["SChkAllLocation"] = "1";
                }
                else 
                {
                    Session["SChkAllLocation"] = "0";
                }

                Session["SddlLocation"] = ddlLocation.SelectedValue;


                if (ChkAllKarat.Checked == true)
                {
                    Session["SChkAllKarat"] = "1";
                }
                else
                {
                    Session["SChkAllKarat"] = "0";
                }

                Session["SddlKarat"] = ddlKarat.SelectedValue;

                


                if (rbtAvgCurrencyRate.Checked == true)
                {
                    Session["SrbtAvgCurrencyRate"] = "1";
                }
                else 
                {
                    Session["SrbtAvgCurrencyRate"] = "0";
                }

                if (rbtAvgMetalRate.Checked == true)
                {
                    Session["SrbtAvgMetalRate"] = "1";
                }
                else 
                {
                    Session["SrbtAvgMetalRate"] = "0";
                }

                if (rbtAvgMakingRate.Checked == true)
                {
                    Session["SrbtAvgMakingRate"] = "1";
                }
                else 
                {
                    Session["SrbtAvgMakingRate"] = "0";
                }
                
                if (rbtAvgStoneMakingRate.Checked == true)
                {
                    Session["SrbtAvgStoneMakingRate"] = "1";
                }
                else 
                {
                    Session["SrbtAvgStoneMakingRate"] = "0";
                }


                if (rbtAvgCarryingRate.Checked == true)
                {
                    Session["SrbtAvgCarryingRate"] = "1";
                }
                else 
                {
                    Session["SrbtAvgCarryingRate"] = "0";
                }

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtFromDate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txtToDate.Text));

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, ddlCurrency.SelectedValue);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");


                if (rbtAvgCurrencyRate.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsAverageCurrencyExchangeReport");
                }
                else if (rbtAvgMetalRate.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsAverageMetalRateReport");
                }
                else if (rbtAvgMakingRate.Checked == true)
                {
                    if (ChkAllLocation.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                    }
                    else 
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, ddlLocation.SelectedValue);
                    }


                    if (ChkAllKarat.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRAN_SW, 0);
                    }
                    else
                    {
                        int code = Converter.GetInteger(ddlKarat.SelectedValue);

                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRAN_SW, code);
                    }


                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsAverageMakingRateReport");
                }
                else if (rbtAvgStoneMakingRate.Checked == true)
                {
                    if (ChkAllLocation.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, ddlLocation.SelectedValue);
                    }

                    if (ChkAllKarat.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRAN_SW, 0);
                    }
                    else
                    {
                        int code = Converter.GetInteger(ddlKarat.SelectedValue);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRAN_SW, code);
                    }

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsAverageStoneMakingRateReport");
                }
                else if (rbtAvgCarryingRate.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsAverageCarryingRateReport");
                }



                Response.Redirect("ReportServer.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void ChkAllLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllLocation.Checked == true)
            {
                ddlLocation.Enabled = false;
            }
            else
            {
                ddlLocation.Enabled = true;
            }
        }


        protected void ChkAllKarat_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllKarat.Checked == true)
            {
                ddlKarat.Enabled = false;
            }
            else
            {
                ddlKarat.Enabled = true;
            }
        }
        protected void rbtAvgCurrencyRate_CheckedChanged(object sender, EventArgs e)
        {
            ChkAllLocation.Visible = false;
            ddlLocation.Visible = false;

            ChkAllKarat.Visible = false;
            ddlKarat.Visible = false;
        }

        protected void rbtAvgMetalRate_CheckedChanged(object sender, EventArgs e)
        {
            ChkAllLocation.Visible = false;
            ddlLocation.Visible = false;

            ChkAllKarat.Visible = false;
            ddlKarat.Visible = false;
        }

        protected void rbtAvgMakingRate_CheckedChanged(object sender, EventArgs e)
        {
            ChkAllLocation.Visible = true;
            ddlLocation.Visible = true;

            ChkAllKarat.Visible = true;
            ddlKarat.Visible = true;
        }

        protected void rbtAvgStoneMakingRate_CheckedChanged(object sender, EventArgs e)
        {
            ChkAllLocation.Visible = true;
            ddlLocation.Visible = true;

            ChkAllKarat.Visible = true;
            ddlKarat.Visible = true;
        }

        protected void rbtAvgCarryingRate_CheckedChanged(object sender, EventArgs e)
        {
            ChkAllLocation.Visible = false;
            ddlLocation.Visible = false;

            ChkAllKarat.Visible = false;
            ddlKarat.Visible = false;
        }

        
    }
}
