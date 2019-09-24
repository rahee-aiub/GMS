using ATOZWEBGMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBGMS.Pages
{
    public partial class A2ZDailyReverseTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnPrmValue.Text = Request.QueryString["a%b"];
                string b = hdnPrmValue.Text;
                HdnModule.Text = b;


                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                CtrlTrnDate.Text = date;


                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                txtVoucherNo.Focus();
                btnDelete.Visible = false;
                btnCancel.Visible = false;

                ValidityFlag.Text = "0";

                DivExit.Visible = false;



            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            gvDetailInfo.Visible = true;

            var prm = new object[2];


            prm[0] = txtVoucherNo.Text;
            prm[1] = hdnID.Text;



            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GetRevTransaction", prm, "A2ZACGMS"));
            if (result == 0)
            {
                string qry = "SELECT Id,AccNo,FuncOpt,PayType FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                if (dt.Rows.Count > 0)
                {
                    HdnAccountNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                    HdnFuncOpt.Text = Converter.GetString(dt.Rows[0]["FuncOpt"]);
                    lblPayType.Text = Converter.GetString(dt.Rows[0]["PayType"]);

                    txtVoucherNo.ReadOnly = true;
                    btnDelete.Visible = true;
                    btnCancel.Visible = true;
                    gvDetailInfo.Visible = true;
                    gvPreview();
                }
                else
                {
                    btnDelete.Visible = false;
                    btnCancel.Visible = false;
                    txtVoucherNo.Text = string.Empty;
                    txtVoucherNo.Focus();
                    VoucherMSG();
                }
            }
            else
            {
                btnDelete.Visible = false;
                btnCancel.Visible = false;

                txtVoucherNo.Text = string.Empty;
                txtVoucherNo.Focus();
                VoucherMSG();
            }

        }


        private void gvPreview()
        {
            string Qry = "SELECT FuncOptDesc,TrnDesc,TrnDebitAmt,TrnCreditAmt FROM WF_REVERSETRANSACTION WHERE VchNo = '" + txtVoucherNo.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZACGMS");
        }




        private void Successful()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Reverse Successfully Completed');", true);
            return;

        }

        private void VoucherMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher not Found');", true);
            return;

        }

        private void AccountClosedMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Account Already Closed');", true);
            return;

        }
        private void InvalidReverseMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Voucher Reverse');", true);
        }

        protected void AccessAccAmountMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Account Balance');", true);
            return;

        }

        protected void AccessCashAmountMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Cash Balance');", true);
            return;

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtVoucherNo.Text = string.Empty;
            txtVoucherNo.ReadOnly = false;
            txtVoucherNo.Focus();
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            gvDetailInfo.Visible = false;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");


                //if (lblChgFlag.Text == "1")
                //{
                //    TextBox lblTrnAmt = (TextBox)e.Row.FindControl("Amount");

                //    Button BtnDel = (Button)e.Row.FindControl("BtnDel");
                //    Button BtnChg = (Button)e.Row.FindControl("BtnChg");
                //    Button BtnUpd = (Button)e.Row.FindControl("BtnUpd");
                //    Button BtnCan = (Button)e.Row.FindControl("BtnCan");

                //    lblTrnAmt.Enabled = true;
                //    BtnDel.Visible = false;
                //    BtnChg.Visible = false;
                //    BtnUpd.Visible = true;
                //    BtnCan.Visible = true;
                //}


            }
        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {

            DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var prm = new object[2];

          
            prm[0] = txtVoucherNo.Text;
            prm[1] = hdnID.Text;
            

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_DeleteTransaction", prm, "A2ZACGMS"));
            if (result == 0)
            {
              
                gvDetailInfo.Visible = false;
                btnDelete.Visible = false;
                btnCancel.Visible = false;
                txtVoucherNo.ReadOnly = false;
                txtVoucherNo.Text = string.Empty;
                txtVoucherNo.ReadOnly = false;
                txtVoucherNo.Focus();
                Successful();
            }



        }

        protected void gvDetailInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        protected void btnOkay_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

    }
}