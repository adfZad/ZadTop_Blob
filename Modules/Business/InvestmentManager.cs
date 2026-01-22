using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Data;
using System.Data;

namespace ZadHolding.Business
{
    public class InvestmentManager
    {
        public static bool Insert(InvestmentInfo investmentInfo)
        {
            return InvestmentDAL.Insert(investmentInfo);
        }

        public static byte InsertForApproval(InvestmentInfo investmentInfo, long userId)
        {
            return InvestmentDAL .InsertForApproval(investmentInfo, userId);
        }

        public static bool Update(InvestmentInfo investmentInfo)
        {
            return InvestmentDAL.Update(investmentInfo);
        }

        public static bool Delete(long investmentId)
        {
            return InvestmentDAL.Delete(investmentId);
        }

        public static InvestmentInfo Get(long investmentId)
        {
            return InvestmentDAL.Get(investmentId);
        }

        public static InvestmentInfoCollection GetAll()
        {
            return InvestmentDAL.GetAll();
        }

        public static InvestmentInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return InvestmentDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }

        public static InvestmentInfoCollection SearchDtExcludePaging(CallSearchParams callSearchParams, out long totalRows)
        {
            return InvestmentDAL.SearchDtExcludePaging(callSearchParams, out totalRows);
        }

        public static DataTable SearchApproveValues() 
        {
            DataTable dt =  InvestmentDAL.SearchApproveValues();

            DataColumn[] dcKey = new DataColumn[1];
            dcKey[0] = dt.Columns["id"];
            dt.PrimaryKey = dcKey;

            return dt;
        }

        public static bool UpdateApproval(long id, long userId) 
        {
            return InvestmentDAL.UpdateApproval(id, userId);
        }

    }
}
