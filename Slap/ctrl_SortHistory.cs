using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;

namespace Slap
{
    public partial class ctrl_SortHistory : UserControl
    {
        private string FolderName = "";
        private string ParcelListFilePath = "", RouteListFilePath = "", FloorPlanFilePath = "", SortPlanFilePath = "";
        private int FolderRowIndex = -1;

        public ctrl_SortHistory()
        {
            InitializeComponent();
            Reset();
        }

        // reset
        private void Reset()
        {
            FolderName = "";
            ParcelListFilePath = "";
            RouteListFilePath = "";
            FloorPlanFilePath = "";
            SortPlanFilePath = "";

            FolderRowIndex = -1;

            btn_Download.Enabled = false;
            pb_DL_ParcelList.Enabled = false;
            pb_DL_RouteList.Enabled = false;
            pb_DL_FloorPlan.Enabled = false;
            pb_DL_SortPlan.Enabled = false;
            pb_DL_ZipFiles.Enabled = false;

            btn_Download.BackColor = Color.Gray;
            pb_DL_ParcelList.Image = Properties.Resources.fileGray;
            pb_DL_RouteList.Image = Properties.Resources.fileGray;
            pb_DL_FloorPlan.Image = Properties.Resources.fileGray;
            pb_DL_SortPlan.Image = Properties.Resources.fileGray;
            pb_DL_ZipFiles.Image = Properties.Resources.zipGray;
        }

        private void EnableViewFile()
        {
            pb_DL_ParcelList.Enabled = true;
            pb_DL_RouteList.Enabled = true;
            pb_DL_FloorPlan.Enabled = true;
            pb_DL_SortPlan.Enabled = true;
            pb_DL_ZipFiles.Enabled = true;

            pb_DL_ParcelList.Image = Properties.Resources.fileOrange;
            pb_DL_RouteList.Image = Properties.Resources.fileOrange;
            pb_DL_FloorPlan.Image = Properties.Resources.fileOrange;
            pb_DL_SortPlan.Image = Properties.Resources.fileOrange;
            pb_DL_ZipFiles.Image = Properties.Resources.zipOrange;
        }

        // Download and Clear buttons
        private void pb_Search_Click(object sender, EventArgs e)
        {
            Reset();

            string folderName = dtp_SearchDate.Value.ToString("yyyyMMdd") + "_Sort";

            string pageToken = null;

            var request = GoogleDrive.GetDriveService().Files.List();

            //This is a query, to search for name of files/folder.
            request.Q = "name contains '" + folderName + "' and mimeType ='application/vnd.google-apps.folder'";
            request.Fields = "nextPageToken, files(name)";
            request.PageToken = pageToken;

            var result = request.Execute();

            if (result.Files.Count > 0)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Folder Name");

                foreach (Google.Apis.Drive.v3.Data.File file in result.Files)
                {
                    DataRow dr = dt.NewRow();
                    dr["Folder Name"] = file.Name;

                    dt.Rows.Add(dr);
                }

                dgv_FileData.DataSource = dt;
                dgv_FileData.RowHeadersVisible = false;
                dgv_FileData.Columns[0].Width = dgv_FileData.Width;
            }
            else
            {
                dgv_FileData.DataSource = null;
                Reset();
            }
        }

        private void dgv_Cell_Click(object sender, DataGridViewCellEventArgs e)
        {
            FolderRowIndex = e.RowIndex;
            btn_Download.BackColor = Color.FromArgb(255, 128, 0);
            btn_Download.Enabled = true;
        }

