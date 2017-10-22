using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OV_ModTool_V1._0.Properties;
using System.Data.SQLite;


namespace OV_ModTool_V1._0
{
    public partial class MainWin : Form
    {
        //Common Members
        string dbPath;
        string ovSaveFolder;
        string selectedTblStr;
        string sqlStr;

        List<String> tableList;

        bool bIsDB;
        bool bIsSave;
        bool bWasErr;

        SQLiteConnection dbConn;

        //Query Browser Members
        SQLiteDataAdapter dbDataAdpt;
        DataSet qryDataSet;
        List<String> colList;
        bool bIsQB;

        //Advanced Query Browser Members
        SQLiteDataAdapter dbDataAdpt2;
        DataSet qryDataSet2;
        List<String> colList2;
        bool bIsAQB;


        //Database Editor Members
        SQLiteDataAdapter dbDataAdpt3;
        DataSet qryDataSet3;
        bool bIsDBEdit;
        bool bWMIsEdit;
        bool bWMIsCreate;
        List<string> colList3;
        List<Control> conList;
        List<Label> lblList;
        List<TextBox> tboxList;
        RichTextBox descBox;



        public MainWin() => InitializeComponent();

        private void Form1_Load(object sender, EventArgs e)
        {
            tableList = new List<string>();
            colList = new List<string>();
            colList2 = new List<string>();
            bIsDB = false;
            bIsSave = false;
            bWasErr = false;
            bIsQB = false;
            bIsAQB = false;
            bIsDBEdit = false;
            bWMIsEdit = false;
            bWMIsCreate = false;
            colList3 = new List<string>();
            conList = new List<Control>();
            lblList = new List<Label>();
            tboxList = new List<TextBox>();
            descBox = new RichTextBox();

            setupControlLists();
        }

