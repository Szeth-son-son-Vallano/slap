using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ZXing;
using System.Drawing;
using ZXing.QrCode;
using System.IO.Compression;

namespace Slap
{
    public partial class ctrl_NewSort_Output : UserControl
    {
        // variables for parcels
        private string ParcelListFilePath = "", RouteListFilePath = "", FloorPlanFilePath = "", SortPlanFilePath = "";
        private string[] ParcelData, RouteData;
        private Parcel[] parcelArray;
        private List<Parcel> filteredParcelList;
        private List<Parcel> sortedParcelList;

        // variables for routeGroups and routes
        private List<RouteGroup> routeGroupList;
        private List<RouteGroup> sortedRouteGroupList;

        public ctrl_NewSort_Output()
        {
            InitializeComponent();
            Reset();
        }

        public void AddData(string parcelFileName, string routeFileName, string[] parcelData, string[] routeData)
        {
            ParcelListFilePath = parcelFileName;
            RouteListFilePath = routeFileName;
            ParcelData = parcelData;
            RouteData = routeData;
        }

        // reset
        private void Reset()
        {
            ParcelListFilePath = "";
            RouteListFilePath = "";
            FloorPlanFilePath = "";
            SortPlanFilePath = "";

            ParcelData = null;
            RouteData = null;
            parcelArray = null;
            filteredParcelList = null;
            sortedParcelList = null;
            
            pb_DL_ParcelList.Enabled = false;
            pb_DL_RouteList.Enabled = false;
            pb_DL_FloorPlan.Enabled = false;
            pb_DL_SortPlan.Enabled = false;
            
            pb_DL_ParcelList.Image = Properties.Resources.fileGray;
            pb_DL_RouteList.Image = Properties.Resources.fileGray;
            pb_DL_FloorPlan.Image = Properties.Resources.fileGray;
            pb_DL_SortPlan.Image = Properties.Resources.fileGray;
        }

        // Sort and Clear buttons
        private void btn_Sort_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Sort.Enabled = false;
            FilterParcels();
            btn_Sort.Enabled = true;
        }

        private void btn_Back_MouseDown(object sender, MouseEventArgs e)
        {
            Hide();
            Reset();
        }

