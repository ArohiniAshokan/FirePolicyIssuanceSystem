using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class TariffMasterHistoryManager
    {
        public DataTable TariffMasterHistoryGridBind()
        {
            try
            {
                
                DataTable result = null;
                string query = "SELECT TM_UID, TM_SRL, DECODE(TM_ACTION_TYPE, 'I', 'INSERT', 'U', 'UPDATE', 'D', 'DELETE') TM_ACTION_TYPE,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_CLASS_FM AND CM_TYPE = 'RISK_CLASS') AS RISK_CLASS_FM,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_CLASS_TO AND CM_TYPE = 'RISK_CLASS') AS RISK_CLASS_TO,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_OCCP_FM AND CM_TYPE = 'OCCUPANCY') AS RISK_OCCP_FM,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_OCCP_TO AND CM_TYPE = 'OCCUPANCY') AS RISK_OCCP_TO, TM_RISK_SI_FM, TM_RISK_SI_TO, TM_RISK_RATE FROM TARIFF_MASTER_HISTORY ORDER BY TM_UID DESC, TM_SRL DESC";

                result = DBConnection.ExecuteDataset(query);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
        public DataTable TariffMasterHistoryGridBindUnique(int pTmUid)
        {
            try
            {
                DataTable result = null;

                string query = $"SELECT TM_UID, TM_SRL, DECODE(TM_ACTION_TYPE, 'I', 'INSERT', 'U', 'UPDATE', 'D', 'DELETE') TM_ACTION_TYPE,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_CLASS_FM AND CM_TYPE = 'RISK_CLASS') AS RISK_CLASS_FM,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_CLASS_TO AND CM_TYPE = 'RISK_CLASS') AS RISK_CLASS_TO,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_OCCP_FM AND CM_TYPE = 'OCCUPANCY') AS RISK_OCCP_FM,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_OCCP_TO AND CM_TYPE = 'OCCUPANCY') AS RISK_OCCP_TO, TM_RISK_SI_FM, TM_RISK_SI_TO, TM_RISK_RATE FROM TARIFF_MASTER_HISTORY  WHERE TM_UID = {pTmUid}";

                result = DBConnection.ExecuteDataset(query);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
