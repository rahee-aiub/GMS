using System;
using System.Web;
using System.Web.UI;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using System.Data;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;


namespace ATOZWEBGMS.Pages
{
    public partial class A2ZSaleFixingReturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                lblProcesDate.Text = date;

                PartyDropdown();
                CurrencyDropdown();

                ddlCurrency.SelectedIndex = 1;

                ConvertCurrencyDropdown();
                TruncateWF();
                divPost.Visible = false;
                DivReInput.Visible = false;

            }
        }



        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZITEMGOLD WHERE UserId='" + lblID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZACGMS"));
        }

        private void ImportAEDRate()
        {
            string qry = "SELECT ExchangeRate FROM A2ZCURRENCY WHERE CurrencyCode = " + ddlCurrency.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec > 0)
            {
                lblAEDRate.Text = Converter.GetString(dt.Rows[0]["ExchangeRate"]);
            }


        }
        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=51 and GroupCode !=21 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }

        private void ConvertCurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode != 99";
            ddlConvCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlConvCurrency, "A2ZACGMS");

        }


        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode > 1 AND CurrencyCode != 99";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            txtMetal.Text = txtTotalValue.Text;
            txtTotalValueView.Text = txtTotalValue.Text;
            txtConvMetal.Text = txtTotalValue.Text;
            txtConvNetAmt.Text = txtTotalValue.Text;
            txtTotalNet.Text = txtGoldPureWt.Text;
            txtDiscount.Text = "0,0.00";
            txtConvRate.ReadOnly = true;
            ddlConvCurrency.SelectedValue = ddlCurrency.SelectedValue;
            ConvertCalculate(1);
            CalculateConvertTotal();

            divPost.Visible = true;

            divPost.Style.Add("Top", "170px");
            divPost.Style.Add("left", "350px");
            divPost.Style.Add("position", "fixed");

            divMain.Attributes.CssStyle.Add("opacity", "0.5");

            divPost.Attributes.CssStyle.Add("opacity", "400");
            divPost.Attributes.CssStyle.Add("z-index", "400");



        }


        private void PrintVoucher()
        {
            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);


            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, CtrlVoucherNo.Text);
            // SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txtToDate.Text));

            //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");


            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsFixedPurchaseInvoice");


            Response.Redirect("ReportServer.aspx", false);
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
                    txtPartyAddress.Text = Converter.GetString(getDTO.PartyAddresssLine1) + " " + Converter.GetString(getDTO.PartyAddresssLine2) + " " + Converter.GetString(getDTO.PartyAddresssLine3);

                    ddlCurrency_SelectedIndexChanged(this, e);
                    CurrencyAndLocationSelection();
                }
            }
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
                    ImportAEDRate();
                    txtRateUSD.Focus();
                }

            }
        }



        protected void txtRateUSD_TextChanged(object sender, EventArgs e)
        {
            if (txtRateUSD.Text != string.Empty)
            {
                lblReInput.Text = "Please Re-Input Rate USD";

                lblReInputFlag.Text = "1";

                ReInputScreen();
            }
        }

        protected void txtGoldPureWt_TextChanged(object sender, EventArgs e)
        {
            lblReInput.Text = "Please Re-Input Pure Wt";

            lblReInputFlag.Text = "2";

            ReInputScreen2();
        }

        private void CalculateTotalValue()
        {
            if (txtGoldPureWt.Text != string.Empty || txtMetalRate.Text != string.Empty)
            {
                Decimal PureWt = Converter.GetDecimal(txtGoldPureWt.Text);
                Decimal GoldRate = Converter.GetDecimal(txtMetalRate.Text);

                Decimal TotalValue = Math.Round(PureWt * GoldRate, 3);

                txtGoldPureWt.Text = PureWt.ToString("0,0.000");

                txtTotalValue.Text = TotalValue.ToString("0,0.000");

              
            }
        }



        protected void btnPost_Click(object sender, EventArgs e)
        {
            A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

            getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
            CtrlVoucherNo.Text = "SF" + getDTO.RecLastNo.ToString("000000");



            lblDescription.Text = "Pw:" + txtGoldPureWt.Text + ",Tv:" + txtTotalValue.Text;

            try
            {
                var prm = new object[20];

                prm[0] = ddlLocation.SelectedValue;
                prm[1] = ddlLocation.SelectedItem.Text;
                prm[2] = ddlCurrency.SelectedValue;
                prm[3] = CtrlVoucherNo.Text;
                prm[4] = lblID.Text;
                prm[5] = txtPartyCode.Text;
                prm[6] = lblPartyAccType.Text;
                prm[7] = "1";
                prm[8] = "0";
                prm[9] = txtGoldPureWt.Text;
                prm[10] = txtMetal.Text;
                prm[11] = lblDescription.Text;
                prm[12] = lblPartyAccno.Text;
                prm[13] = txtRateUSD.Text;
                prm[14] = txtMetalRate.Text;
                prm[15] = Converter.GetDateToYYYYMMDD(lblProcesDate.Text);
                prm[16] = ddlConvCurrency.SelectedValue;
                prm[17] = txtConvRate.Text;
                prm[18] = txtConvNetAmt.Text;
                prm[19] = txtConvDiscount.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_SaleFixingReturn]", prm, "A2ZACGMS"));


                if (result == 0)
                {
                    //  PrintVoucher();

                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        protected void txtConvDiscount_TextChanged(object sender, EventArgs e)
        {

            txtConvNetAmt.Text = (Converter.GetDecimal(txtConvMetal.Text) - Converter.GetDecimal(txtConvDiscount.Text)).ToString("0,0.00");


            //if (txtConvDiscount.Text != string.Empty && ddlConvCurrency.SelectedValue == "1")
            //{
            //    txtDiscount.Text = (Converter.GetDecimal(txtConvDiscount.Text) / Converter.GetDecimal(lblAEDRate.Text)).ToString("0.00");
            //    txtConvNetAmt.Text = (((Converter.GetDecimal(txtTotalValue.Text) * (Converter.GetDecimal(lblAEDRate.Text))) - Converter.GetDecimal(txtConvDiscount.Text))).ToString("0.00");
            //    txtTotalValueView.Text = (Converter.GetDecimal(txtTotalValue.Text) - Converter.GetDecimal(txtDiscount.Text)).ToString("0.00");
            //}
            //else if (txtConvDiscount.Text != string.Empty && ddlConvCurrency.SelectedValue == "2")
            //{
            //    txtDiscount.Text = Converter.GetDecimal(txtConvDiscount.Text).ToString("0.00");
            //    txtConvNetAmt.Text = (Converter.GetDecimal(txtTotalValue.Text) - Converter.GetDecimal(txtConvDiscount.Text)).ToString("0.00");
            //    txtTotalValueView.Text = (Converter.GetDecimal(txtTotalValue.Text) - Converter.GetDecimal(txtDiscount.Text)).ToString("0.00");
  
            //}
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            divPost.Visible = false;
            divMain.Attributes.CssStyle.Add("opacity", "300");

            txtTotalNet.Text = string.Empty;
            txtMetal.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtTotalValueView.Text = string.Empty;
            txtConvRate.Text = string.Empty;
            txtConvMetal.Text = string.Empty;
            txtConvDiscount.Text = string.Empty;
            txtConvNetAmt.Text = string.Empty;
            ddlConvCurrency.SelectedIndex = 0;
        }

        protected void ddlConvCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlConvCurrency.SelectedValue == "2")
            {
                ConvertCalculate(1);
                txtConvRate.ReadOnly = true;

            }
            else
            {
                ConvertCalculate(Converter.GetDecimal(lblAEDRate.Text));

                txtConvRate.ReadOnly = false;
                txtConvRate.Focus();

            }

            txtConvDiscount.Text = "0,0.00";
            txtDiscount.Text = "0,0.00";
            CalculateConvertTotal();
        }

        private void CalculateConvertTotal()
        {
            txtConvNetAmt.Text = (Converter.GetDecimal(txtConvMetal.Text) - Converter.GetDecimal(txtConvDiscount.Text)).ToString("0,0.00");
        }
        protected void txtConvRate_TextChanged(object sender, EventArgs e)
        {
            ConvertCalculate(Converter.GetDecimal(txtConvRate.Text));
            txtConvDiscount.Text = "0,0.00";
            txtDiscount.Text = "0,0.00";
            CalculateConvertTotal();
        }
        private void ConvertCalculate(Decimal ConvRate)
        {
            txtConvRate.Text = ConvRate.ToString();
            txtConvMetal.Text = (ConvRate * Converter.GetDecimal(txtMetal.Text)).ToString("0,0.00");
            txtConvDiscount.Text = (ConvRate * Converter.GetDecimal(txtDiscount.Text)).ToString("0,0.00");
            txtConvNetAmt.Text = (ConvRate * Converter.GetDecimal(txtTotalValueView.Text)).ToString("0,0.00");
        }

        protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        {
            int PartyCode = Converter.GetInteger(txtPartyCode.Text);
            A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

            if (getDTO.PartyName != string.Empty)
            {
                ddlPartyName.SelectedValue = Converter.GetString(getDTO.PartyCode);
                txtPartyAddress.Text = Converter.GetString(getDTO.PartyAddresssLine1) + " " + Converter.GetString(getDTO.PartyAddresssLine2) + " " + Converter.GetString(getDTO.PartyAddresssLine3);

                ddlCurrency_SelectedIndexChanged(this, e);
                CurrencyAndLocationSelection();
            }
            else
            {
                ddlPartyName.SelectedIndex = 0;
                txtPartyCode.Text = string.Empty;
                txtPartyCode.Focus();
            }
        }

        protected void txtReInput_TextChanged(object sender, EventArgs e)
        {
            DivReInput.Visible = false;

            if (lblReInputFlag.Text == "1")
            {
                if (txtRateUSD.Text == txtReInput.Text)
                {
                    txtReInput.Text = string.Empty;
                    Double x = Converter.GetDouble(txtRateUSD.Text) * (13.7777);
                    Double y = 116.64;
                    Double result = (x / y);
                    result = Math.Round(result, 3);
                    txtMetalRate.Text = result.ToString();
                    CalculateTotalValue();
                }
                else
                {
                    txtReInput.Text = string.Empty;
                    txtRateUSD.Text = string.Empty;
                    txtRateUSD.Focus();
                }
            }
            else if (lblReInputFlag.Text == "2")
            {
                if (txtGoldPureWt.Text == txtReInput.Text)
                {
                    txtReInput.Text = string.Empty;
                    CalculateTotalValue();
                }
                else
                {
                    txtReInput.Text = string.Empty;
                    txtGoldPureWt.Text = string.Empty;
                    txtGoldPureWt.Focus();
                }
            }

        }


        private void ReInputScreen()
        {
            DivReInput.Visible = true;
            DivReInput.Style.Add("Top", "80px");
            DivReInput.Style.Add("left", "480px");
            DivReInput.Style.Add("position", "fixed");
            DivReInput.Attributes.CssStyle.Add("opacity", "200");
            DivReInput.Attributes.CssStyle.Add("z-index", "200");

            txtReInput.Focus();
        }
        private void ReInputScreen2()
        {
            DivReInput.Visible = true;
            DivReInput.Style.Add("Top", "200px");
            DivReInput.Style.Add("left", "480px");
            DivReInput.Style.Add("position", "fixed");
            DivReInput.Attributes.CssStyle.Add("opacity", "200");
            DivReInput.Attributes.CssStyle.Add("z-index", "200");

            txtReInput.Focus();
        }

        private void CurrencyAndLocationSelection()
        {
            if (ddlPartyName.SelectedValue.Substring(0, 2) == "14" || txtPartyCode.Text.Substring(0, 2) == "14")
            {
                ddlLocation.SelectedValue = "2";
                ddlConvCurrency.Enabled = false;
            }
            else
            {
                ddlLocation.SelectedIndex = 0;
                ddlConvCurrency.Enabled = true;
            }
        }
    }
}
