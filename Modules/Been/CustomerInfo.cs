using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class CustomerInfo
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string QID { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryAltDescription { get; set; }
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryDescription { get; set; }
        public string CountryAltDescription { get; set; }
        public string PassportNo { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string POBox { get; set; }
        public string Mobile { get; set; }
        public string Land { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string AltDescription { get; set; }
        public bool Active { get; set; }
        public long CreatedId { get; set; }
        public string CreatedName { get; set; }
        public DateTime CreatedTime { get; set; }
        public long UpdatedId { get; set; }
        public string UpdatedName { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}


