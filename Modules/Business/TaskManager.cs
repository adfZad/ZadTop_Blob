using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class TaskManager
    {
        public static byte Insert(TaskInfo taskInfo)
        {
            return TaskDAL.Insert(taskInfo);
        }

        public static byte Update(TaskInfo taskInfo)
        {
            return TaskDAL.Update(taskInfo);
        }

        public static byte Update_Start_Time(TaskInfo taskInfo)
        {
            return TaskDAL.Update_Start_Time(taskInfo);
        }

        public static byte Update_End_Time(TaskInfo taskInfo)
        {
            return TaskDAL.Update_End_Time(taskInfo);
        }

        public static byte Update_Return_End_Time(TaskInfo taskInfo)
        {
            return TaskDAL.Update_Return_End_Time(taskInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return TaskDAL.Delete(exchangeRateId);
        }

        public static TaskInfo Get(long exchangeRateId)
        {
            return TaskDAL.Get(exchangeRateId);
        }

        public static TaskInfo GetNextTask(long documentId)
        {
            return TaskDAL.GetNextTask(documentId);
        }
        public static TaskInfoCollection GetAll()
        {
            return TaskDAL.GetAll();
        }
        public static TaskInfoCollection GetTaskByFlow(long flowId)
        {
            return TaskDAL.GetTaskByFlow(flowId);
        }
        public static TaskInfoCollection GetTaskByDocument(long documentId)
        {
            return TaskDAL.GetTaskByDocument(documentId);
        }
        public static TaskInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return TaskDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static TaskInfoCollection Search(TaskSearchParams taskSearchParams, out long totalRows)
        {
            return TaskDAL.Search(taskSearchParams, out totalRows);
        }
    }
}