        // On Click functions for Individual File Download
        private void pb_DL_ParcelList_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_ParcelList.Image = Properties.Resources.fileLightPurple;
            System.Diagnostics.Process.Start(ParcelListFilePath);
        }

        private void pb_DL_RouteList_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_RouteList.Image = Properties.Resources.fileLightPurple;
            System.Diagnostics.Process.Start(RouteListFilePath);
        }

        private void pb_DL_FloorPlan_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_FloorPlan.Image = Properties.Resources.fileLightPurple;
            System.Diagnostics.Process.Start(FloorPlanFilePath);
        }

        private void pb_DL_SortPlan_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_SortPlan.Image = Properties.Resources.fileLightPurple;
            System.Diagnostics.Process.Start(SortPlanFilePath);
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
        public bool ReadParcels()
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
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }

                string[] txtDataLines = txtData.Split('\n');

                // to remove occurence of empty last line
                if (txtDataLines[txtDataLines.Length - 1].Equals(""))
                {
                    string[] txtDataTemp = new string[txtDataLines.Length - 1];
                    Array.Copy(txtDataLines, 0, txtDataTemp, 0, txtDataLines.Length - 1);
                    txtDataLines = txtDataTemp;
                }

                // first line to create header
                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                string[] headerLabels = CSVParser.Split(txtDataLines[0]);

                for (int i = 0; i < headerLabels.Length; i++)
                {
                    // remove carriage return
                    headerLabels[i] = Regex.Replace(headerLabels[i], "[^a-zA-Z0-9 +–=_.,!\"\'/$]", "");
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
                    string[] parcelData = CSVParser.Split(txtDataLines[row]);

                    // clean up the fields (remove " and leading spaces)
                    for (int i = 0; i < parcelData.Length; i++)
                    {
                        // remove carriage return
                        parcelData[i] = Regex.Replace(parcelData[i], "[^a-zA-Z0-9 +–=_.,!\"\'/$]", "");

                        parcelData[i] = parcelData[i].TrimStart(' ', '"');
                        parcelData[i] = parcelData[i].TrimEnd('"');
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
                            if (parcelData[col] == null || parcelData[col] == "")
                            {
                                dataRow[headerWord] = "";
                                col++;
                            }
                            else
                            {
                                dataRow[headerWord] = parcelData[col];

                                switch (headerWord)
                                {
                                    case "AWB":
                                        AWB = parcelData[col];
                                        break;
                                    case "ConsigneeCompany":
                                        ConsigneeCompany = parcelData[col];
                                        break;
                                    case "ConsigneeAddr1":
                                        ConsigneeAddress = parcelData[col];
                                        break;
                                    case "ConsigneePostal":
                                        ConsigneePostal = parcelData[col];
                                        break;
                                    case "SelectCd":
                                        SelectCd = parcelData[col];
                                        break;
                                    case "DestLocCd":
                                        DestLocCd = parcelData[col];
                                        break;
                                    case "CourierRoute":
                                        CourierRoute = parcelData[col];
                                        break;
                                    case "PieceQty":
                                        PieceQty = Convert.ToInt32(parcelData[col]);
                                        break;
                                    case "KiloWgt":
                                        KiloWgt = Convert.ToDouble(parcelData[col]);
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
                            Console.WriteLine(e.Message);
                            dataRow[headerWord] = null;
                        }
                    }

                    // add data row into data table
                    dataTable.Rows.Add(dataRow);
                }

                dgv_FileData.DataSource = dataTable;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadRoutes()
        {
            if (RouteData != null)
            {
                // extract all data from the file
                string txtData = "";
                foreach(string line in RouteData)
                {
                    try
                    {
                        txtData = File.ReadAllText(line);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }
                
                string[] txtDataLines = txtData.Split('\n');

                // to remove occurence of empty last line
                if (txtDataLines[txtDataLines.Length - 1].Equals(""))
                {
                    string[] txtDataTemp = new string[txtDataLines.Length - 1];
                    Array.Copy(txtDataLines, 0, txtDataTemp, 0, txtDataLines.Length - 1);
                    txtDataLines = txtDataTemp;
                }
                
                // create Regex object
                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                // process RouteGroup data
                int routeGroupID = 0;
                routeGroupList = new List<RouteGroup>();
                
                // add one RouteGroup for parcels that are on HOLD
                routeGroupList.Add(new RouteGroup(routeGroupID++, new List<string>()));
                
                for (int row = 0; row < txtDataLines.Length; row++)
                {
                    string[] routeGroupData = CSVParser.Split(txtDataLines[row]);

                    /// DEBUGGING PURPOSES
                    foreach(var data in routeGroupData)
                    {
                        Console.WriteLine("1 " + row + ": " + data);
                    }

                    // clean up the fields (remove " and leading spaces)
                    for (int i = 0; i < routeGroupData.Length; i++)
                    {
                        // remove carriage return
                        routeGroupData[i] = Regex.Replace(routeGroupData[i], "[^a-zA-Z0-9 +–=_.,!\"\'/$]", "");

                        routeGroupData[i] = routeGroupData[i].TrimStart(' ', '"');
                        routeGroupData[i] = routeGroupData[i].TrimEnd('"');

                        Console.WriteLine("2 RouteGroupData: " + routeGroupData[i]);
                    }
                    
                    // read the column data of each row
                    foreach (var routeGroup in routeGroupData)
                    {
                        string[] routes = CSVParser.Split(routeGroup);

                        List<string> routesList = new List<string>();
                        foreach (var route in routes)
                        {
                            Console.WriteLine("3 " + route);
                            routesList.Add(route);
                        }

                        routeGroupList.Add(new RouteGroup(routeGroupID++, routesList));
                    }

                    foreach (var rg in routeGroupList)
                    {
                        foreach(var route in rg.CourierRoutes)
                        {

                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void FilterParcels()
        {
            filteredParcelList = new List<Parcel>();
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

                // check for Bulk requirements
                string[] DestLocCdToProcess = { "KUL", "XKL" };

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

                    filteredParcelList.Add(parcelArray[i]);
                }
            }

            dgv_FileData.DataSource = dt;

            SortParcels();
        }

        private void SortParcels()
        {
            sortedParcelList = new List<Parcel>();

            Dictionary<string, int> routeGroupRoutesDict = new Dictionary<string, int>();

            // iterate throuch each RouteGroup
            // collect all routes into one dictionary
            foreach (RouteGroup routeGroup in routeGroupList)
            {
                foreach (string courierRoute in routeGroup.CourierRoutes)
                {
                    routeGroupRoutesDict.Add(courierRoute, routeGroup.RouteGroupID);
                }
            }

            // iterate through each parcel to be sorted into routeGroups
            // assign parcels to routegroups
            foreach (Parcel parcel in filteredParcelList)
            {
                if (routeGroupRoutesDict.ContainsKey(parcel.CourierRoute))
                {
                    if (parcel.ClearedStatus)
                    {
                        //parcel.RouteGroup = routeGroupRoutesDict[parcel.CourierRoute];
                        routeGroupList[routeGroupRoutesDict[parcel.CourierRoute]].ParcelList.Add(parcel);
                    }
                    else
                    {
                        //parcel.RouteGroup = 0;
                        routeGroupList[0].ParcelList.Add(parcel);
                    }
                    sortedParcelList.Add(parcel);
                }
            }

            // iterate through each RouteGroup
            // assign lanes based on estimated volume
            sortedRouteGroupList = new List<RouteGroup>();

            char lane = 'A';
            foreach (RouteGroup routeGroup in routeGroupList)
            {
                foreach (Parcel parcel in routeGroup.ParcelList)
                {
                    routeGroup.LaneEstimateVolumeCur += parcel.EstimatedVol;
                }
                Console.WriteLine(routeGroup.LaneEstimateVolumeCur);

                int numOfLanes = (int)Math.Ceiling(routeGroup.LaneEstimateVolumeCur / routeGroup.LaneEstimateVolumeMax);
                string lanes = "";

                if (routeGroup.RouteGroupID == 0)
                {
                    lanes = "HOLD";
                    routeGroup.Lanes = lanes;
                    sortedRouteGroupList.Add(routeGroup);
                }
                else
                {
                    for (int i = 0; i < numOfLanes; i++)
                    {
                        lanes += lane;
                        lane++;
                    }

                    if (!lanes.Equals(""))
                    {
                        routeGroup.Lanes = lanes;
                        sortedRouteGroupList.Add(routeGroup);
                    }
                }

                foreach (Parcel parcel in routeGroup.ParcelList)
                {
                    parcel.Lanes = lanes;
                }

                // print console for debugging purposes
                Console.WriteLine("RouteGroup: " + routeGroup.RouteGroupID);
                string routesConsole = "";
                foreach (string route in routeGroup.CourierRoutes)
                {
                    routesConsole += route + " ";
                }
                Console.WriteLine("Routes: " + routesConsole);
                double kiloWgtConsole = 0.0;
                foreach (Parcel parcel in routeGroup.ParcelList)
                {
                    kiloWgtConsole += parcel.KiloWgt;
                }
                Console.WriteLine("KiloWgt: " + kiloWgtConsole);
                Console.WriteLine("Lanes: " + lanes);
                Console.WriteLine();
            }

            DisplayParcels();
            InitializeFileUpload();
        }

        private void DisplayParcels()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("AWB"));
            dt.Columns.Add(new DataColumn("ConsigneeCompany"));
            dt.Columns.Add(new DataColumn("ConsigneeAddress"));
            dt.Columns.Add(new DataColumn("ConsigneePostal"));
            dt.Columns.Add(new DataColumn("SelectCd"));
            dt.Columns.Add(new DataColumn("Cleared"));
            dt.Columns.Add(new DataColumn("CourierRoute"));
            dt.Columns.Add(new DataColumn("Lanes"));
            dt.Columns.Add(new DataColumn("PieceQty"));
            dt.Columns.Add(new DataColumn("KiloWgt"));

            for (int i = 0; i < sortedParcelList.Count; i++)
            {
                DataRow dr = dt.NewRow();

                dr["AWB"] = sortedParcelList[i].AWB;
                dr["ConsigneeCompany"] = sortedParcelList[i].ConsigneeCompany;
                dr["ConsigneeAddress"] = sortedParcelList[i].ConsigneeAddress;
                dr["ConsigneePostal"] = sortedParcelList[i].ConsigneePostal;
                dr["SelectCd"] = sortedParcelList[i].SelectCd;
                dr["Cleared"] = sortedParcelList[i].ClearedStatus;
                dr["CourierRoute"] = sortedParcelList[i].CourierRoute;
                dr["Lanes"] = sortedParcelList[i].Lanes;
                dr["PieceQty"] = sortedParcelList[i].PieceQty;
                dr["KiloWgt"] = sortedParcelList[i].KiloWgt;

                dt.Rows.Add(dr);
            }

            dgv_FileData.DataSource = dt;
        }

        private void InitializeFileUpload()
        {
            // get current date time
            DateTime now = DateTime.Now;
            string dateStr = now.ToString("yyyyMMdd");
            string timeStr = now.ToString("HHmmss");
            string dateTimeDisplay = now.ToString("dddd, dd MMMM yyyy - hh:mm tt");

            // determine Sort Number
            string folderName = "";
            bool folderExists = true;
            int sortNumber = 0;
            while (folderExists)
            {
                sortNumber++;
                folderName = dateStr + "_Sort" + sortNumber.ToString("D3");
                folderExists = GoogleDrive.FindFolder(folderName);
            }

            // Google Drive
            string folderID = GoogleDrive.CreateFolder(folderName);
            string GoogleDrive_FloorPlanFilePath = folderName + "_" + timeStr + "_FloorPlan.pdf";
            string GoogleDrive_SortPlanFilePath = folderName + "_" + timeStr + "_SortPlan.pdf";
            string GoogleDrive_ParcelListFilePath = folderName + "_" + timeStr + "_ParcelList.csv";
            string GoogleDrive_RouteListFilePath = folderName + "_" + timeStr + "_RouteList.csv";
            
            FloorPlanFilePath = Path.Combine(KnownFolders.GetPath(KnownFolder.Downloads), GoogleDrive_FloorPlanFilePath);
            SortPlanFilePath = Path.Combine(KnownFolders.GetPath(KnownFolder.Downloads), GoogleDrive_SortPlanFilePath);
            
            // generate PDF files
            GeneratePDF_FloorPlan(FloorPlanFilePath, dateTimeDisplay);
            GeneratePDF_SortPlan(SortPlanFilePath, dateTimeDisplay);
            
            GoogleDrive.UploadFile(FloorPlanFilePath, Path.GetFileName(GoogleDrive_FloorPlanFilePath), folderID);
            GoogleDrive.UploadFile(SortPlanFilePath, Path.GetFileName(GoogleDrive_SortPlanFilePath), folderID);
            GoogleDrive.UploadFile(ParcelListFilePath, Path.GetFileName(GoogleDrive_ParcelListFilePath), folderID);
            GoogleDrive.UploadFile(RouteListFilePath, Path.GetFileName(GoogleDrive_RouteListFilePath), folderID);

            EnableViewFile();
        }

        private void EnableViewFile()
        {
            pb_DL_ParcelList.Enabled = true;
            pb_DL_RouteList.Enabled = true;
            pb_DL_FloorPlan.Enabled = true;
            pb_DL_SortPlan.Enabled = true;
            
            pb_DL_ParcelList.Image = Properties.Resources.filePurple;
            pb_DL_RouteList.Image = Properties.Resources.filePurple;
            pb_DL_FloorPlan.Image = Properties.Resources.filePurple;
            pb_DL_SortPlan.Image = Properties.Resources.filePurple;
        }

        private void GeneratePDF_FloorPlan(string filePath, string dateTimeStr)
        {
            // create PDF file
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();

            // create table
            int gridRows = RouteGroup.PalletCount;
            int gridCols = 28;
            PdfPTable table = new PdfPTable(gridRows + 2);

            iTextSharp.text.Font font = new iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 8));

            // create table title
            PdfPCell titleCell = new PdfPCell(new Phrase("FLOOR PLAN - " + dateTimeStr));
            titleCell.Rowspan = gridCols;
            titleCell.Rotation = 270;
            titleCell.HorizontalAlignment = 1;

            // fill in the used lanes
            int currentLane = 0;
            float minimumHeight = 25.0f;
            bool insertTitle = false;
            foreach (RouteGroup routeGroup in sortedRouteGroupList)
            {
                if (!routeGroup.Lanes.Equals("HOLD"))
                {
                    foreach (char lane in routeGroup.Lanes)
                    {
                        PdfPCell cellLane = new PdfPCell(new Phrase(Char.ToString(lane), font))
                        {
                            Rotation = 270,
                            Colspan = 1,
                            MinimumHeight = minimumHeight
                        };
                        table.AddCell(cellLane);

                        for (int row = 0; row < gridRows; row++)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(" "))
                            {
                                Rotation = 270,
                                Colspan = 1,
                                MinimumHeight = minimumHeight,
                                BackgroundColor = SlapColor.Color[routeGroup.RouteGroupID]
                            };
                            table.AddCell(cell);

                            if (!insertTitle && row == gridRows - 1)
                            {
                                table.AddCell(titleCell);
                                insertTitle = true;
                            }
                        }
                        currentLane++;
                    }
                }
            }

            // fill in the empty lanes
            for (int i = currentLane; i < gridCols - 4; i++, currentLane++)
            {
                PdfPCell cellLane = new PdfPCell(new Phrase(Char.ToString((char)(currentLane + 65)), font))
                {
                    Rotation = 270,
                    Colspan = 1,
                    MinimumHeight = minimumHeight
                };
                table.AddCell(cellLane);
                for (int row = 0; row < gridRows; row++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(" "))
                    {
                        Colspan = 1,
                        MinimumHeight = minimumHeight,
                        BackgroundColor = SlapColor.Color[SlapColor.Color.Count - 1]
                    };

                    table.AddCell(cell);
                }
            }

            // fill in the holding lanes
            if (sortedRouteGroupList[0].Lanes.Equals("HOLD"))
            {
                for (int col = 0; col < 4; col++)
                {
                    PdfPCell cellLane = new PdfPCell(new Phrase(sortedRouteGroupList[0].Lanes, font))
                    {
                        Rotation = 270,
                        Colspan = 1,
                        MinimumHeight = minimumHeight
                    };
                    table.AddCell(cellLane);

                    for (int row = 0; row < gridRows; row++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(" "))
                        {
                            Colspan = 1,
                            MinimumHeight = minimumHeight,
                            BackgroundColor = SlapColor.Color[0]
                        };

                        table.AddCell(cell);
                    }
                }
            }

            doc.Add(table);

            doc.Close();
            writer.Close();
        }

        private void GeneratePDF_SortPlan(string filePath, string dateTimeStr)
        {
            // create PDF file
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();

            // Add table
            PdfPTable table = new PdfPTable(3);
            float[] colWidth = new float[] { 2.0f, 5.0f, 7.0f };
            table.SetWidths(colWidth);

            PdfPCell cell = new PdfPCell(new Phrase("SORT PLAN - " + dateTimeStr));
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            table.AddCell("Lane");
            table.AddCell("AWB");
            table.AddCell("Barcode");

            sortedParcelList.Sort(new LaneComparer());

            for (int i = 0; i < sortedParcelList.Count; i++)
            {
                // Add image
                BarcodeWriter barcodeWriter = new BarcodeWriter()
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new QrCodeEncodingOptions()
                    {
                        Width = 300,
                        Height = 100
                    }
                };
                Bitmap bmp = barcodeWriter.Write(sortedParcelList[i].AWB);
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Bmp);
                image.ScaleToFit(150.0F, 50.0F);
                image.Alignment = Element.ALIGN_CENTER;

                PdfPCell imgCell = new PdfPCell() { PaddingLeft = 5, PaddingRight = 5 };
                imgCell.AddElement(image);

                table.AddCell(sortedParcelList[i].Lanes);
                table.AddCell(sortedParcelList[i].AWB);
                table.AddCell(imgCell);
            }

            doc.Add(table);

            doc.Close();
            writer.Close();
        }

        private void Zip_File(string folderPath, string fileName, string storagePath)
        {
            try
            {
                if (Directory.Exists(storagePath))
                {

                }

                else
                {
                    Directory.CreateDirectory(storagePath);
                }

                ZipFile.CreateFromDirectory(folderPath, fileName);

            }
            catch (Exception)
            {
                //If system throw any exception message box will display "SOME ERROR"  
                MessageBox.Show("Some Error");
            }

            MessageBox.Show("Zip Filename : " + fileName + " Created Successfully");
        }

        private void DeleteFileFolder(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            if(Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }
    }
}
