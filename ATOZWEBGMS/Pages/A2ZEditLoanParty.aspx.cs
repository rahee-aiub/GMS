using System;
using System.Web;
using System.Web.UI;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBGMS.Pages
{
    public partial class A2ZEditLoanParty : System.Web.UI.Page
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
            string sqlquery = "SELECT LPartyCode,LPartyName from A2ZLOANPARTY";
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
                A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                if (getDTO.PartyName != string.Empty)
                {
                    txtLoanPartyCode.Text = Converter.GetString(getDTO.PartyCode);
                    txtPartyAddressL1.Text = Converter.GetString(getDTO.PartyAddresssLine1);
                    txtPartyAddressL2.Text = Converter.GetString(getDTO.PartyAddresssLine2);
                    txtPartyAddressL3.Text = Converter.GetString(getDTO.PartyAddresssLine3);
                    txtMobileNo.Text = Converter.GetString(getDTO.PartyMobileNo);
                    txtPartyEmail.Text = Converter.GetString(getDTO.PartyEmail);
                    txtPartyName.Text = Converter.GetString(getDTO.PartyName);
                }
            }
        }

        protected void txtLoanPartyCode_TextChanged(object sender, EventArgs e)
        {
            if (txtLoanPartyCode.Text != string.Empty)
            {
                int PartyCode = Converter.GetInteger(txtLoanPartyCode.Text);
                A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                if (getDTO.PartyName == null)
                {

                    ddlPartyName.SelectedIndex = 0;
                    txtPartyAddressL1.Text = string.Empty;
                    txtPartyAddressL2.Text = string.Empty;
                    txtPartyAddressL3.Text = string.Empty;
                    txtMobileNo.Text = string.Empty;
                    txtPartyEmail.Text = string.Empty;


                    txtLoanPartyCode.Text = string.Empty;
                    txtLoanPartyCode.Focus();
                }

                else
                {
                    ddlPartyName.SelectedValue = Converter.GetString(getDTO.PartyCode);
                    txtPartyAddressL1.Text = Converter.GetString(getDTO.PartyAddresssLine1);
                    txtPartyAddressL2.Text = Converter.GetString(getDTO.PartyAddresssLine2);
                    txtPartyAddressL3.Text = Converter.GetString(getDTO.PartyAddresssLine3);
                    txtMobileNo.Text = Converter.GetString(getDTO.PartyMobileNo);
                    txtPartyEmail.Text = Converter.GetString(getDTO.PartyEmail);
                    txtPartyName.Text = Converter.GetString(getDTO.PartyName);
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            A2ZPARTYDTO objDTO = new A2ZPARTYDTO();
            objDTO.PartyName = Converter.GetString(txtPartyName.Text);
            objDTO.PartyAddresssLine1 = Converter.GetString(txtPartyAddressL1.Text);
            objDTO.PartyAddresssLine2 = Converter.GetString(txtPartyAddressL2.Text);
            objDTO.PartyAddresssLine3 = Converter.GetString(txtPartyAddressL3.Text);
            objDTO.PartyMobileNo = Converter.GetString(txtMobileNo.Text);
            objDTO.PartyEmail = Converter.GetString(txtPartyEmail.Text);
            objDTO.PartyCode = Converter.GetInteger(txtLoanPartyCode.Text);

            //int roweffect = A2ZPARTYDTO.UpdateLoanParty(objDTO);
            //if (roweffect > 0)
            //{
            //    Response.Redirect(Request.RawUrl);
            //}
        }
    }
}
