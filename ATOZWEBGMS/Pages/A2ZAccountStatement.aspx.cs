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
    public partial class A2ZAccountStatement : System.Web.UI.Page
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

                    }
                    else
                    {
                        lblModule.Text = Module;
                    }



                    
                    AccountType1Dropdown();

                    CurrencyDropdown();

                    

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

                        chkAllCurrency.Checked = true;
                        ddlCurrency.Enabled = false;
                        ddlCurrency.SelectedIndex = 0;

                        lblAccountNo.Visible = false;
                        txtAccountNo.Visible = false;
                        lblAccCurrency.Visible = false;


                        FunctionName();



                        lblAccType.Visible = true;
                        txtAccType.Visible = true;
                        ddlAccType.Visible = true;





                    }
                    else
                    {

                        
                        string RtxtAccType = (string)Session["StxtAccType"];
                        string RddlAccType = (string)Session["SddlAccType"];

                        string RtxtPartyCode = (string)Session["StxtPartyCode"];
                        string RddlPartyName = (string)Session["SddlPartyName"];

                        string RchkAllCurrency = (string)Session["SchkAllCurrency"];

                        string RddlCurrency = (string)Session["SddlCurrency"];

                        string RtxtAccountNo = (string)Session["StxtAccountNo"];


                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];

                        string RChkAccStatus = (string)Session["SChkAccStatus"];





                        string Cflag = (string)Session["CFlag"];
                        CFlag.Text = Cflag;

                        //txtMemberNo.Text = RtxtMemberNo;
                        //lblMemType.Text = RlblMemType;
                        //lblMemNo.Text = RlblMemNo;
                        //lblMemName.Text = RlblMemName;

                        


                        lblAccType.Visible = true;
                        txtAccType.Visible = true;
                        ddlAccType.Visible = true;



                        


                        txtAccType.Text = RtxtAccType;
                        ddlAccType.SelectedValue = RddlAccType;

                        PartyDropdown();

                        txtPartyCode.Text = RtxtPartyCode;
                        ddlPartyName.Text = RddlPartyName;

                                                                      

                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;


                        if (RchkAllCurrency == "1")
                        {
                            chkAllCurrency.Checked = true;
                            ddlCurrency.SelectedIndex = 0;
                            ddlCurrency.Enabled = false;

                            lblAccountNo.Visible = false;
                            txtAccountNo.Visible = false;
                            lblAccCurrency.Visible = false;

                        }
                        else
                        {
                            chkAllCurrency.Checked = false;
                            ddlCurrency.SelectedValue = RddlCurrency;
                            ddlCurrency.Enabled = true;

                            lblAccountNo.Visible = true;
                            txtAccountNo.Visible = true;
                            lblAccCurrency.Visible = true;

                            txtAccountNo.Text = RtxtAccountNo;

                        }



                        if (RChkAccStatus == "1")
                        {
                            ChkAccStatus.Checked = true;
                        }
                        else
                        {
                            ChkAccStatus.Checked = false;
                        }

                        

                        txtAccountNo.Focus();

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
        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode='" + txtAccType.Text + "' GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }
        protected void AccountType1Dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription FROM A2ZACCTYPE";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlAccType, "A2ZACGMS");
        }




        protected void FunctionName()
        {

            lblStatementFunc.Text = "Loan Detail Report";

        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";

            Session["StxtAccType"] = txtAccType.Text;
            Session["SddlAccType"] = ddlAccType.SelectedValue;

            Session["StxtPartyCode"] = txtPartyCode.Text;
            Session["SddlPartyName"] = ddlPartyName.SelectedValue;

            if (chkAllCurrency.Checked == true)
            {
                Session["SchkAllCurrency"] = "1";
            }
            else
            {
                Session["SchkAllCurrency"] = "0";
            }

            Session["SddlCurrency"] = ddlCurrency.SelectedValue;

            //Session["StxtAccNo"] = txtAccNo.Text;

            //Session["SddlAccNo"] = ddlAccNo.SelectedValue;


            Session["StxtAccountNo"] = txtAccountNo.Text;

            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;


                      
            


        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAccType.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Account Type.');", true);
                    return;
                }

                if (txtPartyCode.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Party Code');", true);
                    return;
                }

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

                int atype = 0;

                //atype = Converter.GetInteger(txtAccType.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, atype);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlAccType.SelectedItem.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCTYPE, txtAccType.Text);
                //CtrlAccType.Text = Converter.GetString(txtAccType.Text);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.KEYCODE, txtPartyCode.Text);


                if (chkAllCurrency.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, txtAccountNo.Text);
                }


                


                
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlPartyName.SelectedItem.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblAccCurrency.Text);

                //int id = Converter.GetInteger(lblID.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.USERNO, id);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.USERNAME, lblIDName.Text);

                //============== End For Multi User Parameter =========================
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGoldLoanDetailListReport");

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



            Session["StxtAccType"] = string.Empty;
            Session["SddlAccType"] = string.Empty;

            Session["StxtPartyCode"] = string.Empty;
            Session["SddlPartyName"] = string.Empty;



            Session["StxtAccountNo"] = string.Empty;
          


            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;


            

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


        protected void GetInfo()
        {
            try
            {
                Int64 AccNumber = Converter.GetLong(txtAccountNo.Text);
                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));

                if (getDTO.a > 0)
                {
                    CtrlAccStatus.Text = Converter.GetString(getDTO.AccStatus);
                    CtrlAccType.Text = Converter.GetString(getDTO.AccType);


                    if (CtrlAccStatus.Text == "98")
                    {
                        txtAccountNo.Text = string.Empty;
                        txtAccountNo.Focus();
                        AccTransferedMSG();
                        return;
                    }

                    //Int16 AccStat = Converter.GetSmallInteger(CtrlAccStatus.Text);
                    //A2ZACCSTATUSDTO get3DTO = (A2ZACCSTATUSDTO.GetInformation(AccStat));
                    //if (get3DTO.AccStatusCode > 0)
                    //{
                    //    txtstat.Text = Converter.GetString(get3DTO.AccStatusDescription);
                    //}

                    txtPartyCode.Text = Converter.GetString(getDTO.AccPartyNo);

                    txtAccType.Text = Converter.GetString(CtrlAccType.Text);
                    ddlAccType.SelectedValue = Converter.GetString(CtrlAccType.Text);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetInfo Problem');</script>");


            }
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


        private void InvalidMemMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Member No.');", true);
            return;
        }


        protected void gvGroupAccInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }



        protected void gvGroupAccInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvGroupAccInfo.SelectedRow;
            CtrlAccType.Text = row.Cells[0].Text;

            //if (CtrlAccType.Text == "999")
            //{
            //    lblAccTypeTitle.Text = row.Cells[1].Text;
            //}
            //else
            //{
            //    lblAccTypeTitle.Text = string.Empty;
            //}

            txtAccountNo.Text = row.Cells[2].Text;
            lblAccCurrency.Text = row.Cells[3].Text;

            GetInfo();
        }


        private void InvalidAcc()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not Found');", true);
            return;
        }


        protected void txtAccountNo_TextChanged(object sender, EventArgs e)
        {
            int a = txtAccountNo.Text.Length;

            if (lblUnitFlag.Text != "1" && a > 15)
            {
                txtAccountNo.Text = string.Empty;
                txtAccountNo.Focus();
                InvalidAcc();
                return;
            }
            else if (lblUnitFlag.Text == "1" && a > 12)
            {
                txtAccountNo.Text = string.Empty;
                txtAccountNo.Focus();
                InvalidAcc();
                return;
            }

            string b = txtAccountNo.Text;

            string c = string.Empty;

            if (lblUnitFlag.Text != "1")
            {
                c = b.Substring(3, 3);
            }
            else
            {
                c = b.Substring(0, 3);
            }

            CtrlAccType.Text = c;

            GetInfo();
        }



        protected void ChkAccStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (txtPartyCode.Text == string.Empty)
            {
                txtPartyCode.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Party Code');", true);
                return;
            }

            if (ChkAccStatus.Checked)
            {
                ChkAccStatus.Text = "Active A/c";
            }
            else
            {
                ChkAccStatus.Text = "Closed A/c";
            }

            //if (txtAccType.Text != string.Empty)
            //{
            //    gvGroupDetail();
            //}


        }

        private void InitializedRecords()
        {
            txtPartyCode.Text = string.Empty;
            if (ddlPartyName.SelectedIndex != -1)
            {
                ddlPartyName.SelectedIndex = 0;
            }
            
            txtAccountNo.Text = string.Empty;
            lblAccCurrency.Text = string.Empty;

        }
        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {

            InitializedRecords();

            MultiAccFlag.Text = "0";
            gvGroupAccInfo.Visible = false;



            int atype;
            A2ZACCTYPEDTO getDTO = new A2ZACCTYPEDTO();

            atype = Converter.GetInteger(txtAccType.Text);
            getDTO = (A2ZACCTYPEDTO.GetInformation(atype));


            if (getDTO.AccTypeCode > 0)
            {
                txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                CtrlAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                ddlAccType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);

                PartyDropdown();


                

            }
            else
            {
                txtAccType.Text = string.Empty;
                txtAccType.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid A/C Type');", true);
                return;
            }
        }


        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAccType.Text = ddlAccType.SelectedValue;

            InitializedRecords();

            int atype;
            A2ZACCTYPEDTO getDTO = new A2ZACCTYPEDTO();


            atype = Converter.GetInteger(txtAccType.Text);
            getDTO = (A2ZACCTYPEDTO.GetInformation(atype));


            if (getDTO.AccTypeCode > 0)
            {
                txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                CtrlAccType.Text = Converter.GetString(getDTO.AccTypeCode);

                PartyDropdown();

                //MultiAccFlag.Text = "0";
                //gvGroupAccInfo.Visible = false;

                //FindAccountNo();
                //if (MSGFlag.Text == "1")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not In File');", true);
                //    txtAccType.Text = string.Empty;
                //    ddlAccType.SelectedIndex = 0;
                //    txtAccountNo.Text = string.Empty;
                //    txtAccountNo.Visible = true;
                //    txtAccType.Focus();
                //    return;
                //}

            }
            else
            {
                txtAccType.Text = string.Empty;
                txtAccType.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid A/C Type');", true);
                return;
            }
        }


        private void FindAccountNo()
        {
            MSGFlag.Text = "0";

            string qry = string.Empty;

            if (ChkAccStatus.Checked == true)
            {
                qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT WHERE AccPartyNo='" + txtPartyCode.Text + "' AND AccType='" + txtAccType.Text + "' AND AccCurrency='" + ddlCurrency.SelectedValue + "' AND AccStatus < 98";
            }
            else
            {
                qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT WHERE AccPartyNo='" + txtPartyCode.Text + "' AND AccType='" + txtAccType.Text + "' AND AccCurrency='" + ddlCurrency.SelectedValue + "' AND AccStatus = 99";
            }


            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        txtAccountNo.Visible = true;
                        txtAccountNo.Text = dr["AccNo"].ToString();
                        lblCurrency.Text = dr["AccCurrency"].ToString();

                        //int code = Converter.GetInteger(lblCurrency.Text);
                        //A2ZCURRENCYDTO get1DTO = (A2ZCURRENCYDTO.GetInformation(code));

                        //if (get1DTO.CurrencyCode > 0)
                        //{
                        //    lblAccCurrency.Text = Converter.GetString(get1DTO.CurrencyName);
                         
                        //}


                        GetInfo();
                    }
                }
                else
                {
                    MultiAccFlag.Text = "1";

                    txtAccountNo.Text = string.Empty;
                    txtAccountNo.Focus();
                    txtAccountNo.Visible = true;

                    gvGroupAccInfo.Visible = true;

                    string sqlquery3 = "SELECT distinct AccType,A2ZACCTYPE.AccTypeDescription,AccNo,A2ZCURRENCY.CurrencyName,AccStatus,AccStatusDescription FROM A2ZACCOUNT inner join A2ZACCTYPE on A2ZACCTYPE.AccTypeCode=A2ZACCOUNT.AccType inner join A2ZCURRENCY on A2ZCURRENCY.CurrencyCode=A2ZACCOUNT.AccCurrency inner join A2ZACCSTATUS on A2ZACCSTATUS.AccStatusCode=A2ZACCOUNT.AccStatus WHERE AccPartyNo='" + txtPartyCode.Text + "' AND AccType='" + txtAccType.Text + "' AND AccStatus < 98";
                    gvGroupAccInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvGroupAccInfo, "A2ZACGMS");

                }
            }
            else
            {
                MSGFlag.Text = "1";
            }


        }



        protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        {
            if (txtPartyCode.Text != string.Empty)
            {
                int PartyCode = Converter.GetInteger(txtPartyCode.Text);
                A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                if (getDTO.PartyName == null)
                {
                    txtPartyName.Text = string.Empty;
                    txtPartyCode.Text = string.Empty;
                    txtPartyCode.Focus();
                }

                else
                {
                    txtPartyName.Text = Converter.GetString(getDTO.PartyName);

                    

                }
            }
        }


        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyName.SelectedIndex != 0)
            {
                int PartyCode = Converter.GetInteger(ddlPartyName.SelectedValue);
                A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                if (getDTO.PartyName != string.Empty)
                {
                    txtPartyCode.Text = Converter.GetString(getDTO.PartyCode);

                    
                }
            }
        }

        protected void chkAllCurrency_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCurrency.Checked)
            {
                ddlCurrency.Enabled = false;
                ddlCurrency.SelectedIndex = 0;

                lblAccountNo.Visible = false;
                txtAccountNo.Visible = false;
                lblAccCurrency.Visible = false;
            }
            else
            {
                ddlCurrency.Enabled = true;
            }
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiAccFlag.Text = "0";
            gvGroupAccInfo.Visible = false;

            FindAccountNo();
            if (MSGFlag.Text == "1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not In File');", true);
                txtAccType.Text = string.Empty;
                ddlAccType.SelectedIndex = 0;
                txtAccountNo.Text = string.Empty;
                txtAccountNo.Visible = true;
                txtAccType.Focus();
                return;
            }
        }

        

    }

}
