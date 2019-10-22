using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Slap
{
    public partial class ctrl_SortHistory : UserControl
    {
        private string[] ParcelData, RouteData;
        private Parcel[] parcelArray;
        private bool sortLoaded = false;

        public ctrl_SortHistory()
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
            sortLoaded = false;
            pb_DL_ParcelList.Image = Properties.Resources.fileGray;
            pb_DL_RouteList.Image = Properties.Resources.fileGray;
            pb_DL_FloorPlan.Image = Properties.Resources.fileGray;
            pb_DL_SortPlan.Image = Properties.Resources.fileGray;
        }

        private void sortLoadedReset()
        {
            sortLoaded = true;
            pb_DL_ParcelList.Image = Properties.Resources.fileOrange;
            pb_DL_RouteList.Image = Properties.Resources.fileOrange;
            pb_DL_FloorPlan.Image = Properties.Resources.fileOrange;
            pb_DL_SortPlan.Image = Properties.Resources.fileOrange;
        }

        // Download and Clear buttons
        private void pb_Search_Click(object sender, EventArgs e)
        {
            sortLoadedReset();
        }

        private void btn_Download_MouseDown(object sender, MouseEventArgs e)
        {
            // . . .
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
