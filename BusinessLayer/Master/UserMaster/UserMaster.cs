using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserMaster
    {
        public string UserId{ get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserActiveYn { get; set; }
        public string UserCrBy { get; set; }
        public DateTime UserCrDt { get; set; }
        public string UserUpBy { get; set; }
        public DateTime UserUpDt { get; set; }
    }
}
