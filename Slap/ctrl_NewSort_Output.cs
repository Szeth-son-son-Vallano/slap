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
    public partial class ctrl_NewSort_Output : UserControl
    {
        private string[] ParcelData, RouteData;

        public ctrl_NewSort_Output()
        {
            InitializeComponent();
            Reset();
        }

        public void addData(string[] parcelData, string[] routeData)
        {
            ParcelData = parcelData;
            RouteData = routeData;
        }

        // reset
        private void Reset()
        {
            pb_DL_ParcelList.Image = Properties.Resources.filePurple;
            pb_DL_RouteList.Image = Properties.Resources.filePurple;
        }

        // New Sort and Clear buttons
        private void btn_Download_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btn_Back_MouseDown(object sender, MouseEventArgs e)
        {
            this.Hide();
        }
        
        // On Click functions for Individual File Download
        private void pb_DL_ParcelList_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_ParcelList.Image = Properties.Resources.fileOrange;
        }

        private void pb_DL_RouteList_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_RouteList.Image = Properties.Resources.fileOrange;

        }

        private void pb_DL_FloorPlan_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_FloorPlan.Image = Properties.Resources.fileOrange;

        }

        private void pb_DL_SortPlan_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_SortPlan.Image = Properties.Resources.fileOrange;

        }

        private void pb_DL_ParcelList_MouseUp(object sender, MouseEventArgs e)
        {

            pb_DL_ParcelList.Image = Properties.Resources.filePurple;
        }

        private void pb_DL_RouteList_MouseUp(object sender, MouseEventArgs e)
        {

            pb_DL_RouteList.Image = Properties.Resources.filePurple;
        }

        private void pb_DL_FloorPlan_MouseUp(object sender, MouseEventArgs e)
        {
            pb_DL_FloorPlan.Image = Properties.Resources.filePurple;
        }

        private void pb_DL_SortPlan_MouseUp(object sender, MouseEventArgs e)
        {
            pb_DL_SortPlan.Image = Properties.Resources.filePurple;
        }

        // Sorting Method
        public void Sort()
        {
            if (ParcelData != null)
            {
                string txtData = "";
                foreach (string line in ParcelData)
                {
                    txtData = File.ReadAllText(line);
                }

                DataTable dataTable = new DataTable();

                string[] txtDataLines = txtData.Split('\n');

                // first line to create header
                string[] headerLabels = txtDataLines[0].Split(',');

                foreach (string headerWord in headerLabels)
                {
                    dataTable.Columns.Add(new DataColumn(headerWord));
                }


                // for data
                for (int row = 1; row < txtDataLines.Length; row++)
                {
                    string[] dataWords = txtDataLines[row].Split(',');
                    DataRow dataRow = dataTable.NewRow();
                    int col = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        try
                        {
                            if (dataWords[col] == null || dataWords[col] == "")
                            {
                                dataRow[headerWord] = null;
                                col++;
                            }
                            else
                            {
                                dataRow[headerWord] = dataWords[col];
                                col++;
                            }
                        }
                        catch (Exception e)
                        {
                            dataRow[headerWord] = null;
                        }
                    }

                    dataTable.Rows.Add(dataRow);
                }

                // load data table into data grid view
                dgv_FileData.DataSource = dataTable;
            }
        }
    }
}
