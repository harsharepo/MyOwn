using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;
using System.Web.Optimization;

namespace VedicaAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            WebSecurity.InitializeDatabaseConnection("VedicaAyurveda", "Users", "UserId", "EmailAddress", autoCreateTables: true);

            if (!Roles.RoleExists(System.Configuration.ConfigurationManager.AppSettings["AdminRole"].ToString()))
            {
                Roles.CreateRole(System.Configuration.ConfigurationManager.AppSettings["AdminRole"].ToString());
            }
            if (!Roles.RoleExists(System.Configuration.ConfigurationManager.AppSettings["SalesRole"].ToString()))
            {
                Roles.CreateRole(System.Configuration.ConfigurationManager.AppSettings["SalesRole"].ToString());
            }
            if (Membership.GetUser(System.Configuration.ConfigurationManager.AppSettings["AdminUserName"].ToString()) == null)
            {
                WebSecurity.CreateUserAndAccount(System.Configuration.ConfigurationManager.AppSettings["AdminUserName"].ToString(), System.Configuration.ConfigurationManager.AppSettings["AdminPassword"].ToString(), new { CreatedDate = DateTime.Now, IsDeleted = "N" });
            }
            if (Membership.GetUser(System.Configuration.ConfigurationManager.AppSettings["SalesUserName"].ToString()) == null)
            {
                WebSecurity.CreateUserAndAccount(System.Configuration.ConfigurationManager.AppSettings["SalesUserName"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SalesPassword"].ToString(), new { CreatedDate = DateTime.Now, IsDeleted = "N" });
            }
            if (!Roles.IsUserInRole(System.Configuration.ConfigurationManager.AppSettings["AdminUserName"].ToString(), System.Configuration.ConfigurationManager.AppSettings["AdminRole"].ToString()))
            {
                Roles.AddUserToRole(System.Configuration.ConfigurationManager.AppSettings["AdminUserName"].ToString(), System.Configuration.ConfigurationManager.AppSettings["AdminRole"].ToString());
            }
            if (!Roles.IsUserInRole(System.Configuration.ConfigurationManager.AppSettings["SalesUserName"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SalesRole"].ToString()))
            {
                Roles.AddUserToRole(System.Configuration.ConfigurationManager.AppSettings["SalesUserName"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SalesRole"].ToString());
            }
        }
    }
}
