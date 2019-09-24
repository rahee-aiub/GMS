using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZACGUARDTO
    {
        public Int32 Id { set; get; }
        public int BranchNo { set; get; }
        public int LoanApplicationNo { set; get; }
        public Int64 LoanAccNo { set; get; }
        public Int16 MemType { set; get; }
        public int MemNo { set; get; }
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public decimal AccAmount { set; get; }        
        public int AccStatus { set; get; }
        public DateTime AccStatDate { set; get; }
        public decimal AccSuretyAmt { set; get; }
        public DateTime AccSuretyReleaseDt { set; get; }
        public string AccSuretyVchNo {set;get;}
        public int ReleaseStatus { set; get; }
        public string AccTypeDescription { set; get; }

    }
}