        private void btn_Download_MouseDown(object sender, MouseEventArgs e)
        {
            if (FolderRowIndex != -1)
            {
                FolderName = dgv_FileData.Rows[FolderRowIndex].Cells[0].Value.ToString();
                List<string> filePaths = GoogleDrive.DownloadFiles(FolderName);

                foreach(var filePath in filePaths)
                {
                    if (filePath.Contains("ParcelList"))
                    {
                        ParcelListFilePath = filePath;
                    }
                    else if (filePath.Contains("RouteList"))
                    {
                        RouteListFilePath = filePath;
                    }
                    else if (filePath.Contains("FloorPlan"))
                    {
                        FloorPlanFilePath = filePath;
                    }
                    else if (filePath.Contains("SortPlan"))
                    {
                        SortPlanFilePath = filePath;
                    }
                }

                EnableViewFile();
            }
        }

        private void btn_Clear_MouseDown(object sender, MouseEventArgs e)
        {
            Reset();
            dgv_FileData.DataSource = null;
        }

        // On Click functions for Individual File Download
        private void pb_DL_ParcelList_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_ParcelList.Image = Properties.Resources.fileLightOrange;
            System.Diagnostics.Process.Start(ParcelListFilePath);
        }

        private void pb_DL_RouteList_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_RouteList.Image = Properties.Resources.fileLightOrange;
            System.Diagnostics.Process.Start(RouteListFilePath);
        }

        private void pb_DL_FloorPlan_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_FloorPlan.Image = Properties.Resources.fileLightOrange;
            System.Diagnostics.Process.Start(FloorPlanFilePath);
        }

        private void pb_DL_SortPlan_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_SortPlan.Image = Properties.Resources.fileLightOrange;
            System.Diagnostics.Process.Start(SortPlanFilePath);
        }

        private void pb_DL_ParcelList_MouseUp(object sender, MouseEventArgs e)
        {
            pb_DL_ParcelList.Image = Properties.Resources.fileOrange;
        }

        private void pb_DL_ZipFiles_Click(object sender, EventArgs e)
        {
            // Create and open a new ZIP file
            string DownloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
            FolderName += ".zip";
            string zipFolderName = FolderName;

            int i = 0;
            while (System.IO.File.Exists(Path.Combine(DownloadsPath, zipFolderName)))
            {
                i++;
                zipFolderName = Path.GetFileNameWithoutExtension(FolderName) + "(" + i + ")" + Path.GetExtension(FolderName);
            }
            zipFolderName = Path.Combine(DownloadsPath, zipFolderName);

            var zip = ZipFile.Open(zipFolderName, ZipArchiveMode.Create);

            if (File.Exists(ParcelListFilePath))
            {
                zip.CreateEntryFromFile(ParcelListFilePath, Path.GetFileName(ParcelListFilePath), CompressionLevel.Optimal);
                File.Delete(ParcelListFilePath);
            }
            if (File.Exists(RouteListFilePath))
            {
                zip.CreateEntryFromFile(FloorPlanFilePath, Path.GetFileName(RouteListFilePath), CompressionLevel.Optimal);
                File.Delete(RouteListFilePath);
            }
            if (File.Exists(FloorPlanFilePath))
            {
                zip.CreateEntryFromFile(FloorPlanFilePath, Path.GetFileName(FloorPlanFilePath), CompressionLevel.Optimal);
                File.Delete(FloorPlanFilePath);
            }
            if (File.Exists(SortPlanFilePath))
            {
                zip.CreateEntryFromFile(SortPlanFilePath, Path.GetFileName(SortPlanFilePath), CompressionLevel.Optimal);
                File.Delete(SortPlanFilePath);
            }

            // Dispose of the object when we are done
            zip.Dispose();

            Reset();
        }

        private void pb_DL_RouteList_MouseUp(object sender, MouseEventArgs e)
        {
            pb_DL_RouteList.Image = Properties.Resources.fileOrange;
        }

        private void pb_DL_FloorPlan_MouseUp(object sender, MouseEventArgs e)
        {
            pb_DL_FloorPlan.Image = Properties.Resources.fileOrange;
        }

        private void pb_DL_SortPlan_MouseUp(object sender, MouseEventArgs e)
        {
            pb_DL_SortPlan.Image = Properties.Resources.fileOrange;
        }
    }
}
