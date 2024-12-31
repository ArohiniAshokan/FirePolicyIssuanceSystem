using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

namespace FirePolicyIssuanceSystem.Master
{
    public partial class TariffMasterHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (!IsPostBack)
                {
                    if (Request.QueryString["mode"].ToString() == "U")
                    {
                        int tmUid = Convert.ToInt32(Request.QueryString["tmUid"].ToString());
                        BindHistoryData(tmUid);
                    }
                    else if(Request.QueryString["mode"].ToString()=="I")
                    {
                        BindData();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void BindHistoryData(int tmUid)
        {
            try
            {
                TariffMasterHistoryManager objTariffMasterHistoryManager = new TariffMasterHistoryManager();

                DataTable tariffHistoryDataUnique = objTariffMasterHistoryManager.TariffMasterHistoryGridBindUnique(tmUid);

                GridView1.DataSource = tariffHistoryDataUnique;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void BindData()
        {
            try
            {
                TariffMasterHistoryManager objTariffMasterHistoryManager = new TariffMasterHistoryManager();

                DataTable tariffHistoryData = objTariffMasterHistoryManager.TariffMasterHistoryGridBind();

                GridView1.DataSource = tariffHistoryData;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnTmhBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["mode"].ToString() == "U")
                {
                    Response.Redirect("~/Master/TariffMasterListing.aspx");
                }
                else if (Request.QueryString["mode"].ToString() == "I")
                {
                    Response.Redirect("~/Dashboard.aspx");
                }

                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}