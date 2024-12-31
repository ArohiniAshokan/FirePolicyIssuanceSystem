using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirePolicyIssuanceSystem.MasterPages
{
    public partial class UserTemplate : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          if(Session["User"] != null)
            {
                lblWelcomeMessage.Text = "WELCOME "+Session["User"].ToString().ToUpper();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/Login.aspx?logout=true");
        }

       
        protected void btnHome_Click1(object sender, EventArgs e)
        {
            Response.Redirect("/Dashboard.aspx");
        }
    }
}