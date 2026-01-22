using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Business;
using ZadHolding.Been.Collection;
using ZadHolding.PortalBase;
using ZadHolding.Data;

public partial class management_Task_List : System.Web.UI.Page
{
    int rowIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl = this.Master.FindControl("lblMenuName") as Label;
        lbl.Text = "Task Details";



        if (!IsPostBack)
        {
            BindData();
        }
    }
        
    private void BindData()
    {
        TaskInfoCollection taskInfoCollection = TaskManager.GetAll();
        rptTask.DataSource = taskInfoCollection;
        rptTask.DataBind();
    }
    
}