using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZadHolding.PortalBase;
using ZadHolding.Business;
using ZadHolding.Been;

public partial class AdminTools_UserRole : System.Web.UI.Page
{
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl = this.Master.FindControl("lblMenuName") as Label;
        lbl.Text = "User Role";

        pager.PageSize = WebConfigKeys.GridPageSize;

        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void rptUserRole_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl = e.Item.FindControl("lblRowNo") as Label;
            lbl.Text = (rowIndex++).ToString();
        }
    }

    protected void rptUserRole_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("UserRole.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, pager.CurrentIndex);
                Response.Redirect(redirectUrl);
                break;
            case "delete":
                long id = 0;
                long.TryParse(e.CommandArgument.ToString(), out id);

                UserRoleManager.Delete(id);
                Response.Redirect("UserRole.aspx");

                break;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("UserRole.aspx?action=add&pg={0}", pager.CurrentIndex));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserRoleInfo userRoleInfo = new UserRoleInfo();
        userRoleInfo.RoleName = txtRoleName.Text.Trim();
        userRoleInfo.Description = txtDesc.Text.Trim();

        byte result = UserRoleManager.Insert(userRoleInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("UserRole.aspx?pg={0}", pager.CurrentIndex));
        }
        else if(result == 2)
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
        long id = 0;
        long.TryParse(hdnId.Value, out id);

        UserRoleInfo userRoleInfo = new UserRoleInfo();
        userRoleInfo.Id = id;
        userRoleInfo.RoleName = txtRoleName.Text.Trim();
        userRoleInfo.Description = txtDesc.Text.Trim();

        byte result = UserRoleManager.Update(userRoleInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("UserRole.aspx?pg={0}", pager.CurrentIndex));
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
        Response.Redirect(string.Format("UserRole.aspx?pg={0}", pager.CurrentIndex));
    }

    protected void pager_Command(object sender, CommandEventArgs e)
    {
        int currentPageIndex = 0;
        int.TryParse(e.CommandArgument.ToString(), out currentPageIndex);
        pager.CurrentIndex = currentPageIndex;

        string queryString = string.Empty;
        string queryCon = string.Empty;

        if (string.IsNullOrEmpty(Request.QueryString["pg"]))
        {
            BindData();
            return;
        }

        foreach (string key in Request.QueryString.AllKeys)
        {
            if (key == "pg")
            {
                queryCon = string.IsNullOrEmpty(queryString) ? "?" : "&";
                queryString += string.Format("{0}{1}={2}", queryCon, key, pager.CurrentIndex);
            }
            else
            {
                queryCon = string.IsNullOrEmpty(queryString) ? "?" : "&";
                queryString += string.Format("{0}{1}={2}", queryCon, key, Request.QueryString[key]);
            }
        }

        Response.Redirect(string.Format("UserRole.aspx{0}", queryString));
    }

    private void BindData()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["pg"]))
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString["pg"], out pageIndex);
            pager.CurrentIndex = pageIndex;
        }

        long totalRows = 0;
        rowIndex = ((pager.CurrentIndex - 1) * pager.PageSize) + 1;

        UserRoleInfoCollection userRoleInfoCollection = UserRoleManager.Search(pager.CurrentIndex, pager.PageSize, "", "", out totalRows);
        rptUserRole.DataSource = userRoleInfoCollection;
        rptUserRole.DataBind();

        pager.ItemCount = totalRows;

        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtRoleName.Text = string.Empty;
                    txtDesc.Text = string.Empty;

                    dvInput.Style.Add("display", "block");
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "edit":
                    long id = 0;
                    long.TryParse(Request.QueryString["id"], out id);

                    UserRoleInfo userRoleInfo = UserRoleManager.Get(id);
                    if (userRoleInfo != null)
                    {
                        hdnId.Value = userRoleInfo.Id.ToString();
                        txtRoleName.Text = userRoleInfo.RoleName;
                        txtDesc.Text = userRoleInfo.Description;
                    }

                    dvInput.Style.Add("display", "block");
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
            RequiredFieldValidator1.Enabled = false;
        }
    }
}