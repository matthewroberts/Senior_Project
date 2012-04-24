using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EMREC.Core
{
    public class WebConfig
    {
        private static string GetConnectionString(string connectionStringName)
        {
            //Check if the connection string exists
            if (ConfigurationManager.ConnectionStrings[connectionStringName] == null)
                throw new ArgumentNullException(connectionStringName, connectionStringName + " does not exist in web.config");

            //Check if the connection string is null or empty
            if (String.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString))
                throw new ArgumentNullException(connectionStringName, connectionStringName + " is null or empty in web.config");

            return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public static string EMRECConnectionString
        {
            get { return GetConnectionString("EMRECConnectionString"); }
        }
    }
}
