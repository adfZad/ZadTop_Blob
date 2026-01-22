using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ZadHolding.Been;
using ZadHolding.Business;
using ZadHolding.PortalBase;
using System.Collections.Specialized;

public partial class admin_Flow_CreateFlowDetail : System.Web.UI.Page
{
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {//Label lbl = this.Master.FindControl("lblMenuName") as Label;
        //    //lbl.Text = "Department";
        btnSave.Attributes.Add("onclick", "return askConfirm();");
        //    //btnAdd.Attributes.Add("onclick", "return askConfirm();");
        //    //btnCancel.Attributes.Add("onclick", "return askConfirm();");
        //    //btnUpdate.Attributes.Add("onclick", "return askConfirm();");
        //    //pager.PageSize = WebConfigKeys.GridPageSize;

        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {

        BindFlow();
        BindFlowTemplate();
        BindUserName();
        BindLevel();



    }

    //private void BindEmployee()
    //{
    //    EmployeeInfoCollection callerInfoCollection = new EmployeeInfoCollection();
    //    EmployeeInfoCollection employeeInfoCollection = EmployeeManager.GetAll();
    //    foreach (EmployeeInfo employeeInfo in employeeInfoCollection)
    //    {
    //        if (employeeInfo.PC)
    //            callerInfoCollection.Add(employeeInfo);
    //    }
    //    ddlCaller.DataSource = employeeInfoCollection;
    //    ddlCaller.DataTextField = "EmployeeName";
    //    ddlCaller.DataValueField = "Id";
    //    //ddlCaller.AutoselectFirstItem = true;
    //    ddlCaller.DataTextField.ToList();
    //    ddlCaller.DataBind();
    //    ddlCaller.Items.Insert(0, new ListItem("", "0"));
    //}

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



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateFlowDetail.aspx");
    }


  









    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            long lvalue = 0;
            DateTime date = DateTime.MinValue;
            decimal dvalue = 0;
            FlowDetailInfo calldetailsInfo = new FlowDetailInfo();

            long.TryParse(ddlFlow.SelectedItem.Value, out lvalue);
            calldetailsInfo.FlowId = lvalue;
            long.TryParse(ddlFlowTemplate.SelectedItem.Value, out lvalue);
            calldetailsInfo.FlowTemplateId = lvalue;
            long.TryParse(ddlUserName.SelectedItem.Value, out lvalue);
            calldetailsInfo.UserId = lvalue;
            long.TryParse(ddlLevel.SelectedItem.Value, out lvalue);
            calldetailsInfo.LevelId = lvalue;
            calldetailsInfo.Description = txtDesc.Text.ToString();
            calldetailsInfo.AltDescription = txtAltDesc.Text.ToString();
            calldetailsInfo.AltDescription = txtAltDesc.Text.ToString();
            calldetailsInfo.Name = txtMake.Text.ToString();
            calldetailsInfo.CreatedId = PortalUser.Current.UserId;
            calldetailsInfo.CreatedTime = DateTime.Now;
            calldetailsInfo.UpdatedId = PortalUser.Current.UserId;
            calldetailsInfo.UpdatedTime = DateTime.Now;






            byte result = FlowDetailManager.Insert(calldetailsInfo);

            if (result == 2)
            {

                lblMessage.Text = "Duplicate values not allowed.";
            }
            else if (result == 1)
            {
                Response.Redirect("CreateFlowDetail.aspx");
            }
            else
            {
                lblMessage.Text = "Error occured. Try again.";
            }

        }
    }
}
