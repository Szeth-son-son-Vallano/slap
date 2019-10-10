using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slap
{
    public partial class ctrl_NewSort_Input : UserControl
    {
        private Boolean parcelListReady = false;
        private Boolean routeListReady = false;

        public ctrl_NewSort_Input()
        {
            InitializeComponent();
            pb_DND_ParcelList.AllowDrop = true;
            pb_DND_RouteList.AllowDrop = true;
            Reset();
        }

        // reset
        private void Reset()
        {
            lbl_ErrorMessage.Text = "";

            pb_DND_ParcelList.Image = Properties.Resources.fileGrayFrame;
            lbl_ParcelListFile.Text = "Drag and Drop";
            pb_DND_RouteList.Image = Properties.Resources.fileGrayFrame;
            lbl_RouteListFile.Text = "Drag and Drop";
        }

        // Functions to Load files (Drag and Drop and Open File Dialog)
        private void checkParcelList(string[] fileNames)
        {
            if (fileNames.Length > 0)
            {
                char[] separator = { '\\', '/' };
                string[] strList = fileNames[0].Split(separator);
                string fileName = strList[strList.GetUpperBound(0)];

                string[] fileType = fileName.Split('.');

                if (fileType[fileType.GetUpperBound(0)] == "csv")
                {
                    lbl_ParcelListFile.Text = fileName;
                    pb_DND_ParcelList.Image = Properties.Resources.filePurpleFrame;
                    parcelListReady = true;
                }
                else
                {
                    lbl_ParcelListFile.Text = "File type not accepted";
                    pb_DND_ParcelList.Image = Properties.Resources.fileRedFrame;
                    parcelListReady = false;
                }
            }
        }

        private void checkRouteList(string[] fileNames)
        {
            if (fileNames.Length > 0)
            {
                char[] separator = { '\\', '/' };
                string[] strList = fileNames[0].Split(separator);
                string fileName = strList[strList.GetUpperBound(0)];

                string[] fileType = fileName.Split('.');

                if (fileType[fileType.GetUpperBound(0)] == "csv")
                {
                    lbl_RouteListFile.Text = fileName;
                    pb_DND_RouteList.Image = Properties.Resources.filePurpleFrame;
                    routeListReady = true;
                }
                else
                {
                    lbl_RouteListFile.Text = "File type not accepted";
                    pb_DND_RouteList.Image = Properties.Resources.fileRedFrame;
                    routeListReady = false;
                }
            }
        }

        private void pb_DND_ParcelList_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];
                checkParcelList(fileNames);
            }
        }

        private void pb_DND_RouteList_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                string[] fileNames = data as string[];
                checkRouteList(fileNames);
            }
        }

        private void pb_DND_RouteList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pb_DND_ParcelList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pb_DND_ParcelList_Click(object sender, EventArgs e)
        {
            DialogResult dialog = openFileDialog1.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                string[] fileNames = new string[1];
                fileNames[0] = openFileDialog1.FileName;

                checkParcelList(fileNames);
            }
        }

        private void pb_DND_RouteList_Click(object sender, EventArgs e)
        {
            DialogResult dialog = openFileDialog1.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                string[] fileNames = new string[1];
                fileNames[0] = openFileDialog1.FileName;

                checkRouteList(fileNames);
            }
        }

        // Sort and Clear buttons
        private void btn_Sort_MouseDown(object sender, MouseEventArgs e)
        {
            if (parcelListReady && routeListReady)
            {
                // . . .
                lbl_ErrorMessage.Text = "";
            }
            else
            {
                lbl_ErrorMessage.Text = "Make sure both files have valid file types, eg: .csv";
            }
        }

        private void btn_Clear_MouseDown(object sender, MouseEventArgs e)
        {
            Reset();
        }
    }
}
