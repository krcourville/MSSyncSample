using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data.SqlServerCe;

namespace SyncSQLServerAndSQLCompact
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlCeConnection clientConn = new SqlCeConnection(@"Data Source='C:\dev\SyncTest\SyncSQLServerAndSQLCompact\data\SyncCompactDB.sdf'");
            SqlConnection serverConn = new SqlConnection(@"Data Source=.\SQL2008; Initial Catalog=SyncDB; Integrated Security=True");
            SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
            syncOrchestrator.LocalProvider = new SqlCeSyncProvider("ProductsScope", clientConn);
            syncOrchestrator.RemoteProvider = new SqlSyncProvider("ProductsScope", serverConn);
            syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
            ((SqlCeSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
            SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();

            Console.WriteLine("Start Time: " + syncStats.SyncStartTime);
            Console.WriteLine("Total Changes Uploaded: " + syncStats.UploadChangesTotal);
            Console.WriteLine("Total Changes Downloaded: " + syncStats.DownloadChangesTotal);
            Console.WriteLine("Complete Time: " + syncStats.SyncEndTime);
            Console.WriteLine(String.Empty);
        }

        static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            // display conflict type
            Console.WriteLine(e.Conflict.Type);

            // display error message 
            Console.WriteLine(e.Error);
        }
    }
}
