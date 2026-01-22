using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZadHolding.Been;
using ZadHolding.Business;
using System.Web.Security;

using ZadHolding.PortalBase;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //dvTime.InnerHtml = string.Format("{0:dddd}, {0:MMMM} {0:dd}, {0:yyyy} - {0:hh:mm tt}", DateTime.Now);
    }
    
   
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        UserInfo userInfo = UserManager.GetByUserName(txtUserName.Text.Trim());

        if (userInfo != null)
        {
            if (userInfo.Password.Trim() == txtPassword.Text.Trim())
            {
                bool isCookiePersistent = true;

                PortalUser.Current.Authenticate(userInfo, 60, isCookiePersistent);

                if (PortalUser.Current.IsAuthenticated)
                {
                    string strRedirect = Request["ReturnUrl"];

                    if (String.IsNullOrEmpty(strRedirect))
                        strRedirect = PortalUser.Current.GetHomeUrl();

                    Response.Redirect(strRedirect, true);
                }
            }
            else 
            {
                //lblMessage.Text = "Invalid password.";
            }
        }
        else
        {
            //lblMessage.Text = "Invalid user name.";
        }
    }
}