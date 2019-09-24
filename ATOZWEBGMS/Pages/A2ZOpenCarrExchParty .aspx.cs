using System;
using System.Web;
using System.Web.UI;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBGMS.Pages
{
    public partial class A2ZOpenCarrExchParty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
          
            a = "Generated New Carrier/Currency Exchange Party No.";
            b = string.Format(lblNewCPartyNo.Text);
 
            Msg += a;
            Msg += b;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;

        }

        protected void ClearRecords()
        {
            txtPartyName.Text = string.Empty;
            txtPartyAddressL1.Text = string.Empty;
            txtPartyAddressL2.Text = string.Empty;
            txtPartyAddressL3.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtPartyEmail.Text = string.Empty;
        }
        protected void GenerateNewCPartyNo()
        {
            string input1 = Converter.GetString(lblLastCPartyNo.Text).Length.ToString();

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
                lblNewCPartyNo.Text = "30" + lblLastCPartyNo.Text;
            }
            else 
            {
                lblNewCPartyNo.Text = "30" + result1 + lblLastCPartyNo.Text;
            }
            

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPartyName.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Party Name');", true);
                return;
            }

            if (txtMobileNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Party Mobile No.');", true);
                return;
            }

            if (txtPartyEmail.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Party Email');", true);
                return;
            }

            if (txtPartyAddressL1.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Address');", true);
                return;
            }

            try
            {
                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(3));
                lblLastCPartyNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                GenerateNewCPartyNo();

                A2ZCARREXCHPARTYDTO objDTO = new A2ZCARREXCHPARTYDTO();

                objDTO.CPartyCode = Converter.GetInteger(lblNewCPartyNo.Text);
                objDTO.CPartyName = Converter.GetString(txtPartyName.Text);
                objDTO.CPartyAddresssLine1 = Converter.GetString(txtPartyAddressL1.Text);
                objDTO.CPartyAddresssLine2 = Converter.GetString(txtPartyAddressL2.Text);
                objDTO.CPartyAddresssLine3 = Converter.GetString(txtPartyAddressL3.Text);
                objDTO.CPartyMobileNo = Converter.GetString(txtMobileNo.Text);
                objDTO.CPartyEmail = Converter.GetString(txtPartyEmail.Text);

                int roweffect = A2ZCARREXCHPARTYDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    UpdatedMSG();

                    ClearRecords();

                    txtPartyName.Focus();

                    //Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data not inserted');", true);
                return;
            }

        }
    }
}
