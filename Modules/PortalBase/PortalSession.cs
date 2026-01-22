using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

using ZadHolding.Been;

namespace ZadHolding.PortalBase
{
    public class PortalSession
    {
        public static UserInfo User 
        {
            set 
            {
                HttpContext.Current.Session["UserInfo"] = value;
            }

            get 
            {
                if (HttpContext.Current.Session["UserInfo"] != null) 
                {
                    return (UserInfo)HttpContext.Current.Session["UserInfo"];
                }

                return null;
            }
        }
    }
}
