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
    public partial class A2ZBankAccountStatement : System.Web.UI.Page
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

                    BankDropdown();
                    CurrencyDropdown();

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

                        chkAllCurrency.Checked = true;
                        ddlCurrency.Enabled = false;
                        ddlCurrency.SelectedIndex = 0;

                        lblLedgerCode.Visible = false;
                        txtAccNo.Visible = false;
                        ddlAccNo.Visible = false;
                        

                    }
                    else
                    {
                        lblModule.Text = Module;
                    }


                    lblLedgerCode.Visible = false;
                    txtAccNo.Visible = false;
                    ddlAccNo.Visible = false;


                    


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
                        txtfdate.Text = Converter.GetString(date);
                        txttdate.Text = Converter.GetString(date);
                        lblProcDate.Text = Converter.GetString(date);

                        CtrlOldAccNo.Text = Converter.GetString(dto.OldAccNoFlag);



                        FunctionName();

                    }
                    else
                    {

                        string RddlBank = (string)Session["SddlBank"];

                        string RchkAllCurrency = (string)Session["SchkAllCurrency"];

                        string RddlCurrency = (string)Session["SddlCurrency"];
                        string RtxtAccNo = (string)Session["StxtAccNo"];
                        string RddlAccNo = (string)Session["SddlAccNo"];

                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];





                        string Cflag = (string)Session["CFlag"];
                        CFlag.Text = Cflag;

                        

                        ddlBank.SelectedValue = RddlBank;


                        if (RchkAllCurrency == "1")
                        {
                            chkAllCurrency.Checked = true;
                            ddlCurrency.SelectedIndex = 0;   
                            ddlCurrency.Enabled = false;

                            lblLedgerCode.Visible = false;
                            txtAccNo.Visible = false;
                            ddlAccNo.Visible = false;

                        }
                        else
                        {
                            chkAllCurrency.Checked = false;
                            ddlCurrency.SelectedValue = RddlCurrency;
                            ddlCurrency.Enabled = true;

                            lblLedgerCode.Visible =true;
                            txtAccNo.Visible = true;
                            ddlAccNo.Visible = false;

                            txtAccNo.Text = RtxtAccNo;
                           
                        }



                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;


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

        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode != 99";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");
        }

        private void BankDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 22 GROUP BY PartyCode,PartyName";
            ddlBank = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlBank, "A2ZACGMS");
        }

        

        protected void FunctionName()
        {

            lblStatementFunc.Text = "Bank Account Statement";

        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";

            Session["SddlBank"] = ddlBank.SelectedValue;


            if (chkAllCurrency.Checked == true)
            {
                Session["SchkAllCurrency"] = "1";
            }
            else
            {
                Session["SchkAllCurrency"] = "0";
            }

            Session["SddlCurrency"] = ddlCurrency.SelectedValue;

            Session["StxtAccNo"] = txtAccNo.Text;

            Session["SddlAccNo"] = ddlAccNo.SelectedValue;

            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;

            


            Session["SlblProcDate"] = lblProcDate.Text;



            Session["SModule"] = lblModule.Text;

            Session["flag"] = "1";



            Session["CFlag"] = CFlag.Text;

            Session["SlblAutoRenewal"] = lblAutoRenewal.Text;
            Session["SlblDepositAmount"] = lblDepositAmount.Text;
            Session["SlblTotDepositAmount"] = lblTotDepositAmount.Text;
            Session["StAccFixedMthInt"] = tAccFixedMthInt.Text;

            Session["SMultiAccFlag"] = MultiAccFlag.Text;


        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlBank.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Bank Code');", true);
                    return;
                }


                //if (ddlCurrency.SelectedIndex == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Currency Code');", true);
                //    return;
                //}


                StoreRecordsSession();

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                int fYear = fdate.Year;
                int bYear = Converter.GetInteger(lblBegFinYear.Text);

                if (fYear < bYear)
                {
                    txtfdate.Text = lblProcDate.Text;
                    txtfdate.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid From Date Input');", true);
                    return;
                }


                // FOR From Date and To Date
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));

                //  For Account Type and Name Desc. 




                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.KEYCODE, ddlBank.SelectedValue);


                if (chkAllCurrency.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, 0);
                   
                }
                else 
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, txtAccNo.Text);
                    
                }

                
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlBank.SelectedItem.Text);
               

                int id = Converter.GetInteger(lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.USERNO, id);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.USERNAME, lblIDName.Text);

                //============== End For Multi User Parameter =========================
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMBankAccountStatement");

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

            Session["SddlBranchNo"] = string.Empty;

            Session["StxtAccNo"] = string.Empty;
            Session["SlblAccTitle"] = string.Empty;

            Session["SlblBranchNo"] = string.Empty;

            Session["SCtrlAccType"] = string.Empty;

            Session["StxtAccType"] = string.Empty;
            Session["SddlAccType"] = string.Empty;

            Session["StxtAccType1"] = string.Empty;
            Session["SddlAccType1"] = string.Empty;

            Session["StxtOldAccNo1"] = string.Empty;


            Session["StxtCULBMemNo"] = string.Empty;
            Session["SlblMemName"] = string.Empty;


            Session["StxtMemberNo"] = string.Empty;
            Session["SlblMemType"] = string.Empty;
            Session["SlblMemNo"] = string.Empty;


            Session["StxtAccountNo"] = string.Empty;
            Session["Stxtstat"] = string.Empty;


            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;


            
            Session["SChkAccStatus"] = string.Empty;

            Session["SlblProcDate"] = string.Empty;


            



        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }


        protected void InvalidCreditUnionMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union');", true);
            return;
        }

        protected void InvalidMemberMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Member No.');", true);
            return;
        }
        protected void InvalidAccountMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
            return;
        }


        protected void InvalidAccountNoMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Does Not Exist');", true);

            return;
        }

        protected void InvalidCuNoMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union');", true);
            return;
        }

        protected void AccessDeniedMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Allowed Credit Union');", true);
            return;
        }


        private void InvalidAccountNotMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Number');", true);
            return;
        }
        protected void AccTransferedMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Transfered');", true);
            return;
        }


        

        public void clearInfo()
        {

            tOpenDt.Text = string.Empty;
            tMaturityDt.Text = string.Empty;
            tRenewalDt.Text = string.Empty;
            tAccPeriod.Text = string.Empty;
            tOrgAmt.Text = string.Empty;
            tPrincipleAmt.Text = string.Empty;
            tIntRate.Text = string.Empty;
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            AccDropdown();
        }



        private void AccDropdown()
        {
            lblMsgFlag.Text = "0";

            string sqlquery = "SELECT AccType,AccNo from A2ZACCOUNT WHERE AccPartyNo = '" + ddlBank.SelectedValue + "' AND AccCurrency = '" + ddlCurrency.SelectedValue + "' AND AccStatus = 1";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZACGMS");


            string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + ddlBank.SelectedValue + "' AND AccCurrency = '" + ddlCurrency.SelectedValue + "' AND AccStatus = 1";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec == 1)
            {
                CtrlAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                txtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
               
                
                //Int64 accno = Converter.GetLong(txtAccNo.Text);
                //A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfoAccNo(accno));

                //if (getDTO.AccNo > 0)
                //{

                //    int code = Converter.GetInteger(lblLdgCurrencyCode.Text);
                //    A2ZCURRENCYDTO get1DTO = (A2ZCURRENCYDTO.GetInformation(code));

                //    if (get1DTO.CurrencyCode > 0)
                //    {
                //        txtLedgerCurrency.Text = Converter.GetString(get1DTO.CurrencyName);
                //        txtledgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance)) + " " + txtLedgerCurrency.Text;

                //    }

                //}

                lblLedgerCode.Visible = true;
                txtAccNo.Visible = true;
                ddlAccNo.Visible = false;
            }
            else if (totrec > 1)
            {
                lblLedgerCode.Visible = true;
                txtAccNo.Visible = false;

                ddlAccNo.Visible = true;
            }
            else
            {
                lblMsgFlag.Text = "1";
            }
        }

        protected void chkAllCurrency_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCurrency.Checked)
            { 
                ddlCurrency.Enabled = false;
                ddlCurrency.SelectedIndex = 0;

                lblLedgerCode.Visible = false;
                txtAccNo.Visible = false;
                ddlAccNo.Visible = false;
            }
            else
            {            
                ddlCurrency.Enabled = true;        
            }
        }

               

    }

}
