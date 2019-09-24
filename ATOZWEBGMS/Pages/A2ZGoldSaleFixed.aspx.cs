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
    public partial class A2ZGoldSaleFixed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                divPost.Visible = false;

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                lblProcesDate.Text = date;
                txtDueDate.Text = date;

                ddlLocation.SelectedValue = "1";
                ddlLocation.Enabled = false;

                CodeDropdown();
                PartyDropdown();
                CurrencyDropdown();
                ConvertCurrencyDropdown();

                ddlCurrency.SelectedIndex = 1;
                TruncateWF();
                CalculateAvgRate();

                divLastSaleInfo.Visible = false;
                ddlCode.Enabled = false;
                ddlKarat.Enabled = false;

                DivStockView.Visible = false;

                ddlLocation.SelectedValue = "1";

            }
        }


        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZITEMGOLD WHERE UserId='" + lblID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZACGMS"));
        }

        private void CalculateAvgRate()
        {
            int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_GenerateTodaysMetalAvgRate]", "A2ZACGMS"));

            int result2 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_GenerateTodaysMakingAvgRate]", "A2ZACGMS"));

            int result3 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_GenerateTodaysStoneMakingAvgRate]", "A2ZACGMS"));

            int result4 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_GenerateTodaysCarryingAvgRate]", "A2ZACGMS"));

            int result5 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_CalculateItemGoldBalance]", "A2ZACGMS"));
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

        private void ConvertCurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode != 99";
            ddlConvCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlConvCurrency, "A2ZACGMS");
        }
        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode > 1 AND CurrencyCode != 99";
            ddlCurrency = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCurrency, "A2ZACGMS");
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
            txtCarryingValue.Text = lblCarryingValue.Text;
            txtDiscount.Text = "0,0.00";

            ddlConvCurrency.SelectedValue = ddlCurrency.SelectedValue;
            ConvertCalculate(1);
            CalculateConvertTotal();

            if (ddlCurrency.SelectedValue == "2")
            {
                txtFinalCarringRate.ReadOnly = true;
                txtConvCarrying.ReadOnly = true;

                txtFinalCarringRate.Text = "0.00";
                txtConvCarrying.Text = "0.00";

            }
            txtConvRate.ReadOnly = true;
            divPost.Visible = true;


            divPost.Style.Add("Top", "170px");
            divPost.Style.Add("left", "350px");
            divPost.Style.Add("position", "fixed");

            divMain.Attributes.CssStyle.Add("opacity", "0.5");

            divPost.Attributes.CssStyle.Add("opacity", "400");
            divPost.Attributes.CssStyle.Add("z-index", "400");
        }

        private void gvItemDetailsInfo()
        {
            EmptyTotalInfo();


            string sqlquery = "(SELECT Id,ItemGroupName,Karat,ItemName,Purity,GrossWt,StoneWt,NetWt,PureWt,MakingRate,StoneMakingRate,CarringRate,MakingValue,StoneMakingValue,TotalMetalValue,CarringValue,TotalValue,StockPureWt,StockMakingValue,StockStoneMakingValue,StockCarringValue FROM WFA2ZITEMGOLD WHERE UserId = " + lblID.Text + ")";
            gvItemDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvItemDetails, "A2ZACGMS");
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
            txtTotalMetalValue.Text = string.Empty;
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
            txtTotalMetalValue.Text = string.Empty;
            txtTotalValue.Text = string.Empty;
            txtCarryingRate.Text = string.Empty;
            txtCarryingValue.Text = string.Empty;
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
            lblCarryingValue.Text = string.Empty;
            lblTotalValue.Text = string.Empty;
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

                txtNetWt.Text = NetWt.ToString("0,0.00");
                txtPureWt.Text = PureWt.ToString("0,0.00");
           
        }

        private void CalculateMakingValue()
        {
            
                decimal MakingRate = Converter.GetDecimal(txtNetWt.Text) * Converter.GetDecimal(txtMakingRate.Text);
                txtMakingValue.Text = MakingRate.ToString("0,0.00");
           
        }

        private void CalculateStoneMakingValue()
        {
            if (txtStoneWt.Text != string.Empty && txtStoneMakingRate.Text != string.Empty)
            {
                decimal StoneValue = Converter.GetDecimal(txtStoneWt.Text) * Converter.GetDecimal(txtStoneMakingRate.Text);
                txtStoneMakingValue.Text = StoneValue.ToString("0,0.00");
            }
        }

        private void CalculateTotalMetalValue()
        {
            decimal MetalRate = 0;
            decimal PureWt = 0;
            decimal TotalMetalValue = 0;
            if (txtMetalRate.Text != string.Empty)
            {
                MetalRate = Converter.GetDecimal(txtMetalRate.Text);
            }
            if (txtPureWt.Text != string.Empty)
            {
                PureWt = Converter.GetDecimal(txtPureWt.Text);
            }

            TotalMetalValue = MetalRate * PureWt;
            txtTotalMetalValue.Text = TotalMetalValue.ToString("0,0.00");
        }

        private void CalculateCarryingValue()
        {
            if (txtCarryingRate.Text != string.Empty && txtGrossWt.Text != string.Empty)
            {
                decimal GrossWt = Converter.GetDecimal(txtGrossWt.Text);
                decimal CarryingRate = Converter.GetDecimal(txtCarryingRate.Text);
                decimal CarryingValue = GrossWt * CarryingRate;

                txtCarryingValue.Text = CarryingValue.ToString("0,0.00");
            }
        }

        private void CalculateTotalValue()
        {
            decimal MetalValue = 0;
            decimal MakingValue = 0;
            decimal StoneMakingValue = 0;
            if (txtTotalMetalValue.Text != string.Empty)
            {
                MetalValue = Converter.GetDecimal(txtTotalMetalValue.Text);
            }
            if (txtMakingValue.Text != string.Empty)
            {
                MakingValue = Converter.GetDecimal(txtMakingValue.Text);
            }
            if (txtStoneMakingValue.Text != string.Empty)
            {
                StoneMakingValue = Converter.GetDecimal(txtStoneMakingValue.Text);
            }

            decimal TotalValue = MetalValue + StoneMakingValue + MakingValue;
            txtTotalValue.Text = TotalValue.ToString("0,0.00");
        }

        private void CalculateStockValue()
        {
            lblStockPureWt.Text = (Converter.GetDecimal(txtNetWt.Text) * Converter.GetDecimal(lblStockPurity.Text)).ToString("0,0.00");
            lblStockMakingValue.Text = (Converter.GetDecimal(txtNetWt.Text) * Converter.GetDecimal(lblStockMakingRate.Text)).ToString("0,0.00");
            lblStockStoneMakingValue.Text = (Converter.GetDecimal(txtStoneWt.Text) * Converter.GetDecimal(lblStockStoneMakingRate.Text)).ToString("0,0.00");
            lblStockCarringValue.Text = (Converter.GetDecimal(txtGrossWt.Text) * Converter.GetDecimal(lblStockCarringRate.Text)).ToString("0,0.00"); ;
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


            ValidationWeight();

            if (Converter.GetDecimal(txtStoneWt.Text) > Converter.GetDecimal(lblBalStoneWt.Text))
            {
                txtStoneWt.Text = string.Empty;
                txtStoneWt.Focus();
                return;
            }

            CalculateNetWtPureWt();
            CalculateTotalMetalValue();
            CalculateMakingValue();
            CalculateCarryingValue();
            CalculateStoneMakingValue();
            txtMakingRate.Focus();
            CalculateStockValue();
            CalculateTotalValue();
        }

        protected void txtGrossWt_TextChanged(object sender, EventArgs e)
        {
            ValidationWeight();

            if (Converter.GetDecimal(txtGrossWt.Text) > Converter.GetDecimal(lblBalGrossWt.Text))
            {
                txtGrossWt.Text = string.Empty;
                txtGrossWt.Focus();
                return;
            }

            CalculateNetWtPureWt();
            CalculateTotalMetalValue();
            CalculateMakingValue();
            CalculateCarryingValue();
            CalculateStoneMakingValue();
            txtStoneWt.Focus();
            CalculateStockValue();
            CalculateTotalValue();

            txtRateUSD.Enabled = false;
        }

        private void ValidationWeight()
        {
            string qry = "SELECT GrossWt,StoneWt FROM WFA2ZITEMGOLDBAL WHERE Location = " + ddlLocation.SelectedValue + " AND ItemGroupCode = " + ddlCode.SelectedValue + " AND Karat = " + ddlKarat.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            if (dt.Rows.Count > 0)
            {
                lblBalGrossWt.Text = Converter.GetString(dt.Rows[0]["GrossWt"]);
                lblBalStoneWt.Text = Converter.GetString(dt.Rows[0]["StoneWt"]);
            }
        }
        protected void txtMakingRate_TextChanged(object sender, EventArgs e)
        {
            Decimal StockMakingRate = Converter.GetDecimal(lblStockMakingRate.Text);
            Decimal InputMakingRate = Converter.GetDecimal(txtMakingRate.Text);
            if (StockMakingRate > InputMakingRate)
            {
                txtMakingRate.Text = lblStockMakingRate.Text;
                txtMakingRate.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Making Rate');", true);
                return;
            }
            else
            {
                CalculateMakingValue();
                CalculateTotalValue();
                txtStoneMakingRate.Focus();
            }
        }

        protected void txtStoneMaking_TextChanged(object sender, EventArgs e)
        {
            Decimal StockStoneMakingRate = Converter.GetDecimal(lblStockStoneMakingRate.Text);
            Decimal InputStoneMakingRate = Converter.GetDecimal(txtStoneMakingRate.Text);
            if (StockStoneMakingRate > InputStoneMakingRate)
            {
                txtStoneMakingRate.Text = lblStockStoneMakingRate.Text;
                txtStoneMakingRate.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Stone Making Rate');", true);
                return;
            }
            else
            {
                CalculateStoneMakingValue();
                CalculateTotalValue();
                txtCarryingRate.Focus();
            }
        }

        protected void txtCarryingRate_TextChanged(object sender, EventArgs e)
        {
            Decimal StockCarryingRate = Converter.GetDecimal(lblStockCarringRate.Text);
            Decimal InputCarryingRate = Converter.GetDecimal(txtCarryingRate.Text);
            if (StockCarryingRate > InputCarryingRate)
            {
                txtCarryingRate.Text = lblStockCarringRate.Text;
                txtCarryingRate.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Carrying Rate');", true);
                return;
            }
            else
            {
                CalculateCarryingValue();
            }
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
            Decimal Carrying = 0;

            for (int i = 0; i < gvItemDetails.Rows.Count; ++i)
            {
                String Gw = gvItemDetails.Rows[i].Cells[5].Text.ToString();
                String Sw = gvItemDetails.Rows[i].Cells[6].Text.ToString();
                String Nw = gvItemDetails.Rows[i].Cells[7].Text.ToString();
                String Pw = gvItemDetails.Rows[i].Cells[8].Text.ToString();
                String Mv = gvItemDetails.Rows[i].Cells[12].Text.ToString();
                String Sv = gvItemDetails.Rows[i].Cells[13].Text.ToString();
                String TMv = gvItemDetails.Rows[i].Cells[14].Text.ToString();
                String Cv = gvItemDetails.Rows[i].Cells[15].Text.ToString();
                String Tv = gvItemDetails.Rows[i].Cells[16].Text.ToString();

                GrossWt += Converter.GetDecimal(Gw);
                StoneWt += Converter.GetDecimal(Sw);
                NetWt += Converter.GetDecimal(Nw);
                PureWt += Converter.GetDecimal(Pw);
                MValue += Converter.GetDecimal(Mv);
                SValue += Converter.GetDecimal(Sv);
                MetalValue += Converter.GetDecimal(TMv);
                Carrying += Converter.GetDecimal(Cv);
                TotalValue += Converter.GetDecimal(Tv);

                lblGrossWt.Text = Converter.GetString(GrossWt.ToString(("0,0.00")));
                lblStoneWt.Text = Converter.GetString(StoneWt.ToString(("0,0.00")));
                lblNetWt.Text = Converter.GetString(NetWt.ToString(("0,0.00")));
                lblPureWt.Text = Converter.GetString(PureWt.ToString(("0,0.00")));
                lblMaking.Text = Converter.GetString(MValue.ToString(("0,0.00")));
                lblStoneValue.Text = Converter.GetString(SValue.ToString(("0,0.00")));
                lblMetalValue.Text = Converter.GetString(MetalValue.ToString(("0,0.00")));
                lblCarryingValue.Text = Converter.GetString(Carrying.ToString(("0,0.00")));
                lblTotalValue.Text = Converter.GetString(TotalValue.ToString(("0,0.00")));
            }
        }

        protected void ddlKarat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string qry = "SELECT ItemGroupCode  FROM WFA2ZITEMGOLD WHERE ItemGroupCode = " + ddlCode.SelectedValue + " AND Karat = " + ddlKarat.SelectedValue + " AND UserId=" + lblID.Text + "";
            //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            //if (dt.Rows.Count > 0)
            //{
            //    ddlKarat.SelectedIndex = 0;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Item Already Added');", true);
            //    return;
            //}



            MakingAvgRate();
            StoneMakingAvgRate();
            CarryingAvgRate();


            LoadPurityDropdown(Converter.GetInteger(ddlKarat.SelectedValue));
            LastTransactionInfo();
            txtItemName.Focus();
        }


        private void MakingAvgRate()
        {
            string qry = "SELECT RecCode,AvgRate FROM A2ZPURCHASE WHERE Location = " + ddlLocation.SelectedValue + " AND RecCode = 2  AND ItemGroupCode = " + ddlCode.SelectedValue + " AND Karat = " + ddlKarat.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec > 0)
            {
                lblStockMakingRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AvgRate"]));
                txtMakingRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AvgRate"]));
            }
        }


        private void StoneMakingAvgRate()
        {
            string qry = "SELECT RecCode,AvgRate FROM A2ZPURCHASE WHERE Location = " + ddlLocation.SelectedValue + " AND RecCode = 3 AND ItemGroupCode = " + ddlCode.SelectedValue + " AND Karat = " + ddlKarat.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec > 0)
            {
                lblStockStoneMakingRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AvgRate"]));
                txtStoneMakingRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AvgRate"]));
            }
        }

        private void CarryingAvgRate()
        {
            string qry = "SELECT RecCode,AvgRate FROM A2ZPURCHASE WHERE RecCode = 4";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec > 0)
            {
                lblStockCarringRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AvgRate"]));
                txtCarryingRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AvgRate"]));
                txtFinalCarringRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AvgRate"]));
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

            lblStockPurity.Text = txtPurity.Text;
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

                }
            }
        }

        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializedInfo();

        }


        

        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            txtPurity.Focus();
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrency.SelectedIndex != 0)
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

        protected void txtPurity_TextChanged(object sender, EventArgs e)
        {

            Decimal StockPurity = Converter.GetDecimal(lblStockPurity.Text);
            Decimal InputPurity = Converter.GetDecimal(txtPurity.Text);
            if (StockPurity > InputPurity || InputPurity > 1)
            {
                txtPurity.Text = lblStockPurity.Text;
                txtPurity.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Purity');", true);
                return;
            }
            else
            {
                txtGrossWt.Focus();
            }
        }

        protected void BtnAddItem_Click(object sender, EventArgs e)
        {
            if (ddlCurrency.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Currency Code');", true);
                return;
            }

            if (ddlKarat.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Karat');", true);
                return;
            }


            if (txtItemName.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Item Name');", true);
                return;
            }


            if (Converter.GetDecimal(txtTotalValue.Text) < 0 || txtTotalValue.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input all fields');", true);
                return;
            }

            //---------------
         
            //-----------------
            try
            {
                txtPartyCode.ReadOnly = true;
                ddlPartyName.Enabled = false;

                var prm = new object[39];
                prm[0] = Converter.GetDateToYYYYMMDD(lblProcesDate.Text);
                prm[1] = ddlCode.SelectedValue;
                prm[2] = ddlCode.SelectedItem.Text.Substring(0, 4);
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
                prm[15] = txtTotalMetalValue.Text; // txtTotalMetalValue.Text;
                prm[16] = txtTotalValue.Text;
                prm[17] = "21"; //Fixed Purchase Type
                prm[18] = "Fixed Sale"; //Sale Type
                prm[19] = "1"; //FixedUnfixed
                prm[20] = ddlLocation.SelectedValue;
                prm[21] = ddlLocation.SelectedItem.Text;
                prm[22] = ddlPartyName.SelectedValue;
                prm[23] = lblPartyAccno.Text;
                prm[24] = txtRefName.Text;
                prm[25] = Converter.GetDateToYYYYMMDD(txtDueDate.Text);
                prm[26] = txtMetalRate.Text;
                prm[27] = txtCarryingRate.Text; //Carrying Rate
                prm[28] = txtCarryingValue.Text; //Carrying Value
                prm[29] = txtRateUSD.Text;
                prm[30] = lblID.Text;
                prm[31] = lblStockPurity.Text;
                prm[32] = lblStockPureWt.Text;
                prm[33] = lblStockMakingRate.Text;
                prm[34] = lblStockMakingValue.Text;
                prm[35] = lblStockStoneMakingRate.Text;
                prm[36] = lblStockStoneMakingValue.Text;
                prm[37] = lblStockCarringRate.Text;
                prm[38] = lblStockCarringValue.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertWfSaleItemGold]", prm, "A2ZACGMS"));

                if (result == 0)
                {
                    ClearInfo();
                    gvItemDetailsInfo();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

            getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
            CtrlVoucherNo.Text = "GSF" + getDTO.RecLastNo.ToString("000000");

            lblDescription.Text = "Gw:" + lblGrossWt.Text + ",St:" + lblStoneWt.Text + ",Nw:" + lblNetWt.Text + ",Pt:" + lblPureWt.Text + ",Sv:" + lblStoneValue.Text + ",Mc:" + lblMaking.Text;

            try
            {
                var prm = new object[17];

                prm[0] = ddlLocation.SelectedValue;
                prm[1] = ddlCurrency.SelectedValue;
                prm[2] = CtrlVoucherNo.Text;
                prm[3] = lblID.Text;
                prm[4] = txtPartyCode.Text;
                prm[5] = lblPartyAccType.Text;
                prm[6] = "91";
                prm[7] = "Fixed Sale";
                prm[8] = "1";
                prm[9] = "1";
                prm[10] = lblPureWt.Text;
                prm[11] = txtConvNetAmt.Text;
                prm[12] = lblDescription.Text;
                prm[13] = ddlConvCurrency.SelectedValue;
                prm[14] = txtConvRate.Text;

                prm[15] = txtFinalCarringRate.Text;
                prm[16] = txtConvDiscount.Text;
               

                
                    
                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertSaleItemGold]", prm, "A2ZACGMS"));

                if (result == 0)
                {
                    PrintVoucher();

                    //Response.Redirect(Request.RawUrl);
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


            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsFixedSaleInvoice");


            Response.Redirect("ReportServer.aspx", false);
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
            txtConvCarrying.Text = string.Empty;
            txtConvDiscount.Text = string.Empty;
            txtConvNetAmt.Text = string.Empty;
            ddlConvCurrency.SelectedIndex = 0;
            txtFinalCarringRate.Text = string.Empty;
        }



        protected void txtConvDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateConvertTotal();
        }


        private void LastTransactionInfo()
        {
            if (ddlKarat.SelectedIndex != 0 && ddlCode.SelectedIndex != 0)
            {
                string qry = "SELECT TOP 1 ItemName,Purity,MakingRate,StoneMakingRate,CarringRate,MetalRate FROM A2ZITEMGOLD WHERE RecordType = 21 AND ItemGroupCode = " + ddlCode.SelectedValue + " AND Karat = " + ddlKarat.SelectedValue + " ORDER BY Id DESC";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                int totrec = dt.Rows.Count;
                if (totrec > 0)
                {
                    divLastSaleInfo.Visible = true;

                    lblLastSaleItemName.Text = Converter.GetString(dt.Rows[0]["ItemName"]);
                    lblLastSalePurity.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["Purity"]));
                    lblLastSaleMakingRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["MakingRate"]));
                    lblLastSaleStoneMakingRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["StoneMakingRate"]));
                    lblLastSaleCarringRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["CarringRate"]));
                    lblLastSaleMetalRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["MetalRate"]));
                }
                else
                {
                    divLastSaleInfo.Visible = false;

                    lblLastSaleItemName.Text = string.Empty;
                    lblLastSalePurity.Text = string.Empty;
                    lblLastSaleMakingRate.Text = string.Empty;
                    lblLastSaleStoneMakingRate.Text = string.Empty;
                    lblLastSaleCarringRate.Text = string.Empty;
                    lblLastSaleMetalRate.Text = string.Empty;
                }
            }
        }

        protected void txtRateUSD_TextChanged(object sender, EventArgs e)
        {
            if (txtRateUSD.Text != string.Empty)
            {
                Double x = Converter.GetDouble(txtRateUSD.Text) * (13.7777);
                Double y = 116.64;
                Double result = (x / y);
                result = Math.Round(result, 3);
                txtMetalRate.Text = result.ToString();
                ddlCode.Enabled = true;
                ddlKarat.Enabled = true;
            }
        }

        protected void txtConvRate_TextChanged(object sender, EventArgs e)
        {

            ConvertCalculate(Converter.GetDecimal(txtConvRate.Text));
            txtConvDiscount.Text = "0.00";
            txtDiscount.Text = "0.00";
            CalculateConvertTotal();
            txtFinalCarringRate.Focus();
        }

        protected void ddlConvCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlConvCurrency.SelectedValue == "2")
            {
                ConvertCalculate(1);
                txtConvRate.ReadOnly = true;
                txtFinalCarringRate.Focus();
                txtFinalCarringRate.Text = "0.00";
                txtConvCarrying.Text = "0.00";
                txtFinalCarringRate.ReadOnly = true;
                txtConvCarrying.ReadOnly = true;
              
            }
            else
            {
                ConvertCalculate(Converter.GetDecimal(lblAEDRate.Text));

                if (Converter.GetDecimal(txtConvRate.Text) != 1)
                {
                    txtConvCarrying.Text = (Converter.GetDecimal(lblCarryingValue.Text)).ToString("0,0.00");
                }

                txtConvRate.ReadOnly = false;
                txtConvRate.Focus();

                txtFinalCarringRate.ReadOnly = false;
                txtConvCarrying.ReadOnly = false;
                txtFinalCarringRate.Text = lblStockCarringRate.Text;
            }

            txtConvDiscount.Text = "0.00";
            txtDiscount.Text = "0.00";
            CalculateConvertTotal();

        }

        private void CalculateConvertTotal()
        {
            if (Converter.GetDecimal(txtConvRate.Text) != 1)
            {
                txtConvNetAmt.Text = (Converter.GetDecimal(txtConvMetal.Text) + Converter.GetDecimal(txtConvMaking.Text) + Converter.GetDecimal(txtConvStone.Text) + Converter.GetDecimal(txtConvCarrying.Text) - Converter.GetDecimal(txtConvDiscount.Text)).ToString("0,0.00");
            }
            else
            {    
                txtConvNetAmt.Text = (Converter.GetDecimal(txtConvMetal.Text) + Converter.GetDecimal(txtConvMaking.Text) + Converter.GetDecimal(txtConvStone.Text) - Converter.GetDecimal(txtConvDiscount.Text)).ToString("0,0.00");
            }
        }

        private void ConvertCalculate(Decimal ConvRate)
        {
            txtConvRate.Text = ConvRate.ToString();
            txtConvMetal.Text = (ConvRate * Converter.GetDecimal(lblMetalValue.Text)).ToString("0,0.00");
            txtConvMaking.Text = (ConvRate * Converter.GetDecimal(lblMaking.Text)).ToString("0,0.00");
            txtConvStone.Text = (ConvRate * Converter.GetDecimal(lblStoneValue.Text)).ToString("0,0.00");
            txtConvDiscount.Text = (ConvRate * Converter.GetDecimal(txtDiscount.Text)).ToString("0,0.00");
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

            }
            else
            {
                ddlPartyName.SelectedIndex = 0;
                txtPartyCode.Text = string.Empty;
                txtPartyCode.Focus();
            }
        }

        protected void txtFinalCarringRate_TextChanged(object sender, EventArgs e)
        {
            if (txtFinalCarringRate.Text != string.Empty)
            {
                txtConvCarrying.Text = (Converter.GetDecimal(txtFinalCarringRate.Text) * Converter.GetDecimal(txtTotalGross.Text)).ToString("0.00");
                CalculateConvertTotal();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            DivStockView.Style.Add("Top", "100px");
            DivStockView.Style.Add("left", "300px");
            DivStockView.Style.Add("position", "fixed");

            divMain.Attributes.CssStyle.Add("opacity", "0.5");

            DivStockView.Attributes.CssStyle.Add("opacity", "400");
            DivStockView.Attributes.CssStyle.Add("z-index", "400");

            DivStockView.Visible = true;
            btnBack.Visible = true;

            lblStockLocation.Text = ddlLocation.SelectedItem.Text;

            BtnAddItem.Enabled = false;

            gvItemStockDetailsInfo();
        }

        private void gvItemStockDetailsInfo()
        {
            string sqlquery = "SELECT Id,ItemGroupCode,ItemGroupName,Karat,Purity,GrossWt,StoneWt,NetWt,MakingRate,StoneMakingRate FROM WFA2ZITEMGOLDBAL Where Location = " + ddlLocation.SelectedValue + " AND GrossWt > 0";
            gvItemStockDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvItemStockDetails, "A2ZACGMS");
        }

        protected void btnBackStock_Click(object sender, EventArgs e)
        {
            DivStockView.Visible = false;
            //btnBackStock.Visible = false;
            BtnAddItem.Enabled = true;

            divMain.Attributes.CssStyle.Add("opacity", "300");
        }

        protected void btnViewEstimate_Click(object sender, EventArgs e)
        {
            try
            {

                var prm = new object[3];

                prm[0] = txtConvRate.Text;
                prm[1] = txtFinalCarringRate.Text;
                prm[2] = lblID.Text;



                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_GenerateFixedSaleEstimate]", prm, "A2ZACGMS"));

                if (result == 0)
                {



                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, txtConvDiscount.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.USERNO, lblID.Text);


                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACGMS");


                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsFixedSaleEstimate");

                    
                    Response.Redirect("ReportServer.aspx", false);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
