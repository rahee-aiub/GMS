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
    public partial class A2ZLoanInterestRateChange : System.Web.UI.Page
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

            txtNewIntRate.Text = string.Empty;

            txtPartyName.Text = string.Empty;
            ddlLoanType.SelectedIndex = 0;
           
            txtPartyCode.Text = string.Empty;
            ddlAccNo.SelectedIndex = 0;
            txtCurrentBalance.Text = string.Empty;
            txtCurrency.Text = string.Empty;
            txtInterestRate.Text = string.Empty;
            

        }
        protected void UpdatedMSG()
        {
            string Msg = "";
            string a = "";
            string b = "";
            string c = "";
            string d = "";

            a = "Data Update Sucessfully";
            b = string.Format(ddlAccNo.SelectedValue);

            c = "Generated New Voucher No.";
            d = string.Format(CtrlVoucherNo.Text);

            Msg += a;
            //Msg += b;
            //Msg += "\\n";
            //Msg += "\\n";
            //Msg += c;
            //Msg += d;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;

        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               
                var prm = new object[2];
                
                prm[0] = ddlAccNo.SelectedValue;
                prm[1] = txtNewIntRate.Text;
             
                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateInterestRateChange", prm, "A2ZACGMS"));

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
