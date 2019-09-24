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
    public partial class A2ZEditPartyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrencyDropdown();
                DivLedgerMaint.Visible = false;

                btnNewLedger.Visible = false;

                lblCarringRate.Visible = false;
                txtCarringRate.Visible = false;
            }
        }


        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE  where GroupCode='" + ddlGroup.Text + "' GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }

        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY";
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




        protected void InitializedRecords()
        {
            
            txtPartyName.Text = string.Empty;
            txtPartyAddressL1.Text = string.Empty;
            txtPartyAddressL2.Text = string.Empty;
            txtPartyAddressL3.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtPartyEmail.Text = string.Empty;
            

            ddlGroup.Enabled = true;
            txtPartyName.Enabled = true;
            txtPartyAddressL1.Enabled = true;
            txtPartyAddressL2.Enabled = true;
            txtPartyAddressL3.Enabled = true;
            txtMobileNo.Enabled = true;
            txtPartyEmail.Enabled = true;

            txtPartyCode.Text = string.Empty;
            if (ddlPartyName.SelectedIndex > 0)
            {
                ddlPartyName.SelectedIndex = 0;
            }
            

            ctrlNewAccNo.Text = string.Empty;

            if (ddlCurrency.SelectedIndex > 0)
            {
                ddlCurrency.SelectedIndex = 0;
            }
            

        }


        protected void ClearRecords()
        {
            ddlGroup.SelectedIndex = 0;
            txtPartyName.Text = string.Empty;
            txtPartyAddressL1.Text = string.Empty;
            txtPartyAddressL2.Text = string.Empty;
            txtPartyAddressL3.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtPartyEmail.Text = string.Empty;

            txtCarringRate.Text = string.Empty;

            ddlCurrency.SelectedIndex = 0;

            ddlGroup.Enabled = true;
            txtPartyName.Enabled = true;
            txtPartyAddressL1.Enabled = true;
            txtPartyAddressL2.Enabled = true;
            txtPartyAddressL3.Enabled = true;
            txtMobileNo.Enabled = true;
            txtPartyEmail.Enabled = true;

            txtPartyCode.Text = string.Empty;
            ddlPartyName.SelectedIndex = 0;

            ctrlNewAccNo.Text = string.Empty;
            ddlCurrency.SelectedIndex = 0;

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Group Code');", true);
                return;
            }


            if (txtPartyName.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Party Name');", true);
                return;
            }

            //if (txtMobileNo.Text == string.Empty)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Party Mobile No.');", true);
            //    return;
            //}

            //if (txtPartyEmail.Text == string.Empty)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Party Email');", true);
            //    return;
            //}

            //if (txtPartyAddressL1.Text == string.Empty)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Address');", true);
            //    return;
            //}

            try
            {

                A2ZPARTYDTO objDTO = new A2ZPARTYDTO();

                objDTO.GroupCode = Converter.GetInteger(ddlGroup.SelectedValue);
                objDTO.GroupName = Converter.GetString(ddlGroup.SelectedItem.Text);
                objDTO.PartyCode = Converter.GetInteger(txtPartyCode.Text);
                objDTO.PartyAccNo = Converter.GetLong(ctrlNewAccNo.Text);
                objDTO.PartyName = Converter.GetString(txtPartyName.Text);
                objDTO.PartyAddresssLine1 = Converter.GetString(txtPartyAddressL1.Text);
                objDTO.PartyAddresssLine2 = Converter.GetString(txtPartyAddressL2.Text);
                objDTO.PartyAddresssLine3 = Converter.GetString(txtPartyAddressL3.Text);
                objDTO.PartyMobileNo = Converter.GetString(txtMobileNo.Text);
                objDTO.PartyEmail = Converter.GetString(txtPartyEmail.Text);
                objDTO.PartyCurrencyCode = Converter.GetInteger(ddlCurrency.SelectedValue);
                objDTO.PartyCarringRate = Converter.GetDecimal(txtCarringRate.Text);

                int roweffect = A2ZPARTYDTO.UpdateParty(objDTO);
                if (roweffect > 0)
                {


                    ClearRecords();

                    ddlGroup.Focus();
                }



            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data not inserted');", true);
                return;
            }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            InitializedRecords();


            PartyDropdown();

            if (ddlGroup.SelectedValue == "13" ||
                ddlGroup.SelectedValue == "15" || 
                ddlGroup.SelectedValue == "17" || 
                ddlGroup.SelectedValue == "18" || 
                ddlGroup.SelectedValue == "19" || 
                ddlGroup.SelectedValue == "20")
            {
                ddlCurrency.SelectedIndex = 0;

                lblAdd1.Visible = true;
                lblAdd2.Visible = true;
                lblAdd3.Visible = true;
                lblMobile.Visible = true;
                lblEmail.Visible = true;

                txtPartyAddressL1.Visible = true;
                txtPartyAddressL2.Visible = true;
                txtPartyAddressL3.Visible = true;
                txtMobileNo.Visible = true;
                txtPartyEmail.Visible = true;

                lblCarringRate.Visible = true;
                txtCarringRate.Visible = true;

                txtPartyName.Focus();

            }

            else if (ddlGroup.SelectedValue == "16" || ddlGroup.SelectedValue == "21" || ddlGroup.SelectedValue == "22")
            {

                btnNewLedger.Visible = false;
                ddlCurrency.SelectedIndex = 0;

                lblAdd1.Visible = false;
                lblAdd2.Visible = false;
                lblAdd3.Visible = false;
                lblMobile.Visible = false;
                lblEmail.Visible = false;
                txtPartyAddressL1.Visible = false;
                txtPartyAddressL2.Visible = false;
                txtPartyAddressL3.Visible = false;
                txtMobileNo.Visible = false;
                txtPartyEmail.Visible = false;

                lblCarringRate.Visible = false;
                txtCarringRate.Visible = false;

                txtPartyName.Focus();

            }
            else if (ddlGroup.SelectedValue == "51")
            {
                btnNewLedger.Visible = false;

                ddlCurrency.SelectedIndex = 0;

                lblAdd1.Visible = false;
                lblAdd2.Visible = false;
                lblAdd3.Visible = false;
                lblMobile.Visible = false;
                lblEmail.Visible = false;

                txtPartyAddressL1.Visible = false;
                txtPartyAddressL2.Visible = false;
                txtPartyAddressL3.Visible = false;
                txtMobileNo.Visible = false;
                txtPartyEmail.Visible = false;

                lblCarringRate.Visible = false;
                txtCarringRate.Visible = false;

                txtPartyName.Focus();
            }
            else
            {
                btnNewLedger.Visible = false;

                ddlCurrency.SelectedIndex = 0;

                lblAdd1.Visible = true;
                lblAdd2.Visible = true;
                lblAdd3.Visible = true;
                lblMobile.Visible = true;
                lblEmail.Visible = true;

                txtPartyAddressL1.Visible = true;
                txtPartyAddressL2.Visible = true;
                txtPartyAddressL3.Visible = true;
                txtMobileNo.Visible = true;
                txtPartyEmail.Visible = true;

                lblCarringRate.Visible = false;
                txtCarringRate.Visible = false;

                txtPartyCode.Focus();
            }

            if (ddlGroup.SelectedValue == "11" || ddlGroup.SelectedValue == "12")
            {
                btnNewLedger.Visible = true;
            }
            
        }


        protected void GetAccountCount()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where AccPartyNo='" + txtPartyCode.Text + "' and AccType='" + ddlGroup.SelectedValue + "'";
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
                result1 = "000";
            }
            if (input1 == "2")
            {
                result1 = "00";
            }
            if (input1 == "3")
            {
                result1 = "0";
            }

            if (input1 == "4")
            {
                ctrlNewAccNo.Text = txtPartyCode.Text + hdnNewAccNo.Text;
            }

            if (input1 != "4")
            {
                ctrlNewAccNo.Text = txtPartyCode.Text + result1 + hdnNewAccNo.Text;
            }

        }

        private void UpdateNewLedger()
        {
            DivLedgerMaint.Visible = true;

            btnLedgerMaint.Visible = false;
            btnLdgChk.Visible = true;
            btnLdgExit.Visible = true;

            //DivMainHeader.Attributes.CssStyle.Add("opacity", "0.7");
            //DivChkShowZero.Attributes.CssStyle.Add("opacity", "0.7");

            DivLedgerMaint.Style.Add("Top", "240px");
            DivLedgerMaint.Style.Add("left", "610px");
            DivLedgerMaint.Style.Add("position", "fixed");

            DivMain.Attributes.CssStyle.Add("opacity", "0.3");

            DivLedgerMaint.Attributes.CssStyle.Add("opacity", "400");
            DivLedgerMaint.Attributes.CssStyle.Add("z-index", "400");


            ddlGroup.Enabled = false;
            txtPartyName.Enabled = false;
            txtPartyAddressL1.Enabled = false;
            txtPartyAddressL2.Enabled = false;
            txtPartyAddressL3.Enabled = false;
            txtMobileNo.Enabled = false;
            txtPartyEmail.Enabled = false;




        }
        protected void btnLedgerMaint_Click(object sender, EventArgs e)
        {

            btnNewLedger.Enabled = true;
            btnSubmit.Enabled = true;
            btnCancel.Enabled = true;
            BtnExit.Enabled = true;

            DivLedgerMaint.Visible = false;

            DivMain.Attributes.CssStyle.Add("opacity", "300");



            //Response.Redirect(Request.RawUrl);

        }

        protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        {
            if (txtPartyCode.Text != string.Empty)
            {

                string input = Converter.GetString(txtPartyCode.Text);
                string GroupCode = input.Substring(0, 2);

                if (GroupCode != ddlGroup.SelectedValue)
                {
                    txtPartyCode.Text = string.Empty;
                    txtPartyCode.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Party Code');", true);
                    return;
                }


                int PartyCode = Converter.GetInteger(txtPartyCode.Text);
                A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                if (getDTO.PartyCode > 0)
                {
                    txtPartyName.Text = Converter.GetString(getDTO.PartyName);

                    txtPartyAddressL1.Text = Converter.GetString(getDTO.PartyAddresssLine1);
                    txtPartyAddressL2.Text = Converter.GetString(getDTO.PartyAddresssLine2);
                    txtPartyAddressL3.Text = Converter.GetString(getDTO.PartyAddresssLine3);
                    txtMobileNo.Text = Converter.GetString(getDTO.PartyMobileNo);
                    txtPartyEmail.Text = Converter.GetString(getDTO.PartyEmail);
                    
                    txtCarringRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.PartyCarringRate));
                }
                else 
                {
                    txtPartyCode.Text = string.Empty;
                    txtPartyCode.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Party Code');", true);
                    return;
                }
            }
        }

        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPartyCode.Text = ddlPartyName.SelectedValue;


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

                txtPartyAddressL1.Text = Converter.GetString(getDTO.PartyAddresssLine1);
                txtPartyAddressL2.Text = Converter.GetString(getDTO.PartyAddresssLine2);
                txtPartyAddressL3.Text = Converter.GetString(getDTO.PartyAddresssLine3);
                txtMobileNo.Text = Converter.GetString(getDTO.PartyMobileNo);
                txtPartyEmail.Text = Converter.GetString(getDTO.PartyEmail);

                txtCarringRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.PartyCarringRate));
               

            }



        }

        protected void VerifyNewLedger()
        {
            CrlMsgFlag.Text = "0";

            string qry = "SELECT PartyCode,PartyName FROM A2ZPARTYCODE where GroupCode='" + ddlGroup.SelectedValue + "' AND PartyCode='" + txtPartyCode.Text + "' AND PartyCurrencyCode='" + ddlCurrency.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec > 0)
            {
                if (ddlGroup.SelectedValue != "11" && ddlGroup.SelectedValue != "12")
                {
                    CrlMsgFlag.Text = "1";
                    return;
                }
            }



        }
        protected void btnNewLedger_Click(object sender, EventArgs e)
        {
            ctrlNewAccNo.Text = string.Empty;
            ddlCurrency.SelectedIndex = 0;

            btnNewLedger.Enabled = false;
            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;
            BtnExit.Enabled = false;

            UpdateNewLedger();




        }


        protected void btnLdgChk_Click(object sender, EventArgs e)
        {
            if (ddlCurrency.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Currency Code');", true);
                return;
            }


            VerifyNewLedger();
            if (CrlMsgFlag.Text == "1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Multi Ledger Open');", true);
                return;
            }

            btnLedgerMaint.Visible = true;
            btnLdgChk.Visible = false;
            btnLdgExit.Visible = false;

            GenerateNewAccNo();
        }

        protected void btnLdgExit_Click(object sender, EventArgs e)
        {
            ctrlNewAccNo.Text = string.Empty;
            ddlCurrency.SelectedIndex = 0;

            btnNewLedger.Enabled = true;
            btnSubmit.Enabled = true;
            btnCancel.Enabled = true;
            BtnExit.Enabled = true;

            DivLedgerMaint.Visible = false;
            DivMain.Attributes.CssStyle.Add("opacity", "300");
        }
    }
}
