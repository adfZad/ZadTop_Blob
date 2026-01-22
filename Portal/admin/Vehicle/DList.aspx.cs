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

public partial class admin_Vehicle_DList : System.Web.UI.Page
{
    int rowIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
            BindData();
            
        }
    }



    private void BindData()
    {
         VehicleInfoCollection vehicleInfoCollection = VehicleManager.GetAll();

         gvCustomers.DataSource = vehicleInfoCollection;
         gvCustomers.DataBind();
   
    }


}