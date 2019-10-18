﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
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
        private void btn_Process_MouseDown(object sender, MouseEventArgs e)
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

                string[] txtDataLines = txtData.Split('\n');

                // to remove occurence of empty last line
                if (txtDataLines[txtDataLines.Length - 1] == "")
                {
                    string[] temp = new string[txtDataLines.Length - 1];
                    Array.Copy(txtDataLines, 0, temp, 0, txtDataLines.Length - 1);
                    txtDataLines = temp;
                }

                // first line to create header
                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                
                string[] headerLabels = CSVParser.Split(txtDataLines[0]);

                for (int i = 0; i < headerLabels.Length; i++)
                {
                    headerLabels[i] = Regex.Replace(headerLabels[i], "[^a-zA-Z0-9]", "");
                    Console.WriteLine(":"+headerLabels[i]+":");
                }

                parcelArray = new Parcel[txtDataLines.Length - 1];

                DataTable dataTable = new DataTable();

                foreach (string headerWord in headerLabels)
                {
                    dataTable.Columns.Add(new DataColumn(headerWord));
                }

                // second line onwards to process data
                // to be able to read data encapsulated by ""
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

                    string AWB = "", ConsigneeCompany = "", ConsigneeAddress = "", ConsigneePostal = "";
                    string SelectCd = "", DestLocCd = "", CourierRoute = "";
                    int PieceQty = 0;
                    double KiloWgt = 0.0;

                    foreach (string headerWord in headerLabels)
                    {
                        try
                        {
                            if (dataWords[col] == null || dataWords[col] == "")
                            {
                                dataRow[headerWord] = "";
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
                                    case "ConsigneeCompany":
                                        ConsigneeCompany = dataWords[col];
                                        break;
                                    case "ConsigneeAddr1":
                                        ConsigneeAddress = dataWords[col];
                                        break;
                                    case "ConsigneePostal":
                                        ConsigneePostal = dataWords[col];
                                        break;
                                    case "SelectCd":
                                        SelectCd = dataWords[col];
                                        break;
                                    case "DestLocCd":
                                        DestLocCd = dataWords[col];
                                        break;
                                    case "CourierRoute":
                                        CourierRoute = dataWords[col];
                                        break;
                                    case "PieceQty":
                                        PieceQty = Convert.ToInt32(dataWords[col]);
                                        break;
                                    case "KiloWgt":
                                        KiloWgt = Convert.ToDouble(dataWords[col]);
                                        break;
                                }

                                col++;
                            }

                            // Add the Parcel to the Parcel Array
                            Parcel parcel = new Parcel(
                                AWB, ConsigneeCompany, ConsigneeAddress, ConsigneePostal,
                                SelectCd, DestLocCd, CourierRoute, PieceQty, KiloWgt);
                            
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

        private void displayArray()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AWB"));
            dt.Columns.Add(new DataColumn("ConsigneeCompany"));
            dt.Columns.Add(new DataColumn("ConsigneeAddress"));
            dt.Columns.Add(new DataColumn("ConsigneePostal"));
            dt.Columns.Add(new DataColumn("SelectCd"));
            dt.Columns.Add(new DataColumn("Cleared"));
            dt.Columns.Add(new DataColumn("DestLocCd"));
            dt.Columns.Add(new DataColumn("CourierRoute"));
            dt.Columns.Add(new DataColumn("PieceQty"));
            dt.Columns.Add(new DataColumn("KiloWgt"));

            for (int i = 0; i < parcelArray.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["AWB"] = parcelArray[i].AWB;
                dr["ConsigneeCompany"] = parcelArray[i].ConsigneeCompany;
                dr["ConsigneeAddress"] = parcelArray[i].ConsigneeAddress;
                dr["ConsigneePostal"] = parcelArray[i].ConsigneePostal;
                dr["SelectCd"] = parcelArray[i].SelectCd;
                dr["Cleared"] = parcelArray[i].ClearedStatus;
                dr["DestLocCd"] = parcelArray[i].DestLocCd;
                dr["CourierRoute"] = parcelArray[i].CourierRoute;
                dr["PieceQty"] = parcelArray[i].PieceQty;
                dr["KiloWgt"] = parcelArray[i].KiloWgt;

                dt.Rows.Add(dr);
            }

            dgv_FileData.DataSource = dt;

            displaySortedArray();
        }

        private void displaySortedArray()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("AWB"));
            dt.Columns.Add(new DataColumn("ConsigneeCompany"));
            dt.Columns.Add(new DataColumn("ConsigneeAddress"));
            dt.Columns.Add(new DataColumn("ConsigneePostal"));
            dt.Columns.Add(new DataColumn("SelectCd"));
            dt.Columns.Add(new DataColumn("Cleared"));
            dt.Columns.Add(new DataColumn("DestLocCd"));
            dt.Columns.Add(new DataColumn("CourierRoute"));
            dt.Columns.Add(new DataColumn("PieceQty"));
            dt.Columns.Add(new DataColumn("KiloWgt"));

            for (int i = 0; i < parcelArray.Length; i++)
            {
                bool isBulk = false;
                string[] DestLocCdToProcess = {"KUL","XKL"};

                // check for Bulk requirements
                if (DestLocCdToProcess.Contains(parcelArray[i].DestLocCd))
                {
                    // check for Quantity and KiloWeight
                    if (parcelArray[i].PieceQty >= 50 ||
                        (parcelArray[i].PieceQty == 1 && parcelArray[i].KiloWgt >= 34) ||
                        (parcelArray[i].PieceQty > 1 && parcelArray[i].KiloWgt >= 225))
                    {
                        isBulk = true;
                    }

                    // check for multiple shipper to single Consignee Address
                    for (int j = 0; j < parcelArray.Length; j++)
                    {
                        if (parcelArray[i].ConsigneeAddress == parcelArray[j].ConsigneeAddress &&
                            parcelArray[i].AWB != parcelArray[j].AWB)
                        {
                            isBulk = true;
                        }
                    }
                }

                if (isBulk)
                {
                    DataRow dr = dt.NewRow();

                    dr["AWB"] = parcelArray[i].AWB;
                    dr["ConsigneeCompany"] = parcelArray[i].ConsigneeCompany;
                    dr["ConsigneeAddress"] = parcelArray[i].ConsigneeAddress;
                    dr["ConsigneePostal"] = parcelArray[i].ConsigneePostal;
                    dr["SelectCd"] = parcelArray[i].SelectCd;
                    dr["Cleared"] = parcelArray[i].ClearedStatus;
                    dr["DestLocCd"] = parcelArray[i].DestLocCd;
                    dr["CourierRoute"] = parcelArray[i].CourierRoute;
                    dr["PieceQty"] = parcelArray[i].PieceQty;
                    dr["KiloWgt"] = parcelArray[i].KiloWgt;

                    dt.Rows.Add(dr);
                }
            }

            dgv_FileData.DataSource = dt;
        }
    }
}
