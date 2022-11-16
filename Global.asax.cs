using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TeleAtendimentoWeb
{
    public class Global : System.Web.HttpApplication
    {
        public static string APP_NAME = "TeleAtendimento";
        public static string APP_VERSION = "0.0.1";
        protected void Application_Start(object sender, EventArgs e)
        {
        }
    }
}