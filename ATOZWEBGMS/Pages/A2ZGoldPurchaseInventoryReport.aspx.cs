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
    public partial class A2ZGoldPurchaseInventoryReport : System.Web.UI.Page
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

                    string RrbtPurchaseAndReturn = (string)Session["SrbtPurchaseAndReturn"];
                    string RrbtPurchaseFixing = (string)Session["SrbtPurchaseFixing"];
                    string RrbtDetails = (string)Session["SrbtDetails"];
                    string RrbtSummary = (string)Session["SrbtSummary"];
                    string RchkAllParty = (string)Session["SchkAllParty"];
                    string RddlPartyName = (string)Session["SddlPartyName"];
                    
                    string RtxtFromDate = (string)Session["StxtFromDate"];
                    string RtxtToDate = (string)Session["StxtToDate"];

                    txtFromDate.Text = RtxtFromDate;
                    txtToDate.Text = RtxtToDate;

                    if (RrbtPurchaseAndReturn == "1")
                    {
                        rbtPurchaseAndReturn.Checked = true;
                    }
                    else
                    {
                        rbtPurchaseAndReturn.Checked = false;
                    }

                    if (RrbtPurchaseFixing == "1")
                    {
                        rbtPurchaseFixing.Checked = true;
                        rbtDetails.Visible = false;
                        rbtSummary.Visible = false;
                    }
                    else
                    {
                        rbtPurchaseFixing.Checked = false;
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

                if (rbtPurchaseAndReturn.Checked == true)
                {
                    Session["SrbtPurchaseAndReturn"] = "1";
                }
                else
                {
                    Session["SrbtPurchaseAndReturn"] = "0";
                }

                if (rbtPurchaseFixing.Checked == true)
                {
                    Session["SrbtPurchaseFixing"] = "1";
                }
                else
                {
                    Session["SrbtPurchaseFixing"] = "0";
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


                if (rbtPurchaseAndReturn.Checked == true)
                {
                    if (rbtSummary.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsPurchaseAndPurchaseReturnReport");
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsPurchaseAndPurchaseReturnReportDtl");
                    }
                }
                else if (rbtPurchaseFixing.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsPurchaseFixingReport");
                }



                Response.Redirect("ReportServer.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void rbtPurchaseFixing_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPurchaseFixing.Checked == true)
            {
                rbtDetails.Visible = false;
                rbtSummary.Visible = false;
            }

        }

        protected void rbtPurchaseAndReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPurchaseAndReturn.Checked == true)
            {
                rbtDetails.Visible = true;
                rbtSummary.Visible = true;
                rbtSummary.Checked = true;

            }
        }

        //protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtPartyCode.Text != string.Empty)
        //    {
        //        int PartyCode = Converter.GetInteger(txtPartyCode.Text);
        //        A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

        //        if (getDTO.PartyName == null)
        //        {
        //            //txtPartyName.Text = string.Empty;
        //            txtPartyCode.Text = string.Empty;
        //            txtPartyCode.Focus();
        //        }

        //        else
        //        {
        //            ddlPartyName.SelectedValue = txtPartyCode.Text;


        //        }
        //    }
        //}

        //protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPartyName.SelectedIndex != 0)
        //    {
        //        int PartyCode = Converter.GetInteger(ddlPartyName.SelectedValue);
        //        A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

        //        if (getDTO.PartyName != string.Empty)
        //        {
        //            txtPartyCode.Text = Converter.GetString(getDTO.PartyCode);

        //        }
        //    }
        //}


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
    }
}
