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
    public partial class A2ZGoldSaleInventoryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;

                PartyDropdown();

                if (CtrlProgFlag.Text != "1")
                {

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    CtrlProcDate.Text = date;
                    txtFromDate.Text = date;
                    txtToDate.Text = date;
                }
                else 
                {
                    string RrbtSaleAndReturn = (string)Session["SrbtSaleAndReturn"];
                    string RrbtSaleFixing = (string)Session["SrbtSaleFixing"];
                    string RrbtDetails = (string)Session["SrbtDetails"];
                    string RrbtSummary = (string)Session["SrbtSummary"];
                    string RchkAllParty = (string)Session["SchkAllParty"];
                    string RddlPartyName = (string)Session["SddlPartyName"];


                    string RtxtFromDate = (string)Session["StxtFromDate"];
                    string RtxtToDate = (string)Session["StxtToDate"];

                    txtFromDate.Text = RtxtFromDate;
                    txtToDate.Text = RtxtToDate;

                    if (RrbtSaleAndReturn == "1")
                    {
                        rbtSaleAndReturn.Checked = true;
                    }
                    else
                    {
                        rbtSaleAndReturn.Checked = false;
                    }

                    if (RrbtSaleFixing == "1")
                    {
                        rbtSaleFixing.Checked = true;
                        rbtDetails.Visible = false;
                        rbtSummary.Visible = false;
                    }
                    else
                    {
                        rbtSaleFixing.Checked = false;
                        rbtDetails.Visible = true;
                        rbtSummary.Visible = true;
                    }

                    if (RrbtDetails == "1")
                    {
                        rbtDetails.Checked = true;
                    }
                    else
                    {
                        rbtDetails.Checked = false;
                    }

                    if (RrbtSummary == "1")
                    {
                        rbtSummary.Checked = true;
                    }
                    else
                    {
                        rbtSummary.Checked = false;
                    }

                    if (RchkAllParty == "1")
                    {
                        chkAllParty.Checked = true;
                        ddlPartyName.Enabled = false;
                    }
                    else
                    {
                        chkAllParty.Checked = false;
                        ddlPartyName.Enabled = true;
                        ddlPartyName.SelectedValue = RddlPartyName;
                    }
                
                }

            }
        }

        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=51 and GroupCode !=22 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }
        protected void RemoveSession()
        {
           
            Session["ProgFlag"] = string.Empty;
            Session["StxtFromDate"] = string.Empty;
            Session["StxtToDate"] = string.Empty;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                Session["ProgFlag"] = "1";
                Session["StxtFromDate"] = txtFromDate.Text;
                Session["StxtToDate"] = txtToDate.Text;

                if (rbtSaleAndReturn.Checked == true)
                {
                    Session["SrbtSaleAndReturn"] = "1";
                }
                else
                {
                    Session["SrbtSaleAndReturn"] = "0";
                }

                if (rbtSaleFixing.Checked == true)
                {
                    Session["SrbtSaleFixing"] = "1";
                }
                else
                {
                    Session["SrbtSaleFixing"] = "0";
                }

                if (rbtDetails.Checked == true)
                {
                    Session["SrbtDetails"] = "1";
                }
                else
                {
                    Session["SrbtDetails"] = "0";
                }

                if (rbtSummary.Checked == true)
                {
                    Session["SrbtSummary"] = "1";
                }
                else
                {
                    Session["SrbtSummary"] = "0";
                }


                if (chkAllParty.Checked == true)
                {
                    Session["SchkAllParty"] = "1";
                }
                else
                {
                    Session["SchkAllParty"] = "0";
                }

                Session["SddlPartyName"] = ddlPartyName.SelectedValue;



                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtFromDate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txtToDate.Text));

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 31);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");


                if (chkAllParty.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "ALL");
                }
                else
                {
                    int code = Converter.GetInteger(ddlPartyName.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, code);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlPartyName.SelectedItem.Text);
                }


                if (rbtSaleAndReturn.Checked == true)
                {
                    if (rbtSummary.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsSaleAndSaleReturnReport");
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsSaleAndSaleReturnReportDtl");
                    }
                }
                else if (rbtSaleFixing.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsSaleFixingReport");
                }


                Response.Redirect("ReportServer.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void chkAllParty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllParty.Checked == true)
            {
                ddlPartyName.Enabled = false;
            }
            else
            {
                ddlPartyName.Enabled = true;
            }

        }

        protected void rbtSaleAndReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSaleAndReturn.Checked == true)
            {
                rbtDetails.Visible = true;
                rbtSummary.Visible = true;
                rbtSummary.Checked = true;
            }
        }
        protected void rbtSaleFixing_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSaleFixing.Checked == true)
            {
                rbtDetails.Visible = false;
                rbtSummary.Visible = false;
            }
        }

        
    }
}
