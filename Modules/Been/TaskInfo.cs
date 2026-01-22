using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class TaskInfo
    {
        public long Id { get; set; }
        public long DocumentId { get; set; }
        public string DocumentName { get; set; }
        public long TaskStatusId { get; set; }
        public string TaskStatusName { get; set; }
        public long DivisionId { get; set; }
        public string DivisionName { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long TaskTypeId { get; set; }
        public string TaskTypeName { get; set; }
        public long ApproveId { get; set; }
        public string ApproveName { get; set; }
        public long ApprovalStatusId { get; set; }
        public string ApprovalStatusName { get; set; }
        public long DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentPurpose { get; set; }
        public string DocumentDescription { get; set; }
        public string DocumentAltDescription { get; set; }
        public decimal DocumentAmount { get; set; }
        public decimal OtherAmount { get; set; }
        public decimal Rate { get; set; }
        public string Currency { get; set; }
        public long DocumentCreatedId { get; set; }
        public string DocumentCreatedName { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public string UserFullName
        {
            get
            {
                return string.Format("{0} {1}", UserFirstName, UserLastName);
            }
        }
        public long UserDesignationId { get; set; }
        public string UserDesignationName { get; set; }
        public string UserDescription { get; set; }
        public string UserAltDescription { get; set; }
        public long LevelId { get; set; }
        public string LevelName { get; set; }
        public string Description { get; set; }
        public string AltDescription { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool Active { get; set; }

       
    }
}

