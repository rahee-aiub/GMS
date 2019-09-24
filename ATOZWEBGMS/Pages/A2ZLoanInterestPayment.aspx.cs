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
    public partial class A2ZLoanInterestPayment : System.Web.UI.Page
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
                

                lblChequeNo.Visible = false;
                txtChequeNo.Visible = false;
                lblAccountName.Visible = false;
                ddlAccountName.Visible = false;
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

        private void AccTypeDropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE";
            ddlLoanType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLoanType, "A2ZACGMS");
        }
        
        private void LoanAccDropdown(int Atype)
        {
            string sqlquery = "SELECT AccNo,AccNo from A2ZACCOUNT WHERE AccType = " + Atype + " AND AccStatus = 1";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZACGMS");
        }


        private void CashCodeDropdown()
        {
            string sqlquery = "SELECT CashCode,CashName from A2ZCASH";
            ddlAccountName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccountName, "A2ZACGMS");
        }

        private void BankCodeDropdown()
        {
            string sqlquery = "SELECT BankCode,BankName from A2ZBANK";
            ddlAccountName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccountName, "A2ZACGMS");
        }
        protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtCurrency.Text = string.Empty;
            txtInterestRate.Text = string.Empty;
            txtCurrentBalance.Text = string.Empty;

            if (ddlLoanType.SelectedIndex != 0)
            {
                if (ddlLoanType.SelectedValue == "61")
                {
                    LoanAccDropdown(61);
                }
                if (ddlLoanType.SelectedValue == "62")
                {
                    LoanAccDropdown(62);
                }
            }



        }

        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransactionType.SelectedValue == "47")
            {
                lblChequeNo.Visible = true;
                txtChequeNo.Visible = true;
                lblAccountName.Visible = true;
                ddlAccountName.Visible = true;
                BankCodeDropdown();
            }
            else
            {
                lblChequeNo.Visible = false;
                txtChequeNo.Visible = false;
                lblAccountName.Visible = true;
                ddlAccountName.Visible = true;
                CashCodeDropdown();
            }
        }

        
        protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        {
            if (txtPartyCode.Text != string.Empty)
            {
                int PartyCode = Converter.GetInteger(txtPartyCode.Text);
                A2ZLOANPARTYDTO getDTO = (A2ZLOANPARTYDTO.GetLoanPartyByPartyCode(PartyCode));

                if (getDTO.LPartyName == null)
                {
                    txtPartyName.Text = string.Empty;
                    txtPartyCode.Text = string.Empty;
                    txtPartyCode.Focus();
                }

                else
                {
                    txtPartyName.Text = Converter.GetString(getDTO.LPartyName);

                }
            }
        }

        

        protected void ClearRecords()
        {
            txtChequeNo.Text = string.Empty;
            txtInterestRate.Text = string.Empty;
            txtTranDesc.Text = string.Empty;
            txtPartyName.Text = string.Empty;
            ddlLoanType.SelectedIndex = 0;
            ddlTransactionType.SelectedIndex = 0;
            ddlAccountName.SelectedIndex = 0;
            txtPartyCode.Text = string.Empty;
            ddlAccNo.SelectedIndex = 0;
            txtCurrentBalance.Text = string.Empty;
            txtCurrency.Text = string.Empty;
            txtLoanIntAmount.Text = string.Empty;

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

                var prm = new object[16];

                prm[0] = lblID.Text;
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);
                prm[3] = "4";
                prm[4] = "Loan Interest Payment";
                prm[5] = txtPartyCode.Text;
                prm[6] = ddlLoanType.Text;
                prm[7] = ddlAccNo.SelectedValue;
                prm[8] = lblCurrencyCode.Text;
                prm[9] = ddlTransactionType.SelectedValue;
                if (ddlTransactionType.SelectedValue == "47")
                {
                    prm[10] = txtChequeNo.Text;
                }
                else
                {
                    prm[10] = "0";
                }

                prm[11] = "0";
                prm[12] = ddlAccountName.SelectedValue;
                prm[13] = txtLoanIntAmount.Text;
                prm[14] = txtInterestRate.Text;
                prm[15] = txtTranDesc.Text;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateLoanInterestPayment", prm, "A2ZACGMS"));

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

                    int code = Converter.GetInteger(lblCurrencyCode.Text);
                    A2ZCURRENCYDTO get1DTO = (A2ZCURRENCYDTO.GetInformation(code));

                    if (get1DTO.CurrencyCode > 0)
                    {
                        txtCurrency.Text = Converter.GetString(get1DTO.CurrencyName);
                    }

                }

            }
        }
    }
}
