using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using System.Linq;

namespace Slap
{
    public partial class ctrl_SortHistory : UserControl
    {
        private string ImplicitDownloadLocation;
        private string FolderName = "";
        private string ParcelListFilePath = "", RouteListFilePath = "", FloorPlanFilePath = "", SortPlanFilePath = "";
        private int FolderRowIndex = -1;

        public ctrl_SortHistory()
        {
            InitializeComponent();
            Reset();

            SetDownloadLocation();
        }

        private void SetDownloadLocation()
        {
            ImplicitDownloadLocation = Properties.Settings.Default.DownloadLocation;

            if (ImplicitDownloadLocation == null || ImplicitDownloadLocation.Equals(""))
            {
                ImplicitDownloadLocation = KnownFolders.GetPath(KnownFolder.Downloads);
                Properties.Settings.Default.DownloadLocation = ImplicitDownloadLocation;
                Properties.Settings.Default.Save();
            }
            txt_DownloadLocation.Text = ImplicitDownloadLocation;
        }

        // reset
        private void Reset()
        {
            // reset parcel list, route list, floor plan and sort plan data
            FolderName = "";
            ParcelListFilePath = "";
            RouteListFilePath = "";
            FloorPlanFilePath = "";
            SortPlanFilePath = "";

            FolderRowIndex = -1;

            // reset UI display
            lbl_Message.Text = "";
            btn_Download.Enabled = false;
            pb_DL_ParcelList.Enabled = false;
            pb_DL_RouteList.Enabled = false;
            pb_DL_FloorPlan.Enabled = false;
            pb_DL_SortPlan.Enabled = false;
            pb_DL_ZipFiles.Enabled = false;

            lbl_Message.ForeColor = Color.Red;
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

        // Download and Clear functions
        private void pb_UpdateDownloadLocation_Click(object sender, EventArgs e)
        {
            string ExplicitDownloadLocation = txt_DownloadLocation.Text;
            bool validDownloadLocation = false;

            if (ExplicitDownloadLocation != null || !ExplicitDownloadLocation.Equals(""))
            {
                if (Directory.Exists(ExplicitDownloadLocation))
                {
                    MessageBox.Show("Download Location has been updated");
                    Properties.Settings.Default.DownloadLocation = ExplicitDownloadLocation;
                    Properties.Settings.Default.Save();

                    validDownloadLocation = true;
                }
            }

            if (validDownloadLocation)
            {
                txt_DownloadLocation.Text = ExplicitDownloadLocation;
                ImplicitDownloadLocation = ExplicitDownloadLocation;
            }
            else
            {
                //ImplicitDownloadLocation = KnownFolders.GetPath(KnownFolder.Downloads);
                txt_DownloadLocation.Text = ImplicitDownloadLocation;
            }
        }

        private void pb_Search_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception exception)
            {
                lbl_Message.Text = "Search failed.";
                lbl_Message.ForeColor = Color.Red;
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
            try
            {
                if (FolderRowIndex != -1)
                {
                    lbl_Message.Text = "Download in progress . . .";
                    lbl_Message.ForeColor = Color.Red;

                    FolderName = dgv_FileData.Rows[FolderRowIndex].Cells[0].Value.ToString();
                    List<string> filePaths = GoogleDrive.DownloadFiles(FolderName);

                    if (filePaths != null)
                    {
                        lbl_Message.Text = "Download complete";
                        lbl_Message.ForeColor = Color.Green;

                        foreach (var filePath in filePaths)
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
                    else
                    {
                        lbl_Message.Text = "Download failed";
                        lbl_Message.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                lbl_Message.Text = "Download failed";
                lbl_Message.ForeColor = Color.Red;
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
            try
            {
                pb_DL_ParcelList.Image = Properties.Resources.fileLightOrange;
                System.Diagnostics.Process.Start(ParcelListFilePath);
            }
            catch (Exception exception)
            {
                lbl_Message.Text = "View file failed";
                lbl_Message.ForeColor = Color.Red;
            }
        }

        private void pb_DL_RouteList_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                pb_DL_RouteList.Image = Properties.Resources.fileLightOrange;
                System.Diagnostics.Process.Start(RouteListFilePath);
            }
            catch (Exception exception)
            {
                lbl_Message.Text = "View file failed";
                lbl_Message.ForeColor = Color.Red;
            }
        }

        private void pb_DL_FloorPlan_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                pb_DL_FloorPlan.Image = Properties.Resources.fileLightOrange;
                System.Diagnostics.Process.Start(FloorPlanFilePath);
            }
            catch (Exception exception)
            {
                lbl_Message.Text = "View file failed";
                lbl_Message.ForeColor = Color.Red;
            }
}

        private void pb_DL_SortPlan_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                pb_DL_SortPlan.Image = Properties.Resources.fileLightOrange;
                System.Diagnostics.Process.Start(SortPlanFilePath);
            }
            catch (Exception exception)
            {
                lbl_Message.Text = "View file failed";
                lbl_Message.ForeColor = Color.Red;
            }
        }

        private void pb_DL_ZipFiles_Click(object sender, EventArgs e)
        {
            try
            {
                // Create and open a new ZIP file
                string DownloadsPath = Properties.Settings.Default.DownloadLocation;
                if (Directory.EnumerateFileSystemEntries(Path.Combine(DownloadsPath, FolderName)).Any())
                {
                    string folderName = FolderName + ".zip";
                    string zipFolderName = folderName;

                    int i = 0;
                    while (System.IO.File.Exists(Path.Combine(DownloadsPath, zipFolderName)))
                    {
                        i++;
                        zipFolderName = Path.GetFileNameWithoutExtension(folderName) + "(" + i + ")" + Path.GetExtension(folderName);
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

                    // Remove empty folders
                    string removeFolderName = Path.Combine(DownloadsPath, Path.GetFileNameWithoutExtension(FolderName));
                    if (!Directory.EnumerateFileSystemEntries(removeFolderName).Any())
                    {
                        Directory.Delete(removeFolderName);
                    }

                    Reset();

                    lbl_Message.Text = "Zip complete";
                    lbl_Message.ForeColor = Color.Green;
                }
                else
                {
                    lbl_Message.Text = "Folder is empty";
                    lbl_Message.ForeColor = Color.Red;
                }
            }
            catch (Exception exception)
            {
                lbl_Message.Text = "Zip failed";
                lbl_Message.ForeColor = Color.Red;

                Console.WriteLine(exception.Message);
            }
        }

        private void txt_DownloadLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                pb_UpdateDownloadLocation_Click(null, e);
            }
        }

        private void dtp_SearchDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                pb_Search_Click(null, e);
            }
        }

        private void pb_DL_ParcelList_MouseUp(object sender, MouseEventArgs e)
        {
            pb_DL_ParcelList.Image = Properties.Resources.fileOrange;
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
