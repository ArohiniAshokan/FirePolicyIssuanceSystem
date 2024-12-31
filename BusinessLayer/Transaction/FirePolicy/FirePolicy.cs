using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class FirePolicy
    {
        public int PolUid { get; set; }
        public string PolNo { get; set; }
        public DateTime? PolIssDt { get; set; }
        public DateTime? PolFmDt { get; set; }
        public DateTime? PolToDt { get; set; }
        public string PolProdCode { get; set; }
        public string PolAssrName { get; set; }
        public string PolAssrAddress { get; set; }
        public string PolAssrMobile { get; set; }
        public string PolAssrEmail { get; set; }
        public DateTime? PolAssrDob { get; set; }
        public string PolAssrOccupation { get; set; }
        public string PolAssrType { get; set; }
        public string PolAssrCivilId { get; set; }
        public string PolMultRiskYn { get; set; }
        public string PolSiCurrency { get; set; }
        public double PolSiCurrRate { get; set; }
        public double PolFcSi { get; set; }
        public double PolLcSi { get; set; }
        public string PolPremCurrency { get; set; }
        public double PolPremCurrRate { get; set; }
        public double PolGrossFcPrem { get; set; }
        public double PolGrossLcPrem { get; set; }
        public double PolVatFcAmt { get; set; }
        public double PolVatLcAmt { get; set; }
        public double PolNetFcPrem { get; set; }
        public double PolNetLcPrem { get; set; }
        public string PolApprStatus { get; set; }
        public DateTime? PolApprDt { get; set; }
        public string PolApprBy { get; set; }
        public string PolCrBy { get; set; }
        public DateTime? PolCrDt { get; set; }
        public string PolUpBy { get; set; }
        public DateTime? PolUpDt { get; set; }

    }
}
