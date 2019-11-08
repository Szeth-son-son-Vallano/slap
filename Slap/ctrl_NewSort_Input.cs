using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Slap
{
    public partial class ctrl_NewSort_Input : UserControl
    {
        private bool parcelListReady = false;
        private bool routeListReady = false;
        private string parcelFilePath = "", routeFilePath = "";
        private string[] ParcelData, RouteData;

        public ctrl_NewSort_Input()
        {
            InitializeComponent();
            pb_DND_ParcelList.AllowDrop = true;
            pb_DND_RouteList.AllowDrop = true;
            Reset();

            ctrl_NewSort_Output1.Hide();
        }

        // reset function
        private void Reset()
        {
            // reset UI display
            lbl_ErrorMessage.Text = "";

            pb_DND_ParcelList.Image = Properties.Resources.fileGrayFrame;
            lbl_ParcelListFile.Text = "Drag and Drop";
            pb_DND_RouteList.Image = Properties.Resources.fileGrayFrame;
            lbl_RouteListFile.Text = "Drag and Drop";

            // reset parcel data and route data
            parcelListReady = false;
            routeListReady = false;
            ParcelData = null;
            RouteData = null;
        }

        // Functions to Load files (Drag and Drop and Open File Dialog)
        private void checkParcelListFileType(string[] fileData)
        {
            if (fileData.Length > 0)
            {
                parcelFilePath = fileData[0];
                string parcelFileName = Path.GetFileName(parcelFilePath);

                string fileType = System.IO.Path.GetExtension(parcelFileName);

                if (fileType.ToLower().Equals(".csv"))
                {
                    parcelListReady = true;
                    ParcelData = fileData;
                    lbl_ParcelListFile.Text = parcelFileName;
                    pb_DND_ParcelList.Image = Properties.Resources.filePurpleFrame;
                }
                else
                {
                    parcelListReady = false;
                    lbl_ParcelListFile.Text = "File type not accepted";
                    pb_DND_ParcelList.Image = Properties.Resources.fileRedFrame;
                }
            }
        }

        private void checkRouteListFileType(string[] fileData)
        {
            if (fileData.Length > 0)
            {
                routeFilePath = fileData[0];
                string routeFileName = Path.GetFileName(routeFilePath);

                string fileType = System.IO.Path.GetExtension(routeFileName);

                if (fileType.ToLower().Equals(".csv"))
                {
                    routeListReady = true;
                    RouteData = fileData;
                    lbl_RouteListFile.Text = routeFileName;
                    pb_DND_RouteList.Image = Properties.Resources.filePurpleFrame;
                }
                else
                {
                    routeListReady = false;
                    lbl_RouteListFile.Text = "File type not accepted";
                    pb_DND_RouteList.Image = Properties.Resources.fileRedFrame;
                }
            }
        }

        private void pb_DND_ParcelList_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];
            checkParcelListFileType(data);
        }

        private void pb_DND_RouteList_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];
            checkRouteListFileType(data);
        }

        private void pb_DND_ParcelList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pb_DND_RouteList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pb_DND_ParcelList_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] fileData = new string[1];
                fileData[0] = openFileDialog1.FileName;

                checkParcelListFileType(fileData);
            }
        }

        private void pb_DND_RouteList_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] fileData = new string[1];
                fileData[0] = openFileDialog1.FileName;

                checkRouteListFileType(fileData);
            }
        }

        // Read and Clear buttons
        private void btn_Read_MouseDown(object sender, MouseEventArgs e)
        {
            if (parcelListReady && routeListReady)
            {
                lbl_ErrorMessage.Text = "";

                ctrl_NewSort_Output1.AddData(parcelFilePath, routeFilePath, ParcelData, RouteData);
                bool successfulReadParcels = ctrl_NewSort_Output1.ReadParcels();
                bool successfulReadRoutes = ctrl_NewSort_Output1.ReadRoutes();
                if (successfulReadParcels && successfulReadRoutes)
                {
                    ctrl_NewSort_Output1.Show();
                    Reset();
                }
                else
                {
                    lbl_ErrorMessage.Text = "Please close the loaded files to allow for file reading";
                }
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
