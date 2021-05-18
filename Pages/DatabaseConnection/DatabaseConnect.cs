using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrototypeDatabase.Pages.DatabaseConnection
{// Database connection for easier callback (Not used)
    public class DatabaseConnect
    {
        public string DatabaseString ()
        {
            string DbString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = PrototypeDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            return DbString;
        }
    }
}
