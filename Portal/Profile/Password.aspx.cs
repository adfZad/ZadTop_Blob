using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Business;
using ZadHolding.PortalBase;
using System.IO;
using ZadHolding.Utilities;
using System.Drawing.Imaging;

public partial class Profile_Password : System.Web.UI.Page
{
    public UserInfo userInfo { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserInfo userInfo = UserManager.Get(PortalUser.Current.UserId);
        if (userInfo == null)
            return;
        if (txtOldPassword.Text == userInfo.Password)
        {

            if (txtNewPassword.Text == txtConfirmPassword.Text)
            {
                userInfo.Password = txtNewPassword.Text;
                byte result = UserManager.Update(userInfo);
                if (result == 1)
                {

                    lblDelMessage.Text = "Your password sucessfully Changed;Please Logout and Login with new password";
                    Response.Write("<script type='text/javascript'>");
                    Response.Write("alert('Password Changed Successfully!Please Login with new password');");
                    Response.Write("document.location.href='../Login.aspx';");
                    Response.Write("</script>");
                    PortalUser.Current.SignOut();
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                lblDelMessage.Text = "New Password does not match";
            }
        }
        else
        {
            lblDelMessage.Text = "Old Password is not correct";
        }




    }

}