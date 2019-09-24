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
    public partial class A2ZOpenPartyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrencyDropdown();
                DivLedgerMaint.Visible = false;

                lblCarringRate.Visible = false;
                txtCarringRate.Visible = false;
                
            }
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


        protected void UpdatedMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            string d = "";
            string e = "";


            a = ddlGroup.SelectedItem.Text;

            b = "Generated New Party Code  :  ";
            c = string.Format(lblNewLPartyNo.Text);

            d = "Generated New Ledger Code :  ";
            e = string.Format(ctrlNewAccNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b;
            Msg += c;
            //Msg += "\\n";
            //Msg += "\\n";
            //Msg += d;
            //Msg += e;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;

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

            btnSubmit.Enabled = true;
            btnCancel.Enabled = true;
            BtnExit.Enabled = true;


        }
        protected void GenerateNewLPartyNo()
        {
            string input1 = Converter.GetString(lblLastLPartyNo.Text).Length.ToString();

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
                lblNewLPartyNo.Text = ddlGroup.SelectedValue + lblLastLPartyNo.Text;
            }
            else
            {
                lblNewLPartyNo.Text = ddlGroup.SelectedValue + result1 + lblLastLPartyNo.Text;
            }


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
                Int16 code = Converter.GetSmallInteger(ddlGroup.SelectedValue);
                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(code));
                lblLastLPartyNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                GenerateNewLPartyNo();

                GenerateNewAccNo();

                UpdateRecords();

                //GenerateMAG();

               
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data not inserted');", true);
                return;
            }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
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

                txtPartyName.Focus();
            }
        }

       
        protected void GetAccountCount()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where AccPartyNo='" + lblNewLPartyNo.Text + "' and AccType='" + ddlGroup.SelectedValue + "'";
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
                ctrlNewAccNo.Text = lblNewLPartyNo.Text + hdnNewAccNo.Text;
            }

            if (input1 != "4")
            {
                ctrlNewAccNo.Text = lblNewLPartyNo.Text + result1 + hdnNewAccNo.Text;
            }

        }

        private void GenerateMAG()
        {
            DivLedgerMaint.Visible = true;

            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;
            BtnExit.Enabled = false;

            //DivMainHeader.Attributes.CssStyle.Add("opacity", "0.7");
            //DivChkShowZero.Attributes.CssStyle.Add("opacity", "0.7");

            DivLedgerMaint.Style.Add("Top", "250px");
            DivLedgerMaint.Style.Add("left", "610px");
            DivLedgerMaint.Style.Add("position", "fixed");

            DivMain.Attributes.CssStyle.Add("opacity", "0.3");

            DivLedgerMaint.Attributes.CssStyle.Add("opacity", "300");
            DivLedgerMaint.Attributes.CssStyle.Add("z-index", "300");


            ddlGroup.Enabled = false;
            txtPartyName.Enabled = false;
            txtPartyAddressL1.Enabled = false;
            txtPartyAddressL2.Enabled = false;
            txtPartyAddressL3.Enabled = false;
            txtMobileNo.Enabled = false;
            txtPartyEmail.Enabled = false;



           
        }

        private void UpdateRecords()
        {
            
            A2ZPARTYDTO objDTO = new A2ZPARTYDTO();

            objDTO.GroupCode = Converter.GetInteger(ddlGroup.SelectedValue);
            objDTO.GroupName = Converter.GetString(ddlGroup.SelectedItem.Text);
            objDTO.PartyCode = Converter.GetInteger(lblNewLPartyNo.Text);
            //objDTO.PartyAccNo = Converter.GetLong(ctrlNewAccNo.Text);
            objDTO.PartyName = Converter.GetString(txtPartyName.Text);
            objDTO.PartyAddresssLine1 = Converter.GetString(txtPartyAddressL1.Text);
            objDTO.PartyAddresssLine2 = Converter.GetString(txtPartyAddressL2.Text);
            objDTO.PartyAddresssLine3 = Converter.GetString(txtPartyAddressL3.Text);
            objDTO.PartyMobileNo = Converter.GetString(txtMobileNo.Text);
            objDTO.PartyEmail = Converter.GetString(txtPartyEmail.Text);
            objDTO.PartyCarringRate = Converter.GetDecimal(txtCarringRate.Text);
           

            int roweffect = A2ZPARTYDTO.InsertInformation(objDTO);
            if (roweffect > 0)
            {
                UpdatedMSG();

                //DivLedgerMaint.Visible = false;

                //DivMain.Attributes.CssStyle.Add("opacity", "300");



                ClearRecords();

                txtPartyName.Focus();

                //Response.Redirect(Request.RawUrl);
            }
        }
    }
}
