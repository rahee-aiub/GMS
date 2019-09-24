using System;
using System.Web;
using System.Web.UI;
using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using System.Data;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;


namespace ATOZWEBGMS.Pages
{
    public partial class A2ZGoldRcvPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                lblProcesDate.Text = date;

                PartyDropdown();

                PayPartyDropdown();

                //CurrencyDropdown();
                DivReInput.Visible = false;
            }
        }





        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=51 and GroupCode !=21 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }

        private void PayPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=51 and GroupCode !=21 GROUP BY PartyCode,PartyName";
            ddlPayPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPayPartyName, "A2ZACGMS");
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

            getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());


            CtrlVoucherNo.Text = "GRP" + getDTO.RecLastNo.ToString("000000");




            try
            {
                var prm = new object[14];

               
                prm[0] = ddlLocation.SelectedValue;
                prm[1] = ddlLocation.SelectedItem.Text;
                prm[2] = CtrlVoucherNo.Text;
                prm[3] = lblID.Text;
                prm[4] = txtPartyCode.Text;
                prm[5] = lblPartyAccType.Text;
                prm[6] = lblPartyAccno.Text;
                prm[7] = txtGoldPureWt.Text;

                prm[8] = txtPayPartyCode.Text;
                prm[9] = lblPayPartyAccType.Text;
                prm[10] = lblPayPartyAccno.Text;
             
                if (txtPremurm.Text == string.Empty)
                {
                    prm[11] = "0";
                }
                else
                {
                    prm[11] = txtPremurm.Text;
                }

                prm[12] = txttrnDesc.Text;
                
                prm[13] = Converter.GetDateToYYYYMMDD(lblProcesDate.Text);

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_GoldReceivePayment]", prm, "A2ZACGMS"));

                if (result == 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }


        private void PrintVoucher()
        {
            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);


            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, CtrlVoucherNo.Text);
            // SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txtToDate.Text));

            //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");


            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsFixedPurchaseInvoice");


            Response.Redirect("ReportServer.aspx", false);
        }



        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyName.SelectedIndex != 0)
            {
                int PartyCode = Converter.GetInteger(ddlPartyName.SelectedValue);
                A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                if (getDTO.PartyName != string.Empty)
                {
                    txtPartyCode.Text = Converter.GetString(getDTO.PartyCode);
                    //txtPartyAddress.Text = Converter.GetString(getDTO.PartyAddresssLine1) + " " + Converter.GetString(getDTO.PartyAddresssLine2) + " " + Converter.GetString(getDTO.PartyAddresssLine3);

                    string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + ddlPartyName.SelectedValue + "' AND AccCurrency = '99' AND AccStatus = 1";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                    int totrec = dt.Rows.Count;
                    if (totrec > 0)
                    {
                        lblPartyAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                        lblPartyAccno.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                    }

                }
            }
        }

        protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        {
            int PartyCode = Converter.GetInteger(txtPartyCode.Text);
            A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

            if (getDTO.PartyName != string.Empty)
            {
                ddlPartyName.SelectedValue = Converter.GetString(getDTO.PartyCode);
                //txtPartyAddress.Text = Converter.GetString(getDTO.PartyAddresssLine1) + " " + Converter.GetString(getDTO.PartyAddresssLine2) + " " + Converter.GetString(getDTO.PartyAddresssLine3);

                string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + ddlPartyName.SelectedValue + "' AND AccCurrency = '99' AND AccStatus = 1";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                int totrec = dt.Rows.Count;
                if (totrec > 0)
                {
                    lblPartyAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                    lblPartyAccno.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                }

            }
            else
            {
                ddlPartyName.SelectedIndex = 0;
                txtPartyCode.Text = string.Empty;
                txtPartyCode.Focus();
            }
        }
        private void ReInputScreen()
        {
            DivReInput.Visible = true;

            BtnSubmit.Enabled = false;
            btnCancel.Enabled = false;
            BtnExit.Enabled = false;

            DivReInput.Style.Add("Top", "250px");
            DivReInput.Style.Add("left", "530px");
            DivReInput.Style.Add("position", "fixed");

            DivMain.Attributes.CssStyle.Add("opacity", "0.3");

            DivReInput.Attributes.CssStyle.Add("opacity", "300");
            DivReInput.Attributes.CssStyle.Add("z-index", "300");

            txtReInput.Text = string.Empty;
            txtReInput.Focus();
        }

        protected void txtGoldPureWt_TextChanged(object sender, EventArgs e)
        {
            lblReInput.Text = "Please Re-Input Gold Pure Wt";

            ReInputScreen();
        }

        protected void txtReInput_TextChanged(object sender, EventArgs e)
        {
            DivReInput.Visible = false;
            BtnSubmit.Enabled = true;
            btnCancel.Enabled = true;
            BtnExit.Enabled = true;

            DivMain.Attributes.CssStyle.Add("opacity", "300");


            if (txtGoldPureWt.Text == txtReInput.Text)
            {
                txtReInput.Text = string.Empty;
                txtPremurm.Focus();
            }
            else
            {
                txtReInput.Text = string.Empty;
                txtGoldPureWt.Text = string.Empty;
                txtGoldPureWt.Focus();
            }
        }

        protected void txtPayPartyCode_TextChanged(object sender, EventArgs e)
        {
            int PartyCode = Converter.GetInteger(txtPayPartyCode.Text);
            A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

            if (getDTO.PartyName != string.Empty)
            {
                ddlPayPartyName.SelectedValue = Converter.GetString(getDTO.PartyCode);
                //txtPayPartyAddress.Text = Converter.GetString(getDTO.PartyAddresssLine1) + " " + Converter.GetString(getDTO.PartyAddresssLine2) + " " + Converter.GetString(getDTO.PartyAddresssLine3);

                string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + ddlPayPartyName.SelectedValue + "' AND AccCurrency = '99' AND AccStatus = 1";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                int totrec = dt.Rows.Count;
                if (totrec > 0)
                {
                    lblPayPartyAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                    lblPayPartyAccno.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                }

            }
            else
            {
                ddlPayPartyName.SelectedIndex = 0;
                txtPayPartyCode.Text = string.Empty;
                txtPayPartyCode.Focus();
            }
        }

        protected void ddlPayPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPayPartyName.SelectedIndex != 0)
            {
                int PartyCode = Converter.GetInteger(ddlPayPartyName.SelectedValue);
                A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                if (getDTO.PartyName != string.Empty)
                {
                    txtPayPartyCode.Text = Converter.GetString(getDTO.PartyCode);
                    //txtPayPartyAddress.Text = Converter.GetString(getDTO.PartyAddresssLine1) + " " + Converter.GetString(getDTO.PartyAddresssLine2) + " " + Converter.GetString(getDTO.PartyAddresssLine3);

                    string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + ddlPayPartyName.SelectedValue + "' AND AccCurrency = '99' AND AccStatus = 1";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                    int totrec = dt.Rows.Count;
                    if (totrec > 0)
                    {
                        lblPayPartyAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                        lblPayPartyAccno.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                    }

                }
            }
        }

    }
}
