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

public partial class Tools_Damage : System.Web.UI.Page
{
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Label lbl = this.Master.FindControl("lblMenuName") as Label;
        //lbl.Text = "Damage";
        btnSave.Attributes.Add("onclick", "return askConfirm();");
        btnAdd.Attributes.Add("onclick", "return askConfirm();");
        //btnCancel.Attributes.Add("onclick", "return askConfirm();");
        btnUpdate.Attributes.Add("onclick", "return askConfirm();"); 
        pager.PageSize = WebConfigKeys.GridPageSize;

        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void rptDamage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl = e.Item.FindControl("lblRowNo") as Label;
            lbl.Text = (rowIndex++).ToString();

            LinkButton button = e.Item.FindControl("lnkDelete") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");

            button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptDamage_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Damage.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, pager.CurrentIndex);
                Response.Redirect(redirectUrl);
                break;
            case "delete":
                long id = 0;
                long.TryParse(e.CommandArgument.ToString(), out id);
                byte result = DamageManager.Delete(id);
                if (result == 1)
                {
                    Response.Redirect(string.Format("Damage.aspx?pg={0}", pager.CurrentIndex));
                }
                else
                {
                    lblDelMessage.Text = "Corresponding Damage had more references";
                    BindData(); 
                }
                break;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Damage.aspx?action=add&pg={0}", pager.CurrentIndex));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        DamageInfo damageInfo = new DamageInfo();
        damageInfo.Name = txtDamage.Text.Trim();
        damageInfo.Description = txtDesc.Text.Trim();

        byte result = DamageManager.Insert(damageInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Damage.aspx?pg={0}", pager.CurrentIndex));
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

        DamageInfo damageInfo = new DamageInfo();
        damageInfo.Id = id;
        damageInfo.Name = txtDamage.Text.Trim();
        damageInfo.Description = txtDesc.Text.Trim();

        byte result = DamageManager.Update(damageInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Damage.aspx?pg={0}", pager.CurrentIndex));
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
        Response.Redirect(string.Format("Damage.aspx?pg={0}", pager.CurrentIndex));
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

        Response.Redirect(string.Format("Damage.aspx{0}", queryString));
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

        DamageInfoCollection damageInfoCollection = DamageManager.Search(pager.CurrentIndex, pager.PageSize, "", "", out totalRows);
        rptDamage.DataSource = damageInfoCollection;
        rptDamage.DataBind();

        pager.ItemCount = totalRows;

        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtDamage.Text = string.Empty;
                    txtDesc.Text = string.Empty;

                    dvInput.Style.Remove("display");
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "edit":
                    long id = 0;
                    long.TryParse(Request.QueryString["id"], out id);

                    DamageInfo damageInfo = DamageManager.Get(id);
                    if (damageInfo != null)
                    {
                        hdnId.Value = damageInfo.Id.ToString();
                        txtDamage.Text = damageInfo.Name;
                        txtDesc.Text = damageInfo.Description;
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
            RequiredFieldValidator1.Enabled = false;
        }
    }
}