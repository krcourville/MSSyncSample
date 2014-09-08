using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Microsoft.Synchronization.Data.SqlServerCe;

namespace ProvisionSecondCompactClient
{
    class Program
    {
        static void Main(string[] args)
        {

            // connect to the first compact client DB
            SqlCeConnection clientConn = new SqlCeConnection(@"Data Source='C:\dev\SyncTest\SyncSQLServerAndSQLCompact\data\SyncCompactDB.sdf'");

            // create a snapshot of SyncCompactDB and save it to SyncCompactDB2 database
            SqlCeSyncStoreSnapshotInitialization syncStoreSnapshot = new SqlCeSyncStoreSnapshotInitialization();
            syncStoreSnapshot.GenerateSnapshot(clientConn, @"C:\dev\SyncTest\SyncSQLServerAndSQLCompact\data\SyncCompactDB2.sdf");
        }
    }
}
