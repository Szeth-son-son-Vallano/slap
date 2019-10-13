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
        private Boolean parcelListReady = false;
        private Boolean routeListReady = false;
        private string[] ParcelData, RouteData;

        public ctrl_NewSort_Input()
        {
            InitializeComponent();
            pb_DND_ParcelList.AllowDrop = true;
            pb_DND_RouteList.AllowDrop = true;
            Reset();

            dataGridView1.Hide();
            richTextBox1.Hide();
            //ctrl_NewSort_Output1.Hide();
        }

        // reset function
        private void Reset()
        {
            lbl_ErrorMessage.Text = "";

            pb_DND_ParcelList.Image = Properties.Resources.fileGrayFrame;
            lbl_ParcelListFile.Text = "Drag and Drop";
            pb_DND_RouteList.Image = Properties.Resources.fileGrayFrame;
            lbl_RouteListFile.Text = "Drag and Drop";
        }

        // Functions to Load files (Drag and Drop and Open File Dialog)
        private void checkParcelListFileType(string[] fileData)
        {
            //ParcelData = fileData;
            
            if (fileData.Length > 0)
            {
                char[] separator = { '\\', '/' };
                string[] strList = fileData[0].Split(separator);
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

        private void checkRouteListFileType(string[] fileData)
        {
            RouteData = fileData;

            if (fileData.Length > 0)
            {
                char[] separator = { '\\', '/' };
                string[] strList = fileData[0].Split(separator);
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
            string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];

            //foreach(string line in data)
            //{
            //    richTextBox1.Text += File.ReadAllText(line);
            //}
            
            checkParcelListFileType(data);

            if (data != null)
            {
                string txtData = "";
                foreach(string line in data)
                {
                    txtData = File.ReadAllText(line);
                }


                DataTable dataTable = new DataTable();

                string[] txtDataLines = txtData.Split('\n');
                richTextBox1.Text = txtDataLines.Length.ToString();
                foreach(string line in txtDataLines)
                {
                    richTextBox1.Text += line;
                }

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
                        catch (Exception exception)
                        {
                            dataRow[headerWord] = null;
                        }
                    }

                    dataTable.Rows.Add(dataRow);
                }

                // load data table into data grid view
                dataGridView1.DataSource = dataTable;
            }
        }

        private void pb_DND_RouteList_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
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

        // Sort and Clear buttons
        private void btn_Sort_MouseDown(object sender, MouseEventArgs e)
        {
            if (parcelListReady && routeListReady)
            {
                lbl_ErrorMessage.Text = "";
                
                dataGridView1.Show();
                richTextBox1.Show();
                //ctrl_NewSort_Output1.Show();
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
