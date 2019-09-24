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
    public partial class A2ZNewLoanReceived : System.Web.UI.Page
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
                PartyDropdown();
                CurrencyDropdown();

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
        private void PartyDropdown()
        {
            string sqlquery = "SELECT LPartyCode,LPartyName from A2ZLOANPARTY ORDER BY LPartyName ASC";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }

        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");
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

            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime ProcDate = Converter.GetDateTime(dto.ProcessDate.ToShortDateString());

            txtEffecDate.Text = ProcDate.ToString("dd/MM/yyyy");

            DateTime SLTEndDate = ProcDate.AddMonths(6);
            DateTime LLTEndDate = ProcDate.AddYears(1);


            if (ddlLoanType.SelectedValue == "61")
            {
                txtEndDate.Text = LLTEndDate.ToString("dd/MM/yyyy");
                txtCalculationDays.Text = (LLTEndDate - ProcDate).TotalDays.ToString();
            }

            if (ddlLoanType.SelectedValue == "62")
            {
                txtEndDate.Text = SLTEndDate.ToString("dd/MM/yyyy");
                txtCalculationDays.Text = (SLTEndDate - ProcDate).TotalDays.ToString();

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

        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyName.SelectedIndex != 0)
            {
                int PartyCode = Converter.GetInteger(ddlPartyName.SelectedValue);
                A2ZLOANPARTYDTO getDTO = (A2ZLOANPARTYDTO.GetLoanPartyByPartyCode(PartyCode));

                if (getDTO.LPartyName != string.Empty)
                {
                    txtPartyCode.Text = Converter.GetString(getDTO.LPartyCode);
                }
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
                    ddlPartyName.SelectedIndex = 0;
                    txtPartyCode.Text = string.Empty;
                    txtPartyCode.Focus();
                }

                else
                {
                    ddlPartyName.SelectedValue = Converter.GetString(getDTO.LPartyCode);

                }
            }
        }

        protected void GetAccountCount()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where AccPartyNo='" + txtPartyCode.Text + "' and AccType='" + ddlLoanType.SelectedValue + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);
        }

        protected void GenerateNewAccNo()
        {
            GetAccountCount();

            string input1 = Converter.GetString(hdnNewAccNo.Text).Length.ToString();

            string result1 = "";

            if (input1 == "1")
            {
                result1 = "00";
            }
            if (input1 == "2")
            {
                result1 = "0";
            }

            if (input1 == "3")
            {
                ctrlNewAccNo.Text = ddlLoanType.SelectedValue + txtPartyCode.Text + hdnNewAccNo.Text;
            }

            if (input1 != "3")
            {
                ctrlNewAccNo.Text = ddlLoanType.SelectedValue + txtPartyCode.Text + result1 + hdnNewAccNo.Text;
            }

        }


        protected void ClearRecords()
        {
            txtPartyCode.Text = string.Empty;
            txtEffecDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtCalculationDays.Text = string.Empty;
            txtChequeNo.Text = string.Empty;
            txtLoanAmount.Text = string.Empty;
            txtInterestRate.Text = string.Empty;
            txtTranDesc.Text = string.Empty;
            ddlPartyName.SelectedIndex = 0;
            ddlLoanType.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            ddlTransactionType.SelectedIndex = 0;
            ddlAccountName.SelectedIndex = 0;
        }
        protected void UpdatedMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";
            string d = "";

            a = "Generated New Loan Account No.";
            b = string.Format(ctrlNewAccNo.Text);

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
            GenerateNewAccNo();

            A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

            getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
            CtrlVoucherNo.Text = "C" + getDTO.RecLastNo;

            var prm = new object[19];
           
            prm[0] = lblID.Text;
            prm[1] = CtrlVoucherNo.Text;
            prm[2] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);
            prm[3] = "1";
            prm[4] = "New Loan Received"; 
            prm[5] = txtPartyCode.Text;
            prm[6] = ddlLoanType.Text;
            prm[7] = ctrlNewAccNo.Text;
            prm[8] = ddlCurrency.SelectedValue;
            prm[9] = ddlTransactionType.SelectedValue;
            if (ddlTransactionType.SelectedValue == "47")
            {
                prm[10] = txtChequeNo.Text;
            }
            else 
            {
                prm[10] = "0";
            }

            prm[11] = "1";
            prm[12] = ddlAccountName.SelectedValue;
            prm[13] = txtLoanAmount.Text;
            prm[14] = txtInterestRate.Text;
            prm[15] = txtTranDesc.Text;
            prm[16] = Converter.GetDateToYYYYMMDD(txtEffecDate.Text);
            prm[17] = Converter.GetDateToYYYYMMDD(txtEndDate.Text);
            prm[18] = txtCalculationDays.Text;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateLoanReceiveTransaction", prm, "A2ZACGMS"));

            if (result == 0)
            {
                UpdatedMSG();

                ClearRecords();

                txtPartyCode.Focus();
            }


        }
    }
}
