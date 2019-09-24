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
    public partial class A2ZGoldPurchaseUnfixed : System.Web.UI.Page
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
                txtDueDate.Text = date;

                ddlLocation.SelectedValue = "2";
                ddlLocation.Enabled = false;
                divPost.Visible = false;

                CodeDropdown();
                PartyDropdown();
                CurrencyDropdown();

                ddlCurrency.SelectedIndex = 1;

                ConvertCurrencyDropdown();
                TruncateWF();
            }
        }

        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZITEMGOLD WHERE UserId='" + lblID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZACGMS"));
        }

        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=51 and GroupCode !=21 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }
        
        private void CodeDropdown()
        {
            string sqlquery = "SELECT GroupCode,CONCAT(GroupName,'  (',MakingRangeFrom, '  to ',MakingRangeTo,')') AS GroupName FROM A2ZITEMGROUP";
            ddlCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCode, "A2ZACGMS");
        }
        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode > 1 AND CurrencyCode != 99";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");
        }

        private void ConvertCurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode != 99";
            ddlConvCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlConvCurrency, "A2ZACGMS");
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
            lblDescription.Text = "Gw:" + lblGrossWt.Text + ",St:" + lblStoneWt.Text + ",Nw:" + lblNetWt.Text + ",Pt:" + lblPureWt.Text + ",Sv:" + lblStoneValue.Text + ",Mc:" + lblMaking.Text;

            if (lblTotalValue.Text == string.Empty || Converter.GetDecimal(lblTotalValue.Text) < 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insert At least One item');", true);
                return;
            }

            txtTotalGross.Text = lblGrossWt.Text;
            txtTotalNet.Text = lblNetWt.Text;
            txtMetal.Text = lblMetalValue.Text;
            txtMaking.Text = lblMaking.Text;
            txtStone.Text = lblStoneValue.Text;
            txtTotalValueView.Text = lblTotalValue.Text;
            txtDiscount.Text = "0,0.00";

            ddlConvCurrency.SelectedValue = ddlCurrency.SelectedValue;
            ConvertCalculate(1);
            CalculateConvertTotal();

            txtConvRate.ReadOnly = true;
            divPost.Visible = true;
            divPost.Style.Add("Top", "170px");
            divPost.Style.Add("left", "350px");
            divPost.Style.Add("position", "fixed");
            divMain.Attributes.CssStyle.Add("opacity", "0.5");
            divPost.Attributes.CssStyle.Add("opacity", "400");
            divPost.Attributes.CssStyle.Add("z-index", "400");
            btnPost.Focus();
        }


        private void PrintVoucher()
        {
            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, CtrlVoucherNo.Text);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsUnFixedPurchaseInvoice");

            Response.Redirect("ReportServer.aspx", false);
        }
        private void gvItemDetailsInfo()
        {
            EmptyTotalInfo();
            string sqlquery = "(SELECT Id,ItemGroupName,Karat,ItemName,Purity,GrossWt,StoneWt,NetWt,PureWt,MakingRate,StoneMakingRate,MakingValue,StoneMakingValue,TotalMetalValue,TotalValue FROM WFA2ZITEMGOLD WHERE UserId = " + lblID.Text + ")";
            gvItemDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvItemDetails, "A2ZACGMS");
        }
        private void EmptyTotalInfo()
        {
            lblGrossWt.Text = string.Empty;
            lblStoneWt.Text = string.Empty;
            lblNetWt.Text = string.Empty;
            lblPureWt.Text = string.Empty;
            lblMaking.Text = string.Empty;
            lblStoneValue.Text = string.Empty;
            lblMetalValue.Text = string.Empty;           
            lblTotalValue.Text = string.Empty;
        }

        protected void BtnAddItem_Click(object sender, EventArgs e)
        {
            if (ddlKarat.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Karat');", true);
                return;
            }
            try
            {
                txtPartyCode.ReadOnly = true;
                ddlPartyName.Enabled = false;

                var prm = new object[31];
                prm[0] = Converter.GetDateToYYYYMMDD(lblProcesDate.Text);
                prm[1] = ddlCode.SelectedValue;
                prm[2] = ddlCode.SelectedItem.Text.Substring(0,4);
                prm[3] = ddlCurrency.SelectedValue;
                prm[4] = ddlKarat.SelectedValue;
                prm[5] = txtItemName.Text;
                prm[6] = txtPurity.Text;
                prm[7] = txtGrossWt.Text;
                prm[8] = txtStoneWt.Text;
                prm[9] = txtNetWt.Text;
                prm[10] = txtPureWt.Text;
                prm[11] = txtMakingRate.Text;
                prm[12] = txtStoneMakingRate.Text;
                prm[13] = txtMakingValue.Text;
                prm[14] = txtStoneMakingValue.Text;
                prm[15] = "0"; // txtTotalMetalValue.Text;
                prm[16] = txtTotalValue.Text;
                prm[17] = "2"; //Fixed Purchase Type
                prm[18] = "UnFixed Purchase";
                prm[19] = "2"; //FixedUnfixed       
                prm[20] = ddlLocation.SelectedValue;
                prm[21] = ddlLocation.SelectedItem.Text;
                prm[22] = ddlPartyName.SelectedValue;
                prm[23] = lblPartyAccno.Text;
                prm[24] = txtRefName.Text;
                prm[25] = Converter.GetDateToYYYYMMDD(txtDueDate.Text);
                prm[26] = "0";
                prm[27] = "0";
                prm[28] = "0";
                prm[29] = "0";
                prm[30] = lblID.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertWfItemGold]", prm, "A2ZACGMS"));

                if (result == 0)
                {
                    ClearInfo();
                    gvItemDetailsInfo();
                    BtnSubmit.Focus();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label IdNo = (Label)gvItemDetails.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                int Id = Converter.GetInteger(IdNo.Text);

                string sqlQuery = string.Empty;
                int rowEffect;
                sqlQuery = @"DELETE  FROM WFA2ZITEMGOLD WHERE  Id = '" + Id + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACGMS"));
                gvItemDetailsInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InitializedInfo()
        {

            ddlKarat.SelectedIndex = 0;
            txtPurity.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtGrossWt.Text = string.Empty;
            txtStoneWt.Text = string.Empty;
            txtNetWt.Text = string.Empty;
            txtPureWt.Text = string.Empty;
            txtMakingRate.Text = string.Empty;
            txtStoneMakingRate.Text = string.Empty;
            txtMakingValue.Text = string.Empty;
            txtStoneMakingValue.Text = string.Empty;
            txtTotalValue.Text = string.Empty;
        }

        private void ClearInfo()
        {
            ddlCode.SelectedIndex = 0;
            ddlKarat.SelectedIndex = 0;
            txtPurity.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtGrossWt.Text = string.Empty;
            txtStoneWt.Text = string.Empty;
            txtNetWt.Text = string.Empty;
            txtPureWt.Text = string.Empty;
            txtMakingRate.Text = string.Empty;
            txtStoneMakingRate.Text = string.Empty;
            txtMakingValue.Text = string.Empty;
            txtStoneMakingValue.Text = string.Empty;
            txtTotalValue.Text = string.Empty;
        }
        private void CalculateNetWtPureWt()
        {
            if (txtStoneWt.Text == string.Empty)
            {
                txtStoneWt.Text = "0";
                txtStoneMakingRate.Text = "0";
                txtStoneMakingValue.Text = "0";
            }

            decimal NetWt = Converter.GetDecimal(txtGrossWt.Text) - Converter.GetDecimal(txtStoneWt.Text);
            decimal PureWt = NetWt * Converter.GetDecimal(txtPurity.Text);

            txtNetWt.Text = NetWt.ToString("0.00");
            txtPureWt.Text = PureWt.ToString("0.00");
        }

        private void CalculateMakingValue()
        {
            decimal MakingRate = Converter.GetDecimal(txtNetWt.Text) * Converter.GetDecimal(txtMakingRate.Text);
            txtMakingValue.Text = MakingRate.ToString("0.00");
        }

        private void CalculateStoneMakingValue()
        {
            if (txtStoneWt.Text != string.Empty && txtStoneMakingRate.Text != string.Empty)
            {
                decimal StoneValue = Converter.GetDecimal(txtStoneWt.Text) * Converter.GetDecimal(txtStoneMakingRate.Text);
                txtStoneMakingValue.Text = StoneValue.ToString("0.00");
            }
        }
        private void CalculateTotalMetalValue()
        {
            decimal MetalRate = 0;
            decimal PureWt = 0;
            decimal TotalMetalValue = 0;

            if (txtPureWt.Text != string.Empty)
            {
                PureWt = Converter.GetDecimal(txtPureWt.Text);
            }

            TotalMetalValue = MetalRate * PureWt;
        }

        private void CalculateTotalValue()
        {
            decimal MakingValue = 0;
            decimal StoneMakingValue = 0;

            if (txtMakingValue.Text != string.Empty)
            {
                MakingValue = Converter.GetDecimal(txtMakingValue.Text);
            }
            if (txtStoneMakingValue.Text != string.Empty)
            {
                StoneMakingValue = Converter.GetDecimal(txtStoneMakingValue.Text);
            }

            decimal TotalValue = StoneMakingValue + MakingValue;
            txtTotalValue.Text = TotalValue.ToString("0.00");
        }

        private void ValidCheckStoneWt()
        {
            lblMsgFlag.Text = "0";

            decimal grosswt = 0;
            decimal stonewt = 0;
            decimal netwt = 0;

            grosswt = Converter.GetDecimal(txtGrossWt.Text);
            stonewt = Converter.GetDecimal(txtStoneWt.Text);
            netwt = (grosswt - stonewt);

            if (netwt <= 0)
            {
                lblMsgFlag.Text = "1";
            }
        }

        protected void txtStoneWt_TextChanged(object sender, EventArgs e)
        {
            ValidCheckStoneWt();
            if (lblMsgFlag.Text == "1")
            {
                txtStoneWt.Text = string.Empty;
                txtStoneWt.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Excess Stone Weight');", true);
                return;
            }

            CalculateNetWtPureWt();
            CalculateTotalValue();
            CalculateTotalMetalValue();
            txtMakingRate.Focus();
        }

        protected void txtGrossWt_TextChanged(object sender, EventArgs e)
        {
            CalculateNetWtPureWt();
            CalculateTotalValue();
            CalculateTotalMetalValue();
            txtStoneWt.Focus();
        }

        protected void txtMakingRate_TextChanged(object sender, EventArgs e)
        {
            CalculateMakingValue();
            CalculateTotalValue();
            txtStoneMakingRate.Focus();
        }

        protected void txtStoneMaking_TextChanged(object sender, EventArgs e)
        {
            CalculateStoneMakingValue();
            CalculateTotalValue();
            BtnAddItem.Focus();
        }

        protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Decimal GrossWt = 0;
            Decimal StoneWt = 0;
            Decimal NetWt = 0;
            Decimal PureWt = 0;
            Decimal MValue = 0;
            Decimal SValue = 0;
            Decimal MetalValue = 0;
            Decimal TotalValue = 0;

            for (int i = 0; i < gvItemDetails.Rows.Count; ++i)
            {
                String txtTotal5 = gvItemDetails.Rows[i].Cells[5].Text.ToString();
                String txtTotal6 = gvItemDetails.Rows[i].Cells[6].Text.ToString();
                String txtTotal7 = gvItemDetails.Rows[i].Cells[7].Text.ToString();
                String txtTotal8 = gvItemDetails.Rows[i].Cells[8].Text.ToString();
                String txtTotal11 = gvItemDetails.Rows[i].Cells[11].Text.ToString();
                String txtTotal12 = gvItemDetails.Rows[i].Cells[12].Text.ToString();
                String txtTotal13 = gvItemDetails.Rows[i].Cells[13].Text.ToString();
                String txtTotal14 = gvItemDetails.Rows[i].Cells[14].Text.ToString();

                GrossWt += Converter.GetDecimal(txtTotal5);
                StoneWt += Converter.GetDecimal(txtTotal6);
                NetWt += Converter.GetDecimal(txtTotal7);
                PureWt += Converter.GetDecimal(txtTotal8);
                MValue += Converter.GetDecimal(txtTotal11);
                SValue += Converter.GetDecimal(txtTotal12);
                MetalValue += Converter.GetDecimal(txtTotal13);
                TotalValue += Converter.GetDecimal(txtTotal14);

                lblGrossWt.Text = Converter.GetString(GrossWt);
                lblStoneWt.Text = Converter.GetString(StoneWt);
                lblNetWt.Text = Converter.GetString(NetWt);
                lblPureWt.Text = Converter.GetString(PureWt);
                lblMaking.Text = Converter.GetString(MValue);
                lblStoneValue.Text = Converter.GetString(SValue);
                lblMetalValue.Text = Converter.GetString(MetalValue);
                lblTotalValue.Text = Converter.GetString(TotalValue);
            }
        }

        protected void ddlKarat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlKarat.SelectedIndex != 0)
            {
                //string qry = "SELECT ItemGroupCode  FROM WFA2ZITEMGOLD WHERE ItemGroupCode = " + ddlCode.SelectedValue + " AND Karat = " + ddlKarat.SelectedValue + " AND UserId=" + lblID.Text + "";
                //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                //if (dt.Rows.Count > 0)
                //{
                //    ddlKarat.SelectedIndex = 0;
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Item Already Added');", true);
                //    return;
                //}

                LoadPurityDropdown(Converter.GetInteger(ddlKarat.SelectedValue));
                txtItemName.Focus();
            }
        }

        private void LoadPurityDropdown(int karat)
        {
            if (karat == 22)
            {
                string qry = "SELECT MakingRangeFrom,MakingRangeTo,Purity22 FROM A2ZITEMGROUP WHERE GroupCode = " + ddlCode.SelectedValue + "";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                if (dt.Rows.Count > 0)
                {
                    txtPurity.Text = Converter.GetString(dt.Rows[0]["Purity22"]);
                    string range = Converter.GetString(dt.Rows[0]["MakingRangeFrom"]) + " To " + Converter.GetString(dt.Rows[0]["MakingRangeTo"]);
                    txtItemName.Attributes.Add("placeholder", "" + range + "");
                }
            }
            if (karat == 21)
            {
                string qry = "SELECT MakingRangeFrom,MakingRangeTo,Purity21 FROM A2ZITEMGROUP WHERE GroupCode = " + ddlCode.SelectedValue + "";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                if (dt.Rows.Count > 0)
                {
                    txtPurity.Text = Converter.GetString(dt.Rows[0]["Purity21"]);
                    string range = Converter.GetString(dt.Rows[0]["MakingRangeFrom"]) + " To " + Converter.GetString(dt.Rows[0]["MakingRangeTo"]);
                    txtItemName.Attributes.Add("placeholder", "" + range + "");
                }
            }
            if (karat == 18)
            {
                string qry = "SELECT MakingRangeFrom,MakingRangeTo,Purity18 FROM A2ZITEMGROUP WHERE GroupCode = " + ddlCode.SelectedValue + "";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                if (dt.Rows.Count > 0)
                {
                    txtPurity.Text = Converter.GetString(dt.Rows[0]["Purity18"]);
                    string range = Converter.GetString(dt.Rows[0]["MakingRangeFrom"]) + " To " + Converter.GetString(dt.Rows[0]["MakingRangeTo"]);
                    txtItemName.Attributes.Add("placeholder", "" + range + "");
                }
            }
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
                    txtPartyAddress.Text = Converter.GetString(getDTO.PartyAddresssLine1) + " " + Converter.GetString(getDTO.PartyAddresssLine2) + " " + Converter.GetString(getDTO.PartyAddresssLine3);

                    ddlCurrency_SelectedIndexChanged(this, e);
                    CurrencyAndLocationSelection();
                }
            }
        }

        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializedInfo();
        }

        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            txtGrossWt.Focus();
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyName.SelectedIndex != 0)
            {
                string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + ddlPartyName.SelectedValue + "' AND AccCurrency = '" + ddlCurrency.SelectedValue + "' AND AccStatus = 1";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                int totrec = dt.Rows.Count;
                if (totrec > 0)
                {
                    lblPartyAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                    lblPartyAccno.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                    ImportAEDRate();
                }
            }
        }

        private void ImportAEDRate()
        {
            string qry = "SELECT ExchangeRate FROM A2ZCURRENCY WHERE CurrencyCode = " + ddlCurrency.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec > 0)
            {
                lblAEDRate.Text = Converter.GetString(dt.Rows[0]["ExchangeRate"]);
            }
        }

        protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        {
            int PartyCode = Converter.GetInteger(txtPartyCode.Text);
            A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

            if (getDTO.PartyName != string.Empty)
            {
                ddlPartyName.SelectedValue = Converter.GetString(getDTO.PartyCode);
                txtPartyAddress.Text = Converter.GetString(getDTO.PartyAddresssLine1) + " " + Converter.GetString(getDTO.PartyAddresssLine2) + " " + Converter.GetString(getDTO.PartyAddresssLine3);

                ddlCurrency_SelectedIndexChanged(this, e);
                CurrencyAndLocationSelection();
            }
            else
            {
                ddlPartyName.SelectedIndex = 0;
                txtPartyCode.Text = string.Empty;
                txtPartyCode.Focus();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            divPost.Visible = false;
            divMain.Attributes.CssStyle.Add("opacity", "300");

            txtTotalGross.Text = string.Empty;
            txtTotalNet.Text = string.Empty;
            txtMetal.Text = string.Empty;
            txtMaking.Text = string.Empty;
            txtStone.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtTotalValueView.Text = string.Empty;
            txtConvRate.Text = string.Empty;
            txtConvMetal.Text = string.Empty;
            txtConvMaking.Text = string.Empty;
            txtConvStone.Text = string.Empty;
            txtConvDiscount.Text = string.Empty;
            txtConvNetAmt.Text = string.Empty;
            ddlConvCurrency.SelectedIndex = 0;
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();
                getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
                CtrlVoucherNo.Text = "GPU" + getDTO.RecLastNo.ToString("000000");

                var prm = new object[19];

                prm[0] = ddlLocation.SelectedValue;
                prm[1] = ddlCurrency.SelectedValue;
                prm[2] = CtrlVoucherNo.Text;
                prm[3] = lblID.Text;
                prm[4] = txtPartyCode.Text;
                prm[5] = lblPartyAccType.Text;
                prm[6] = "82";
                prm[7] = "Unfixed Purchase";
                prm[8] = "1";
                prm[9] = "1";
                prm[10] = lblPureWt.Text;
                prm[11] = lblMaking.Text;
                prm[12] = lblStoneValue.Text;
                prm[13] = "0"; // carrying values
                prm[14] = txtConvNetAmt.Text;
                prm[15] = lblDescription.Text;
                prm[16] = ddlConvCurrency.SelectedValue;
                prm[17] = txtConvRate.Text;
                prm[18] = txtConvDiscount.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertItemGold]", prm, "A2ZACGMS"));

                if (result == 0)
                {
                    PrintVoucher();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void txtConvRate_TextChanged(object sender, EventArgs e)
        {
            ConvertCalculate(Converter.GetDecimal(txtConvRate.Text));
            txtConvDiscount.Text = "0.00";
            txtDiscount.Text = "0.00";
            CalculateConvertTotal();
            btnPost.Focus();
        }

        private void ConvertCalculate(Decimal ConvRate)
        {
            txtConvRate.Text = ConvRate.ToString();
            txtConvMetal.Text = (ConvRate * Converter.GetDecimal(lblMetalValue.Text)).ToString("0,0.00");
            txtConvMaking.Text = (ConvRate * Converter.GetDecimal(lblMaking.Text)).ToString("0,0.00");
            txtConvStone.Text = (ConvRate * Converter.GetDecimal(lblStoneValue.Text)).ToString("0,0.00");
            txtConvDiscount.Text = (ConvRate * Converter.GetDecimal(txtDiscount.Text)).ToString("0,0.00");
        }

        private void CalculateConvertTotal()
        {
            if (Converter.GetDecimal(txtConvRate.Text) != 1)
            {
                txtConvNetAmt.Text = (Converter.GetDecimal(txtConvMetal.Text) + Converter.GetDecimal(txtConvMaking.Text) + Converter.GetDecimal(txtConvStone.Text) - Converter.GetDecimal(txtConvDiscount.Text)).ToString("0,0.00");
            }
            else
            {
                txtConvNetAmt.Text = (Converter.GetDecimal(txtConvMetal.Text) + Converter.GetDecimal(txtConvMaking.Text) + Converter.GetDecimal(txtConvStone.Text) - Converter.GetDecimal(txtConvDiscount.Text)).ToString("0,0.00");
            }
        }

        protected void ddlConvCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlConvCurrency.SelectedValue == "2")
            {
                ConvertCalculate(1);
                txtConvRate.ReadOnly = true;
            }
            else
            {
                ConvertCalculate(Converter.GetDecimal(lblAEDRate.Text));
                txtConvRate.ReadOnly = false;
                txtConvRate.Focus();
            }

            txtConvDiscount.Text = "0.00";
            txtDiscount.Text = "0.00";
            CalculateConvertTotal();
        }

        protected void txtConvDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateConvertTotal();
        }
        private void CurrencyAndLocationSelection()
        {
            if (ddlPartyName.SelectedValue.Substring(0, 2) == "14" || txtPartyCode.Text.Substring(0, 2) == "14")
            {
                ddlLocation.SelectedValue = "2";
                ddlConvCurrency.Enabled = false;
            }
            else
            {
                ddlLocation.SelectedIndex = 0;
                ddlConvCurrency.Enabled = true;
            }
        }
    }
}
