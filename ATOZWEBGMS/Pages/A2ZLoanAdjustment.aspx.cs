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
    public partial class A2ZLoanAdjustment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                CtrlProcDate.Text = date;

                AccTypeDropdown();
                CurrencyDropdown();
                DivReInput.Visible = false;
                ddlAccNo.Visible = false;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode != 99";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");
        }
        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode='" + ddlLoanType.SelectedValue + "' GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }
        private void AccTypeDropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE";
            ddlLoanType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLoanType, "A2ZACGMS");
        }

        private void LoanAccDropdown(int Atype)
        {
            lblMsgFlag.Text = "0";

            string sqlquery = "SELECT AccNo,CONCAT(AccNo,'-',A2ZCURRENCY.CurrencyName) AS AccNum from A2ZACCOUNT inner join A2ZCURRENCY on A2ZCURRENCY.CurrencyCode=A2ZACCOUNT.AccCurrency WHERE AccType = " + Atype + " AND AccPartyNo = " + txtPartyCode.Text + " AND AccCurrency = " + ddlCurrency.SelectedValue + " AND AccStatus = 1 AND AccLoanAmt > 0";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZACGMS");

            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where AccPartyNo='" + txtPartyCode.Text + "' and AccType='" + ddlLoanType.SelectedValue + "' AND AccCurrency = " + ddlCurrency.SelectedValue + " AND AccStatus = 1 AND AccLoanAmt > 0";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec == 1)
            {
                txtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);



                Int64 accno = Converter.GetLong(txtAccNo.Text);
                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfoAccNo(accno));

                if (getDTO.AccNo > 0)
                {
                    txtCurrentBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance));
                    txtPrincipal.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccPrincipal));
                    txtIntBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccInterest));
                    txtInterestRate.Text = Converter.GetString(String.Format("{0:0,0.000}", getDTO.AccIntRate));
                    lblCurrencyCode.Text = Converter.GetString(getDTO.AccCurrency);


                }


                txtAccNo.Visible = true;
                ddlAccNo.Visible = false;
                return;
            }
            else if (totrec > 1)
            {
                txtAccNo.Visible = false;
                ddlAccNo.Visible = true;
                return;
            }
            else
            {
                lblMsgFlag.Text = "1";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Account Not Found');", true);
                return;
            }


        }


        //private void CashCodeDropdown()
        //{
        //    string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode = 16 AND PartyCurrencyCode = " + lblCurrencyCode.Text + "";
        //    ddlAccountName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccountName, "A2ZACGMS");
        //}

        //private void BankCodeDropdown()
        //{
        //    string sqlquery = "SELECT BankCode,BankName from A2ZBANK";
        //    ddlAccountName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccountName, "A2ZACGMS");
        //}
        protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {

            PartyDropdown();



        }

        //protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlTransactionType.SelectedValue == "47")
        //    {
        //        lblChequeNo.Visible = true;
        //        txtChequeNo.Visible = true;
        //        lblAccountName.Visible = true;
        //        ddlAccountName.Visible = true;
        //        BankCodeDropdown();
        //    }
        //    else
        //    {
        //        lblChequeNo.Visible = false;
        //        txtChequeNo.Visible = false;
        //        lblAccountName.Visible = true;
        //        ddlAccountName.Visible = true;
        //        CashCodeDropdown();
        //    }
        //}


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
                    ddlPartyName.SelectedValue = txtPartyCode.Text;

                }
            }
        }

        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PartyCode = Converter.GetInteger(ddlPartyName.SelectedValue);
            A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

            if (getDTO.PartyName == null)
            {
                txtPartyName.Text = string.Empty;
                txtPartyCode.Text = string.Empty;
                txtPartyCode.Focus();
            }

            else
            {
                txtPartyCode.Text = Converter.GetString(getDTO.PartyCode);
                txtPartyName.Text = Converter.GetString(getDTO.PartyName);


                
                txtInterestRate.Text = string.Empty;
                txtCurrentBalance.Text = string.Empty;

                

            }
        }


        protected void ClearRecords()
        {
            //txtChequeNo.Text = string.Empty;
            txtInterestRate.Text = string.Empty;
            txtTranDesc.Text = string.Empty;
            txtPartyName.Text = string.Empty;
            ddlPartyName.SelectedIndex = 0;
            ddlLoanType.SelectedIndex = 0;
            //ddlTransactionType.SelectedIndex = 0;
            //ddlAccountName.SelectedIndex = 0;
            txtPartyCode.Text = string.Empty;
            ddlAccNo.SelectedIndex = 0;
            txtCurrentBalance.Text = string.Empty;
           
            txtAdjustmentAmt.Text = string.Empty;
            txtPrincipal.Text = string.Empty;
            txtIntBalance.Text = string.Empty;
            ddlAdjustmentMode.SelectedIndex = 0;
            txtAccNo.Text = string.Empty;
            ddlAccNo.SelectedIndex = 0;

        }
        protected void UpdatedMSG()
        {
            string Msg = "";
            string a = "";
            string b = "";
            string c = "";
            string d = "";

            a = "Additional Amount Added to Account No.";
            b = string.Format(ddlAccNo.SelectedValue);

            c = "Generated New Voucher No.";
            d = string.Format(CtrlVoucherNo.Text);

            Msg += a;
            Msg += b;
            Msg += "\\n";
            Msg += "\\n";
            Msg += c;
            Msg += d;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;

        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

                getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
                CtrlVoucherNo.Text = "C" + getDTO.RecLastNo;

                var prm = new object[17];

                prm[0] = lblID.Text;
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);

                if (ddlTrnMode.SelectedValue == "0" && ddlAdjustmentMode.SelectedValue == "1")
                {
                    prm[3] = "6";
                    prm[4] = "Loan Principal Debit Adjustment";
                }
                if (ddlTrnMode.SelectedValue == "0" && ddlAdjustmentMode.SelectedValue == "2")
                {
                    prm[3] = "8";
                    prm[4] = "Loan Interest Debit Adjustment";
                }
                if (ddlTrnMode.SelectedValue == "1" && ddlAdjustmentMode.SelectedValue == "1")
                {
                    prm[3] = "7";
                    prm[4] = "Loan Principal Credit Adjustment";
                }
                if (ddlTrnMode.SelectedValue == "1" && ddlAdjustmentMode.SelectedValue == "2")
                {
                    prm[3] = "8";
                    prm[4] = "Loan Interest Credit Adjustment";
                }


                prm[5] = txtPartyCode.Text;
                prm[6] = ddlLoanType.Text;
                prm[7] = txtAccNo.Text;
                prm[8] = lblCurrencyCode.Text;
                prm[9] = "3";
                //if (ddlTransactionType.SelectedValue == "47")
                //{
                //    prm[10] = txtChequeNo.Text;
                //}
                //else
                //{
                    prm[10] = "0";
                //}

                prm[11] = ddlTrnMode.SelectedValue;
                prm[12] = "0";
                prm[13] = txtAdjustmentAmt.Text;
                prm[14] = txtInterestRate.Text;
                prm[15] = txtTranDesc.Text;
                prm[16] = ddlAdjustmentMode.SelectedValue;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateLoanAdjustmentPayment", prm, "A2ZACGMS"));

                if (result == 0)
                {
                    UpdatedMSG();

                    ClearRecords();

                    txtPartyCode.Focus();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        protected void ddlAccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccNo.SelectedIndex != 0)
            {
                Int64 accno = Converter.GetLong(ddlAccNo.SelectedValue);
                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfoAccNo(accno));

                if (getDTO.AccNo > 0)
                {
                    txtCurrentBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance));
                    txtPrincipal.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccPrincipal));
                    txtIntBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccInterest));
                    txtInterestRate.Text = Converter.GetString(String.Format("{0:0,0.000}", getDTO.AccIntRate));
                    lblCurrencyCode.Text = Converter.GetString(getDTO.AccCurrency);

                    //int code = Converter.GetInteger(lblCurrencyCode.Text);
                    //A2ZCURRENCYDTO get1DTO = (A2ZCURRENCYDTO.GetInformation(code));

                    //if (get1DTO.CurrencyCode > 0)
                    //{
                    //    txtCurrency.Text = Converter.GetString(get1DTO.CurrencyName);
                    //}

                }

            }
        }

        private void ReInputScreen()
        {
            DivReInput.Visible = true;

            btnUpdate.Enabled = false;
            btnCancel.Enabled = false;
            BtnExit.Enabled = false;


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
            btnUpdate.Enabled = true;
            btnCancel.Enabled = true;
            BtnExit.Enabled = true;

            DivMain.Attributes.CssStyle.Add("opacity", "300");


            if (txtAdjustmentAmt.Text == txtReInput.Text)
            {
                txtReInput.Text = string.Empty;
                txtTranDesc.Focus();
            }
            else
            {
                txtReInput.Text = string.Empty;
                txtAdjustmentAmt.Text = string.Empty;
                txtAdjustmentAmt.Focus();
            }
        }

        protected void txtAdjustmentAmt_TextChanged(object sender, EventArgs e)
        {
            lblReInput.Text = "Please Re-Input Adjustment Loan Amount";

            ReInputScreen();
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLoanType.SelectedIndex != 0)
            {
                if (ddlLoanType.SelectedValue == "11")
                {
                    LoanAccDropdown(11);
                }
                if (ddlLoanType.SelectedValue == "12")
                {
                    LoanAccDropdown(12);
                }
            }

            if (lblMsgFlag.Text == "1")
            {
                txtPartyCode.Text = string.Empty;
                ddlPartyName.SelectedIndex = 0;
                txtPartyCode.Focus();
            }
        }


    }
}
