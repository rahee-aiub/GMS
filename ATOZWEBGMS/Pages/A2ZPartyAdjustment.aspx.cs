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
    public partial class A2ZPartyAdjustment : System.Web.UI.Page
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

                CurrencyDropdown();
                PartyDropdown();
                DivReInput.Visible = false;
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
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=51 and GroupCode !=21 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }

        protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PartyDropdown();
        }

        protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        {
            if (txtPartyCode.Text != string.Empty)
            {
                int PartyCode = Converter.GetInteger(txtPartyCode.Text);
                A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                if (getDTO.PartyName == null)
                {
                    txtPartyCode.Text = string.Empty;
                    txtPartyCode.Focus();
                }

                else
                {
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
                txtPartyCode.Text = string.Empty;
                txtPartyCode.Focus();
            }

            else
            {
                txtPartyCode.Text = Converter.GetString(getDTO.PartyCode);
                txtCurrentBalance.Text = string.Empty;
            }
        }

        protected void ClearRecords()
        {
            txtTranDesc.Text = string.Empty;
            ddlPartyName.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            txtPartyCode.Text = string.Empty;
            txtCurrentBalance.Text = string.Empty;
            txtAdjustmentAmt.Text = string.Empty;
        }

        protected void UpdatedMSG()
        {
            string Msg = "";
            string a = "";
            string b = "";
            string c = "";
            string d = "";

            a = "Party Account Adjustment Succesfull";
           // b = string.Format(ddlAccNo.SelectedValue);

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
                CtrlVoucherNo.Text = "PA" + getDTO.RecLastNo.ToString("0000");

                var prm = new object[12];

                prm[0] = lblID.Text;
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);
                prm[3] = txtPartyCode.Text;
                prm[4] = lblPartyAccType.Text;
                prm[5] = lblPartyAccno.Text;
                prm[6] = lblCurrencyCode.Text;
                prm[7] = "1"; //TrnType              
                prm[8] = ddlTrnMode.SelectedValue;
                prm[9] = "0";
                prm[10] = txtAdjustmentAmt.Text;
                prm[11] = txtTranDesc.Text;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdatePartyAdjustmentPayment", prm, "A2ZACGMS"));

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


        private void ReInputScreen()
        {
            DivReInput.Visible = true;

            btnUpdate.Enabled = false;
            btnCancel.Enabled = false;
            BtnExit.Enabled = false;

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
            lblReInput.Text = "Please Re-Input Adjustment Amount";
            ReInputScreen();
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyName.SelectedIndex != 0)
            {
                string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + ddlPartyName.SelectedValue + "' AND AccCurrency = '" + ddlCurrency.SelectedValue + "' AND AccStatus = 1";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                int totrec = dt.Rows.Count;
                if (totrec > 0)
                {
                    lblPartyAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                    lblPartyAccno.Text = Converter.GetString(dt.Rows[0]["AccNo"]);

                    Int64 accno = Converter.GetLong(lblPartyAccno.Text);
                    A2ZACCOUNTDTO AccDTO = (A2ZACCOUNTDTO.GetInfoAccNo(accno));

                    if (AccDTO.AccNo > 0)
                    {
                        txtCurrentBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", AccDTO.AccBalance));
                        lblCurrencyCode.Text = Converter.GetString(AccDTO.AccCurrency);
                    }
                }
            }
        }
    }
}
