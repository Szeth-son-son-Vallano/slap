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
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace Slap
{
    public partial class ctrl_NewSort_Output : UserControl
    {
        private string[] ParcelData, RouteData;
        private Parcel[] parcelArray;

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
            displayArray();
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
        public bool Sort()
        {
            if (ParcelData != null)
            {
                // extract all data from the file
                string txtData = "";
                foreach (string line in ParcelData)
                {
                    try
                    {
                        txtData = File.ReadAllText(line);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }

                // first line to create header
                string[] txtDataLines = txtData.Split('\n');
                string[] headerLabels = txtDataLines[0].Split(',');

                parcelArray = new Parcel[txtDataLines.Length - 1];

                DataTable dataTable = new DataTable();

                foreach (string headerWord in headerLabels)
                {
                    dataTable.Columns.Add(new DataColumn(headerWord));
                }

                // second line onwards to process data
                // to be able to read data encapsulated by ""
                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                
                for (int row = 1; row < txtDataLines.Length; row++)
                {
                    string[] dataWords = CSVParser.Split(txtDataLines[row]);

                    // clean up the fields (remove " and leading spaces)
                    for (int i = 0; i < dataWords.Length; i++)
                    {
                        dataWords[i] = dataWords[i].TrimStart(' ', '"');
                        dataWords[i] = dataWords[i].TrimEnd('"');
                    }

                    // read the column data of each row
                    DataRow dataRow = dataTable.NewRow();
                    int col = 0;

                    string AWB = "", SelectCd = "", DestLocCd = "", ConsigneePostal = "";
                    double KiloWgt = 0.0;

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

                                switch (headerWord)
                                {
                                    case "AWB":
                                        AWB = dataWords[col];
                                        break;
                                    case "SelectCd":
                                        SelectCd = dataWords[col];
                                        break;
                                    case "DestLocCd":
                                        DestLocCd = dataWords[col];
                                        break;
                                    case "ConsigneePostal":
                                        ConsigneePostal = dataWords[col];
                                        break;
                                    case "KiloWgt":
                                        KiloWgt = Convert.ToDouble(dataWords[col]);
                                        break;
                                }

                                col++;
                            }

                            // Add the Parcel to the Parcel Array
                            Parcel parcel = new Parcel(AWB, SelectCd, DestLocCd, ConsigneePostal, KiloWgt);
                            parcelArray[row - 1] = parcel;
                        }
                        catch (Exception e)
                        {
                            dataRow[headerWord] = null;
                        }
                    }

                    // add data row into data table
                    dataTable.Rows.Add(dataRow);
                }

                // load data table into data grid view
                dgv_FileData.DataSource = dataTable;
            }

            return true;
        }

        private int clickSwitcher = 0;
        private void displayArray()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AWB"));
            dt.Columns.Add(new DataColumn("SelectCd"));
            dt.Columns.Add(new DataColumn("DestLocCd"));
            dt.Columns.Add(new DataColumn("ConsigneePostal"));
            dt.Columns.Add(new DataColumn("KiloWgt"));

            for (int i = 0; i < parcelArray.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["AWB"] = parcelArray[i].getAWB();
                dr["SelectCd"] = parcelArray[i].getSelectCd();
                dr["DestLocCd"] = parcelArray[i].getDestLocCd();
                dr["ConsigneePostal"] = parcelArray[i].getConsigneePostal();
                dr["KiloWgt"] = parcelArray[i].getKiloWeight();

                dt.Rows.Add(dr);
            }

            dgv_FileData.DataSource = dt;

            if (clickSwitcher % 2 == 1)
            {
                displayAllDestLocCd();
            }
            clickSwitcher++;
        }

        private void displayAllDestLocCd()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DestLocCd"));

            for (int i = 0; i < parcelArray.Length; i++)
            {
                bool newDestLocCd = true;
                for (int j = 0; j < i; j++)
                {
                    if (parcelArray[i].getDestLocCd() == parcelArray[j].getDestLocCd())
                    {
                        newDestLocCd = false;
                        break;
                    }
                }

                if (newDestLocCd)
                {
                    DataRow dr = dt.NewRow();
                    dr["DestLocCd"] = parcelArray[i].getDestLocCd();
                    dt.Rows.Add(dr);
                }
            }

            dgv_FileData.DataSource = dt;
        }
    }
}
