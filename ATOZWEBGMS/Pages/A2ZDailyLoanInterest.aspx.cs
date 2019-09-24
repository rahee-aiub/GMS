using System;
using System.Web;
using System.Web.UI;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using System.Data;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBGMS.Pages
{
    public partial class A2ZDailyLoanInterest : System.Web.UI.Page
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
                txtFromDate.Text = date;
                

                
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnIntCharge_Click(object sender, EventArgs e)
        {
            try
            {
                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GenerateLoanInterest", "A2ZACGMS"));

                if (result == 0)
                {
                    lblLoanIntStat.Text = "1";
                    UpdateLoanIntStat();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Int. Charge Sucessfully Done');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

       

        protected void UpdateLoanIntStat()
        {
            try
            {
                Int16 BStat = Converter.GetSmallInteger(lblLoanIntStat.Text);

                int roweffect = A2ZERPSYSPRMDTO.UpdateLoanIntStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateBackUpStat Problem');</script>");
                //throw ex;
            }

        }



    }
}
