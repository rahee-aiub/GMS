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
    public partial class A2ZConvertCurrencyAmount : System.Web.UI.Page
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

                FromCurrencyDropdown();
                ToCurrencyDropdown();


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

        private void FromCurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode != 99";
            ddlFromCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFromCurrency, "A2ZACGMS");
        }


        private void ToCurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode != 99";
            ddlToCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlToCurrency, "A2ZACGMS");
        }
        private void FromCashDropdown()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 AND GroupCode != 12 AND GroupCode != 51  AND PartyCurrencyCode = " + ddlFromCurrency.SelectedValue + "";
            ddlFromCash = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFromCash, "A2ZACGMS");
        }

        private void ToCashDropdown()
        {
            string sqlquery = "SELECT PartyAccNo,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 AND GroupCode != 12 AND GroupCode != 51 AND PartyCurrencyCode = " + ddlToCurrency.SelectedValue + "";
            ddlToCash = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlToCash, "A2ZACGMS");
        }


        protected void ClearRecords()
        {
            ddlFromCurrency.SelectedIndex = 0;
            ddlFromCash.SelectedIndex = 0;
            txtFromBalance.Text = string.Empty;
            ddlToCurrency.SelectedIndex = 0;
            ddlToCash.SelectedIndex = 0;
            txtToBalance.Text = string.Empty;
            txtAmt.Text = string.Empty;
            
            txtRate.Text = string.Empty;
            txtConvertAmt.Text = string.Empty;
            txtNarration.Text = string.Empty;

        }
        protected void UpdatedMSG()
        {
            string Msg = "";
            string a = "";
            string b = "";
            string c = "";
            string d = "";

            a = "Currency Purchase Succesfully Done";
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
                CtrlVoucherNo.Text = "V" + getDTO.RecLastNo;

                var prm = new object[13];

                prm[0] = lblID.Text;
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);
                prm[3] = "41";
                prm[4] = "Convert Currency";
                prm[5] = ddlFromCurrency.SelectedValue;
                prm[6] = ddlFromCash.SelectedValue;
                prm[7] = ddlToCurrency.SelectedValue;
                prm[8] = ddlToCash.SelectedValue;
                prm[9] = txtAmt.Text;
                prm[10] = txtRate.Text;
                prm[11] = txtConvertAmt.Text;
                prm[12] = txtNarration.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateConvertCurrencyTransaction", prm, "A2ZACGMS"));

                if (result == 0)
                {
                    UpdatedMSG();

                    ClearRecords();


                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



        private void GenerateNarration()
        {
            if (txtRate.Text != string.Empty && txtAmt.Text != string.Empty)
            {
              txtNarration.Text  = "Convert Amount " + txtAmt.Text + "_" + ddlFromCurrency.SelectedItem.Text + " @" + txtRate.Text + "_" + "To Amount " + txtConvertAmt.Text + "_" + ddlToCurrency.SelectedItem.Text + "";
            }

        }

        protected void txtAmt_TextChanged(object sender, EventArgs e)
        {
            lblReInput.Text = "Please Re-Input Amount";

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
            Decimal Amt = Converter.GetDecimal(txtAmt.Text);
            Decimal Rate = Converter.GetDecimal(txtRate.Text);
            Decimal TotalAmt = 0;



            TotalAmt = Amt * Rate;



            txtConvertAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", TotalAmt));
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

            if (lblReInputFlag.Text == "1")
            {
                if (txtAmt.Text == txtReInput.Text)
                {
                    txtReInput.Text = string.Empty;
                    txtRate.Focus();

                }
                else
                {
                    txtReInput.Text = string.Empty;
                    txtAmt.Text = string.Empty;
                    txtAmt.Focus();
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

        protected void ddlFromCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            FromCashDropdown();
        }


        protected void ddlToCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToCashDropdown();
        }


        protected void ddlFromCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 keyno = Converter.GetLong(ddlFromCash.SelectedValue);
            A2ZACCOUNTDTO get1DTO = (A2ZACCOUNTDTO.GetInfoAccNo(keyno));

            if (get1DTO.AccNo > 0)
            {
                txtFromBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", get1DTO.AccBalance));
               
            }
        }

        protected void ddlToCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 keyno = Converter.GetLong(ddlToCash.SelectedValue);
            A2ZACCOUNTDTO get1DTO = (A2ZACCOUNTDTO.GetInfoAccNo(keyno));

            if (get1DTO.AccNo > 0)
            {
                txtToBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", get1DTO.AccBalance));
                txtAmt.Focus();
            }
        }






    }
}
