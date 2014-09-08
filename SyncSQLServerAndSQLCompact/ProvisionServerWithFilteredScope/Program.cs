using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace ProvisionServerWithFilteredScope
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection serverConn = new SqlConnection(@"Data Source=.\SQL2008; Initial Catalog=SyncDB; Integrated Security=true");
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("OrdersScope-NC");
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Orders", serverConn);
            scopeDesc.Tables.Add(tableDesc);
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);
            serverProvision.Tables["Orders"].AddFilterColumn("OriginState");
            serverProvision.Tables["Orders"].FilterClause = "[side].[OriginState] = 'NC'";
            serverProvision.Apply();
        }
    }
}
