using System;
using ATOZWEBGMS.WebSessionStore;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.SystemControl;

namespace ATOZWEBGMS.Pages
{
    public partial class A2ZExpenseTransactionReport : System.Web.UI.Page
    {

        //public string TmpOpenDate;
        //public string TmpAccMatureDate;
        //public string TmpAccPrevRenwlDate;
        //public Int16 TmpAccPeriod;
        //public Decimal TmpAccOrgAmt;
        //public Decimal TmpAccPrincipal;
        //public Decimal TmpAccIntRate;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    string NewAccNo = (string)Session["NewAccNo"];
                    string flag = (string)Session["flag"];
                    lblflag.Text = flag;

                    CashDropdown();


                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));


                    //lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    //lblBranchNo.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.BRNO));
                    //lblUnitFlag.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_UNIT_FLAG));
                    //lblUserLabel.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_LEVEL));


                    string cflag = (string)Session["CFlag"];

                    CFlag.Text = cflag;

                    string Module = (string)Session["SModule"];

                    if (lblflag.Text == string.Empty)
                    {
                        lblModule.Text = Request.QueryString["a%b"];
                        MultiAccFlag.Text = "0";

                        chkAllJournal.Checked = true;

                        ddlAccNo.Enabled = false;
                        ddlAccNo.SelectedIndex = 0;

                    }
                    else
                    {
                        lblModule.Text = Module;
                    }





                    string PFlag = (string)Session["ProgFlag"];
                    CtrlProgFlag.Text = PFlag;

                    if (CtrlProgFlag.Text != "1")
                    {


                        var p = A2ZERPSYSPRMDTO.GetParameterValue();
                        lblCompanyName.Text = Converter.GetString(p.PrmUnitName);
                        lblBranchName.Text = Converter.GetString(p.PrmUnitName);


                        //var p = A2ZERPSYSPRMDTO.GetParameterValue();
                        //lblBranchNo.Text = Converter.GetString(p.PrmUnitNo);

                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtFromDate.Text = Converter.GetString(date);
                        txtToDate.Text = Converter.GetString(date);
                        lblProcDate.Text = Converter.GetString(date);

                        CtrlOldAccNo.Text = Converter.GetString(dto.OldAccNoFlag);



                        FunctionName();

                    }
                    else
                    {

                        string RddlCash = (string)Session["SddlCash"];

                        string RlblPartyAccNo = (string)Session["SlblPartyAccNo"];


                        string RchkAllJournal = (string)Session["SchkAllJournal"];


                        string RddlAccNo = (string)Session["SddlAccNo"];

                        string RtxtFromDate = (string)Session["StxtFromDate"];
                        string RtxtToDate = (string)Session["StxtToDate"];

                        string RrbtDetails = (string)Session["SrbtDetails"];
                     



                        string Cflag = (string)Session["CFlag"];
                        CFlag.Text = Cflag;


                        ddlCash.SelectedValue = RddlCash;

                        ExpenseDropdown();

                        lblPartyAccNo.Text = RlblPartyAccNo;

                        if (RchkAllJournal == "1")
                        {
                            chkAllJournal.Checked = true;
                            ddlAccNo.SelectedIndex = 0;
                            ddlAccNo.Enabled = false;


                        }
                        else
                        {
                            chkAllJournal.Checked = false;
                            ddlAccNo.SelectedValue = RddlAccNo;
                            ddlAccNo.Enabled = true;

                        }


                        if (RrbtDetails == "1")
                        {
                            rbtDetails.Checked = true;
                            rbtSummary.Checked = false;
                            rbtDailyExpense.Checked = false;

                        }
                        else if (RrbtDetails == "2")
                        {
                            rbtDetails.Checked = false;
                            rbtSummary.Checked = true;
                            rbtDailyExpense.Checked = false;

                        }
                        else if (RrbtDetails == "3")
                        {
                            rbtDetails.Checked = false;
                            rbtSummary.Checked = false;
                            rbtDailyExpense.Checked = true;
                        }


                        txtFromDate.Text = RtxtFromDate;
                        txtToDate.Text = RtxtToDate;


                    }


                    A2ZERPSYSPRMDTO dto1 = A2ZERPSYSPRMDTO.GetParameterValue();
                    lblBegFinYear.Text = Converter.GetString(dto1.PrmBegFinYear);

                    FunctionName();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");


                //throw ex;
            }
        }



        private void ExpenseDropdown()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode = 51 AND RIGHT(PartyAccNo,1) = " + ddlCash.SelectedValue.Substring(5, 1) + " GROUP BY PartyAccNo,PartyName";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZACGMS");
        }



        private void CashDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 16 GROUP BY PartyCode,PartyName";
            ddlCash = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCash, "A2ZACGMS");
        }

        protected void FunctionName()
        {

            lblStatementFunc.Text = "Expense Transaction Report";

        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";



            if (chkAllJournal.Checked == true)
            {
                Session["SchkAllJournal"] = "1";
            }
            else
            {
                Session["SchkAllJournal"] = "0";
            }


            Session["SddlCash"] = ddlCash.SelectedValue;

            Session["SlblPartyAccNo"] = lblPartyAccNo.Text;




            Session["SddlAccNo"] = ddlAccNo.SelectedValue;

            Session["StxtFromDate"] = txtFromDate.Text;
            Session["StxtToDate"] = txtToDate.Text;

            if (rbtDetails.Checked == true)
            {
                Session["SrbtDetails"] = "1";
            }
            else if (rbtSummary.Checked == true)
            {
                Session["SrbtDetails"] = "2";
            }
            else if (rbtDailyExpense.Checked == true)
            {
                Session["SrbtDetails"] = "3";
            }


            Session["SlblProcDate"] = lblProcDate.Text;



            Session["SModule"] = lblModule.Text;

            Session["flag"] = "1";



            Session["CFlag"] = CFlag.Text;



        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtDailyExpense.Checked == true && txtFromDate.Text != txtToDate.Text)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date should be same as To Date');", true);
                    return;
                }

                if (ddlCash.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Cash');", true);
                    return;
                }

                StoreRecordsSession();

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                DateTime fdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                int fYear = fdate.Year;
                int bYear = Converter.GetInteger(lblBegFinYear.Text);

                if (fYear < bYear)
                {
                    txtFromDate.Text = lblProcDate.Text;
                    txtFromDate.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid From Date Input');", true);
                    return;
                }


                // FOR From Date and To Date
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtFromDate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txtToDate.Text));

                //  For Account Type and Name Desc. 






                if (chkAllJournal.Checked == true)
                {
                    string a = Converter.GetString(0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, a);
                }
                else
                {
                    string a = Converter.GetString(ddlAccNo.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, a);
                }


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, lblPartyAccNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlCash.SelectedItem.Text);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, Converter.GetInteger(lblID.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblIDName.Text);


                int id = Converter.GetInteger(lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.USERNO, id);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.USERNAME, lblIDName.Text);

                //============== End For Multi User Parameter =========================
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);

                if (rbtSummary.Checked == true && ddlAccNo.SelectedIndex != 0)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsSummaryExpenseSingle");

                }
                else if (rbtDetails.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsDetailExpenseTransactionReport");
                }
                else if (rbtDailyExpense.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsDetailDailyExpenseTransactionReport");
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsSummaryExpenseTransactionReport");
                }

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }
        }


        protected void RemoveSession()
        {
            Session["flag"] = string.Empty;
            Session["NewAccNo"] = string.Empty;
            Session["RTranDate"] = string.Empty;
            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;
            Session["SControlFlag"] = string.Empty;

            Session["ProgFlag"] = string.Empty;

            Session["CFlag"] = string.Empty;

            Session["SddlCash"] = string.Empty;


            Session["SddlAccNo"] = string.Empty;
            Session["SchkAllJournal"] = string.Empty;
            Session["SrbtDetails"] = string.Empty;

            Session["StxtFromDate"] = string.Empty;
            Session["StxtToDate"] = string.Empty;


        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }



        protected void chkAllJournal_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlCash.SelectedIndex != 0)
            {
                if (chkAllJournal.Checked)
                {
                    ddlAccNo.SelectedIndex = 0;
                    ddlAccNo.Enabled = false;
                }
                else
                {

                    ddlAccNo.Enabled = true;
                }
            }
            else
            {
                chkAllJournal.Checked = true;
            }
        }

        protected void ddlCash_SelectedIndexChanged(object sender, EventArgs e)
        {

            string qry = "SELECT PartyAccNo FROM A2ZPARTYCODE  WHERE PartyCode = '" + ddlCash.SelectedValue + "' AND RIGHT(PartyAccNo,1) = " + ddlCash.SelectedValue.Substring(5, 1) + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            if (dt.Rows.Count > 0)
            {
                lblPartyAccNo.Text = Converter.GetString(dt.Rows[0]["PartyAccNo"]);
            }

            ExpenseDropdown();
        }
    }

}
