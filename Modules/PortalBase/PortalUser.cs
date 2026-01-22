using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Security.Principal;

using ZadHolding.Been;
using ZadHolding.Business;

namespace ZadHolding.PortalBase
{
    public class PortalUser
    {
        private const string CookieForUserName = "ZadHolding.UserInfo";
        private const int CookieForUserName_Exipre_Days = 15;
        private const int UserID_Position = 0;
        private const int UserName_Position = 1;
        private const int Name_Position = 2;
        private const int Role_Position = 3;
        private const int DivisionId_Position = 4;
        private const int DivisionName_Position = 5;
        private const int DepartmentId_Position = 6;
        private const int DepartmentName_Position = 7;

        // Methods
        public void Authenticate(UserInfo userInfo, int timeOut, bool isCookiePersistent)
        {
            string userData = string.Format("{0}|{1}|{2} {3}|{4}|{5}|{6}|{7}|{8}",
                userInfo.Id,
                userInfo.UserName,
                userInfo.FirstName,
                userInfo.LastName,
                userInfo.RoleId,
                userInfo.DivisionId,
                userInfo.DivisionName,
                userInfo.DepartmentId,
                userInfo.DepartmentName
                );

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
                userInfo.UserName,
                DateTime.Now,
                DateTime.Now.AddMinutes((double)timeOut),
                isCookiePersistent, userData);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            if (isCookiePersistent)
                authCookie.Expires = authTicket.Expiration;

            HttpContext.Current.Response.Cookies.Add(authCookie);

            FormsIdentity identity = new FormsIdentity(authTicket);
            UserRole userRole = (UserRole)userInfo.RoleId;
            HttpContext.Current.User = new GenericPrincipal(identity, new string[] { userRole.ToString() });
        }

        public void Authorize()
        {
            if (HttpContext.Current.User != null &&
                HttpContext.Current.User.Identity.IsAuthenticated &&
                HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = identity.Ticket;
                string userData = ticket.UserData;
                string roleId = ticket.UserData.Split(new char[] { '|' })[Role_Position];
                UserRole userRole = (UserRole)int.Parse(roleId);
                HttpContext.Current.User = new GenericPrincipal(identity, new string[] { userRole.ToString() });
            }
        }

        public string GetHomeUrl()
        {
            return this.GetHomeUrl(this.Role);
        }

        private string GetHomeUrl(UserRole role)
        {
            string homeUrl = string.Empty;

            switch (role)
            {
                case UserRole.SuperAdmin:
                    homeUrl = URLInformation.SuperAdmin;
                    break;
                case UserRole.Viewer:
                    homeUrl = URLInformation.Viewer;
                    break;
                case UserRole.Manager:
                    homeUrl = URLInformation.Manager;
                    break;
                case UserRole.Operator:
                    homeUrl = URLInformation.Operator;
                    break;
                case UserRole.Supervisor:
                    homeUrl = URLInformation.Supervisor;
                    break;
                case UserRole.Management:
                    homeUrl = URLInformation.Management;
                    break;
                case UserRole.Ceo:
                    homeUrl = URLInformation.Ceo;
                    break;
                default:
                    homeUrl = URLInformation.Viewer;
                    break;
            }

            return homeUrl;
        }

        public static string GetLastLoggedInUserName()
        {
            string str = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("CookieForUserName");

            if (cookie != null)
            {
                str = cookie.Values["UserName"].ToString();
            }

            return str;
        }

        public static string GetLoginUrl(string returnURL)
        {
            string str = "~/Login.aspx";

            if (!string.IsNullOrEmpty(returnURL))
            {
                str = str + "?ReturnUrl=" + HttpUtility.UrlEncode(returnURL);
            }

            return str;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        // Properties
        public static PortalUser Current
        {
            get
            {
                return new PortalUser();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return ((HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated);
            }
        }

        public string Name
        {
            get
            {
                string str = string.Empty;

                if ((HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                    str = identity.Ticket.UserData.Split(new char[] { '|' })[Name_Position].Split(new char[] { ',' })[0];
                }

                return str;
            }
        }

        public UserRole Role
        {
            get
            {
                long roleId = 0L;
                UserRole userRole = UserRole.Viewer;

                if ((HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

                    long.TryParse(identity.Ticket.UserData.Split(new char[] { '|' })[Role_Position], out roleId);

                    userRole = (UserRole)roleId;
                }

                return userRole;
            }
        }

        public long UserId
        {
            get
            {
                long result = 0L;
                if ((HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

                    long.TryParse(identity.Ticket.UserData.Split(new char[] { '|' })[UserID_Position], out result);
                }

                return result;
            }
        }

        public string UserName
        {
            get
            {
                string name = string.Empty;
                if ((HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    name = HttpContext.Current.User.Identity.Name;
                }

                return name;
            }
        }

        public long DivisionId
        {
            get
            {
                long result = 0L;
                if ((HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

                    long.TryParse(identity.Ticket.UserData.Split(new char[] { '|' })[DivisionId_Position], out result);
                }

                return result;
            }
        }

        public long DepartmentId
        {
            get
            {
                long result = 0L;
                if ((HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

                    long.TryParse(identity.Ticket.UserData.Split(new char[] { '|' })[DepartmentId_Position], out result);
                }

                return result;
            }
        }

        public string DivisionName
        {
            get
            {
                string str = string.Empty;
                if ((HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

                    //long.TryParse(identity.Ticket.UserData.Split(new char[] { '|' })[DivisionId_Position], out result);

                    str = identity.Ticket.UserData.Split(new char[] { '|' })[DivisionName_Position];
                    if (str == "")
                    {
                        str = "All";
                    }
                }

                return str;
            }
        }

        public string DepartmentName
        {
            get
            {
                string str = string.Empty;
                if ((HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

                    //long.TryParse(identity.Ticket.UserData.Split(new char[] { '|' })[DivisionId_Position], out result);

                    str = identity.Ticket.UserData.Split(new char[] { '|' })[DepartmentName_Position];
                    if (str == "")
                    {
                        str = "All";
                    }
                }

                return str;
            }
        }
    }
}
