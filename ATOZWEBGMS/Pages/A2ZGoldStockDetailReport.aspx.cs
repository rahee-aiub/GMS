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
    public partial class A2ZGoldStockDetailReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;

                if (CtrlProgFlag.Text != "1")
                {

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    CtrlProcDate.Text = date;
                    txtFromDate.Text = date;
                    txtToDate.Text = date;

                    rbtDetails.Visible = false;
                    rbtSummary.Visible = false;
                    Label7.Text = "Date :";
                    Label1.Visible = false;
                    txtToDate.Visible = false;

                    chkAllLocation.Visible = false;
                    lblLocation.Visible = false;
                    ddlLocation.Visible = false;
                }
                else
                {
                    string RtxtFromDate = (string)Session["StxtFromDate"];
                    string RtxtToDate = (string)Session["StxtToDate"];

                    string RrbtStckTotalDtl = (string)Session["SrbtStckTotalDtl"];
                    string RrbtStckDetail1 = (string)Session["SrbtStckDetail1"];
                    string RrbtStckDetail2 = (string)Session["SrbtStckDetail2"];
                    string RrbtStckDetail3 = (string)Session["SrbtStckDetail3"];

                    string RrbtDetails = (string)Session["SrbtDetails"];

                    string RchkAllLocation = (string)Session["SchkAllLocation"];
                    string RddlLocation = (string)Session["SddlLocation"];


                    txtFromDate.Text = RtxtFromDate;
                    txtToDate.Text = RtxtToDate;

                    if (RrbtStckTotalDtl == "1")
                    {
                        rbtStckTotalDtl.Checked = true;
                        rbtStckDetail1.Checked = false;
                        rbtStckDetail2.Checked = false;
                        rbtStckDetail3.Checked = false;
                        rbtDetails.Visible = false;
                        rbtSummary.Visible = false;
                        Label7.Text = "Date :";
                        Label1.Visible = false;
                        txtToDate.Visible = false;
                    }
                    

                    if (RrbtStckDetail1 == "1")
                    {
                        rbtStckDetail1.Checked = true;
                        rbtStckTotalDtl.Checked = false;
                        rbtStckDetail2.Checked = false;
                        rbtStckDetail3.Checked = false;
                        rbtDetails.Visible = true;
                        rbtSummary.Visible = true;
                        Label7.Text = "From Date :";
                        Label1.Visible = true;
                        txtToDate.Visible = true;
                    }
                    

                    if (RrbtStckDetail2 == "1")
                    {
                        rbtStckDetail2.Checked = true;
                        rbtStckTotalDtl.Checked = false;
                        rbtStckDetail1.Checked = false;
                        rbtStckDetail3.Checked = false;
                        rbtDetails.Visible = true;
                        rbtSummary.Visible = true;
                        Label7.Text = "From Date :";
                        Label1.Visible = true;
                        txtToDate.Visible = true;
                    }
                    


                    if (RrbtStckDetail3 == "1")
                    {
                        rbtStckDetail3.Checked = true;
                        rbtStckTotalDtl.Checked = false;
                        rbtStckDetail1.Checked = false;
                        rbtStckDetail2.Checked = false;
                        rbtDetails.Visible = true;
                        rbtSummary.Visible = true;
                        Label7.Text = "From Date :";
                        Label1.Visible = true;
                        txtToDate.Visible = true;
                    }
                    
                    if (RrbtDetails == "1")
                    {
                        rbtDetails.Checked = true;
                    }
                    else
                    {
                        rbtSummary.Checked = true;
                    }


                    if (RchkAllLocation == "1")
                    {
                        chkAllLocation.Checked = true;
                        ddlLocation.SelectedIndex = 0;
                        ddlLocation.Enabled = false;
                    }
                    else
                    {
                        chkAllLocation.Checked = false;
                        ddlLocation.SelectedValue = RddlLocation;
                        ddlLocation.Enabled = true;
                    }

                }


            }
        }

        protected void RemoveSession()
        {

            Session["ProgFlag"] = string.Empty;
            Session["StxtFromDate"] = string.Empty;
            Session["StxtToDate"] = string.Empty;

            Session["SrbtStckTotalDtl"] = string.Empty;

            Session["SrbtStckDetail1"] = string.Empty;
            Session["SrbtStckDetail2"] = string.Empty;
            Session["SrbtStckDetail3"] = string.Empty;

            Session["SrbtDetails"] = string.Empty;

            Session["SchkAllLocation"] = string.Empty;
            Session["SddlLocation"] = string.Empty;

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

                if (rbtStckTotalDtl.Checked == true)
                {
                    Session["SrbtStckTotalDtl"] = "1";
                }
                else
                {
                    Session["SrbtStckTotalDtl"] = "0";
                }

                if (rbtStckDetail1.Checked == true)
                {
                    Session["SrbtStckDetail1"] = "1";
                }
                else
                {
                    Session["SrbtStckDetail1"] = "0";
                }

                if (rbtStckDetail2.Checked == true)
                {
                    Session["SrbtStckDetail2"] = "1";
                }
                else
                {
                    Session["SrbtStckDetail2"] = "0";
                }

                if (rbtStckDetail3.Checked == true)
                {
                    Session["SrbtStckDetail3"] = "1";
                }
                else
                {
                    Session["SrbtStckDetail3"] = "0";
                }

                if (rbtDetails.Checked == true)
                {
                    Session["SrbtDetails"] = "1";
                }
                else
                {
                    Session["SrbtDetails"] = "0";
                }


                if (chkAllLocation.Checked == true)
                {
                    Session["SchkAllLocation"] = "1";
                }
                else
                {
                    Session["SchkAllLocation"] = "0";
                }

                Session["SddlLocation"] = ddlLocation.SelectedValue;

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtFromDate.Text));

                if (rbtStckTotalDtl.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txtToDate.Text));
                }


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");

                if (rbtStckTotalDtl.Checked == false)
                {
                    if (chkAllLocation.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                    }
                    else
                    {
                        int code = Converter.GetInteger(ddlLocation.SelectedValue);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, code);
                    }
                }

                if (rbtStckTotalDtl.Checked == true)
                {
                    var prm = new object[2];
                    prm[0] = Converter.GetDateToYYYYMMDD(txtFromDate.Text);
                    prm[1] = 0;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_rptGmsTotalStockDetailsReport]", prm, "A2ZACGMS"));

                    if (result == 0)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsTotalStockDetailReport");
                    }
                }
                else if (rbtStckDetail1.Checked == true)
                {
                    if (rbtDetails.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 11);
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 21);
                    }

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsCodeWiseStockDetailReport");
                }
                else if (rbtStckDetail2.Checked == true)
                {
                    if (rbtDetails.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 12);
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 22);
                    }


                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsKaratandPurityWaiseStockDetailReport");
                }
                else if (rbtStckDetail3.Checked == true)
                {
                    if (rbtDetails.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 13);
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 23);
                    }

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsGrossWaiseStockDetail");
                }

                Response.Redirect("ReportServer.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void rbtStckTotalDtl_CheckedChanged(object sender, EventArgs e)
        {
            rbtDetails.Visible = false;
            rbtSummary.Visible = false;
            Label7.Text = "Date :";
            Label1.Visible = false;
            txtToDate.Visible = false;

            chkAllLocation.Visible = false;
            lblLocation.Visible = false;
            ddlLocation.Visible = false;
        }

        protected void rbtStckDetail1_CheckedChanged(object sender, EventArgs e)
        {
            rbtDetails.Visible = true;
            rbtSummary.Visible = true;
            Label7.Text = "From Date :";
            Label1.Visible = true;
            txtToDate.Visible = true;

            chkAllLocation.Checked = true;
            chkAllLocation.Visible = true;
            lblLocation.Visible = true;
            ddlLocation.Visible = true;

            ddlLocation.SelectedIndex = 0;
            ddlLocation.Enabled = false;
        }

        protected void rbtStckDetail2_CheckedChanged(object sender, EventArgs e)
        {
            rbtDetails.Visible = true;
            rbtSummary.Visible = true;
            Label7.Text = "From Date :";
            Label1.Visible = true;
            txtToDate.Visible = true;

            chkAllLocation.Checked = true;
            chkAllLocation.Visible = true;
            lblLocation.Visible = true;
            ddlLocation.Visible = true;

            ddlLocation.SelectedIndex = 0;
            ddlLocation.Enabled = false;
        }

        protected void rbtStckDetail3_CheckedChanged(object sender, EventArgs e)
        {
            rbtDetails.Visible = true;
            rbtSummary.Visible = true;
            Label7.Text = "From Date :";
            Label1.Visible = true;
            txtToDate.Visible = true;

            chkAllLocation.Checked = true;
            chkAllLocation.Visible = true;
            lblLocation.Visible = true;
            ddlLocation.Visible = true;

            ddlLocation.SelectedIndex = 0;
            ddlLocation.Enabled = false;
        }

        protected void chkAllLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllLocation.Checked == true)
            {
                ddlLocation.SelectedIndex = 0;
                ddlLocation.Enabled = false;
            }
            else
            {
                ddlLocation.SelectedIndex = 0;
                ddlLocation.Enabled = true;
            }
        }
    }
}
