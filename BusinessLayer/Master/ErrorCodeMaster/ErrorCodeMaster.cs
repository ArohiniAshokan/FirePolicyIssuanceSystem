using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ErrorCodeMaster
    {
        public string ErrCode { get; set; }
        public string ErrType { get; set; }
        public string ErrDesc { get; set; }
        public string ErrCrBy { get; set; }
        public DateTime ErrCrDt { get; set; }
        public string ErrUpBy { get; set; }
        public DateTime ErrUpDt { get; set; }
    }
}
