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

public partial class admin_Vehicle_Vehicle : System.Web.UI.Page
{
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        btnSave.Attributes.Add("onclick", "return askConfirm();");
       

        if (!IsPostBack)
        {
            BindData();
          
            
        }
    }

    protected void rptVehicle_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptVehicle_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Vehicle.aspx?action=edit&id={0}&pg={1}", e.CommandArgument,1);
                Response.Redirect(redirectUrl);
                break;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Vehicle.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        long id = 0;
        long value = 0;
        decimal dvalue = 0;
        DateTime date = DateTime.MinValue;

        VehicleInfo vehicleInfo       = new VehicleInfo();
        vehicleInfo.Name              = txtVehicle.Text.Trim();
        vehicleInfo.Name              = txtVehicle.Text.Trim();
        vehicleInfo.ChassisNo         = txtChassisNo.Text.Trim();
        vehicleInfo.EngineNo          = txtEngineNo.Text.Trim();
    
       
        vehicleInfo.Description       = txtDesc.Text.Trim();
        vehicleInfo.AltDescription    = txtAltDesc.Text.Trim();
        vehicleInfo.Active            = chkActive.Checked;
        vehicleInfo.CreatedId         = PortalUser.Current.UserId;
        vehicleInfo.CreatedTime       = DateTime.Now;
        vehicleInfo.UpdatedId         = PortalUser.Current.UserId;
        vehicleInfo.UpdatedTime       = DateTime.Now;


        long.TryParse(ddlModel.SelectedItem.Value, out value);
        vehicleInfo.ModelId = value;
        long.TryParse(ddlShape.SelectedItem.Value, out value);
        vehicleInfo.ShapeId = value;
        long.TryParse(txtModelYear.Text.Trim(), out value);
        vehicleInfo.ModelYear = value;
        long.TryParse(ddlColor.SelectedItem.Value, out value);
        vehicleInfo.ColorId = value;
        long.TryParse(ddlCustomer.SelectedItem.Value, out value);
        vehicleInfo.CustomerId = value;
        long.TryParse(txtCylinders.Text.Trim(), out value);
        vehicleInfo.Cylinders = value;
        long.TryParse(txtPassengers.Text.Trim(), out value);
        vehicleInfo.Passengers = value;

        decimal.TryParse(txtPurchaseCost.Text.Trim(), out dvalue);
        vehicleInfo.PurchaseCost = dvalue;

        date = DateTime.ParseExact(txtPurchaseDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        vehicleInfo.PurchaseDate = date;

         byte result = VehicleManager.Insert(vehicleInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Vehicle.aspx?pg={0}",1));
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
        long value = 0;
        DateTime date = DateTime.MinValue;
        decimal dvalue = 0;

        long.TryParse(hdnId.Value, out id);

        VehicleInfo vehicleInfo = new VehicleInfo();
        vehicleInfo.Id = id;
        vehicleInfo.Name = txtVehicle.Text.Trim();
        vehicleInfo.ChassisNo = txtChassisNo.Text.Trim();
        vehicleInfo.EngineNo = txtEngineNo.Text.Trim();
        
       
        vehicleInfo.Description = txtDesc.Text.Trim();
        vehicleInfo.AltDescription = txtAltDesc.Text.Trim();
        vehicleInfo.Active = chkActive.Checked;
        vehicleInfo.UpdatedId = PortalUser.Current.UserId;
        vehicleInfo.UpdatedTime = DateTime.Now;

        long.TryParse(ddlModel.SelectedItem.Value, out value);
        vehicleInfo.ModelId = value;
        long.TryParse(ddlShape.SelectedItem.Value, out value);
        vehicleInfo.ShapeId = value;
        long.TryParse(txtModelYear.Text.Trim(), out value);
        vehicleInfo.ModelYear = value;
        long.TryParse(ddlColor.SelectedItem.Value, out value);
        vehicleInfo.ColorId = value;
        long.TryParse(ddlCustomer.SelectedItem.Value, out value);
        vehicleInfo.CustomerId = value;
        long.TryParse(txtCylinders.Text.Trim(), out value);
        vehicleInfo.Cylinders = value;
        long.TryParse(txtPassengers.Text.Trim(), out value);
        vehicleInfo.Passengers = value;

        decimal.TryParse(txtPurchaseCost.Text.Trim(), out dvalue);
        vehicleInfo.PurchaseCost = dvalue;

        date = DateTime.ParseExact(txtPurchaseDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        vehicleInfo.PurchaseDate = date;

        byte result = VehicleManager.Update(vehicleInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Vehicle.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Vehicle.aspx?pg={0}", 1));
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {

        VehicleInfoCollection vehicleInfoCollection = VehicleManager.GetAll();
        rptVehicle.DataSource = vehicleInfoCollection;
        rptVehicle.DataBind();

       

        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtVehicle.Text = string.Empty;
                    

                    txtDesc.Text = string.Empty;
                    txtAltDesc.Text = string.Empty;
                    chkActive.Checked = true;

                    BindModel();
                    BindShape();
                    BindColor();
                    BindCustomer();
                    dvInput.Style.Remove("display");
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "edit":
                    long id = 0;
                    long.TryParse(Request.QueryString["id"], out id);
                    BindModel();
                    BindShape();
                    BindColor();
                    BindCustomer();
                    VehicleInfo vehicleInfo = VehicleManager.Get(id);
                    if (vehicleInfo != null)
                    {
                        hdnId.Value = vehicleInfo.Id.ToString();
                        txtVehicle.Text = vehicleInfo.Name;
                        ddlModel.SelectedValue = vehicleInfo.ModelId.ToString();
                        ddlShape.SelectedValue = vehicleInfo.ShapeId.ToString();
                        txtModelYear.Text = vehicleInfo.ModelYear.ToString();
                        ddlColor.SelectedValue = vehicleInfo.ColorId.ToString();
                        ddlCustomer.SelectedValue = vehicleInfo.CustomerId.ToString();
                        txtChassisNo.Text = vehicleInfo.ChassisNo.ToString();
                        txtEngineNo.Text = vehicleInfo.EngineNo.ToString();
                        txtCylinders.Text = vehicleInfo.Cylinders.ToString();
                        txtPassengers.Text = vehicleInfo.Passengers.ToString();
                        txtPurchaseDate.Text = string.Format("{0:dd/MM/yyyy}", vehicleInfo.PurchaseDate);
                        txtPurchaseCost.Text = string.Format("{0:0,0.00}", vehicleInfo.PurchaseCost); 


                        txtDesc.Text = vehicleInfo.Description;
                        txtAltDesc.Text = vehicleInfo.AltDescription;
                        chkActive.Checked = vehicleInfo.Active;
                        ddlModel.SelectedValue = vehicleInfo.MakeId.ToString();
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
           
        }
    }

    private void BindModel()
    {
        ModelInfoCollection modelInfoCollection = ModelManager.GetAll();
        ddlModel.DataSource = modelInfoCollection;
        ddlModel.DataTextField = "Name";
        ddlModel.DataValueField = "Id";
        ddlModel.DataBind();
        ddlModel.Items.Insert(0, new ListItem(""));
        ddlModel.SelectedIndex = 0;
    }

    private void BindShape()
    {
        ShapeInfoCollection shapeInfoCollection = ShapeManager.GetAll();
        ddlShape.DataSource = shapeInfoCollection;
        ddlShape.DataTextField = "Name";
        ddlShape.DataValueField = "Id";
        ddlShape.DataBind();
        ddlShape.Items.Insert(0, new ListItem(""));
        ddlShape.SelectedIndex = 0;
    }

    private void BindColor()
    {
        ColorInfoCollection colorInfoCollection = ColorManager.GetAll();
        ddlColor.DataSource = colorInfoCollection;
        ddlColor.DataTextField = "Name";
        ddlColor.DataValueField = "Id";
        ddlColor.DataBind();
        ddlColor.Items.Insert(0, new ListItem(""));
        ddlColor.SelectedIndex = 0;
    }

    private void BindCustomer()
    {
        CustomerInfoCollection customerInfoCollection = CustomerManager.GetAll();
        ddlCustomer.DataSource = customerInfoCollection;
        ddlCustomer.DataTextField = "Name";
        ddlCustomer.DataValueField = "Id";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new ListItem(""));
        ddlCustomer.SelectedIndex = 0;
    }



}