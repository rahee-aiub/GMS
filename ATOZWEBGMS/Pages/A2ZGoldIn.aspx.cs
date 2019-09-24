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
    public partial class A2ZGoldIn : System.Web.UI.Page
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

                CodeDropdown();
                PartyDropdown();
                TruncateWF();

                CalculateAvgRate();
                DivStockView.Visible = false;
                btnBack.Visible = false;

                ddlFromLocation.SelectedValue = "2";
                ddlToLocation.SelectedValue = "1";

                divPost.Visible = false;
            }
        }

        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZITEMGOLD WHERE UserId='" + lblID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZACGMS"));
        }
        private void CalculateAvgRate()
        {
            int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_GenerateTodaysMakingAvgRate]", "A2ZACGMS"));

            int result2 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_GenerateTodaysStoneMakingAvgRate]", "A2ZACGMS"));

            int result3 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_CalculateItemGoldBalance]", "A2ZACGMS"));
        }
        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 13 GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACGMS");
        }

        private void CodeDropdown()
        {
            string sqlquery = "SELECT GroupCode,CONCAT(GroupName,'  (',MakingRangeFrom, '  to ',MakingRangeTo,')') AS GroupName FROM A2ZITEMGROUP";
            ddlCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCode, "A2ZACGMS");
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


            txtConvCarringRate.Text = txtCarringRate.Text;
            txtConvCarringValue.Text = lblTotalCarring.Text;
            txtTotalGross.Text = lblGrossWt.Text;
            txtTotalNet.Text = lblNetWt.Text;
            txtMetal.Text = lblMetalValue.Text;
            txtMaking.Text = lblMaking.Text;
            txtStone.Text = lblStoneValue.Text;
            txtTotalValueView.Text = lblTotalValue.Text;
            txtInputConvCarringValue.Text = lblTotalCarring.Text;
            txtConvAdjAmount.Text = "0";

            divPost.Visible = true;

            divPost.Style.Add("Top", "170px");
            divPost.Style.Add("left", "350px");
            divPost.Style.Add("position", "fixed");

            DivMain.Attributes.CssStyle.Add("opacity", "0.5");

            divPost.Attributes.CssStyle.Add("opacity", "400");
            divPost.Attributes.CssStyle.Add("z-index", "400");

            txtInputConvCarringValue.Focus();
          
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


            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGmsGoldInInvoice");


            Response.Redirect("ReportServer.aspx", false);
        }
        private void gvItemDetailsInfo()
        {

            EmptyTotalInfo();
            string sqlquery = "(SELECT Id,ItemGroupName,Karat,ItemName,Purity,GrossWt,StoneWt,NetWt,PureWt,MakingRate,StoneMakingRate,MakingValue,StoneMakingValue,TotalMetalValue,TotalValue,CarringValue FROM WFA2ZITEMGOLD WHERE UserId = " + lblID.Text + ")";
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

        private void gvItemStockDetailsInfo()
        {
            string sqlquery = "SELECT Id,ItemGroupCode,ItemGroupName,Karat,Purity,GrossWt,StoneWt,NetWt,MakingRate,StoneMakingRate FROM WFA2ZITEMGOLDBAL Where Location = " + ddlFromLocation.SelectedValue + " AND GrossWt > 0";
            gvItemStockDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvItemStockDetails, "A2ZACGMS");
        }


        protected void BtnAddItem_Click(object sender, EventArgs e)
        {

            if (Converter.GetDecimal(txtTotalValue.Text) < 0 || txtTotalValue.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input all fields');", true);
                return;
            }

            if (ddlKarat.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Karat');", true);
                return;
            }
            try
            {
                var prm = new object[33];
                prm[0] = Converter.GetDateToYYYYMMDD(lblProcesDate.Text);

                prm[1] = ddlCode.SelectedValue;
                prm[2] = ddlCode.SelectedItem.Text.Substring(0, 4);
                prm[3] = "2"; // Currency 
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
                prm[17] = "11"; //Fixed Purchase Type
                prm[18] = "Gold In";

                prm[19] = "2"; //FixedUnfixed      
                prm[20] = ddlToLocation.SelectedValue;
                prm[21] = ddlToLocation.SelectedItem.Text;
                prm[22] = ddlFromLocation.SelectedValue;
                prm[23] = ddlFromLocation.SelectedItem.Text;

                prm[24] = ddlPartyName.SelectedValue;
                prm[25] = "0";
                prm[26] = "";
                prm[27] = Converter.GetDateToYYYYMMDD(lblProcesDate.Text);
                prm[28] = "0";
                prm[29] = txtCarringRate.Text;
                prm[30] = txtTotalCarring.Text;
                prm[31] = "0";
                prm[32] = lblID.Text;



                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertWfItemGoldIn]", prm, "A2ZACGMS"));


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
            // txtTotalMetalValue.Text = string.Empty;
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
            // txtTotalMetalValue.Text = string.Empty;
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

        private void CalculateTotalCarringValue()
        {
            decimal CarringRate = 0;
            decimal GrossWt = 0;
            decimal TotalCarringValue = 0;

            if (txtGrossWt.Text != string.Empty)
            {
                GrossWt = Converter.GetDecimal(txtGrossWt.Text);
                CarringRate = Converter.GetDecimal(txtCarringRate.Text);
            }

            TotalCarringValue = GrossWt * CarringRate;
            txtTotalCarring.Text = TotalCarringValue.ToString("0.00");


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
            CalculateTotalValue();
            CalculateTotalMetalValue();
            CalculateTotalCarringValue();

            CalculateMakingValue();
            CalculateStoneMakingValue();
            CalculateTotalValue();

            txtStoneWt.Focus();
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
            CalculateTotalValue();
            CalculateTotalMetalValue();

            CalculateMakingValue();
            CalculateStoneMakingValue();
            CalculateTotalValue();


        }




        private void ValidationWeight()
        {
            string qry = "SELECT GrossWt,StoneWt FROM WFA2ZITEMGOLDBAL WHERE Location = " + ddlFromLocation.SelectedValue + " AND ItemGroupCode = " + ddlCode.SelectedValue + " AND Karat = " + ddlKarat.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            if (dt.Rows.Count > 0)
            {
                lblBalGrossWt.Text = Converter.GetString(dt.Rows[0]["GrossWt"]);
                lblBalStoneWt.Text = Converter.GetString(dt.Rows[0]["StoneWt"]);
            }
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


        private void BalanceValidation()
        {

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
            Decimal CarringValue = 0;

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
                String txtTotal15 = gvItemDetails.Rows[i].Cells[15].Text.ToString();

                GrossWt += Converter.GetDecimal(txtTotal5);
                StoneWt += Converter.GetDecimal(txtTotal6);
                NetWt += Converter.GetDecimal(txtTotal7);
                PureWt += Converter.GetDecimal(txtTotal8);
                MValue += Converter.GetDecimal(txtTotal11);
                SValue += Converter.GetDecimal(txtTotal12);
                MetalValue += Converter.GetDecimal(txtTotal13);
                TotalValue += Converter.GetDecimal(txtTotal14);
                CarringValue += Converter.GetDecimal(txtTotal15);

                lblGrossWt.Text = Converter.GetString(GrossWt);
                lblStoneWt.Text = Converter.GetString(StoneWt);
                lblNetWt.Text = Converter.GetString(NetWt);
                lblPureWt.Text = Converter.GetString(PureWt);
                lblMaking.Text = Converter.GetString(MValue);
                lblStoneValue.Text = Converter.GetString(SValue);
                lblMetalValue.Text = Converter.GetString(MetalValue);
                lblTotalValue.Text = Converter.GetString(TotalValue);
                lblTotalCarring.Text = Converter.GetString(CarringValue);
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

                MakingAvgRate();
                StoneMakingAvgRate();

                LoadPurityDropdown(Converter.GetInteger(ddlKarat.SelectedValue));
                txtItemName.Focus();
            }
        }



        private void MakingAvgRate()
        {
            string qry = "SELECT RecCode,AvgRate FROM A2ZPURCHASE WHERE Location = " + ddlFromLocation.SelectedValue + " AND RecCode = 2  AND ItemGroupCode = " + ddlCode.SelectedValue + " AND Karat = " + ddlKarat.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec > 0)
            {
                txtMakingRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AvgRate"]));
            }
        }


        private void StoneMakingAvgRate()
        {
            string qry = "SELECT RecCode,AvgRate FROM A2ZPURCHASE WHERE Location = " + ddlFromLocation.SelectedValue + " AND RecCode = 3 AND ItemGroupCode = " + ddlCode.SelectedValue + " AND Karat = " + ddlKarat.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
            int totrec = dt.Rows.Count;
            if (totrec > 0)
            {
                txtStoneMakingRate.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AvgRate"]));
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

                    txtCarringRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.PartyCarringRate));


                    string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + txtPartyCode.Text + "' AND AccCurrency = 1 AND AccStatus = 1";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                    int totrec = dt.Rows.Count;
                    if (totrec > 0)
                    {
                        lblPartyAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                        lblPartyAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);

                    }


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



        protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        {
            int PartyCode = Converter.GetInteger(txtPartyCode.Text);
            A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

            if (getDTO.PartyName != string.Empty)
            {
                ddlPartyName.SelectedValue = Converter.GetString(getDTO.PartyCode);
                txtPartyAddress.Text = Converter.GetString(getDTO.PartyAddresssLine1) + " " + Converter.GetString(getDTO.PartyAddresssLine2) + " " + Converter.GetString(getDTO.PartyAddresssLine3);

                txtCarringRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.PartyCarringRate));


                string qry = "SELECT AccType,AccNo,AccCurrency FROM A2ZACCOUNT where AccPartyNo='" + txtPartyCode.Text + "' AND AccCurrency = 1 AND AccStatus = 1";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACGMS");
                int totrec = dt.Rows.Count;
                if (totrec > 0)
                {
                    lblPartyAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                    lblPartyAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                }
            }
            else
            {
                ddlPartyName.SelectedIndex = 0;
                txtPartyCode.Text = string.Empty;
                txtPartyCode.Focus();
            }
        }

        protected void ddlFromLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromLocation.SelectedValue == ddlToLocation.SelectedValue)
            {
                ddlFromLocation.SelectedIndex = 0;
            }
        }
        protected void ddlToLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromLocation.SelectedValue == ddlToLocation.SelectedValue)
            {
                ddlToLocation.SelectedIndex = 0;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            DivStockView.Style.Add("Top", "100px");
            DivStockView.Style.Add("left", "300px");
            DivStockView.Style.Add("position", "fixed");

            DivMain.Attributes.CssStyle.Add("opacity", "0.5");

            DivStockView.Attributes.CssStyle.Add("opacity", "400");
            DivStockView.Attributes.CssStyle.Add("z-index", "400");

            DivStockView.Visible = true;
            btnBack.Visible = true;

            lblStockLocation.Text = ddlFromLocation.SelectedItem.Text;

            BtnAddItem.Enabled = false;

            gvItemStockDetailsInfo();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            DivStockView.Visible = false;
            btnBack.Visible = false;
            BtnAddItem.Enabled = true;

            DivMain.Attributes.CssStyle.Add("opacity", "300");
        }

        protected void btnConvBack_Click(object sender, EventArgs e)
        {



            divPost.Visible = false;
            DivMain.Attributes.CssStyle.Add("opacity", "300");
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

            getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
            CtrlVoucherNo.Text = "UP" + getDTO.RecLastNo.ToString("000000");


            lblDescription.Text = "Gw:" + lblGrossWt.Text + ",St:" + lblStoneWt.Text + ",Nw:" + lblNetWt.Text + ",Pt:" + lblPureWt.Text + ",Sv:" + lblStoneValue.Text + ",Mc:" + lblMaking.Text;

            try
            {


                var prm = new object[21];

                prm[0] = ddlFromLocation.SelectedValue;
                prm[1] = ddlFromLocation.SelectedItem.Text;
                prm[2] = ddlToLocation.SelectedValue;
                prm[3] = ddlToLocation.SelectedItem.Text;
                prm[4] = "2"; // Currency 
                prm[5] = CtrlVoucherNo.Text;
                prm[6] = lblID.Text;
                prm[7] = txtPartyCode.Text;
                prm[8] = lblPartyAccType.Text;
                prm[9] = lblPartyAccNo.Text;
                prm[10] = "85";
                prm[11] = "Gold In";
                prm[12] = "1";
                prm[13] = "1";
                prm[14] = lblPureWt.Text;
                prm[15] = lblMaking.Text;
                prm[16] = lblStoneValue.Text;
                prm[17] = txtInputConvCarringValue.Text; // carrying values
                prm[18] = lblTotalValue.Text;
                prm[19] = lblDescription.Text;
                prm[20] = txtConvAdjAmount.Text;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertItemGoldIn]", prm, "A2ZACGMS"));


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

        protected void txtInputConvCarringValue_TextChanged(object sender, EventArgs e)
        {
            Decimal ConvCarringValue = Converter.GetDecimal(txtConvCarringValue.Text);
            Decimal InputConvCarringValue = Converter.GetDecimal(txtInputConvCarringValue.Text);

            txtConvAdjAmount.Text = Converter.GetString(ConvCarringValue - InputConvCarringValue);
        }





    }
}
