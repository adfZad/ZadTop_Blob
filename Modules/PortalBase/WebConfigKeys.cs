using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ZadHolding.PortalBase
{
    public class WebConfigKeys
    {
        #region Default key values

        const string Default_GridPageSize = "5";
        const string Default_MaxImageSize = "1048576";
        const string Default_UploadImageRootDirectory = @"~\Uploads";
        const string Default_SuperAdminId = "1";
        const string Default_NewJoineesWithInDays = "1";
        const string Default_ITAdminRoleId = "1";
        const string Default_DashBoardNewsCount = "1";
        const string Default_DashBoardJoineeCount = "1";
        
        #endregion

        #region Configuration Keys

        const string GridPageSize_Key = "GridPageSize";
        const string MaxImageSize_Key = "MaxImageSize";
        const string UploadImageRootDirectory_Key = "UploadImageRootDirectory";
        const string SuperAdminId_Key = "SuperAdminId";
        const string NewJoineesWithInDays_Key = "NewJoineesWithInDays";
        const string ITAdminRoleId_Key = "ITAdminRoleId";
        const string DashBoardNewsCount_Key = "DashBoardNewsCount";
        const string DashBoardJoineeCount_Key = "DashBoardJoineeCount";

        #endregion

        #region Key values

        public static int GridPageSize
        {
            get
            {
                return int.Parse(GetKeyValue(GridPageSize_Key, Default_GridPageSize));
            }
        }

        public static int MaxImageSize 
        {
            get 
            {
                return int.Parse(GetKeyValue(MaxImageSize_Key, Default_MaxImageSize));
            }
        }

        public static string UploadImageRootDirectory 
        {
            get 
            {
                return GetKeyValue(UploadImageRootDirectory_Key, Default_UploadImageRootDirectory);
            }
        }

        public static long SuperAdminId 
        {
            get 
            {
                return long.Parse(GetKeyValue(SuperAdminId_Key, Default_SuperAdminId));
            }
        }

        public static int NewJoineesWithInDays 
        {
            get 
            {
                return int.Parse(GetKeyValue(NewJoineesWithInDays_Key, Default_NewJoineesWithInDays));
            }
        }

        public static long ITAdminRoleId 
        {
            get 
            {
                return long.Parse(GetKeyValue(ITAdminRoleId_Key, Default_ITAdminRoleId));
            }
        }

        public static int DashBoardNewsCount 
        {
            get 
            {
                return int.Parse(GetKeyValue(DashBoardNewsCount_Key, Default_DashBoardNewsCount));
            }
        }

        public static int DashBoardJoineeCount 
        {
            get 
            {
                return int.Parse(GetKeyValue(DashBoardJoineeCount_Key, Default_DashBoardJoineeCount));
            }
        }

        #endregion

        #region private method

        private static string GetKeyValue(string KeyName, string defautValue)
        {
            string keyValue = string.Empty;

            try
            {
                keyValue = ConfigurationManager.AppSettings[KeyName];
            }
            catch
            {
                keyValue = defautValue;
            }

            return keyValue;

        }

        #endregion
    }
}
