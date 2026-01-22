using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
        
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public long DivisionId { get; set; }
        public string DivisionName { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string Description { get; set; }
        public string AltDescription { get; set; }
       
        public bool Active { get; set; }
    }
}
