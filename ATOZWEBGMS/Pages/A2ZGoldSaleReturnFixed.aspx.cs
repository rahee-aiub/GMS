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


namespace ATOZWEBGMS.Pages
{
    public partial class A2ZGoldSaleReturnFixed : System.Web.UI.Page
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
                txtMetalRate.Text = "149.719";
                CodeDropdown();
                PartyDropdown();
                CurrencyDropdown();
            }
        }

        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=51 and GroupCode !=21 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }

        private void StockDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 21 AND PartyCurrencyCode = " + ddlCurrency.SelectedValue + "";
            ddlStock = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlStock, "A2ZACGMS");
        }
        private void CodeDropdown()
        {
            string sqlquery = "SELECT GroupCode,GroupName FROM A2ZITEMGROUP";
            ddlCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCode, "A2ZACGMS");
        }

        private void CurrencyDropdown()
        {
            string sqlquery = "SELECT CurrencyCode,CurrencyName from A2ZCURRENCY WHERE CurrencyCode > 1";
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
            lblDescription.Text = "Gw:" + lblGrossWt.Text + ",St:" + lblStoneWt.Text + ",Nw:" + lblNetWt.Text + ",Pt:" + lblPureWt.Text + ",Sv:" + lblStoneValue.Text + ",Mc:" + lblMaking.Text;

            try
            {
                var prm = new object[14];
                prm[0] = CtrlVoucherNo.Text;
                prm[1] = lblID.Text;
                prm[2] = "0"; // PurchaseType Fixed Return
                prm[3] = "3"; // SaleType 
                prm[4] = lblPartyAccType.Text;
                prm[5] = "93";
                prm[6] = "Fixed Sale Return";
                prm[7] = "1"; //Trntype
                prm[8] = "0"; //TrnDrcr
                prm[9] = lblTotalValue.Text;
                prm[10] = lblPureWt.Text;
                prm[11] = lblPureWt.Text;
                prm[12] = lblDescription.Text;
                prm[13] = lblStockAccno.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertItemGoldReturn]", prm, "A2ZACGMS"));


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

        private void gvItemDetailsInfo()
        {
            string sqlquery = "(SELECT Id,ItemGroupName,Karat,ItemName,Purity,GrossWt,StoneWt,NetWt,PureWt,MakingRate,StoneMakingRate,MakingValue,StoneMakingValue,TotalMetalValue,TotalValue,PurchaseType FROM WFA2ZITEMGOLD WHERE UserId = " + lblID.Text + " AND VchNo = '" + CtrlVoucherNo.Text + "')";
            gvItemDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvItemDetails, "A2ZACGMS");
        }

        protected void BtnAddItem_Click(object sender, EventArgs e)
        {

            A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

            getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
            CtrlVoucherNo.Text = "FSR" + getDTO.RecLastNo.ToString("00000");

            try
            {
                var prm = new object[31];
                prm[0] = Converter.GetDateToYYYYMMDD(lblProcesDate.Text);
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = ddlCode.SelectedValue;
                prm[3] = ddlCode.SelectedItem.Text;
                prm[4] = ddlCurrency.SelectedValue;
                prm[5] = ddlKarat.SelectedValue;
                prm[6] = txtItemName.Text;
                prm[7] = txtPurity.Text;
                prm[8] = txtGrossWt.Text;
                prm[9] = txtStoneWt.Text;
                prm[10] = txtNetWt.Text;
                prm[11] = txtPureWt.Text;
                prm[12] = txtMakingRate.Text;
                prm[13] = txtStoneMakingRate.Text;
                prm[14] = txtMakingValue.Text;
                prm[15] = txtStoneMakingValue.Text;
                prm[16] = txtTotalMetalValue.Text; // txtTotalMetalValue.Text;
                prm[17] = txtTotalValue.Text;
                prm[18] = "0"; //Fixed Purchase Type
                prm[19] = "3"; //Sale Type
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



                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertWfItemGoldReturn]", prm, "A2ZACGMS"));


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

        }
        private void CalculateNetWtPureWt()
        {
            if (txtGrossWt.Text != string.Empty && txtStoneWt.Text != string.Empty)
            {
                decimal NetWt = Converter.GetDecimal(txtGrossWt.Text) - Converter.GetDecimal(txtStoneWt.Text);
                decimal PureWt = NetWt * Converter.GetDecimal(txtPurity.Text);

                txtNetWt.Text = NetWt.ToString("0.00");
                txtPureWt.Text = PureWt.ToString("0.00");
            }

        }

        private void CalculateMakingValue()
        {
            if (txtNetWt.Text != string.Empty && txtMakingRate.Text != string.Empty)
            {
                decimal MakingRate = Converter.GetDecimal(txtNetWt.Text) * Converter.GetDecimal(txtMakingRate.Text);
                txtMakingValue.Text = MakingRate.ToString("0.00");
            }

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
            if (txtMetalRate.Text != string.Empty)
            {
                MetalRate = Converter.GetDecimal(txtMetalRate.Text);
            }
            if (txtPureWt.Text != string.Empty)
            {
                PureWt = Converter.GetDecimal(txtPureWt.Text);
            }

            TotalMetalValue = MetalRate * PureWt;
            txtTotalMetalValue.Text = TotalMetalValue.ToString("0.00");

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
            txtTotalValue.Text = TotalValue.ToString("0.00");


        }

        protected void txtStoneWt_TextChanged(object sender, EventArgs e)
        {
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

            LoadPurityDropdown(Converter.GetInteger(ddlKarat.SelectedValue));
            txtItemName.Focus();
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
                }
                StockDropdown();
            }
        }

        protected void ddlStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStock.SelectedIndex != 0)
            {
                string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + ddlStock.SelectedValue + "' AND AccCurrency = '" + ddlCurrency.SelectedValue + "' AND AccStatus = 1";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                int totrec = dt.Rows.Count;
                if (totrec > 0)
                {
                    lblStockAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                    lblStockAccno.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                }

            }
        }


    }
}
