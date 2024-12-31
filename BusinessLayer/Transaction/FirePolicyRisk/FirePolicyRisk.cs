using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class FirePolicyRisk
    {
        public int RiskUid { get; set; }
        public int RiskId { get; set; }
        public int RiskPolUid { get; set; }
        public string RiskClass { get; set; }
        public string RiskOccupType { get; set; }
        public string RiskConstrType { get; set; }
        public string RiskLocation { get; set; }
        public string RiskDesc { get; set; }
        public double RiskFcSi { get; set; }
        public double RiskLcSi { get; set; }
        public double RiskPremRate { get; set; }
        public double RiskFcPrem { get; set; }
        public double RiskLcPrem { get; set; }
        public string RiskCrBy { get; set; }
        public DateTime? RiskCrDt { get; set; }
        public string RiskUpBy { get; set; }
        public DateTime? RiskUpDt { get; set; }
    }
}
