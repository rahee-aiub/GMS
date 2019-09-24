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
    public partial class A2ZEditItemGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GroupDropdown();
            }
        }


        private void GroupDropdown()
        {
            string sqlquery = "SELECT GroupCode,GroupName FROM A2ZITEMGROUP";
            ddlSelectGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSelectGroup, "A2ZACGMS");

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }




        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlSelectGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select One group');", true);
                return;
            }

            if (txtPurity22.Text == string.Empty)
            {
                txtPurity22.Text = "0";
            }
            if (txtPurity21.Text == string.Empty)
            {
                txtPurity21.Text = "0";
            }
            if (txtPurity18.Text == string.Empty)
            {
                txtPurity18.Text = "0";
            }

            try
            {
                var prm = new object[7];
                prm[0] = txtGroupCode.Text;
                prm[1] = txtGroupName.Text;
                prm[2] = txtMRangeFrom.Text;
                prm[3] = txtMRangeTo.Text;
                prm[4] = txtPurity22.Text;
                prm[5] = txtPurity21.Text;
                prm[6] = txtPurity18.Text;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_UpdateItemGroup]", prm, "A2ZACGMS"));


                if (result == 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data not inserted');", true);
                return;
            }

        }

        protected void txtGroupCode_TextChanged(object sender, EventArgs e)
        {
            int GroupCode = Converter.GetInteger(txtGroupCode.Text);
            A2ZITEMGROUPDTO getDTO = (A2ZITEMGROUPDTO.GetInformation(GroupCode));

            if (getDTO.GroupCode > 0)
            {
                txtGroupCode.Text = Converter.GetString(getDTO.GroupCode);
                ddlSelectGroup.SelectedValue = Converter.GetString(getDTO.GroupCode);
                txtGroupName.Text = Converter.GetString(getDTO.GroupName);
                txtMRangeFrom.Text = Converter.GetString(getDTO.MakingRangeFrom);
                txtMRangeTo.Text = Converter.GetString(getDTO.MakingRangeTo);
                txtPurity18.Text = Converter.GetString(getDTO.Purity18);
                txtPurity21.Text = Converter.GetString(getDTO.Purity21);
                txtPurity22.Text = Converter.GetString(getDTO.Purity22);
            }
            else
            {
                txtGroupCode.Text = string.Empty;
                txtGroupCode.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Item Group');", true);
                return;
            }
        }
        protected void ddlSelectGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectGroup.SelectedIndex != 0)
            {
                int GroupCode = Converter.GetInteger(ddlSelectGroup.SelectedValue);
                A2ZITEMGROUPDTO getDTO = (A2ZITEMGROUPDTO.GetInformation(GroupCode));

                if (getDTO.GroupCode > 0)
                {

                    txtGroupCode.Text = Converter.GetString(getDTO.GroupCode);
                    txtGroupName.Text = Converter.GetString(getDTO.GroupName);
                    txtMRangeFrom.Text = Converter.GetString(getDTO.MakingRangeFrom);
                    txtMRangeTo.Text = Converter.GetString(getDTO.MakingRangeTo);
                    txtPurity18.Text = Converter.GetString(getDTO.Purity18);
                    txtPurity21.Text = Converter.GetString(getDTO.Purity21);
                    txtPurity22.Text = Converter.GetString(getDTO.Purity22);       
                }
                else 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Item Group');", true);
                    return;
                }
              
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if(ddlSelectGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select One group');", true);
                return;
            }
            try
            {
                var prm = new object[1];
                prm[0] = txtGroupCode.Text;
             
                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_DeleteItemGroup]", prm, "A2ZACGMS"));


                if (result == 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Can not Delete');", true);
                return;
            }
        }

        
    }
}
