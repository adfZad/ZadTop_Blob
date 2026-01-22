using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class UserSearchParams
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public long EmpID { get; set; }
        public long RoldId { get; set; }
        public long DepartmentId { get; set; }
        public long DivisionId { get; set; }
        public long DesignationId { get; set; }
        public long NationalityId { get; set; }
        public string VisaCompany { get; set; }
        public DateTime PassportExpiryFrom { get; set; }
        public DateTime PassportExpiryTo { get; set; }
        public DateTime RPExpiryFrom { get; set; }
        public DateTime RPExpiryTo { get; set; }
    }
}
