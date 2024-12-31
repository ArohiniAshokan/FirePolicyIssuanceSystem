using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using BusinessLayer;

namespace FirePolicyIssuanceSystem
{
    /// <summary>
    /// Summary description for CalculateLcSi
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class CalculateLcSi : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public double? ConvertCurrency(double? amount, string currencyCode, string currencyType)
        {
            //decimal exchangeRate = GetExchangeRate(currencyCode);
            CodesMasterManager objCodeMasterManager = new CodesMasterManager();
            double exchangeRate = objCodeMasterManager.ReturnVal(currencyCode, currencyType);

            return amount * exchangeRate;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public double? ConvertToFC(double? amount, string currencyCode, string currencyType)
        {
            //decimal exchangeRate = GetExchangeRate(currencyCode);
            CodesMasterManager objCodeMasterManager = new CodesMasterManager();
            double exchangeRate = objCodeMasterManager.ReturnVal(currencyCode, currencyType);

            return amount / exchangeRate;
        }
    }
}
