using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data.SqlServerCe;

using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data.SqlServerCe;

namespace ProvisionClient
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlCeConnection clientConn = new SqlCeConnection(@"Data Source='C:\dev\SyncTest\SyncSQLServerAndSQLCompact\data\SyncCompactDB.sdf'");
            SqlConnection serverConn = new SqlConnection(@"Data Source=.\SQL2008; Initial Catalog=SyncDB; Integrated Security=True");
            DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope("ProductsScope", serverConn);
            SqlCeSyncScopeProvisioning clientProvision = new SqlCeSyncScopeProvisioning(clientConn, scopeDesc);
            clientProvision.Apply();


        }
    }
}
