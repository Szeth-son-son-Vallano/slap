using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static iTextSharp.text.Font;
using ZXing;
using System.Drawing;
using ZXing.QrCode;
using System.IO.Compression;
using Google.Apis.Drive.v3;

namespace Slap
{
    public partial class ctrl_NewSort_Output : UserControl
    {
        // variables for parcels
        private string[] ParcelData, RouteData;
        private Parcel[] parcelArray;
        private List<Parcel> filteredParcelList;
        private List<Parcel> sortedParcelList;

        // variables for routeGroups and routes
        private List<List<string>> routesList;
        private List<RouteGroup> routeGroupList;
        private List<RouteGroup> sortedRouteGroupList;

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
            pb_DL_FloorPlan.Image = Properties.Resources.filePurple;
            pb_DL_SortPlan.Image = Properties.Resources.filePurple;
        }

        // Sort and Clear buttons
        private void btn_Sort_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Sort.Enabled = false;
            ReadRoutes();
            FilterParcels();
            btn_Sort.Enabled = true;
        }

        private void btn_Back_MouseDown(object sender, MouseEventArgs e)
        {
            Hide();
        }

        // On Click functions for Individual File Download
        private void pb_DL_ParcelList_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_ParcelList.Image = Properties.Resources.fileLightPurple;
        }

        private void pb_DL_RouteList_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_RouteList.Image = Properties.Resources.fileLightPurple;

        }

        private void pb_DL_FloorPlan_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_FloorPlan.Image = Properties.Resources.fileLightPurple;

        }

        private void pb_DL_SortPlan_MouseDown(object sender, MouseEventArgs e)
        {
            pb_DL_SortPlan.Image = Properties.Resources.fileLightPurple;

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
                    string[] dataWords = CSVParser.Split(txtDataLines[row]);

                    // clean up the fields (remove " and leading spaces)
                    for (int i = 0; i < dataWords.Length; i++)
                    {
                        // remove carriage return
                        dataWords[i] = Regex.Replace(dataWords[i], "[^a-zA-Z0-9 +–=_.,!\"\'/$]", "");

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
                            Console.WriteLine(e.Message);
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

        private void ReadRoutes()
        {
            //routesList.Add(new List<string> { "810", "811", "812", "813", "814", "815", "816", "817", "818", "819" });
            //routesList.Add(new List<string> { "820", "821", "822", "823", "824" });
            //routesList.Add(new List<string> { "835", "836", "837", "838", "839" });
            //routesList.Add(new List<string> { "850", "851", "852", "853", "854", "855", "856", "857", "858", "859" });
            //routesList.Add(new List<string> { "860", "861", "862", "863", "864", "865", "866", "867", "868", "869" });
            //routesList.Add(new List<string> { "870", "871", "872", "873", "874", "875", "876", "877", "878", "879" });
            //routesList.Add(new List<string> {
            //    "880", "881", "882", "883", "884", "885", "886", "887", "888", "889",
            //    "890", "891", "892", "893", "894", "895", "896", "897", "898", "899" });

            //routesList.Add(new List<string> { "XKLA" });
            //routesList.Add(new List<string> { "KULAAA" });
            //routesList.Add(new List<string> { "KULBK+" });
            //routesList.Add(new List<string> { "RAWANG" });

            routesList = new List<List<string>>();
            
            // given values
            routesList.Add(new List<string> { "835", "837", "839" });
            routesList.Add(new List<string> { "850", "857", "859" });
            routesList.Add(new List<string> { "823", "833" });
            routesList.Add(new List<string> { "815" });
            routesList.Add(new List<string> { "860", "863", "865" });
            routesList.Add(new List<string> { "872", "875", "877" });
            routesList.Add(new List<string> { "880", "883", "885", "890", "893", "895" });
            routesList.Add(new List<string> { "XKLA" });
            routesList.Add(new List<string> { "KULAAA" });
            routesList.Add(new List<string> { "KULBK+" });
            routesList.Add(new List<string> { "RAWANG" });

            // add one RouteGroup for parcels that are on HOLD
            routeGroupList = new List<RouteGroup>();

            int routeGroupID = 0;

            routeGroupList.Add(new RouteGroup(routeGroupID++, new List<string>()));
            foreach (List<string> routes in routesList)
            {
                routeGroupList.Add(new RouteGroup(routeGroupID++, routes));
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

            //// handle file and folder locations
            //string startPath = Application.StartupPath;
            //string databasePath = Path.GetFullPath(Path.Combine(startPath, @"C:\Users\wongz\OneDrive\Desktop\Slap Database"));
            ////string databasePath = Path.GetFullPath(Path.Combine(startPath, @"C:\Users\mxian\Desktop\Slap Database"));

            //string folderPath = System.IO.Path.Combine(databasePath, dateStr);

            //int sortNumber = 1;
            //string sortNumberPath;
            //while (true)
            //{
            //    sortNumberPath = System.IO.Path.Combine(folderPath, sortNumber.ToString());
            //    if (Directory.Exists(sortNumberPath))
            //    {
            //        sortNumber++;
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            //floorPlanFilePath = System.IO.Path.Combine(sortNumberPath, floorPlanFilePath);
            //sortPlanFilePath = System.IO.Path.Combine(sortNumberPath, sortPlanFilePath);

            //System.IO.Directory.CreateDirectory(sortNumberPath);

            // determine Sort Number
            string folderName = "";
            bool folderExists = true;
            int sortNumber = 0;
            while (folderExists)
            {
                sortNumber++;
                folderName = dateStr + "_Sort" + sortNumber.ToString();
                folderExists = GoogleDrive.FindFileFolder(folderName);
            }

            string floorPlanFileName = folderName + "_" + timeStr + "_FloorPlan.pdf";
            string sortPlanFileName = folderName + "_" + timeStr + "_SortPlan.pdf";

            // generate PDF files
            GeneratePDF_FloorPlan(floorPlanFileName, dateTimeDisplay);
            GeneratePDF_SortPlan(sortPlanFileName, dateTimeDisplay);

            // Google Drive
            string folderID = GoogleDrive.CreateFolder(folderName);
            GoogleDrive.UploadPdf(floorPlanFileName, Path.GetFileName(floorPlanFileName), folderID);
            GoogleDrive.UploadPdf(sortPlanFileName, Path.GetFileName(sortPlanFileName), folderID);

            // delete local copy
            DeleteFileFolder(floorPlanFileName);
            DeleteFileFolder(sortPlanFileName);

            //Deleting root folder
            //DeleteFolder(databasePath);
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

            BaseColor[] colors = new BaseColor[15];
            colors[0] = new BaseColor(128, 128, 128);
            colors[1] = new BaseColor(255, 0, 0);
            colors[2] = new BaseColor(255, 128, 0);
            colors[3] = new BaseColor(255, 255, 0);
            colors[4] = new BaseColor(128, 255, 0);
            colors[5] = new BaseColor(0, 255, 0);
            colors[6] = new BaseColor(0, 255, 128);
            colors[7] = new BaseColor(0, 255, 255);
            colors[8] = new BaseColor(0, 128, 255);
            colors[9] = new BaseColor(0, 0, 255);
            colors[10] = new BaseColor(128, 0, 255);
            colors[11] = new BaseColor(255, 0, 255);
            colors[12] = new BaseColor(255, 0, 128);
            colors[13] = new BaseColor(0, 0, 0);
            colors[14] = new BaseColor(255, 255, 255);

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
                                BackgroundColor = colors[routeGroup.RouteGroupID]
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
                        BackgroundColor = colors[colors.Length - 1]
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
                            BackgroundColor = colors[0]
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
