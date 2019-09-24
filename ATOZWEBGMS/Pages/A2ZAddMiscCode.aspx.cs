using System;
using System.Web;
using System.Web.UI;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBGMS.Pages
{
    public partial class A2ZAddMiscCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
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

        protected void GenerateNewMisc()
        {
            string input1 = Converter.GetString(lblLastMiscNo.Text).Length.ToString();

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
                lblNewMiscNo.Text = "80" + lblLastMiscNo.Text;
            }
            else
            {
                lblNewMiscNo.Text = "80" + result1 + lblLastMiscNo.Text;
            }


        }

        protected void UpdatedMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";

            a = "New Miscellaneous Code Added. /n/n Code: ";
            b = string.Format(lblNewMiscNo.Text);

            Msg += a;
            Msg += b;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;

        }

        protected void ClearRecords()
        {
            txtMiscName.Text = string.Empty;         
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(10));
                lblLastMiscNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                GenerateNewMisc();

                A2ZMISCDTO objDTO = new A2ZMISCDTO();

                objDTO.MiscCode = Converter.GetInteger(lblNewMiscNo.Text);
                objDTO.MiscName = Converter.GetString(txtMiscName.Text);


                int roweffect = A2ZMISCDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    UpdatedMSG();

                    ClearRecords();

                    txtMiscName.Focus();

                  
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
