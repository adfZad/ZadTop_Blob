using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZadHolding.PortalBase;

public partial class Master_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void lnkLogout_Click(object sender, EventArgs e) 
    {
        PortalUser.Current.SignOut();

        Response.Redirect("~/Login.aspx");
    }

     
}
