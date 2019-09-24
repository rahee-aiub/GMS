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
    public partial class A2ZGoldInvoiceReport : System.Web.UI.Page
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
               
                

                
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, txtVchNo.Text);
               // SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txtToDate.Text));

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");

                if (CtrlRecordType.Text == "2")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsUnFixedPurchaseInvoice");
                }
                else if (CtrlRecordType.Text == "1")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsFixedPurchaseInvoice");
                }
                else if (CtrlRecordType.Text == "11")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsGoldInInvoice");
                }
                else if (CtrlRecordType.Text == "22")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsUnFixedSaleInvoice");
                }
                else if (CtrlRecordType.Text == "21")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsFixedSaleInvoice");
                }
                Response.Redirect("ReportServer.aspx", false);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

        protected void txtVchNo_TextChanged(object sender, EventArgs e)
        {
            string qry = "SELECT Id,RecordType FROM A2ZITEMGOLD  WHERE VchNo = '" + txtVchNo.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            if (dt.Rows.Count > 0)
            {
                CtrlRecordType.Text = Converter.GetString(dt.Rows[0]["RecordType"]);

                if (CtrlRecordType.Text != "2" && CtrlRecordType.Text != "1" && CtrlRecordType.Text != "11" && CtrlRecordType.Text != "22" && CtrlRecordType.Text != "21")
                {
                    txtVchNo.Text = string.Empty;
                    txtVchNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Voucher No.');", true);
                    return;
                }
            }
            else 
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Voucher No.');", true);
                return;
            }
        }
    }
}
