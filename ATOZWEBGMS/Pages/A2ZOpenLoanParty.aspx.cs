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
    public partial class A2ZOpenLoanPartyOpen : System.Web.UI.Page
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
          
            a = "Generated New Loan Party No.";
            b = string.Format(lblNewLPartyNo.Text);
 
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
                lblNewLPartyNo.Text = "10" + lblLastLPartyNo.Text;
            }
            else 
            {
                lblNewLPartyNo.Text = "10" + result1 + lblLastLPartyNo.Text;
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
                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(1));
                lblLastLPartyNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                GenerateNewLPartyNo();

                A2ZLOANPARTYDTO objDTO = new A2ZLOANPARTYDTO();

                objDTO.LPartyCode = Converter.GetInteger(lblNewLPartyNo.Text);
                objDTO.LPartyName = Converter.GetString(txtPartyName.Text);
                objDTO.LPartyAddresssLine1 = Converter.GetString(txtPartyAddressL1.Text);
                objDTO.LPartyAddresssLine2 = Converter.GetString(txtPartyAddressL2.Text);
                objDTO.LPartyAddresssLine3 = Converter.GetString(txtPartyAddressL3.Text);
                objDTO.LPartyMobileNo = Converter.GetString(txtMobileNo.Text);
                objDTO.LPartyEmail = Converter.GetString(txtPartyEmail.Text);

                int roweffect = A2ZLOANPARTYDTO.InsertInformation(objDTO);
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
