using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data.SqlServerCe;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data;

namespace ExecuteSecondCompactSync
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection serverConn = new SqlConnection(@"Data Source=.\SQL2008; Initial Catalog=SyncDB; Integrated Security=true");
            SqlCeConnection clientConn = new SqlCeConnection(@"Data Source='C:\dev\SyncTest\SyncSQLServerAndSQLCompact\data\SyncCompactDB2.sdf'");

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
