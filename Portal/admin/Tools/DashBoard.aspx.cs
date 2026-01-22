using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Business;
using ZadHolding.Been.Collection;
using ZadHolding.PortalBase;
using ZadHolding.Data;

public partial class admin_Tools_DashBoard : System.Web.UI.Page
{
    public string str = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {

        long totalRows = 0;
        TaskSearchParams taskSearchParams = new TaskSearchParams();
        taskSearchParams.CurrentPage = 1;
        taskSearchParams.PageSize = 100;
        taskSearchParams.SortColumn = "";
        taskSearchParams.SortOrder = "";
        taskSearchParams.UserId = PortalUser.Current.UserId;
        taskSearchParams.TaskStatusId = 1;
        taskSearchParams.TaskTypeId = 1;
        TaskInfoCollection taskInfoCollection = TaskManager.Search(taskSearchParams, out totalRows);
        if (taskInfoCollection != null)
        {
            rpttask.DataSource = taskInfoCollection;
            rpttask.DataBind();
        }
        else
        {
            divTask.Visible = false;
        }

        taskSearchParams.TaskTypeId = 2;
        taskInfoCollection = TaskManager.Search(taskSearchParams, out totalRows);
        if (taskInfoCollection != null)
        {
            rptreturn.DataSource = taskInfoCollection;
            rptreturn.DataBind();
        }
        else
        {
            divReturn.Visible = false;
        }



       
    }

   

   

  

}