        private void loadTableList()
        {
            sqlStr = "SELECT name FROM sqlite_master WHERE type = 'table'";
            dbConn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
            
            try
            {
                dbConn.Open();
                qryDataSet = new DataSet();
                dbDataAdpt = new SQLiteDataAdapter(sqlStr, dbConn);
                dbDataAdpt.Fill(qryDataSet);

                foreach (DataTable table in qryDataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        tableList.AddRange(row.ItemArray.Cast<string>().ToArray());
                        clboxTblList_AQB.Items.AddRange(row.ItemArray.Cast<string>().ToArray());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private void loadColList() => colList = getColNames();
        private void loadColList2() => colList2.AddRange(getColNames());
        private void loadColList3() => colList3.AddRange(getColNames2());

        private void queryData()
        {
            if (bIsDB)
            {
                if (bIsQB)
                {
                    buildSQLStr_QB();
                }
                else if (bIsAQB)
                {
                    buildSQLStr_AQB();
                }
                else if (bIsDBEdit)
                {
                    sqlStr = "select * from " + selectedTblStr;
                }

                if (!bWasErr)
                {
                    dbConn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
                }
                else
                {
                    bWasErr = false;
                    return; //Error occurred, dont process further
                }
            }
            try
            {
                dbConn.Open();

                if (bIsQB)
                {
                    qryDataSet = new DataSet();
                    dbDataAdpt = new SQLiteDataAdapter(sqlStr, dbConn);
                    dbDataAdpt.Fill(qryDataSet);
                    rtnData_QB.DataSource = qryDataSet.Tables[0].DefaultView;
                }
                else if (bIsAQB)
                {
                    qryDataSet2 = new DataSet();
                    dbDataAdpt2 = new SQLiteDataAdapter(sqlStr, dbConn);
                    dbDataAdpt2.Fill(qryDataSet2);
                    rtnData_AQB.DataSource = qryDataSet2.Tables[0].DefaultView;
                }
                else if (bIsDBEdit)
                {
                    qryDataSet3 = new DataSet();
                    dbDataAdpt3 = new SQLiteDataAdapter(sqlStr, dbConn);
                    dbDataAdpt3.Fill(qryDataSet3);
                    rData_DBE.DataSource = qryDataSet3.Tables[0].DefaultView;
                }
            }
            catch (Exception e)
            {
                if (bIsQB)
                {
                    lblQState_QB.ForeColor = Color.Red;
                    lblQState_QB.Text = e.Message;
                }
                else if (bIsAQB)
                {
                    lblQState_AQB.ForeColor = Color.Red;
                    lblQState_AQB.Text = e.Message;
                }
                else if (bIsDBEdit)
                {
                    lblQState_DBE.ForeColor = Color.Red;
                    lblQState_DBE.Text = e.Message;
                }

                bWasErr = true;
            }
        }

        private void buildSQLStr_QB()
        {
            if (tboxWhrConds_QB.Text.Length > 0 && tboxFldList_QB.Text.Length == 0)
            {
                sqlStr = "select * from " + selectedTblStr + " Where " + tboxWhrConds_QB.Text;
            }
            else if (tboxWhrConds_QB.Text.Length == 0 && tboxFldList_QB.Text.Length == 0)
            {
                sqlStr = "select * from " + selectedTblStr;
            }
            else if (tboxWhrConds_QB.Text.Length == 0 && tboxFldList_QB.Text.Length > 0)
            {
                sqlStr = "select " + tboxFldList_QB.Text + "  from " + selectedTblStr;
            }
            else if (tboxWhrConds_QB.Text.Length > 0 && tboxFldList_QB.Text.Length > 0)
            {
                sqlStr = "select " + tboxFldList_QB.Text + "  from " + selectedTblStr + " Where " + tboxWhrConds_QB.Text;
            }
        }

        private void buildSQLStr_AQB()
        {
            //field list cannot be blank
            //inner and where CAN be blank

            if (tboxFldList_AQB.Text.Length == 0)
            {
                lblQState_AQB.ForeColor = Color.Red;
                lblQState_AQB.Text = "Field list cannot be empty";

                bWasErr = true;

                return; //End execution
            }
            else
            {
                lblQState_AQB.Text = "";
                lblQState_AQB.ForeColor = Color.LightGreen;
            }

            sqlStr = "select " + tboxFldList_AQB.Text;

            if (tboxWhrConds_AQB.Text.Length > 0 && tboxJoinConds_AQB.Text.Length == 0)
            {
                sqlStr += " Where " + tboxWhrConds_AQB.Text;
            }
            else if (tboxWhrConds_AQB.Text.Length == 0 && tboxJoinConds_AQB.Text.Length > 0)
            {
                sqlStr += " " + tboxJoinConds_AQB.Text;
            }
            else if (tboxWhrConds_AQB.Text.Length > 0 && tboxFldList_AQB.Text.Length > 0)
            {
                sqlStr += " " + tboxJoinConds_AQB.Text + " Where " + tboxWhrConds_AQB.Text;
            }
        }

        private void loadDatabase(object sender, EventArgs e)
        {
            dbPath = (string)Settings.Default["ovdbPath"];
            bIsDB = true;
            bIsSave = false;

            loadTableList();

            tableList.Sort();
            cboxTblList_QB.DataSource = tableList;
            cboxTblList_QB.Update();

            clboxTblList_AQB.Refresh();

            cboxTblList_DBE.DataSource = tableList;
            cboxTblList_DBE.Update();

        }

        private void btnQExec_Click_QB(object sender, EventArgs e)
        {
            lblQState_QB.Text = "";
            bIsQB = true;
            queryData();

            if (!bWasErr)
            {
                lblQState_QB.ForeColor = Color.LightGreen;
                lblQState_QB.Text = "Query completed successfully";
            }
            else
            {
                bWasErr = false;
            }

            bIsQB = false;
        }

        private void cboxTblListQB_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTblStr = cboxTblList_QB.Text;

            colList.Clear(); //Empty the list so we don't just add the new fields to existing ones
            lboxFldList_QB.DataSource = null;
            loadColList();

            lboxFldList_QB.DataSource = colList;
            lboxFldList_QB.Refresh();
        }

        private void clboxTblList_AQB_ItemCheck(object sender, ItemCheckEventArgs e) => this.BeginInvoke((MethodInvoker)delegate
        {
            lboxFldList_AQB.DataSource = null;
            colList2.Clear();

            foreach (var item in clboxTblList_AQB.CheckedItems)
            {
                selectedTblStr = item.ToString();
                loadColList2();
            }

            lboxFldList_AQB.DataSource = colList2;
            lboxFldList_AQB.Refresh();
        });

        private List<string> getColNames()
        {
            List<string> names = new List<string>();
            sqlStr = "select * from " + selectedTblStr;
            dbConn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");

            try
            {
                dbConn.Open();
                qryDataSet = new DataSet();
                dbDataAdpt = new SQLiteDataAdapter(sqlStr, dbConn);
                dbDataAdpt.Fill(qryDataSet);

                foreach (DataTable table in qryDataSet.Tables)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        string name = selectedTblStr + "." + col.ColumnName; //To allow better identification of what this field belongs to
                        names.Add(name);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return names;
        }

        private List<string> getColNames2()
        {
            List<string> names = new List<string>();
            sqlStr = "select * from " + selectedTblStr;
            dbConn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");

            try
            {
                dbConn.Open();
                qryDataSet = new DataSet();
                dbDataAdpt = new SQLiteDataAdapter(sqlStr, dbConn);
                dbDataAdpt.Fill(qryDataSet);

                foreach (DataTable table in qryDataSet.Tables)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        string name = col.ColumnName; //To allow better identification of what this field belongs to
                        names.Add(name);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return names;
        }

        private void btnQExec_AQB_Click(object sender, EventArgs e)
        {
            lblQState_AQB.Text = "";
            bIsAQB = true;
            queryData();

            if (!bWasErr)
            {
                lblQState_AQB.ForeColor = Color.LightGreen;
                lblQState_AQB.Text = "Query completed successfully";
            }
            else
            {
                bWasErr = false;
            }

            bIsAQB = false;
        }

        private void tboxFldList_AQB_MouseHover(object sender, EventArgs e) => toolTip1.SetToolTip(tboxFldList_AQB, "Type fields here or leave blank for all fields.  Inclued From parameter EX: ID,Name,Email From Customer");
        private void tboxJoinConds_AQB_MouseHover(object sender, EventArgs e) => toolTip1.SetToolTip(tboxJoinConds_AQB, "If using a join, place your join statements here.  EX: inner join Table2 T2 on T1.ID = T2.CID");
        private void tboxWhrConds_AQB_MouseHover(object sender, EventArgs e) => toolTip1.SetToolTip(tboxWhrConds_AQB, "Type where conditions here or leave blank.  EX: ClassID = 2 and (Name = 'Bob' or Name = 'Jim')");
        private void clboxTblList_AQB_MouseHover(object sender, EventArgs e) => toolTip1.SetToolTip(clboxTblList_AQB, "Select the table(s) whose columns will populate the Field List box to the right.");
        private void tboxFldList_QB_MouseHover(object sender, EventArgs e) => toolTip1.SetToolTip(tboxFldList_QB, "Type fields here or leave blank for all fields.  EX: ID,Name,Email");
        private void tboxWhrConds_QB_MouseHover(object sender, EventArgs e) => toolTip1.SetToolTip(tboxWhrConds_QB, "Type where conditions here or leave blank.  EX: ClassID = 2 and (Name = 'Bob' or Name = 'Jim')");
        private void cboxTblList_QB_MouseHover(object sender, EventArgs e) => toolTip1.SetToolTip(cboxTblList_QB, "Select a table to query from.  Columns for this table will also be displayed in the Field List box to the right.");

        private void lblWModeInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Work Mode setting tells the tool how to process your actions.\n\nIn Record Editing mode you will select a table, then select a record from the table display at the bottom of the window.  This will load the data into the upper section for you to modify the values.\n\nIn Record Creation mode you will select a table to insert a new record into and then input the data into the upper section.  The lower section will just show you the existing data for reference purposes.");
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

        }

        private void cboxWMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode = cboxWMode.Text;

            //Reset in case of mode change
            bWMIsCreate = false;
            bWMIsEdit = false;

            if (mode == "Record Editing") { bWMIsEdit = true; }
            else if (mode == "Record Creation") { bWMIsCreate = true; }
        }

        private void cboxTblList_DBE_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTblStr = cboxTblList_DBE.Text;

            colList3.Clear(); //Empty the list so we don't just add the new fields to existing ones

            loadColList3();

            resetControls(); //Remove all data from last table
            updateLblControls();

            //Get table data
            lblQState_DBE.Text = "";
            bIsDBEdit = true;
            queryData();

            if (!bWasErr)
            {
                lblQState_DBE.ForeColor = Color.LightGreen;
                lblQState_DBE.Text = "Query completed successfully";
            }
            else
            {
                bWasErr = false;
            }

            bIsDBEdit = false;
        }

        private void setupControlLists()
        {
            conList = grpbRecVals.Controls.OfType<Control>().ToList();

            conList.Sort((c1, c2) => c1.TabIndex.CompareTo(c2.TabIndex));

            foreach (Control con in conList)
            {
                if (con is Label || con is TextBox || con is RichTextBox)
                {
                    con.Enabled = false;
                    con.Refresh();

                    if (con is Label)
                    {
                        lblList.Add((Label)con);
                    } else if (con is TextBox)
                    {
                        tboxList.Add((TextBox)con);
                    } else if (con is RichTextBox)
                    {
                        descBox = (RichTextBox)con;
                    }
                }
            }
        }

        private void updateLblControls()
        {
            int i2 = 0;
            for (int i1 = 0; i1 < colList3.Count; i1++, i2++)
            {
                //Handling Desc field
                if (i1 == 2 || i1 == 3)
                {
                    if (colList3.ElementAt(i1) != "Description")
                    {
                        i2 += 1;
                        lblList.ElementAt(i2).Text = colList3.ElementAt(i1);
                        lblList.ElementAt(i2).Enabled = true;
                        lblList.ElementAt(i2).Refresh();
                    }
                    else
                    {
                        lblList.ElementAt(i2).Text = colList3.ElementAt(i1);
                        lblList.ElementAt(i2).Enabled = true;
                        lblList.ElementAt(i2).Refresh();
                    }
                }
                else
                {
                    lblList.ElementAt(i2).Text = colList3.ElementAt(i1);
                    lblList.ElementAt(i2).Enabled = true;
                    lblList.ElementAt(i2).Refresh();
                }
            }
        }

        private void resetControls()
        {
            foreach (Label lbl in lblList)
            {
                lbl.Text = "Label";
                lbl.Enabled = false;
                lbl.Refresh();
            }

            foreach (TextBox tbox in tboxList)
            {
                tbox.Text = "";
                tbox.Enabled = false;
                tbox.Refresh();
            }

            descBox.Text = "";
            descBox.Enabled = false;
            descBox.Refresh();
        }

        private void rData_DBE_SelectionChanged(object sender, EventArgs e)
        {
            if (bWMIsEdit)
            {
                foreach (DataGridViewRow row in rData_DBE.SelectedRows)
                {
                    string value1 = row.Cells[0].Value.ToString();
                    string value2 = row.Cells[1].Value.ToString();

                    int i2 = 0;
                    for (int i1 = 0; i1 < row.Cells.Count; i1++,i2++)
                    {
                        if (i1 == 2 || i1 == 3)
                        {
                            if (rData_DBE.Columns[i1].HeaderText != "Description")
                            {
                                tboxList.ElementAt(i2).Text = row.Cells[i1].Value.ToString();
                                tboxList.ElementAt(i2).Enabled = true;
                                tboxList.ElementAt(i2).Refresh();
                            }
                            else
                            {
                                i2--; //So that we don't skip over the next actual textbox value
                                descBox.Text = row.Cells[i1].Value.ToString();
                                descBox.Enabled = true;
                                descBox.Refresh();
                            }
                        }
                        else
                        {
                            tboxList.ElementAt(i2).Text = row.Cells[i1].Value.ToString();
                            tboxList.ElementAt(i2).Enabled = true;
                            tboxList.ElementAt(i2).Refresh();
                        }
                    }
                }
            }
        }
    }
}
