using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class PolicyInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Amendment_No { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool RegFlag { get; set; }
        public decimal InsuredValue { get; set; }
        public decimal Premium { get; set; }
        public decimal Deductibles { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public string AltDescription { get; set; }
        public long CreatedId { get; set; }
        public string CreatedName { get; set; }
        public DateTime CreatedTime { get; set; }
        public long UpdatedId { get; set; }
        public string UpdatedName { get; set; }
        public DateTime UpdatedTime { get; set; }

        public long VehicleId { get; set; }
        public string VehicleName { get; set; }
        public long ModelId { get; set; }
        public long MakeId { get; set; }
        public string MakeName { get; set; }
        public string MakeDescription { get; set; }
        public string MakeAltDescription { get; set; }
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }
        public string ModelAltDescription { get; set; }

        public long ShapeId { get; set; }
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        public string TypeAltDescription { get; set; }
        public string ShapeName { get; set; }
        public string ShapeDescription { get; set; }
        public string ShapeAltDescription { get; set; }

        public long ColorId { get; set; }
        public string ColorName { get; set; }
        public string ColorDescription { get; set; }
        public string ColorAltDescription { get; set; }

        public long CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerQID { get; set; }

        public long CustomerCountryID { get; set; }
        public string CustomerCountryName { get; set; }
        public string CustomerCountryDescription { get; set; }
        public string CustomerCountryAltDescription { get; set; }
        public string CustomerPassportNo { get; set; }

        public long ModelYear { get; set; }

        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }
        public long Cylinders { get; set; }
        public long Passengers { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseCost { get; set; }


        public long CoverageId { get; set; }
        public string CoverageName { get; set; }

        public long CompanyId { get; set; }
        public string CompanyName { get; set; }


    
     
        
      
    }
}


