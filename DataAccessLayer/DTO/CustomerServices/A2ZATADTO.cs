using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZATADTO
    {
        public Int32 Id { set; get; }
        public DateTime TrnDate { set; get; }
        public int BranchNo { set; get; }
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public Int16 MemType { set; get; }
        public int MemNo { set; get; }
        public Int16 TASrlNo { set; get; }
        public DateTime TAFromDate { set; get; }
        public DateTime TATillDate { set; get; }
        public Int16 TANoOfMonth { set; get; }
        public Int16 TANoOfInstallment { set; get; }
        public decimal TAInstallAmt { set; get; }
        public decimal TALastInstallAmt { set; get; }
        public decimal TAIntRate { set; get; }
        public Int16 TAIntFlag { set; get; }
        public Int16 UserId { set; get; }
    }
}
