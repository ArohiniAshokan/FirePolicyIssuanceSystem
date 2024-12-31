using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CodesMaster
    {
        public string CmCode { get; set; }
        public string CmType { get; set; }
        public string CmDesc { get; set; }
        public string CmValue { get; set; }
        public string CmActiveYn { get; set; }
        public string CmCrBy { get; set; }
        public DateTime CmCrDt { get; set; }
        public string CmUpBy { get; set; }
        public DateTime CmUpDt { get; set; }
    }
}
