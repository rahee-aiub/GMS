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
    public partial class A2ZCurrencySale : System.Web.UI.Page
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

                DivReInput.Visible = false;
                ddlAccNo.Visible = false;

                CurrencyDropdown();
                PartyDropdown();
                

            }
        }

        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=21 and GroupCode !=51 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
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
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode > 1 AND CurrencyCode != 99";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");
        }

        private void CashDropdown()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode = 16 AND PartyCurrencyCode = " + ddlCurrency.SelectedValue + "";
            ddlCash = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCash, "A2ZACGMS");
        }

        private void AccDropdown()
        {
            lblMsgFlag.Text = "0";

            string sqlquery = "SELECT AccType,AccNo from A2ZACCOUNT WHERE AccPartyNo = '" + txtPartyCode.Text + "' AND AccCurrency = 1 AND AccStatus = 1";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZACGMS");


            string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + txtPartyCode.Text + "' AND AccCurrency = 1 AND AccStatus = 1";
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
                   // txtledgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance));

                    int code = Converter.GetInteger(lblLdgCurrencyCode.Text);
                    A2ZCURRENCYDTO get1DTO = (A2ZCURRENCYDTO.GetInformation(code));

                    if (get1DTO.CurrencyCode > 0)
                    {
                        txtLedgerCurrency.Text = Converter.GetString(get1DTO.CurrencyName);
                        txtledgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance)) + " " + txtLedgerCurrency.Text;

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

        protected void ClearRecords()
        {
            ddlPartyName.SelectedIndex = 0;
            txtPartyName.Text = string.Empty;
            txtPartyCode.Text = string.Empty;
            ddlCash.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            txtPurchaseAmt.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtTotalAmt.Text = string.Empty;
            txtNarration.Text = string.Empty;
            txtBalance.Text = string.Empty;

            txtAccNo.Text = string.Empty;
            ddlAccNo.SelectedIndex = 0;
            txtLedgerCurrency.Text = string.Empty;
            txtledgerBalance.Text = string.Empty;
            txtDescription.Text = string.Empty;

        }
        protected void UpdatedMSG()
        {
            string Msg = "";
            string a = "";
            string b = "";
            string c = "";
            string d = "";

            a = "Currency Sale Succesfully Done";
            //b = string.Format(ddlAccNo.SelectedValue);

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
                //GenerateNewAccNo();

                A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

                getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
                CtrlVoucherNo.Text = "CS" + getDTO.RecLastNo.ToString("000000");

                var prm = new object[17];

                prm[0] = lblID.Text;
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);
                prm[3] = "32";
                prm[4] = "Sale Currency";
                prm[5] = txtPartyCode.Text;
                prm[6] = CtrlAccType.Text;
                prm[7] = txtAccNo.Text;
                prm[8] = ddlCurrency.SelectedValue;
                prm[9] = "1";
                prm[10] = "1";
                prm[11] = ddlCash.SelectedValue;
                prm[12] = txtPurchaseAmt.Text;
                prm[13] = txtRate.Text;
                prm[14] = txtNarration.Text;
                prm[15] = txtTotalAmt.Text;
                prm[16] = txtDescription.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateCurrencySaleTransaction", prm, "A2ZACGMS"));

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
                ctrlNewAccNo.Text = "31" + txtPartyCode.Text + hdnNewAccNo.Text;
            }

            if (input1 != "3")
            {
                ctrlNewAccNo.Text = "31" + txtPartyCode.Text + result1 + hdnNewAccNo.Text;
            }

        }
        protected void GetAccountCount()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where AccPartyNo='" + txtPartyCode.Text + "' and AccType= 31";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);
        }

        private void GenerateNarration()
        {
            if (txtRate.Text != string.Empty && txtPurchaseAmt.Text != string.Empty)
            {
                txtNarration.Text = "" + ddlCurrency.SelectedItem.Text + "_" + txtPurchaseAmt.Text + " @" + txtRate.Text + "_" + ddlPartyName.SelectedItem.Text + "";
            }

        }
        protected void txtPurchaseAmt_TextChanged(object sender, EventArgs e)
        {
            lblReInput.Text = "Please Re-Input Sale Amount";

            lblReInputFlag.Text = "1";

            ReInputScreen();
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            lblReInput.Text = "Please Re-Input Rate";

            lblReInputFlag.Text = "2";

            ReInputScreen();
        }

        private void CalculateTotalAmt()
        {
            Decimal Amt = Converter.GetDecimal(txtPurchaseAmt.Text);
            Decimal Rate = Converter.GetDecimal(txtRate.Text);
            Decimal TotalAmt = Amt * Rate;
            txtTotalAmt.Text = TotalAmt.ToString("N3");

        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            CashDropdown();
        }

        private void ReInputScreen()
        {
            DivReInput.Visible = true;

            //DivMainHeader.Attributes.CssStyle.Add("opacity", "0.7");
            //DivChkShowZero.Attributes.CssStyle.Add("opacity", "0.7");

            DivReInput.Style.Add("Top", "250px");
            DivReInput.Style.Add("left", "530px");
            DivReInput.Style.Add("position", "fixed");
            DivReInput.Attributes.CssStyle.Add("opacity", "200");
            DivReInput.Attributes.CssStyle.Add("z-index", "200");

            txtReInput.Focus();
        }
        protected void txtReInput_TextChanged(object sender, EventArgs e)
        {
            DivReInput.Visible = false;

            if (lblReInputFlag.Text == "1")
            {
                if (txtPurchaseAmt.Text == txtReInput.Text)
                {
                    txtReInput.Text = string.Empty;
                    CalculateTotalAmt();
                    GenerateNarration();
                    txtRate.Focus();
                }
                else
                {
                    txtReInput.Text = string.Empty;
                    txtPurchaseAmt.Text = string.Empty;
                    txtPurchaseAmt.Focus();
                }
            }
            else if (lblReInputFlag.Text == "2")
            {
                if (txtRate.Text == txtReInput.Text)
                {
                    txtReInput.Text = string.Empty;
                    CalculateTotalAmt();
                    GenerateNarration();
                }
                else
                {
                    txtReInput.Text = string.Empty;
                    txtRate.Text = string.Empty;
                    txtRate.Focus();
                }
            }
        }

        protected void ddlCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 keyno = Converter.GetLong(ddlCash.SelectedValue);
            A2ZACCOUNTDTO get1DTO = (A2ZACCOUNTDTO.GetInfoAccNo(keyno));

            if (get1DTO.AccNo > 0)
            {
                txtBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", get1DTO.AccBalance));
                //txtledgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", get1DTO.AccBalance)) + " " + txtLedgerCurrency.Text;

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
                txtledgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance));



                int code = Converter.GetInteger(lblLdgCurrencyCode.Text);
                A2ZCURRENCYDTO get1DTO = (A2ZCURRENCYDTO.GetInformation(code));

                if (get1DTO.CurrencyCode > 0)
                {
                    txtLedgerCurrency.Text = Converter.GetString(get1DTO.CurrencyName);
                }


            }
        }

        
    }
}
