using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TariffMaster
    {
        public int TmUid { get; set; }
        public string TmRiskClassFm { get; set; }
        public string TmRiskClassTo { get; set; }
        public string TmOccFm { get; set; }
        public string TmOccTo { get; set; }
        public double TmSiFm { get; set; }
        public double TmSiTo { get; set; }
        public double TmRiskRate { get; set; }

        public string TmCrBy { get; set; }
        public DateTime TmCrDt { get; set; }
        public string TmUpBy { get; set; }
        public DateTime TmUpDt { get; set; }
    }
}
