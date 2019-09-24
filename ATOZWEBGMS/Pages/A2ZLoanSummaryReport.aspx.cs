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
    public partial class A2ZLoanSummaryReport : System.Web.UI.Page
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



                        lblAccType.Visible = true;
                        txtAccType.Visible = true;
                        ddlAccType.Visible = true;





                    }
                    else
                    {

                        string RddlBranchNo = (string)Session["SddlBranchNo"];

                        string RlblBranchNo = (string)Session["SlblBranchNo"];

                        string RtxtAccNo = (string)Session["StxtAccNo"];
                        string RlblAccTitle = (string)Session["SlblAccTitle"];


                        string RtxtMemberNo = (string)Session["StxtMemberNo"];
                        string RlblMemType = (string)Session["SlblMemType"];
                        string RlblMemNo = (string)Session["SlblMemNo"];
                        string RlblMemName = (string)Session["SlblMemName"];

                        string RtxtAccountNo = (string)Session["StxtAccountNo"];
                        string Rtxtstat = (string)Session["Stxtstat"];

                        string Rlblcls = (string)Session["Slblcls"];
                        string RlblDivident = (string)Session["SlblDivident"];


                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];

                        string RCtrlAccType = (string)Session["SCtrlAccType"];

                        string RtxtAccType = (string)Session["StxtAccType"];
                        string RlblAccTypeTitle = (string)Session["SlblAccTypeTitle"];

                        string RddlAccType = (string)Session["SddlAccType"];

                        string RtxtOldAccNo1 = (string)Session["StxtOldAccNo1"];

                        string RtxtAccType1 = (string)Session["StxtAccType1"];
                        string RddlAccType1 = (string)Session["SddlAccType1"];

                        string RCtrlOldAccNo = (string)Session["SCtrlOldAccNo"];


                        string RtxtCULBMemNo = (string)Session["StxtCULBMemNo"];

                        string RChkInterest = (string)Session["SChkInterest"];

                        string RChkAccStatus = (string)Session["SChkAccStatus"];


                        string RlblProcDate = (string)Session["SlblProcDate"];

                        string RlblBegFinYear = (string)Session["SlblBegFinYear"];

                        string RtClass = (string)Session["StClass"];
                        string RlblTrnCode = (string)Session["SlblTrnCode"];
                        string RtOpenDt = (string)Session["StOpenDt"];
                        string RtMaturityDt = (string)Session["StMaturityDt"];

                        string RtRenewalDt = (string)Session["StRenewalDt"];
                        string RtAccLoanSancDate = (string)Session["StAccLoanSancDate"];
                        string RtAccDisbDate = (string)Session["StAccDisbDate"];
                        string RtAccPeriod = (string)Session["StAccPeriod"];
                        string RtOrgAmt = (string)Session["StOrgAmt"];
                        string RtPrincipleAmt = (string)Session["StPrincipleAmt"];
                        string RtIntRate = (string)Session["StIntRate"];
                        string RtAccLoanSancAmt = (string)Session["StAccLoanSancAmt"];
                        string RtAccDisbAmt = (string)Session["StAccDisbAmt"];
                        string RtAccNoInstl = (string)Session["StAccNoInstl"];
                        string RtAccLoanInstlAmt = (string)Session["StAccLoanInstlAmt"];
                        string RtAccLoanLastInstlAmt = (string)Session["StAccLoanLastInstlAmt"];

                        string RlblPreAddressLine1 = (string)Session["SlblPreAddressLine1"];
                        string RlblPreTelephone = (string)Session["SlblPreTelephone"];
                        string RlblPreMobile = (string)Session["SlblPreMobile"];

                        string RlblAutoRenewal = (string)Session["SlblAutoRenewal"];
                        string RlblDepositAmount = (string)Session["SlblDepositAmount"];
                        string RlblTotDepositAmount = (string)Session["SlblTotDepositAmount"];
                        string RtAccFixedMthInt = (string)Session["StAccFixedMthInt"];

                        string RMultiAccFlag = (string)Session["SMultiAccFlag"];


                        string Cflag = (string)Session["CFlag"];
                        CFlag.Text = Cflag;

                        //txtMemberNo.Text = RtxtMemberNo;
                        //lblMemType.Text = RlblMemType;
                        //lblMemNo.Text = RlblMemNo;
                        //lblMemName.Text = RlblMemName;

                       
                        CtrlOldAccNo.Text = RCtrlOldAccNo;


                        lblAccType.Visible = true;
                        txtAccType.Visible = true;
                        ddlAccType.Visible = true;






                        lblcls.Text = Rlblcls;

                        lblDivident.Text = RlblDivident;

                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;

                        lblBranchNo.Text = RlblBranchNo;

                        CtrlAccType.Text = RCtrlAccType;

                        lblTrnCode.Text = RlblTrnCode;
                        tOpenDt.Text = RtOpenDt;
                        tMaturityDt.Text = RtMaturityDt;

                        tRenewalDt.Text = RtRenewalDt;
                        tAccLoanSancDate.Text = RtAccLoanSancDate;
                        tAccDisbDate.Text = RtAccDisbDate;
                        tAccPeriod.Text = RtAccPeriod;
                        tOrgAmt.Text = RtOrgAmt;
                        tPrincipleAmt.Text = RtPrincipleAmt;
                        tIntRate.Text = RtIntRate;
                        tAccLoanSancAmt.Text = RtAccLoanSancAmt;
                        tAccDisbAmt.Text = RtAccDisbAmt;
                        tAccNoInstl.Text = RtAccNoInstl;
                        tAccLoanInstlAmt.Text = RtAccLoanInstlAmt;
                        tAccLoanLastInstlAmt.Text = RtAccLoanLastInstlAmt;

                        lblPreAddressLine1.Text = RlblPreAddressLine1;
                        lblPreTelephone.Text = RlblPreTelephone;
                        lblPreMobile.Text = RlblPreMobile;

                        lblAutoRenewal.Text = RlblAutoRenewal;
                        lblDepositAmount.Text = RlblDepositAmount;
                        lblTotDepositAmount.Text = RlblTotDepositAmount;
                        tAccFixedMthInt.Text = RtAccFixedMthInt;







                        
                        lblProcDate.Text = RlblProcDate;


                        MultiAccFlag.Text = RMultiAccFlag;


                        txtAccType.Focus();

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

        

        protected void AccountType1Dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription FROM A2ZACCTYPE";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlAccType, "A2ZACGMS");
        }




        protected void FunctionName()
        {

            lblStatementFunc.Text = "Loan Summary Report";

        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";





            Session["SlblBranchNo"] = lblBranchNo.Text;
            Session["SCtrlAccType"] = CtrlAccType.Text;
            Session["SlblTrnCode"] = lblTrnCode.Text;


            Session["StxtAccType"] = lblTrnCode.Text;
            Session["SlblAccTypeTitle"] = lblTrnCode.Text;




            Session["StOpenDt"] = tOpenDt.Text;
            Session["StMaturityDt"] = tMaturityDt.Text;
            Session["StRenewalDt"] = tRenewalDt.Text;
            Session["StAccLoanSancDate"] = tAccLoanSancDate.Text;
            Session["StAccDisbDate"] = tAccDisbDate.Text;
            Session["StAccPeriod"] = tAccPeriod.Text;
            Session["StOrgAmt"] = tOrgAmt.Text;
            Session["StPrincipleAmt"] = tPrincipleAmt.Text;
            Session["StIntRate"] = tIntRate.Text;
            Session["StAccLoanSancAmt"] = tAccLoanSancAmt.Text;
            Session["StAccDisbAmt"] = tAccDisbAmt.Text;
            Session["StAccNoInstl"] = tAccNoInstl.Text;
            Session["StAccLoanInstlAmt"] = tAccLoanInstlAmt.Text;
            Session["StAccLoanLastInstlAmt"] = tAccLoanLastInstlAmt.Text;


            Session["SlblPreAddressLine1"] = lblPreAddressLine1.Text;
            Session["SlblPreTelephone"] = lblPreTelephone.Text;
            Session["SlblPreMobile"] = lblPreMobile.Text;

           
            Session["StxtAccType"] = txtAccType.Text;
            Session["SddlAccType"] = ddlAccType.SelectedValue;



            Session["SCtrlOldAccNo"] = CtrlOldAccNo.Text;



            Session["Slblcls"] = lblcls.Text;
            Session["SlblDivident"] = lblDivident.Text;



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

                if (txtAccType.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Type Not Selected');", true);
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

                atype = Converter.GetInteger(txtAccType.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, atype);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlAccType.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCTYPE, txtAccType.Text);

                //CtrlAccType.Text = Converter.GetString(txtAccType.Text);


                // For Credit Union No. and Name

                //
                // For Member No. and Name
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, txtMemberNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblMemName.Text);

                // FOR Member Address

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME9, lblPreAddressLine1.Text);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME10, lblPreTelephone.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME11, lblPreMobile.Text);

                //  Account Status

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, txtAccountNo.Text);

                //============== For Multi User Parameter =========================

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMTYPE, lblMemType.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, lblMemNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRN_CODE, lblTrnCode.Text);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, txtAccountNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, txtPartyName.Text);

                //int id = Converter.GetInteger(lblID.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.USERNO, id);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.USERNAME, lblIDName.Text);

                //============== End For Multi User Parameter =========================
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGoldLoanSummaryReport");

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


            Session["SChkInterest"] = string.Empty;

            Session["SChkAccStatus"] = string.Empty;

            Session["SlblProcDate"] = string.Empty;


            Session["StClass"] = string.Empty;
            Session["SlblTrnCode"] = string.Empty;
            Session["StOpenDt"] = string.Empty;
            Session["StMaturityDt"] = string.Empty;
            Session["StRenewalDt"] = string.Empty;
            Session["StAccLoanSancDate"] = string.Empty;
            Session["StAccDisbDate"] = string.Empty;
            Session["StAccPeriod"] = string.Empty;
            Session["StOrgAmt"] = string.Empty;
            Session["StPrincipleAmt"] = string.Empty;
            Session["StIntRate"] = string.Empty;
            Session["StAccLoanSancAmt"] = string.Empty;
            Session["StAccDisbAmt"] = string.Empty;
            Session["StAccNoInstl"] = string.Empty;
            Session["StAccLoanInstlAmt"] = string.Empty;
            Session["StAccLoanLastInstlAmt"] = string.Empty;

            Session["SlblPreAddressLine1"] = string.Empty;
            Session["SlblPreTelephone"] = string.Empty;
            Session["SlblPreMobile"] = string.Empty;

            Session["SlblAutoRenewal"] = string.Empty;
            Session["SlblDepositAmount"] = string.Empty;
            Session["SlblTotDepositAmount"] = string.Empty;
            Session["StAccFixedMthInt"] = string.Empty;

            Session["SMultiAccFlag"] = string.Empty;



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


        private void InvalidMemMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Member No.');", true);
            return;
        }


        

        private void InvalidAcc()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not Found');", true);
            return;
        }


        

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            


            int atype;
            A2ZACCTYPEDTO getDTO = new A2ZACCTYPEDTO();

            atype = Converter.GetInteger(txtAccType.Text);
            getDTO = (A2ZACCTYPEDTO.GetInformation(atype));


            if (getDTO.AccTypeCode > 0)
            {
                txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                CtrlAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                ddlAccType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);

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


            int atype;
            A2ZACCTYPEDTO getDTO = new A2ZACCTYPEDTO();


            atype = Converter.GetInteger(txtAccType.Text);
            getDTO = (A2ZACCTYPEDTO.GetInformation(atype));


            if (getDTO.AccTypeCode > 0)
            {
                txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                CtrlAccType.Text = Converter.GetString(getDTO.AccTypeCode);

            }
            else
            {
                txtAccType.Text = string.Empty;
                txtAccType.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid A/C Type');", true);
                return;
            }
        }


                

    }

}
