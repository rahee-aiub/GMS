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
    public partial class A2ZLossProfitDetailsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnView.Focus();

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;

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
                    string RrbtFixingProfit = (string)Session["SrbtFixingProfit"];
                    string RrbtPMCProfit = (string)Session["SrbtPMCProfit"];
                    string RrbtGoldProfit = (string)Session["SrbtGoldProfit"];
                    string RrbtCurrencyProfitAnalysis = (string)Session["SrbtCurrencyProfitAnalysis"];
                    string RrbtCurrencyProfitPurchaseSales = (string)Session["SrbtCurrencyProfitPurchaseSales"];
                    string RrbtGoldCurrencyProfitPurchaseSales = (string)Session["SrbtGoldCurrencyProfitPurchaseSales"];
                    string RrbtOfficeExpense = (string)Session["SrbtOfficeExpense"];
                    string RrbtOfficeExpenseDetails = (string)Session["SrbtOfficeExpenseDetails"];
                    string RrbtLoanSummary = (string)Session["SrbtLoanSummary"];  
                    string RrbtGmsAdjustmentAndDiscount = (string)Session["SrbtGmsAdjustmentAndDiscount"];
                    string RrbtLossProfit = (string)Session["SrbtLossProfit"];
                    string RrbtProfitDtl = (string)Session["SrbtProfitDtl"];

                    
                    txtFromDate.Text = RtxtFromDate;
                    txtToDate.Text = RtxtToDate;


                    if (RrbtFixingProfit == "1")
                    {
                        rbtFixingProfit.Checked = true;
                    }
                    else
                    {
                        rbtFixingProfit.Checked = false;
                    }

                    if (RrbtPMCProfit == "1")
                    {
                        rbtPMCProfit.Checked = true;
                    }
                    else
                    {
                        rbtPMCProfit.Checked = false;
                    }

                    if (RrbtGoldProfit == "1")
                    {
                        rbtGoldProfit.Checked = true;
                    }
                    else
                    {
                        rbtGoldProfit.Checked = false;
                    }

                    if (RrbtCurrencyProfitAnalysis == "1")
                    {
                        rbtCurrencyProfitAnalysis.Checked = true;
                    }
                    else
                    {
                        rbtCurrencyProfitAnalysis.Checked = false;
                    }

                    if (RrbtCurrencyProfitPurchaseSales == "1")
                    {
                        rbtCurrencyProfitPurchaseSales.Checked = true;
                    }
                    else
                    {
                        rbtCurrencyProfitPurchaseSales.Checked = false;
                    }
                    if (RrbtGoldCurrencyProfitPurchaseSales == "1")
                    {
                        rbtGoldCurrencyProfitPurchaseSales.Checked = true;
                    }
                    else
                    {
                        rbtGoldCurrencyProfitPurchaseSales.Checked = false;
                    }

                    if (RrbtOfficeExpense == "1")
                    {
                        rbtOfficeExpense.Checked = true;
                    }
                    else
                    {
                        rbtOfficeExpense.Checked = false;
                    }

                    if (RrbtOfficeExpenseDetails == "1")
                    {
                        rbtOfficeExpenseDetails.Checked = true;
                    }
                    else
                    {
                        rbtOfficeExpenseDetails.Checked = false;
                    }

                    if (RrbtLoanSummary == "1")
                    {
                        rbtLoanSummary.Checked = true;
                    }
                    else
                    {
                        rbtLoanSummary.Checked = false;
                    }

                    if (RrbtGmsAdjustmentAndDiscount == "1")
                    {
                        rbtGmsAdjustmentAndDiscount.Checked = true;
                    }
                    else
                    {
                        rbtGmsAdjustmentAndDiscount.Checked = false;
                    }

                    if (RrbtLossProfit == "1")
                    {
                        rbtLossProfit.Checked = true;
                    }
                    else
                    {
                        rbtLossProfit.Checked = false;
                    }

                    if (RrbtProfitDtl == "1")
                    {
                        rbtProfitDtl.Checked = true;
                        Label7.Text = "From Date :";
                        Label1.Visible = true;
                        txtToDate.Visible = true;
                    }
                    else
                    {
                        rbtProfitDtl.Checked = false;

                        Label7.Text = "Date :";
                        Label1.Visible = false;
                        txtToDate.Visible = false;
                    }

                }

            }
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
                if (rbtProfitDtl.Checked == false)
                {
                    txtToDate.Text = txtFromDate.Text;
                }
                

                Session["ProgFlag"] = "1";
                Session["StxtFromDate"] = txtFromDate.Text;
                Session["StxtToDate"] = txtToDate.Text;

                if (rbtFixingProfit.Checked == true)
                {
                    Session["SrbtFixingProfit"] = "1";
                }
                else
                {
                    Session["SrbtFixingProfit"] = "0";
                }

                if (rbtPMCProfit.Checked == true)
                {
                    Session["SrbtPMCProfit"] = "1";
                }
                else
                {
                    Session["SrbtPMCProfit"] = "0";
                }

                if (rbtGoldProfit.Checked == true)
                {
                    Session["SrbtGoldProfit"] = "1";
                }
                else
                {
                    Session["SrbtGoldProfit"] = "0";
                }

                if (rbtCurrencyProfitAnalysis.Checked == true)
                {
                    Session["SrbtCurrencyProfitAnalysis"] = "1";
                }
                else
                {
                    Session["SrbtCurrencyProfitAnalysis"] = "0";
                }

                if (rbtCurrencyProfitPurchaseSales.Checked == true)
                {
                    Session["SrbtCurrencyProfitPurchaseSales"] = "1";
                }
                else
                {
                    Session["SrbtCurrencyProfitPurchaseSales"] = "0";
                }
                if (rbtGoldCurrencyProfitPurchaseSales.Checked == true)
                {
                    Session["SrbtGoldCurrencyProfitPurchaseSales"] = "1";
                }
                else
                {
                    Session["SrbtGoldCurrencyProfitPurchaseSales"] = "0";
                }

                if (rbtOfficeExpense.Checked == true)
                {
                    Session["SrbtOfficeExpense"] = "1";
                }
                else
                {
                    Session["SrbtOfficeExpense"] = "0";
                }


                if (rbtOfficeExpenseDetails.Checked == true)
                {
                    Session["SrbtOfficeExpenseDetails"] = "1";
                }
                else
                {
                    Session["SrbtOfficeExpenseDetails"] = "0";
                }

                if (rbtLoanSummary.Checked == true)
                {
                    Session["SrbtLoanSummary"] = "1";
                }
                else
                {
                    Session["SrbtLoanSummary"] = "0";
                } 

                if (rbtGmsAdjustmentAndDiscount.Checked == true)
                {
                    Session["SrbtGmsAdjustmentAndDiscount"] = "1";
                }
                else
                {
                    Session["SrbtGmsAdjustmentAndDiscount"] = "0";
                }

                if (rbtLossProfit.Checked == true)
                {
                    Session["SrbtLossProfit"] = "1";
                }
                else
                {
                    Session["SrbtLossProfit"] = "0";
                }

                if (rbtProfitDtl.Checked == true)
                {
                    Session["SrbtProfitDtl"] = "1";
                }
                else
                {
                    Session["SrbtProfitDtl"] = "0";
                }

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtFromDate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txtToDate.Text));

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 31);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");


                if (rbtFixingProfit.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsFixingProfitDetailReport");
                }
                else if (rbtPMCProfit.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsPuriryMakingCarringProfitReport");
                }
                else if (rbtGoldProfit.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsGoldProfitAnalysis");
                }
                else if (rbtCurrencyProfitAnalysis.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsCurrencyProfitAnalysis");
                }
                else if (rbtCurrencyProfitPurchaseSales.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsCurrencyProfitByPurchaseAndSale");
                }
                else if (rbtGoldCurrencyProfitPurchaseSales.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsCurrencyLossProfitDetail");
                }
                else if (rbtOfficeExpense.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsOfficeExpenseReport");
                }
                else if (rbtOfficeExpenseDetails.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsOfficeExpenseDetailsReport");
                }
                else if (rbtLoanSummary.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsLoanSummaryReport");
                }
                else if (rbtGmsAdjustmentAndDiscount.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsAdjustmentAndDiscount");
                }
                else if (rbtLossProfit.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsProfitReport");
                }
                else if (rbtProfitDtl.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGMSDMReport");
                }
                


                Response.Redirect("ReportServer.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        protected void rbtFixingProfit_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtPMCProfit_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtGoldProfit_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtCurrencyProfitAnalysis_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtCurrencyProfitPurchaseSales_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtGoldCurrencyProfitPurchaseSales_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtOfficeExpense_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtOfficeExpenseDetails_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtLoanSummary_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtGmsAdjustmentAndDiscount_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtLossProfit_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;
        }

        protected void rbtProfitDtl_CheckedChanged(object sender, EventArgs e)
        {
            Label7.Text = "From Date :";
            Label1.Visible = true;
            txtToDate.Visible = true;
        }

    }
}
