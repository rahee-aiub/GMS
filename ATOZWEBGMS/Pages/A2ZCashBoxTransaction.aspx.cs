using System.Drawing;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO.Ports;
using System.Management;
using System.Threading;
using DataAccessLayer.DTO;


namespace ATOZWEBGMS.Pages
{
    public partial class A2ZCashBoxTransaction : Page
    {
        SerialPort SP = new SerialPort();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    rbtOptReceive.Checked = true;
                    lblTrnMode.Text = "0";

                    lblPartyName.Text = "Receive From :";
                    lblCashBoxAmount.Text = "Receive Amount :";

                    ddlAccNo.Visible = false;

                    DivReInput.Visible = false;

                    PartyDropdown();

                    CurrencyDropdown();




                    string flag = (string)Session["flag"];
                    lblflag.Text = flag;


                    string RlblCustNo = (string)Session["SlblCustNo"];
                    string RlblCustName = (string)Session["SlblCustName"];
                    string RlblCustAddL1 = (string)Session["SlblCustAddL1"];
                    string RlblCustAddL2 = (string)Session["SlblCustAddL2"];
                    string RlblCustPhone = (string)Session["SlblCustPhone"];
                    string RlblCustBalance = (string)Session["SlblCustBalancer"];
                    string RlblBankBalance = (string)Session["SlblBankBalance"];


                    string RlblCrCashBalance = (string)Session["SlblCrCashBalance"];



                    string RlblCardName = (string)Session["SlblCardName"];
                    string RddlCardName = (string)Session["SddlCardName"];
                    string RlblCardNo = (string)Session["SlblCardNo"];
                    string RtxtCardNo = (string)Session["StxtCardNo"];
                    string RlblSlipNo = (string)Session["SlblSlipNo"];
                    string RtxtSlipNo = (string)Session["StxtSlipNo"];
                    string RlblAutho = (string)Session["SlblAutho"];
                    string RtxtAutho = (string)Session["StxtAutho"];
                    string RlblCardBankNo = (string)Session["SlblCardBankNo"];
                    string RddlCardBank = (string)Session["SddlCardBank"];

