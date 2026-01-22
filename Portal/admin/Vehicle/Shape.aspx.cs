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

public partial class admin_Vehicle_Shape : System.Web.UI.Page
{
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Label lbl = this.Master.FindControl("lblMenuName") as Label;
        //lbl.Text = "Shape";
        btnSave.Attributes.Add("onclick", "return askConfirm();");
        //btnAdd.Attributes.Add("onclick", "return askConfirm();");
        //btnCancel.Attributes.Add("onclick", "return askConfirm();");
        //btnUpdate.Attributes.Add("onclick", "return askConfirm();"); 
        pager.PageSize = WebConfigKeys.GridPageSize;

        if (!IsPostBack)
        {
            BindData();
            BindType(); 
            
        }
    }

    protected void rptShape_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl = e.Item.FindControl("lblRowNo") as Label;
            lbl.Text = (rowIndex++).ToString();

            //LinkButton button = e.Item.FindControl("lnkDelete") as LinkButton;
            //button.Attributes.Add("onClick", "return askConfirm();");

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptShape_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Shape.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, pager.CurrentIndex);
                Response.Redirect(redirectUrl);
                break;
            case "delete":
                long id = 0;
                long.TryParse(e.CommandArgument.ToString(), out id);
                byte result = ShapeManager.Delete(id);
                if (result == 1)
                {
                    Response.Redirect(string.Format("Shape.aspx?pg={0}", pager.CurrentIndex));
                }
                else
                {
                    lblDelMessage.Text = "Corresponding Shape had more references";
                    //BindData(); 
                }
                break;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Shape.aspx?action=add&pg={0}", pager.CurrentIndex));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        ShapeInfo shapeInfo         = new ShapeInfo();
        shapeInfo.Name              = txtShape.Text.Trim();
        shapeInfo.Description       = txtDesc.Text.Trim();
        shapeInfo.AltDescription    = txtAltDesc.Text.Trim();
        shapeInfo.Active            = chkActive.Checked;
        shapeInfo.CreatedId         = PortalUser.Current.UserId;
        shapeInfo.CreatedTime       = DateTime.Now;
        shapeInfo.UpdatedId         = PortalUser.Current.UserId;
        shapeInfo.UpdatedTime       = DateTime.Now;


        long typeId = 0;
        long.TryParse(ddlType.SelectedItem.Value, out typeId);

        shapeInfo.TypeId = typeId;

        byte result = ShapeManager.Insert(shapeInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Shape.aspx?pg={0}", pager.CurrentIndex));
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

        ShapeInfo shapeInfo = new ShapeInfo();
        shapeInfo.Id = id;
        shapeInfo.Name = txtShape.Text.Trim();
        shapeInfo.Description = txtDesc.Text.Trim();
        shapeInfo.AltDescription = txtAltDesc.Text.Trim();
        shapeInfo.Active = chkActive.Checked;
        shapeInfo.UpdatedId = PortalUser.Current.UserId;
        shapeInfo.UpdatedTime = DateTime.Now;

        long typeId = 0;
        long.TryParse(ddlType.SelectedItem.Value, out typeId);

        shapeInfo.TypeId = typeId;

        byte result = ShapeManager.Update(shapeInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Shape.aspx?pg={0}", pager.CurrentIndex));
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
        Response.Redirect(string.Format("Shape.aspx?pg={0}", pager.CurrentIndex));
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

        Response.Redirect(string.Format("Shape.aspx{0}", queryString));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        ShapeSearchParams shapeSearchParams = new ShapeSearchParams();
        
        
        if (!string.IsNullOrEmpty(Request.QueryString["pg"]))
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString["pg"], out pageIndex);
            pager.CurrentIndex = pageIndex;
        }

        long totalRows = 0;
        long value = 0;
        rowIndex = ((pager.CurrentIndex - 1) * pager.PageSize) + 1;

        long.TryParse(sddlType.SelectedItem.Value, out value);
        shapeSearchParams.TypeId = value;

        shapeSearchParams.CurrentPage = pager.CurrentIndex;
        shapeSearchParams.PageSize = pager.PageSize;
        shapeSearchParams.Name = txtName.Text.ToString();
        shapeSearchParams.Description = txtSDescription.Text.ToString();
        shapeSearchParams.AltDescription = txtSAltDescription.Text.ToString();

        //ShapeInfoCollection shapeInfoCollection = ShapeManager.Search(pager.CurrentIndex, pager.PageSize, "", "", out totalRows);
        ShapeInfoCollection shapeInfoCollection = ShapeManager.Search(shapeSearchParams, out totalRows);
        rptShape.DataSource = shapeInfoCollection;
        rptShape.DataBind();

        pager.ItemCount = totalRows;

        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtShape.Text = string.Empty;
                    txtDesc.Text = string.Empty;
                    txtAltDesc.Text = string.Empty;
                    chkActive.Checked = true;

                    BindType();
                    dvInput.Style.Remove("display");
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "edit":
                    long id = 0;
                    long.TryParse(Request.QueryString["id"], out id);
                    BindType();
                    ShapeInfo shapeInfo = ShapeManager.Get(id);
                    if (shapeInfo != null)
                    {
                        hdnId.Value = shapeInfo.Id.ToString();
                        txtShape.Text = shapeInfo.Name;
                        txtDesc.Text = shapeInfo.Description;
                        txtAltDesc.Text = shapeInfo.AltDescription;
                        chkActive.Checked = shapeInfo.Active;
                        ddlType.SelectedValue = shapeInfo.TypeId.ToString();
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
            valtxtCode.Enabled = false;
            valtxtEnDescription.Enabled = false;
            valArDescription.Enabled = false;
        }
    }

    private void BindType()
    {
        TypeInfoCollection typeInfoCollection = TypeManager.GetAll();
        ddlType.DataSource = typeInfoCollection;
        ddlType.DataTextField = "Name";
        ddlType.DataValueField = "Id";
        ddlType.DataBind();

        sddlType.DataSource = typeInfoCollection;
        sddlType.DataTextField = "Name";
        sddlType.DataValueField = "Id";
        sddlType.DataBind();

        ddlType.Items.Insert(0, new ListItem(""));
        ddlType.SelectedIndex = 0;

        sddlType.Items.Insert(0, new ListItem(""));
        sddlType.SelectedIndex = 0;
        
    }

}