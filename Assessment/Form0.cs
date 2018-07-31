using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Assessment
{
    public partial class Form0 : Form
    {
        string s, acedemicYear, exam, branch, mDbPath;
        public Form0(string ay, string se, string ex, string br)
        {
            InitializeComponent();
            this.s = se;
            this.acedemicYear = ay;
            this.exam = ex;
            this.branch = br;
            this.mDbPath = Application.StartupPath + "\\Databases\\" + acedemicYear + "\\" + exam + "\\" + branch + "\\"+ se +"\\paperassessment.db";
        }

        private void Form0_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            semSelect.Items.Add(s);
            doit();
            semSelect.SelectedItem = s;
            semSelect_SelectedIndexChanged(sender, e);
        }

        public void doit()
        {
            if (!File.Exists(mDbPath))
            {
                string dir = Application.StartupPath + "\\Databases\\" + acedemicYear + "\\" + exam + "\\" + branch + "\\" + s;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                SQLiteConnection.CreateFile(mDbPath);
            }

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();

            string sql = "CREATE TABLE IF NOT EXISTS subject (id int PRIMARY KEY, name varchar(50) UNIQUE, sem int, todayDone int, todayModDone int, sf varchar(10), total int);";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            int num = command.ExecuteNonQuery();
            if (num != -1)
            {
                copySubjects();
            }
            sql = "CREATE TABLE IF NOT EXISTS repdate (date varchar(50), time varchar(50))";
            command = new SQLiteCommand(sql, m_dbConnection);
            int r = command.ExecuteNonQuery();
            if(r != -1)
            {
                sql = "INSERT INTO repdate (time) VALUES ('NA')";
            }
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "CREATE TABLE IF NOT EXISTS allocation (sid int, id int, assessor varchar(50), moderator varchar(50), allocated int, checked int, todayChecked int, moderated int, todayModerated int, PRIMARY KEY(sid, id), UNIQUE(sid, assessor));";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "SELECT * FROM repdate;";
            command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader1 = command.ExecuteReader();
            while(reader1.Read())
                repDate.Text = "Last Report Generated on: " + (string)reader1["time"];
            int y = 30;
            int i = s[4] - '0';
            sql = "SELECT * FROM subject WHERE sem = " + i + " ORDER BY id;";
            command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            panel3.Controls.Add(makeLabel(10, 40, 70, "Subject", "LavenderBlush", true));
            panel3.Controls.Add(makeLabel(80, 40, 80, "Available", "LavenderBlush", true));
            panel3.Controls.Add(makeLabel(160, 40, 80, "Allocated", "LavenderBlush", true));
            panel3.Controls.Add(makeLabel(240, 40, 80, "Checked", "LavenderBlush", true));
            panel3.Controls.Add(makeLabel(320, 40, 120, "To be Checked", "LavenderBlush", true));
            panel3.Controls.Add(makeLabel(440, 40, 90, "Moderated", "LavenderBlush", true));
            panel3.Controls.Add(makeLabel(530, 40, 140, "To be Moderated", "LavenderBlush", true));

            string sf;
            int t;
            while (reader.Read())
            {
                y = y + 30;
                sf = (string)reader["sf"];
                t = (int)reader["total"];
                sql = "SELECT * FROM allocation WHERE sid = " + (int)reader["id"] + ";";
                SQLiteCommand command2 = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader2 = command2.ExecuteReader();
                int sumC = 0, sumA = 0, sumM = 0;
                while (reader2.Read())
                {
                    sumC = sumC + (int)reader2["checked"];
                    sumA = sumA + (int)reader2["allocated"];
                    sumM = sumM + (int)reader2["moderated"];
                }
                int avail = Int32.Parse(t.ToString()) - sumA;
                int toBeChecked = sumA - sumC;
                int toBeModerated = sumC - sumM;
                panel3.Controls.Add(makeLabel(10, 10 + y, 70, sf, "LavenderBlush", true));
                panel3.Controls.Add(makeLabel(80, 10 + y, 80, avail.ToString(), "LavenderBlush", false));
                panel3.Controls.Add(makeLabel(160, 10 + y, 80, sumA.ToString(), "LavenderBlush", false));
                panel3.Controls.Add(makeLabel(240, 10 + y, 80, sumC.ToString(), "LavenderBlush", false));
                panel3.Controls.Add(makeLabel(320, 10 + y, 120, toBeChecked.ToString(), "LavenderBlush", false));
                panel3.Controls.Add(makeLabel(440, 10 + y, 90, sumM.ToString(), "LavenderBlush", false));
                panel3.Controls.Add(makeLabel(530, 10 + y, 140, toBeModerated.ToString(), "LavenderBlush", false));
            }
        }

        public void copySubjects()
        {
            int semest = s[4] - '0';
            string sql = "SELECT * FROM subjects WHERE dept = '"+ branch + "' AND sem = " + semest + " ORDER BY id;";
            SQLiteConnection m_dbConnection1 = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\subjects.db" + ";Version=3;");
            m_dbConnection1.Open();
            SQLiteCommand command1 = new SQLiteCommand(sql, m_dbConnection1);
            SQLiteDataReader reader = command1.ExecuteReader();
            SQLiteConnection m_dbConnection2 = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");

            m_dbConnection2.Open();
            int i = 0;
            while (reader.Read())
            {
                sql = "INSERT INTO subject(id, name, sem, todayDone, todayModDone, sf , total) VALUES ("+ i +", '"+ reader[2].ToString() +"', " + semest + ", 0, 0, '" + reader[4].ToString() + "' , 0)";
                SQLiteCommand command2 = new SQLiteCommand(sql, m_dbConnection2);
                command2.ExecuteNonQuery();
                i++;
            }
            m_dbConnection1.Close();
            m_dbConnection2.Close();
        }
        public static Label makeLabel(int xLoc, int yLoc, int xSize, string t, string color, bool bold)
        {
            Label newLabel = new Label();
            newLabel.Location = new System.Drawing.Point(xLoc, yLoc);
            newLabel.Size = new System.Drawing.Size(xSize, 30);
            if(bold == false)
                newLabel.Font = new Font("Arial", 11);
            else
                newLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            if (color == "green")
                newLabel.BackColor = Color.Green;
            else if (color == "red")
                newLabel.BackColor = Color.Red;
            else newLabel.BackColor = Color.LavenderBlush;
            newLabel.AutoSize = false;
            newLabel.Text = t;
            newLabel.BorderStyle = BorderStyle.FixedSingle;
            newLabel.TextAlign = ContentAlignment.MiddleCenter;
            return newLabel;
        }
        private void semSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            subSelect.Items.Clear();
            int sem = semSelect.Text[4] - '0';
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();

            string sql = "SELECT name FROM subject WHERE sem = " + sem + ";";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                string n = (string)reader["name"];
                subSelect.Items.Add(n);
            }
            subSelect.SelectedIndex = 0;
            subSelect.Focus();
            getTotalPapers(subSelect.Text);
        }

        private void subSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] arr = getTotalPapers(subSelect.Text);

            int n  = arr[0];
            totalToCheck.Text = n.ToString();

            int sumC = arr[1];

            if (n > sumC || n==0)
            {
                button3.Enabled = false;
                button1.Enabled = true;
                totalToCheck.Enabled = true;
            }

            else
            {
                button3.Enabled = true;
                button1.Enabled = false;
                totalToCheck.Enabled = false;
            }
                
        }

        public int[] getTotalPapers(string sub)
        {
            string subject = subSelect.Text;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();

            string sql = "SELECT total, id FROM subject WHERE name = '" + sub + "';";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int n = 0;
            int currsubid = -1;
            while (reader.Read())
            {
                n = (int)reader["total"];
                currsubid = (int)reader["id"];
            }

            sql = "SELECT * FROM allocation WHERE sid = " + currsubid + ";";
            command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader2 = command.ExecuteReader();
            int sumC = 0, sumA = 0 ;
            while (reader2.Read())
            {
                sumC = sumC + (int)reader2["checked"];
                sumA = sumA + (int)reader2["allocated"];
            }
            int[] arr = { n, sumC, sumA, currsubid}; 
            return arr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(totalToCheck.Text == "" || Int32.Parse(totalToCheck.Text) == 0)
            {
                MessageBox.Show("Please Enter the number of papers to be allocated");
                totalToCheck.Focus();
                return;
            }
            string subject = subSelect.Text;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();

            string sql = "SELECT id FROM subject WHERE name = '" + subject + "';";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int n = 0;
            while (reader.Read())
            {
                n = (int)reader["id"];
            }
            string sem = semSelect.Text;
            int ttc = Int32.Parse(totalToCheck.Text);
            if (!validateInp(ttc))
            {
                MessageBox.Show("Number of papers allocated exceed!", "Alert!");
                return;
            }
            Form1 fr = new Form1(n, subject, sem, mDbPath, acedemicYear, exam, branch, ttc, "Checking");
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        public bool validateInp(int ttc)
        {
            int[] arr = getTotalPapers(subSelect.Text);
            int sumA = arr[2];
            if (ttc >= sumA)
                return true;
            else
                return false;
        } 
        private void printReport_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string f = path + "/Reports/" + acedemicYear + "\\" + exam + "\\" + branch + "\\" + s + "/" + DateTime.Now.ToShortDateString() + "/Assesment Details.xlsx";
            bool res = WriteExcel(f, mDbPath);
            if (res == false)
                return;
            f = path + "/Reports/" + acedemicYear + "\\" + exam + "\\" + branch + "\\" + s + "/" + DateTime.Now.ToShortDateString() + "/Report.xlsx";
            WriteRemainingExcel(f, mDbPath);
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();
            string sql = "UPDATE allocation SET todayChecked = 0, todayModerated = 0;";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "UPDATE subject SET todayDone = 0, todayModDone = 0;";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            string str = DateTime.Now.ToString();
            sql = "UPDATE repdate SET time = '" + str + "'";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            repDate.Text = "Last Report Generated on: " + str;
            MessageBox.Show("Today's Report Generated !", "Message");

        }
        public bool WriteExcel(string filename, string database)
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch
                {
                    MessageBox.Show("Please Close the previous Report !");
                    return false;
                }       
            }
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Reports/" + acedemicYear + "\\" + exam + "\\" + branch + "\\" + s + "/" + DateTime.Now.ToShortDateString();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            FileInfo file = new FileInfo(filename);
            SQLiteConnection dbconnect = new SQLiteConnection("Data Source=" + database + ";Version=3;");
            int max2 = 10, max3 = 10;
            using (var p = new ExcelPackage(file))
            {
                int i = s[4] - '0';
                dbconnect.Open();
                int row = 1;
                var ws = p.Workbook.Worksheets.Add("Report");
                string sql = "SELECT name, id FROM subject WHERE sem = " + i.ToString() + ";";
                SQLiteCommand query = new SQLiteCommand(sql, dbconnect);
                var reader = query.ExecuteReader();

                ws.Cells[row, 1, row, 8].Merge = true;
                ws.Cells[row, 1, row, 8].Value = branch + " ("+ exam + ") " + "SEM " + i.ToString() + " - " + DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Year;
                ws.Cells[row, 1, row, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[row, 1, row, 8].Style.Font.Size = 14;
                ws.Cells[row, 1, row, 8].Style.Font.Bold = true;
                row = row + 2;

                while (reader.Read())
                {
                    ws.Cells[row, 1, row, 8].Merge = true;
                    ws.Cells[row, 1, row, 8].Value = reader[0];
                    ws.Cells[row, 1, row, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Cells[row, 1, row, 8].Style.Font.Size = 12;
                    ws.Cells[row, 1, row, 8].Style.Font.Bold = true;
                    row++;

                    sql = "SELECT assessor, moderator, allocated, checked, todayChecked, moderated, todayModerated FROM allocation WHERE sid = "+ reader[1] + ";";
                    SQLiteCommand query2 = new SQLiteCommand(sql, dbconnect);
                    var reader2 = query2.ExecuteReader();
                    ws.Cells[row, 1].Value = "Sr No.";
                    ws.Cells[row, 1].AutoFitColumns();
                    ws.Cells[row, 2].Value = "Assessor";
                    ws.Cells[row, 3].Value = "Moderator";
                    ws.Cells[row, 4].Value = "Allocated";
                    ws.Cells[row, 4].AutoFitColumns();
                    ws.Cells[row, 5].Value = "Total Checked";
                    ws.Cells[row, 5].AutoFitColumns();
                    ws.Cells[row, 6].Value = "Checked Today";
                    ws.Cells[row, 6].AutoFitColumns();
                    ws.Cells[row, 7].Value = "Total Moderated";
                    ws.Cells[row, 7].AutoFitColumns();
                    ws.Cells[row, 8].Value = "Moderated Today";
                    ws.Cells[row, 8].AutoFitColumns();
                    makeBorderTop(ws, row, 8);
                    makeBorderBottom(ws, row, 8);
                    makeBorderLeft(ws, row, 8);
                    makeBorderRight(ws, row, 8);
                    ws.Cells[row, 1, row, 8].Style.Font.Bold = true;
                    row++;

                    int c = 1;
                    int sumA = 0, sumC = 0, sumM = 0, sumT = 0, sumTM = 0;
                    while (reader2.Read())
                    {
                        ws.Cells[row, 1].Value = c.ToString();
                        int t = ((string)reader2[0]).Length;
                        ws.Cells[row, 2].Value = reader2[0];
                        if (t > max2)
                        {
                            ws.Cells[row, 2].AutoFitColumns();
                            max2 = t;
                        }
                        t = ((string)reader2[1]).Length;
                        ws.Cells[row, 3].Value = reader2[1];
                        if (t > max3)
                        {
                            ws.Cells[row, 3].AutoFitColumns();
                            max3 = t;
                        }
                        ws.Cells[row, 4].Value = reader2[2];
                        sumA = sumA + (int)reader2[2];
                        ws.Cells[row, 5].Value = reader2[3];
                        sumC = sumC + (int)reader2[3];
                        ws.Cells[row, 6].Value = reader2[4];
                        sumT = sumT + (int)reader2[4];
                        ws.Cells[row, 7].Value = reader2[5];
                        sumM = sumM + (int)reader2[5];
                        ws.Cells[row, 8].Value = reader2[6];
                        sumTM = sumTM + (int)reader2[6];
                        makeBorderLeft(ws, row, 8);
                        makeBorderRight(ws, row, 8);
                        row++;
                        c++;
                    }
                    makeBorderTop(ws, row, 8);
                    makeBorderBottom(ws, row, 8);
                    makeBorderLeft(ws, row, 8);
                    makeBorderRight(ws, row, 8);
                    ws.Cells[row, 4].Value = sumA;
                    ws.Cells[row, 5].Value = sumC;
                    ws.Cells[row, 6].Value = sumT;
                    ws.Cells[row, 7].Value = sumM;
                    ws.Cells[row, 8].Value = sumTM;
                    row = row + 2;
                }
                dbconnect.Close();
                p.Save();
            }
            return true;
        }

        public bool WriteRemainingExcel(string filename, string database)
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch
                {
                    MessageBox.Show("Please Close the previous Report !");
                    return false;
                }
            }
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Reports/" + acedemicYear + "\\" + exam + "\\" + branch + "\\" + s + "/" + DateTime.Now.ToShortDateString();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            FileInfo file = new FileInfo(filename);
            SQLiteConnection dbconnect = new SQLiteConnection("Data Source=" + database + ";Version=3;");

            using (var p = new ExcelPackage(file))
            {
                int i = s[4] - '0';
                dbconnect.Open();
                int row = 1;
                var ws = p.Workbook.Worksheets.Add("Report");

                string sql = "SELECT * FROM subject WHERE sem = " + i + " ORDER BY id;";

                SQLiteCommand command = new SQLiteCommand(sql, dbconnect);
                SQLiteDataReader reader = command.ExecuteReader();

                ws.Cells[row, 1, row, 4].Merge = true;
                ws.Cells[row, 1, row, 4].Value = branch + " (" + exam + ") " + "SEM " + i.ToString() + " - " + DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Year;
                ws.Cells[row, 1, row, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[row, 1, row, 4].Style.Font.Size = 14;
                ws.Cells[row, 1, row, 4].Style.Font.Bold = true;
                row = row + 2;

                ws.Cells[row, 1].Value = "Subject";
                ws.Cells[row, 1].Style.Font.Size = 12;
                ws.Cells[row, 1].Style.Font.Bold = true;
                ws.Cells[row, 1].AutoFitColumns();
                ws.Cells[row, 2].Value = "Allocated";
                ws.Cells[row, 2].Style.Font.Size = 12;
                ws.Cells[row, 2].Style.Font.Bold = true;
                ws.Cells[row, 2].AutoFitColumns();
                ws.Cells[row, 3].Value = "Checked";
                ws.Cells[row, 3].Style.Font.Size = 12;
                ws.Cells[row, 3].Style.Font.Bold = true;
                ws.Cells[row, 3].AutoFitColumns();
                ws.Cells[row, 4].Value = "Moderated";
                ws.Cells[row, 4].Style.Font.Size = 12;
                ws.Cells[row, 4].Style.Font.Bold = true;
                ws.Cells[row, 4].AutoFitColumns();
                makeBorderTop(ws, row, 4);
                makeBorderBottom(ws, row, 4);
                makeBorderLeft(ws, row, 4);
                makeBorderRight(ws, row, 4);

                row = row + 1;

                string sname;
                int t;
                int max1 = 7;
                while (reader.Read())
                {
                    sname = (string)reader["name"];
                    t = (int)reader["total"];
                    sql = "SELECT * FROM allocation WHERE sid = " + (int)reader["id"] + ";";
                    SQLiteCommand command2 = new SQLiteCommand(sql, dbconnect);
                    SQLiteDataReader reader2 = command2.ExecuteReader();
                    int sumC = 0, sumA = 0, sumM = 0;
                    while (reader2.Read())
                    {
                        sumC = sumC + (int)reader2["checked"];
                        sumA = sumA + (int)reader2["allocated"];
                        sumM = sumM + (int)reader2["moderated"];
                    }
                    ws.Cells[row, 1].Value = sname;
                    if (sname.Length > max1)
                    {
                        ws.Cells[row, 1].AutoFitColumns();
                        max1 = sname.Length;
                    }
                    ws.Cells[row, 2].Value = sumA.ToString() + "/" + t.ToString();
                    ws.Cells[row, 3].Value = sumC.ToString() + "/" + sumA.ToString();
                    ws.Cells[row, 4].Value = sumM.ToString() + "/" + sumC.ToString();
                    makeBorderTop(ws, row, 4);
                    makeBorderBottom(ws, row, 4);
                    makeBorderLeft(ws, row, 4);
                    makeBorderRight(ws, row, 4);
                    row++;
                }

                dbconnect.Close();
                p.Save();
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 fr = new Form2(acedemicYear, s, exam, branch);
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (totalToCheck.Text == "" || Int32.Parse(totalToCheck.Text) == 0)
            {
                MessageBox.Show("Please Enter the number of papers to be allocated");
                totalToCheck.Focus();
                return;
            }

            string subject = subSelect.Text;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();

            string sql = "SELECT id FROM subject WHERE name = '" + subject + "';";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int n = 0;
            while (reader.Read())
            {
                n = (int)reader["id"];
            }
            string sem = semSelect.Text;
            int ttc = Int32.Parse(totalToCheck.Text);
            if (!validateInp(ttc))
            {
                MessageBox.Show("Number of papers allocated exceed!", "Alert!");
                return;
            }
            Form1 fr = new Form1(n, subject, sem, mDbPath, acedemicYear, exam, branch, ttc, "Moderation");
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        private void totalToCheck_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(totalToCheck.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter numbers only.");
                totalToCheck.Text = totalToCheck.Text.Remove(totalToCheck.Text.Length - 1);
            }
        }

        public void makeBorderTop(ExcelWorksheet ws, int row, int col)
        {
            for(int i = 1; i <=col; i++)
            {
                ws.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            }
        }
        public void makeBorderLeft(ExcelWorksheet ws, int row, int col)
        {
            for (int i = 1; i <= col; i++)
            {
                ws.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            }
        }
        public void makeBorderRight(ExcelWorksheet ws, int row, int col)
        {
            for (int i = 1; i <= col; i++)
            {
                ws.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
        }
        public void makeBorderBottom(ExcelWorksheet ws, int row, int col)
        {
            for (int i = 1; i <= col; i++)
            {
                ws.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }
    }
}
