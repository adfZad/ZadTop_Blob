using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Business;
using ZadHolding.PortalBase;
using ZadHolding.Data;
using ZadHolding.Pager;

public partial class admin_Flow_List : System.Web.UI.Page
{
    int rowIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl = this.Master.FindControl("lblMenuName") as Label;
        lbl.Text = "Flow Details";

        pager.PageSize = WebConfigKeys.GridPageSize;

        if (!IsPostBack)
        {


            BindData();
            LoadBindData();
        }
    }

    protected void rptUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl = e.Item.FindControl("lblRowNo") as Label;
            lbl.Text = (++rowIndex).ToString();



        }
    }

    protected void rptUser_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                Response.Redirect(string.Format("Edit.aspx?id={0}", e.CommandArgument));
                break;

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void pager_Command(object sender, CommandEventArgs e)
    {
        int currentPageIndex = 0;
        int.TryParse(e.CommandArgument.ToString(), out currentPageIndex);
        pager.CurrentIndex = currentPageIndex;

        BindData();
    }

    private void BindData()
    {
        long totalRows = 0;
        long value = 0;

        rowIndex = ((pager.CurrentIndex - 1) * pager.PageSize);

        FlowDetailSearchParams calldetailSearchParams = new FlowDetailSearchParams();
        calldetailSearchParams.CurrentPage = pager.CurrentIndex;
        calldetailSearchParams.PageSize = pager.PageSize;

        DateTime date = DateTime.MinValue;





        long.TryParse(ddlFlow.SelectedItem.Value, out value);
        calldetailSearchParams.FlowId = value;


        long.TryParse(ddlFlowTemplate.SelectedItem.Value, out value);
        calldetailSearchParams.FlowTemplateId = value;

        long.TryParse(ddlUserName.SelectedItem.Value, out value);
        calldetailSearchParams.UserId = value;


        long.TryParse(ddlLevel.SelectedItem.Value, out value);
        calldetailSearchParams.LevelId = value;




        calldetailSearchParams.SortColumn = hdnSortColumn.Value;

        FlowDetailInfoCollection userInfoCollection = FlowDetailManager.Search(calldetailSearchParams, out totalRows);
        rptUser.DataSource = userInfoCollection;
        rptUser.DataBind();

        pager.ItemCount = totalRows;
    }

    private void LoadBindData()
    {
        BindFlow();
        BindFlowTemplate();
        BindUserName();
        BindLevel();

    }
    private void BindFlow()
    {
        FlowInfoCollection manufactureInfoCollection = FlowManager.GetAll();
        ddlFlow.DataSource = manufactureInfoCollection;
        ddlFlow.DataTextField = "Name";
        ddlFlow.DataValueField = "Id";
        ddlFlow.DataBind();

        ddlFlow.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlFlow.SelectedIndex = 0;
    }






    private void BindFlowTemplate()
    {
        FlowTemplateInfoCollection manufactureInfoCollection = FlowTemplateManager.GetAll();
        ddlFlowTemplate.DataSource = manufactureInfoCollection;
        ddlFlowTemplate.DataTextField = "Name";
        ddlFlowTemplate.DataValueField = "Id";
        ddlFlowTemplate.DataBind();

        ddlFlowTemplate.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlFlowTemplate.SelectedIndex = 0;


    }







    private void BindUserName()
    {
        UserInfoCollection manufactureInfoCollection = UserManager.GetAll();
        ddlUserName.DataSource = manufactureInfoCollection;
        ddlUserName.DataTextField = "UserName";
        ddlUserName.DataValueField = "Id";
        ddlUserName.DataBind();

        ddlUserName.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlUserName.SelectedIndex = 0;
    }


    private void BindLevel()
    {


        LevelInfoCollection manufactureInfoCollection = LevelManager.GetAll();
        ddlLevel.DataSource = manufactureInfoCollection;
        ddlLevel.DataTextField = "Name";
        ddlLevel.DataValueField = "Id";
        ddlLevel.DataBind();

        ddlLevel.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlLevel.SelectedIndex = 0;
    }

    protected void lnkSort_Click(object sender, EventArgs e)
    {
        LinkButton lnkBtn = (LinkButton)sender;

        string sortColumn = lnkBtn.CommandArgument;
        string sortOrder = "ASC";

        if (!string.IsNullOrEmpty(hdnSortColumn.Value))
        {
            string[] vals = hdnSortColumn.Value.Split(new char[] { '|' });

            if (vals.Length == 2)
            {
                sortOrder = (vals[1].ToLower() == "asc") ? "DESC" : "ASC";
            }
        }

        hdnSortColumn.Value = string.Format("{0}|{1}", sortColumn, sortOrder);
        BindData();
    }
}