using System;
using System.Web;
using System.Web.UI;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBGMS.Pages
{
    public partial class A2ZEditCarrExchParty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PartyDropdown();
            }
        }

        private void PartyDropdown()
        {
            string sqlquery = "SELECT CPartyCode,CPartyName from A2ZCARREXCHPARTY";
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

        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyName.SelectedIndex != 0)
            {
                int PartyCode = Converter.GetInteger(ddlPartyName.SelectedValue);
                A2ZCARREXCHPARTYDTO getDTO = (A2ZCARREXCHPARTYDTO.GetCarrExchPartyByPartyCode(PartyCode));

                if (getDTO.CPartyName != string.Empty)
                {
                    txtCarrExchPartyCode.Text = Converter.GetString(getDTO.CPartyCode);
                    txtPartyAddressL1.Text = Converter.GetString(getDTO.CPartyAddresssLine1);
                    txtPartyAddressL2.Text = Converter.GetString(getDTO.CPartyAddresssLine2);
                    txtPartyAddressL3.Text = Converter.GetString(getDTO.CPartyAddresssLine3);
                    txtMobileNo.Text = Converter.GetString(getDTO.CPartyMobileNo);
                    txtPartyEmail.Text = Converter.GetString(getDTO.CPartyEmail);
                    txtPartyName.Text = Converter.GetString(getDTO.CPartyName);
                }
            }
        }

     

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            A2ZCARREXCHPARTYDTO objDTO = new A2ZCARREXCHPARTYDTO();
            objDTO.CPartyName = Converter.GetString(txtPartyName.Text);
            objDTO.CPartyAddresssLine1 = Converter.GetString(txtPartyAddressL1.Text);
            objDTO.CPartyAddresssLine2 = Converter.GetString(txtPartyAddressL2.Text);
            objDTO.CPartyAddresssLine3 = Converter.GetString(txtPartyAddressL3.Text);
            objDTO.CPartyMobileNo = Converter.GetString(txtMobileNo.Text);
            objDTO.CPartyEmail = Converter.GetString(txtPartyEmail.Text);
            objDTO.CPartyCode = Converter.GetInteger(txtCarrExchPartyCode.Text);

            int roweffect = A2ZCARREXCHPARTYDTO.UpdateCarrExchParty(objDTO);
            if (roweffect > 0)
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void txtCarrExchPartyCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCarrExchPartyCode.Text != string.Empty)
            {
                int PartyCode = Converter.GetInteger(txtCarrExchPartyCode.Text);
                A2ZCARREXCHPARTYDTO getDTO = (A2ZCARREXCHPARTYDTO.GetCarrExchPartyByPartyCode(PartyCode));

                if (getDTO.CPartyName == null)
                {

                    ddlPartyName.SelectedIndex = 0;
                    txtPartyAddressL1.Text = string.Empty;
                    txtPartyAddressL2.Text = string.Empty;
                    txtPartyAddressL3.Text = string.Empty;
                    txtMobileNo.Text = string.Empty;
                    txtPartyEmail.Text = string.Empty;


                    txtCarrExchPartyCode.Text = string.Empty;
                    txtCarrExchPartyCode.Focus();
                }

                else
                {
                    ddlPartyName.SelectedValue = Converter.GetString(getDTO.CPartyCode);
                    txtPartyAddressL1.Text = Converter.GetString(getDTO.CPartyAddresssLine1);
                    txtPartyAddressL2.Text = Converter.GetString(getDTO.CPartyAddresssLine2);
                    txtPartyAddressL3.Text = Converter.GetString(getDTO.CPartyAddresssLine3);
                    txtMobileNo.Text = Converter.GetString(getDTO.CPartyMobileNo);
                    txtPartyEmail.Text = Converter.GetString(getDTO.CPartyEmail);
                    txtPartyName.Text = Converter.GetString(getDTO.CPartyName);
                }
            }
        }
    }
}
