using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Slap
{
    public partial class ctrl_SortHistory : UserControl
    {
        private bool sortLoaded = false;

        public ctrl_SortHistory()
        {
            InitializeComponent();
            Reset();
        }

        // reset
        private void Reset()
        {
            sortLoaded = false;

            btn_Download.Enabled = false;
            
            pb_DL_ParcelList.Enabled = false;
            pb_DL_RouteList.Enabled = false;
            pb_DL_FloorPlan.Enabled = false;
            pb_DL_SortPlan.Enabled = false;

            pb_DL_ParcelList.Image = Properties.Resources.fileGray;
            pb_DL_RouteList.Image = Properties.Resources.fileGray;
            pb_DL_FloorPlan.Image = Properties.Resources.fileGray;
            pb_DL_SortPlan.Image = Properties.Resources.fileGray;
        }

        private void EnabledViewFile()
        {
            sortLoaded = true;

            btn_Download.Enabled = true;

            pb_DL_ParcelList.Enabled = true;
            pb_DL_RouteList.Enabled = true;
            pb_DL_FloorPlan.Enabled = true;
            pb_DL_SortPlan.Enabled = true;

            pb_DL_ParcelList.Image = Properties.Resources.fileOrange;
            pb_DL_RouteList.Image = Properties.Resources.fileOrange;
            pb_DL_FloorPlan.Image = Properties.Resources.fileOrange;
            pb_DL_SortPlan.Image = Properties.Resources.fileOrange;
        }

        // Download and Clear buttons
        private void pb_Search_Click(object sender, EventArgs e)
        {
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
        }

        private void dgv_Cell_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                GoogleDrive.DownloadFiles(dgv_FileData.Rows[e.RowIndex].Cells[0].Value.ToString());
                MessageBox.Show(dgv_FileData.Rows[e.RowIndex].Cells[0].Value.ToString());
                EnabledViewFile();
            }

        }

        private void btn_Download_MouseDown(object sender, MouseEventArgs e)
        {
            // . . .
            GoogleDrive.DownloadFiles(""); 
        }

        private void btn_Clear_MouseDown(object sender, MouseEventArgs e)
        {
            Reset();
        }

        // On Click functions for Individual File Download
        private void pb_DL_ParcelList_MouseDown(object sender, MouseEventArgs e)
        {
            if (sortLoaded)
            {
                pb_DL_ParcelList.Image = Properties.Resources.fileLightOrange;
            }
        }

        private void pb_DL_RouteList_MouseDown(object sender, MouseEventArgs e)
        {
            if (sortLoaded)
            {
                pb_DL_RouteList.Image = Properties.Resources.fileLightOrange;
            }

        }

        private void pb_DL_FloorPlan_MouseDown(object sender, MouseEventArgs e)
        {
            if (sortLoaded)
            {
                pb_DL_FloorPlan.Image = Properties.Resources.fileLightOrange;
            }
        }

        private void pb_DL_SortPlan_MouseDown(object sender, MouseEventArgs e)
        {
            if (sortLoaded)
            {
                pb_DL_SortPlan.Image = Properties.Resources.fileLightOrange;
            }
        }

        private void pb_DL_ParcelList_MouseUp(object sender, MouseEventArgs e)
        {
            if (sortLoaded)
            {
                pb_DL_ParcelList.Image = Properties.Resources.fileOrange;
            }
        }

        private void pb_DL_RouteList_MouseUp(object sender, MouseEventArgs e)
        {
            if (sortLoaded)
            {
                pb_DL_RouteList.Image = Properties.Resources.fileOrange;
            }
        }

        private void pb_DL_FloorPlan_MouseUp(object sender, MouseEventArgs e)
        {
            if (sortLoaded)
            {
                pb_DL_FloorPlan.Image = Properties.Resources.fileOrange;
            }
        }

        private void pb_DL_SortPlan_MouseUp(object sender, MouseEventArgs e)
        {
            if (sortLoaded)
            {
                pb_DL_SortPlan.Image = Properties.Resources.fileOrange;
            }
        }
    }
}
