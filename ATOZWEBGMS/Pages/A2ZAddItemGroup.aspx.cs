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
    public partial class A2ZAddItemGroup : System.Web.UI.Page
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


        //protected void UpdatedMSG()
        //{
        //    string Msg = "";

        //    string a = "";
        //    string b = "";

        //    a = "Generated New Carrier/Currency Exchange Party No.";
        //    b = string.Format(lblNewCPartyNo.Text);

        //    Msg += a;
        //    Msg += b;

        //    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
        //    return;

        //}


        protected void GetItemGroupCode()
        {
            string qry = "SELECT GroupCode,GroupCode FROM A2ZITEMGROUP";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            lblNewItemGroup.Text = Converter.GetString(newaccno);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            GetItemGroupCode();


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
                prm[0] = lblNewItemGroup.Text;
                prm[1] = txtGroupName.Text;
                prm[2] = txtMRangeFrom.Text;
                prm[3] = txtMRangeTo.Text;

                prm[4] = txtPurity22.Text;
                prm[5] = txtPurity21.Text;
                prm[6] = txtPurity18.Text;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertItemGroup]", prm, "A2ZACGMS"));


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

        
    }
}
