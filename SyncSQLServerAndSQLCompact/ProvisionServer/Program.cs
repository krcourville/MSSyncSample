using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace ProvisionServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection serverConn = new SqlConnection(@"Data Source=.\SQL2008; Initial Catalog=SyncDB; Integrated Security=true");
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("ProductsScope");
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Products", serverConn);
            scopeDesc.Tables.Add(tableDesc);
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);
            serverProvision.Apply();
        }
    }
}
