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
using System.Collections.Specialized;

public partial class admin_Tools_User : System.Web.UI.Page
{
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        btnSave.Attributes.Add("onclick", "return askConfirm();");
        
        if (!IsPostBack)
        {
            BindData();
            txtPassword.Attributes["value"] = txtPassword.Text;


        }
    }

    protected void rptUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
            //LinkButton button = e.Item.FindControl("lnkDelete") as LinkButton;
            //button.Attributes.Add("onClick", "return askConfirm();");

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptUser_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("User.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;
          
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("User.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        UserInfo userInfo = new UserInfo();
        userInfo.FirstName = txtFirstName.Text.Trim();
        userInfo.LastName = txtLastName.Text.Trim();
        userInfo.UserName = txtUserName.Text.Trim();
        userInfo.Password = txtPassword.Text.Trim();
        userInfo.Description = txtDescription.Text.Trim();
        userInfo.AltDescription = txtAltDescription.Text.Trim();
        userInfo.Active = chkActive.Checked;
   
      

        long value = 0;
        long.TryParse(ddlDivision.SelectedItem.Value, out value);

        userInfo.DivisionId = value;

        long.TryParse(ddlDesignation.SelectedItem.Value, out value);

        userInfo.DesignationId = value;

        long.TryParse(ddlRole.SelectedItem.Value, out value);

        userInfo.RoleId = value;

        long.TryParse(ddlDepartment.SelectedItem.Value, out value);

        userInfo.DepartmentId = value;

        byte result = UserManager.Insert(userInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("User.aspx?pg={0}", 1));
        }
        else if (result == 2)
        {
            lblMessage.Text = "Duplicate values not allowed.";
        }
        else
        {
            lblMessage.Text = "Error occured. Try again.";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        long id = 0;
        long.TryParse(hdnId.Value, out id);

        UserInfo userInfo = new UserInfo();
        userInfo.Id = id;
        userInfo.FirstName = txtFirstName.Text.Trim();
        userInfo.LastName = txtLastName.Text.Trim();
        userInfo.UserName = txtUserName.Text.Trim();
        userInfo.Password = txtPassword.Text.Trim();
        userInfo.Description = txtDescription.Text.Trim();
        userInfo.AltDescription = txtAltDescription.Text.Trim();
        userInfo.Active = chkActive.Checked;

        long value = 0;
        long.TryParse(ddlDivision.SelectedItem.Value, out value);

        userInfo.DivisionId = value;

        long.TryParse(ddlDesignation.SelectedItem.Value, out value);

        userInfo.DesignationId = value;

        long.TryParse(ddlRole.SelectedItem.Value, out value);

        userInfo.RoleId = value;

        long.TryParse(ddlDepartment.SelectedItem.Value, out value);

        userInfo.DepartmentId = value;

        byte result = UserManager.Update(userInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("User.aspx?pg={0}", 1));
        }
        else if (result == 2)
        {
            lblMessage.Text = "Duplicate values not allowed.";
        }
        else
        {
            lblMessage.Text = "Error occured. Try again.";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("User.aspx?pg={0}", 1));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {

        UserInfoCollection userInfoCollection = UserManager.GetAll();
        rptUser.DataSource = userInfoCollection;
        rptUser.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtFirstName.Text = string.Empty;
                    txtLastName.Text = string.Empty;
                    txtUserName.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                  
                    chkActive.Checked = true;

                    BindDivision();
                    BindRole();
                    BindDesignation();
                    BindDepartment();
                    dvInput.Style.Remove("display");
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "edit":
                    long id = 0;
                    long.TryParse(Request.QueryString["id"], out id);
                    BindDivision();
                    BindRole();
                    BindDesignation();
                    BindDepartment();
                    UserInfo userInfo = UserManager.Get(id);
                    if (userInfo != null)
                    {
                        hdnId.Value = userInfo.Id.ToString();
                        txtFirstName.Text = userInfo.FirstName;
                        txtLastName.Text = userInfo.LastName;
                        txtUserName.Text = userInfo.UserName;
                        txtPassword.Text = userInfo.Password;
                        txtDescription.Text = userInfo.Description;
                        txtAltDescription.Text = userInfo.AltDescription;

                        

                        chkActive.Checked = userInfo.Active;
                        ddlDivision.SelectedValue = userInfo.DivisionId.ToString();
                        ddlRole.SelectedValue = userInfo.RoleId.ToString();
                        ddlDesignation.SelectedValue = userInfo.DesignationId.ToString();
                        ddlDepartment.SelectedValue = userInfo.DepartmentId.ToString();
                    }

                    dvInput.Style.Remove("display");
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;

                    break;
                default:
                    dvInput.Style.Add("display", "none");

                    break;
            }
        }
        else
        {
            dvInput.Style.Add("display", "none");

        }
    }

    private void BindDivision()
    {
        DivisionInfoCollection divisionInfoCollection = DivisionManager.GetAll();
        ddlDivision.DataSource = divisionInfoCollection;
        ddlDivision.DataTextField = "Name";
        ddlDivision.DataValueField = "Id";
        ddlDivision.DataBind();

        ddlDivision.Items.Insert(0, new ListItem(""));
        ddlDivision.SelectedIndex = 0;
    }

    private void BindDepartment()
    {
        DepartmentInfoCollection departmentInfoCollection = DepartmentManager.GetAll();
        ddlDepartment.DataSource = departmentInfoCollection;
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataValueField = "Id";
        ddlDepartment.DataBind();

        ddlDepartment.Items.Insert(0, new ListItem(""));
        ddlDepartment.SelectedIndex = 0;
    }

    private void BindRole()
    {
        UserRoleInfoCollection userRoleInfoCollection = UserRoleManager.GetAll();
        ddlRole.DataSource = userRoleInfoCollection;
        ddlRole.DataTextField = "RoleName";
        ddlRole.DataValueField = "Id";
        ddlRole.DataBind();

        ddlRole.Items.Insert(0, new ListItem(""));
        ddlRole.SelectedIndex = 0;
    }
    private void BindDesignation()
    {
        DesignationInfoCollection designationInfoCollection = DesignationManager.GetAll();
        ddlDesignation.DataSource = designationInfoCollection;
        ddlDesignation.DataTextField = "Name";
        ddlDesignation.DataValueField = "Id";
        ddlDesignation.DataBind();

        ddlDesignation.Items.Insert(0, new ListItem(""));
        ddlDesignation.SelectedIndex = 0;
    }
}