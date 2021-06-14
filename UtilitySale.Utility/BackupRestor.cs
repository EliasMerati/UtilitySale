using System;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilitySale.Utility
{
   public class BackupRestor : IDisposable
    {
        
        private string BackUpConString = @"data source =.\SQLEXPRESS;Initial Catalog =UtilitySale_DB; Integrated Security = True; MultipleActiveResultSets=True";//Connection String baraye Dastyabi be Data base Asli
        private string ReStoreConString = @"data source =.\SQLEXPRESS;Initial Catalog =master;Integrated Security=True";//Connection String baraye dastresi be data base Master


        //------------------------------------------------------------

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }



        //-------------------------------------------------------------
        public void ReStorMyDB()
        {

            if (MessageBox.Show("All Data Stored in the Database may change!!! \n If you agree, select \"Yes\".", "DataBase ReStore", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                SqlConnection.ClearAllPools();
                using (SqlConnection con = new SqlConnection(ReStoreConString))
                {
                    ServerConnection srvConn = new ServerConnection(con);
                    Server srvr = new Server(srvConn);

                    if (srvr != null)
                    {
                        try
                        {

                            Restore rstDatabase = new Restore();
                            rstDatabase.Action = RestoreActionType.Database;
                            rstDatabase.Database = "UtilitySale_DB";//Bayad ham nam ba Data base barname tanzim shavad
                            OpenFileDialog opfd = new OpenFileDialog();
                            opfd.Filter = "BackUp File|*.araDB";
                            if (opfd.ShowDialog() == true)
                            {


                                BackupDeviceItem bkpDevice = new BackupDeviceItem(opfd.FileName, DeviceType.File);

                                rstDatabase.Devices.Add(bkpDevice);
                                rstDatabase.ReplaceDatabase = true;
                                rstDatabase.SqlRestore(srvr);
                                MessageBox.Show("Database succefully restored", "Server", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("ERROR: An error ocurred while restoring the database", "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

            }
        }

        public void BackUpMyDB()
        {
            using (SqlConnection con = new SqlConnection(BackUpConString))
            {
                ServerConnection srvConn = new ServerConnection(con);
                Server srvr = new Server(srvConn);
                if (srvr != null)
                {
                    try
                    {
                        Backup bkpDatabase = new Backup();
                        bkpDatabase.Action = BackupActionType.Database;
                        bkpDatabase.Database = "UtilitySale_DB";//Bayad ham nam ba Data base barname tanzim shavad
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "BackUp File|*.araDB";
                        sfd.FileName = "BackUp_" + (DateTime.Now.ToShortDateString().Replace('/', '.'));
                        if (sfd.ShowDialog() == true)
                        {
                            BackupDeviceItem bkpDevice = new BackupDeviceItem(sfd.FileName, DeviceType.File);
                            bkpDatabase.Devices.Add(bkpDevice);
                            bkpDatabase.SqlBackup(srvr);
                            MessageBox.Show("Bakup of Database successfully created", "Server", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception e) { MessageBox.Show(e.ToString()); }
                }
            }
        }


    }
}