                    string RlblTrnMode = (string)Session["SlblTrnMode"];


                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));





                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblProcDate.Text = date;

                    lblYear.Text = Converter.GetString(dto.CurrentYear);







                    if (lblflag.Text == "1")
                    {

                        lblTrnMode.Text = RlblTrnMode;






                    }



                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //ddlTrnCode.Focus();

        }

        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=21 and GroupCode !=51 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }

        private void MiscDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 51 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }

        private void CashDropdown()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode = 16 AND PartyCurrencyCode = " + ddlCurrency.SelectedValue + "";
            ddlCash = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCash, "A2ZACGMS");
        }

        private void CashDropdown1()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode = 16 AND PartyCurrencyCode = " + ddlCurrency.SelectedValue + " AND RIGHT(PartyAccNo,1) = " + ddlCurrency.SelectedValue + " AND RIGHT(PartyCode,1) = " + ddlCurrency.SelectedValue + "";
            ddlCash = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCash, "A2ZACGMS");
        }

        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode != 99";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");
        }
        private void RemoveSession()
        {
            Session["SFlag"] = string.Empty;
            Session["STrnCode"] = string.Empty;
            Session["key"] = string.Empty;
            Session["cbalance"] = string.Empty;
            Session["ccbalance"] = string.Empty;
            Session["flag"] = string.Empty;

            Session["SlblCustNo"] = string.Empty;
            Session["SlblCustName"] = string.Empty;
            Session["SlblCustAddL1"] = string.Empty;
            Session["SlblCustAddL2"] = string.Empty;
            Session["SlblCustPhone"] = string.Empty;
            Session["SlblCustBalance"] = string.Empty;
            Session["SlblBankBalance"] = string.Empty;

            Session["SlblTrnMode"] = string.Empty;

            Session["SlblCrCashBalance"] = string.Empty;
            Session["SddlCardName"] = string.Empty;
            Session["StxtCardNo"] = string.Empty;
            Session["StxtSlipNo"] = string.Empty;
            Session["StxtAutho"] = string.Empty;
            Session["SddlCardBank"] = string.Empty;



        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {

                RemoveSession();

                Session["RepPrintOpt"] = string.Empty;

                Response.Redirect("A2ZERPModule.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void UpdatedMSG()
        {
            string Msg = "";
            string a = "";
            string b = "";
            string c = "";
            string d = "";

            //a = "Additional Amount Added to Account No.";
            //b = string.Format(ddlAccNo.SelectedValue);

            a = "Data Update Sucessfully";

            c = "Generated New Voucher No.";
            d = string.Format(CtrlVoucherNo.Text);

            Msg += a;
            //Msg += b;
            Msg += "\\n";
            Msg += "\\n";
            Msg += c;
            Msg += d;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text == string.Empty)
            {
                txtDescription.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Description');", true);
                return;
            }


            if (txtAmount.Text == string.Empty)
            {
                txtAmount.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Amount');", true);
                return;
            }

            if (ddlCash.SelectedIndex == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select By Cash Code');", true);
                return;
            }

            try
            {

                A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

                getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());

                if (rbtOptReceive.Checked == true)
                {
                    CtrlVoucherNo.Text = "CBR" + getDTO.RecLastNo.ToString("000000");
                }
                else if (rbtOptPayment.Checked == true)
                {
                    CtrlVoucherNo.Text = "CBP" + getDTO.RecLastNo.ToString("000000");
                }
                else
                {
                    CtrlVoucherNo.Text = "CBJ" + getDTO.RecLastNo.ToString("000000");
                }

                var prm = new object[14];

                prm[0] = lblID.Text;
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = Converter.GetDateToYYYYMMDD(lblProcDate.Text);
                prm[3] = "50";
                prm[4] = "Cash Box";
                prm[5] = txtPartyCode.Text;
                prm[6] = CtrlAccType.Text;
                prm[7] = txtAccNo.Text;

                prm[8] = ddlCurrency.SelectedValue;
                prm[9] = "1";
                prm[10] = lblTrnMode.Text;
                
                prm[11] = ddlCash.SelectedValue;
                prm[12] = txtAmount.Text;

                prm[13] = txtDescription.Text;
                



                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateCashBoxTransaction", prm, "A2ZACGMS"));

                if (result == 0)
                {
                    UpdatedMSG();

                    ClearInformation();

                    Int64 keyno = Converter.GetLong(ddlCash.SelectedValue);
                    A2ZACCOUNTDTO get1DTO = (A2ZACCOUNTDTO.GetInfoAccNo(keyno));

                    if (get1DTO.AccNo > 0)
                    {
                        txtCashBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", get1DTO.AccBalance)) + " " + txtLedgerCurrency.Text;
                    }


                    txtPartyCode.Focus();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        
        protected void PrintTrnVoucher()
        {
            //try
            //{

            //    string qry2 = "SELECT * FROM A2ZTRNCTRL WHERE FuncOpt=10 AND PayType='" + ddlTrnCode.SelectedValue + "'";
            //    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZJWGOLD");
            //    if (dt2.Rows.Count > 0)
            //    {
            //        lblTrnMode.Text = Converter.GetString(dt2.Rows[0]["TrnMode"]);
            //    }


            //    DateTime Pdate = DateTime.ParseExact(lblProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            //    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, lblCompanyName.Text);
            //    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, lblBranchName.Text);
            //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);
            //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlTrnCode.SelectedItem.Text);
            //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblHelpName.Text);

            //    if (ddlTrnCode.SelectedValue == "27")
            //    {
            //        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblHelpName.Text + " - " + txtDescription.Text);
            //    }
            //    else
            //    {
            //        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, txtDescription.Text);
            //    }



            //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CtrlVchNo.Text);
            //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, txtAmount.Text);

            //    if (lblTrnMode.Text == "1")
            //    {
            //        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, "RECEIVED FROM");
            //    }
            //    else
            //    {
            //        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, "PAID TO");

            //    }


            //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, lblID.Text);
            //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);


            //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCashBoxReport");
            //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZJWGOLD");

            //    Session["RepPrintOpt"] = "1";

            //    Response.Redirect("ReportServer.aspx", false);



            //    Session["SlblTrnMode"] = lblTrnMode.Text;

            //    Session["SControlFlag"] = "2";
            //    Session["SFlag"] = "2";

            //    Session["STrnCode"] = ddlTrnCode.SelectedValue;

            //    CtrlKeyNo.Text = (string)Session["skey"];

            //    Session["flag"] = "2";



            //}
            //catch (Exception ex)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
            //    //throw ex;
            //}

        }

        protected void ClearInformation()
        {
            txtPartyCode.Text = string.Empty;
            ddlPartyName.SelectedIndex = 0;
            txtAccNo.Text = string.Empty;
            ddlAccNo.SelectedIndex = 0;
          
            txtAmount.Text = string.Empty;
            txtDescription.Text = string.Empty;

            txtledgerBalance.Text = string.Empty;
            txtCashBalance.Text = string.Empty;

            
            
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {

            lblReInput.Text = "Please Re-Input Amount";


            ReInputScreen();
        }



        protected void rbtOptReceive_CheckedChanged(object sender, EventArgs e)
        {
            lblPartyName.Text = "Receive From :";
            lblCashBoxAmount.Text = "Receive Amount :";
            lblTrnMode.Text = "0";
            ddlCurrency.SelectedIndex = 0;
            if (ddlCash.SelectedIndex != -1)
            {
                ddlCash.SelectedIndex = 0;
            }
            PartyDropdown();
        }

        protected void rbtOptPayment_CheckedChanged(object sender, EventArgs e)
        {
            lblPartyName.Text = "Payment To :";
            lblCashBoxAmount.Text = "Payment Amount";
            lblTrnMode.Text = "1";
            ddlCurrency.SelectedIndex = 0;
            if (ddlCash.SelectedIndex != -1)
            {
                ddlCash.SelectedIndex = 0;
            }
            PartyDropdown();
        }

        protected void rbtOptJournal_CheckedChanged(object sender, EventArgs e)
        {
            lblPartyName.Text = "Journal Code :";
            lblCashBoxAmount.Text = "Journal Amount";
            lblTrnMode.Text = "1";
            ddlCurrency.SelectedIndex = 0;
            if (ddlCash.SelectedIndex != -1)
            {
                ddlCash.SelectedIndex = 0;
            }
            
            MiscDropdown();
        }


        private void AccDropdown()
        {
            lblMsgFlag.Text = "0";

            string sqlquery = "SELECT AccType,AccNo from A2ZACCOUNT WHERE AccPartyNo = '" + txtPartyCode.Text + "' AND AccCurrency = '" + ddlCurrency.SelectedValue + "' AND AccStatus = 1";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZACGMS");


            string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + txtPartyCode.Text + "' AND AccCurrency = '" + ddlCurrency.SelectedValue + "' AND AccStatus = 1";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec == 1)
            {
                CtrlAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                txtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                lblLdgCurrencyCode.Text = Converter.GetString(dt.Rows[0]["AccCurrency"]);


                Int64 accno = Converter.GetLong(txtAccNo.Text);
                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfoAccNo(accno));

                if (getDTO.AccNo > 0)
                {

                    int code = Converter.GetInteger(lblLdgCurrencyCode.Text);
                    A2ZCURRENCYDTO get1DTO = (A2ZCURRENCYDTO.GetInformation(code));

                    if (get1DTO.CurrencyCode > 0)
                    {
                        txtLedgerCurrency.Text = Converter.GetString(get1DTO.CurrencyName);
                        txtledgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance)) + " " + txtLedgerCurrency.Text;

                        txtAmount.Focus();

                        //ddlCurrency.SelectedValue = lblLdgCurrencyCode.Text;

                        //CashDropdown();
                    }

                }


                txtAccNo.Visible = true;
                ddlAccNo.Visible = false;
            }
            else if (totrec > 1)
            {
                txtAccNo.Visible = false;

                ddlAccNo.Visible = true;
            }
            else
            {
                lblMsgFlag.Text = "1";
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

                    AccDropdown();

                    


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

                    AccDropdown();

                }
            }
        }

        protected void ddlAccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAccNo.Text = ddlAccNo.SelectedValue;

            Int64 accno = Converter.GetLong(txtAccNo.Text);
            A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfoAccNo(accno));

            if (getDTO.AccNo > 0)
            {
                CtrlAccType.Text = Converter.GetString(getDTO.AccType);
                lblLdgCurrencyCode.Text = Converter.GetString(getDTO.AccCurrency);



                int code = Converter.GetInteger(lblLdgCurrencyCode.Text);
                A2ZCURRENCYDTO get1DTO = (A2ZCURRENCYDTO.GetInformation(code));

                if (get1DTO.CurrencyCode > 0)
                {
                    txtLedgerCurrency.Text = Converter.GetString(get1DTO.CurrencyName);
                    txtledgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance)) + " " + txtLedgerCurrency.Text;

                    txtAmount.Focus();
                    //ddlCurrency.SelectedValue = lblLdgCurrencyCode.Text;

                    //CashDropdown();
                }
            }
        }


        protected void ddlCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 keyno = Converter.GetLong(ddlCash.SelectedValue);
            A2ZACCOUNTDTO get1DTO = (A2ZACCOUNTDTO.GetInfoAccNo(keyno));

            if (get1DTO.AccNo > 0)
            {
                txtCashBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", get1DTO.AccBalance)) + " " + txtLedgerCurrency.Text;

              
            }
        }

        private void ReInputScreen()
        {
            DivReInput.Visible = true;



            //DivMainHeader.Attributes.CssStyle.Add("opacity", "0.7");
            //DivChkShowZero.Attributes.CssStyle.Add("opacity", "0.7");

            DivReInput.Style.Add("Top", "250px");
            DivReInput.Style.Add("left", "530px");
            DivReInput.Style.Add("position", "fixed");

            DivMain.Attributes.CssStyle.Add("opacity", "0.3");

            DivReInput.Attributes.CssStyle.Add("opacity", "300");
            DivReInput.Attributes.CssStyle.Add("z-index", "300");

            txtReInput.Focus();
        }
        protected void txtReInput_TextChanged(object sender, EventArgs e)
        {
            DivReInput.Visible = false;

            DivMain.Attributes.CssStyle.Add("opacity", "300");


            if (txtAmount.Text == txtReInput.Text)
            {

                Decimal amount = Converter.GetDecimal(txtAmount.Text);

                txtAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", amount));


                txtReInput.Text = string.Empty;
                txtDescription.Focus();
            }
            else
            {
                txtReInput.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtAmount.Focus();
            }

        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtOptJournal.Checked == true)
            {

                CashDropdown1();
            }
            else 
            {
                CashDropdown();
            }

            //if (rbtOptJournal.Checked == true)
            //{
            //    ddlCash.Enabled = false;
            //    if (ddlCurrency.SelectedValue == "1")
            //    {
            //        ddlCash.SelectedValue = "1";
            //    }
            //    else
            //    {
            //        ddlCash.SelectedValue = "2";
            //    }
            //}
            //else 
            //{
            //    ddlCash.Enabled = true;
            //    ddlCash.SelectedIndex = 0;
            //}
            

            AccDropdown();
        }

        





    }
}
