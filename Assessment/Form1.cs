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


namespace Assessment
{
    public partial class Form1 : Form
    {
        int sid;
        int total_entries;
        int txtBoxStartPosition;
        int txtBoxStartPositionV = 25;
        int d;
        int total;
        string name;
        string sem;
        string mDbPath;
        string acedemicYear, exam, branch, cORm;
        bool firstTime;
        public Form1(int sid, string name, string sem, string dbPath, string ay, string ex, string br, int ttc, string cm)
        {
            InitializeComponent();
            this.sid = sid;
            this.name = name;
            this.total_entries = 1;
            this.txtBoxStartPosition = 50;
            this.txtBoxStartPositionV = 25;
            this.d = 48;
            this.sem = sem;
            this.mDbPath = dbPath;
            this.acedemicYear = ay;
            this.exam = ex;
            this.branch = br;
            this.total = ttc;
            this.cORm = cm;
            this.firstTime = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            string[] facultyArray = File.ReadAllLines(Application.StartupPath + "\\Assessors.txt", Encoding.UTF8);
            Array.Sort(facultyArray);
            new_ass.Items.Add("--------- SELECT FACULTY --------");
            
            for (int iter = 0; iter < facultyArray.Length; iter++)
            {
                if(facultyArray[iter] != "" )
                    new_ass.Items.Add(facultyArray[iter]);
            }
            new_ass.SelectedIndex = 0;
            Label ltop = makeLabel(75, 40, 400, name + " (" + sem + " - " + branch + ")");
            ltop.Font = new Font("Arial", 11, FontStyle.Bold);
            this.Controls.Add(ltop);
            if (cORm == "Moderation")
            {
                panel1.Controls.Add(makeLabel(txtBoxStartPosition, txtBoxStartPositionV, 250, "Moderator"));
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 250 + d, txtBoxStartPositionV, 250, "Assessor (Checked)"));
            }
            else
            {
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 100, txtBoxStartPositionV, 250, "Assessor"));
            }
            if(cORm == "Checking")
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 465 + 2 * d - 100, txtBoxStartPositionV, 100, "Total Checked"));
            else
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 465 + 2 * d, txtBoxStartPositionV, 120, "Total Moderated"));
            if (cORm == "Checking")
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 575 + 3 * d - 100, txtBoxStartPositionV, 10, "/"));
            else
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 575 + 3 * d, txtBoxStartPositionV, 10, "/"));
            if (cORm == "Checking")
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 575 + 4 * d - 100, txtBoxStartPositionV, 75, "Allocated"));
            else
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 575 + 4 * d, txtBoxStartPositionV, 75, "Allocated"));
            if (cORm == "Checking")
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 650 + 5 * d - 100, txtBoxStartPositionV, 120, "Checked Today"));
            else
                panel1.Controls.Add(makeLabel(txtBoxStartPosition + 650 + 5 * d, txtBoxStartPositionV, 120, "Moderated Today"));
            txtBoxStartPositionV += 30;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();
            string sql = "UPDATE subject SET total = " + total + " where id = " + sid + ";";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            load();
            if (cORm == "Checking")
            {
                panel2.Hide();
                save_but_Click(sender, e);
            }
            else
            {
                newPanel.Hide();
                saveMod_Click(sender, e);
            }
        }

        public static TextBox makeBox(int xLoc, int yLoc, int xSize, string t,string name, bool enabled, bool isNumber)
        {
            TextBox newText = new TextBox();
            newText.ReadOnly = !enabled;
            newText.Name = name;
            newText.Location = new System.Drawing.Point(xLoc, yLoc);
            newText.Size = new System.Drawing.Size(xSize, 30);
            newText.Font = new Font("Arial", 11);
            newText.Text = t;
            if (isNumber == true)
            {
                newText.TextChanged += (s, e) =>
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(newText.Text, "[^0-9]"))
                    {
                        MessageBox.Show("Please enter only numbers.");
                        newText.Text = newText.Text.Remove(newText.Text.Length - 1);
                    }
                };
            }
            return newText;
        }

        public static Label makeLabel(int xLoc, int yLoc, int xSize, string t)
        {
            Label newLabel = new Label();
            newLabel.Location = new System.Drawing.Point(xLoc, yLoc);
            newLabel.Size = new System.Drawing.Size(xSize, 30);
            newLabel.Font = new Font("Arial", 11);
            newLabel.Text = t;
            return newLabel;
        }

        private void save_but_Click(object sender, EventArgs e)
        {
            int sumChecked = 0, sumAllocated = 0, sumTodayChecked = 0;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();

            string sql;
            SQLiteCommand command;

            sql = "UPDATE subject SET todayDone = 1 WHERE id = " + sid + ";";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            for (int i = 1; i < total_entries; i++)
            {
                string ass;
                int alloc, chkd, tchkd;
                TextBox txtBox = panel1.Controls["ass_" + i.ToString()] as TextBox;
                ass = txtBox.Text;
                txtBox = panel1.Controls["chkd_" + i.ToString()] as TextBox;
                chkd = Int32.Parse(txtBox.Text);
                txtBox = panel1.Controls["tchkd_" + i.ToString()] as TextBox;
                if (txtBox.Text == "")
                {
                    tchkd = 0;
                    txtBox.Text = "0";
                }
                else
                    tchkd = Int32.Parse(txtBox.Text);
                txtBox = panel1.Controls["alloc_" + i.ToString()] as TextBox;
                if (txtBox.Text == "")
                {
                    alloc = 0;
                    txtBox.Text = "0";
                }
                else
                    alloc = Int32.Parse(txtBox.Text);

                
                sql = "SELECT todayChecked FROM allocation WHERE sid = " + sid + " AND id = " + i + ";";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                int actual_add = 0;
                while (reader.Read())
                    actual_add = tchkd - (int)reader["todayChecked"];
                chkd = chkd + actual_add;
                sumTodayChecked += tchkd;
                sumChecked += chkd;
                sumAllocated += alloc;

                if (total - sumAllocated < 0)
                {
                    int maxAlloc = alloc - (sumAllocated - total);
                    MessageBox.Show("NOT SAVED ! Total Papers Allocated is exceeding the limit! Maximum " + maxAlloc.ToString() + " Papers can be allocated", "Alert!");
                    txtBox = panel1.Controls["alloc_" + i.ToString()] as TextBox;
                    txtBox.Text = maxAlloc.ToString();
                    txtBox.Focus();
                    return;
                }
                if (chkd > alloc)
                {
                    MessageBox.Show("NOT Saved! Papers checked greater than Papers allocated!", "Alert!");
                    txtBox = panel1.Controls["tchkd_" + i.ToString()] as TextBox;
                    txtBox.Focus();
                    return;
                }

                sql = "UPDATE allocation SET assessor = '" + ass + "',  allocated = " + alloc + ", checked = " + chkd + ", todayChecked = " + tchkd + " WHERE sid =" + sid + " AND id = " + i + ";";
                command = new SQLiteCommand(sql, m_dbConnection);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    if (ex.Message.Contains("UNIQUE"))
                        MessageBox.Show("Faculty named '"+ ass + "' Already Exists!", "Alert!");
                    m_dbConnection.Close();
                    txtBox = panel1.Controls["ass_" + i.ToString()] as TextBox;
                    txtBox.Focus();
                    return;
                }
                txtBox = panel1.Controls["chkd_" + i.ToString()] as TextBox;
                txtBox.Text = chkd.ToString();
            }
            m_dbConnection.Close();
            sumC.Text = sumChecked.ToString();
            sumA.Text = sumAllocated.ToString();
            sumT.Text = sumTodayChecked.ToString();
            rem.Text = "To be allocated: " + (total - sumAllocated).ToString();
            rem.Font = new Font("Arial", 11, FontStyle.Bold);
            /*if (!firstTime)
                MessageBox.Show("Saved!", "Alert!");
            else
                firstTime = false;
            */
        }

        private void new_chkd_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(new_chkd.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter numbers only.");
                new_chkd.Text = new_chkd.Text.Remove(new_chkd.Text.Length - 1);
            }
        }

        private void new_alloc_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(new_alloc.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter numbers only.");
                new_alloc.Text = new_alloc.Text.Remove(new_alloc.Text.Length - 1);
            }
        }

        private void new_tchkd_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(new_tchkd.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter numbers only.");
                new_tchkd.Text = new_tchkd.Text.Remove(new_tchkd.Text.Length - 1);
            }
        }

        private void saveMod_Click(object sender, EventArgs e)
        {
            int sumModerated = 0, sumAllocated = 0, sumTodayModerated = 0;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();

            string sql;
            SQLiteCommand command;

            sql = "UPDATE subject SET todayModDone = 1  WHERE id = " + sid + ";";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            for (int i = 1; i < total_entries; i++)
            {
                string ass, mod;
                int alloc, mode, tmode;
                TextBox txtBox = panel1.Controls["ass_" + i.ToString()] as TextBox;
                ass = txtBox.Text;
                ComboBox cb = panel1.Controls["mod_" + i.ToString()] as ComboBox;
                mod = cb.Text;
                txtBox = panel1.Controls["mode_" + i.ToString()] as TextBox;
                mode = Int32.Parse(txtBox.Text);
                txtBox = panel1.Controls["tmode_" + i.ToString()] as TextBox;
                if (txtBox.Text == "")
                {
                    tmode = 0;
                    txtBox.Text = "0";
                }
                else
                    tmode = Int32.Parse(txtBox.Text);
                txtBox = panel1.Controls["alloc_" + i.ToString()] as TextBox;
                if (txtBox.Text == "")
                {
                    alloc = 0;
                    txtBox.Text = "0";
                }
                else
                    alloc = Int32.Parse(txtBox.Text);
                if (mod == "--------- SELECT FACULTY --------")
                    mod = "";
                sql = "SELECT todayModerated, checked FROM allocation WHERE sid = " + sid + " AND id = " + i + ";";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                int actual_add = 0, c = 0;
                while (reader.Read())
                {
                    actual_add = tmode - (int)reader["todayModerated"];
                    c = (int)reader["checked"];
                }
                    
                mode = mode + actual_add;
                sumTodayModerated += tmode;
                sumModerated += mode;
                sumAllocated += alloc;

                if (mod == "" && mode != 0)
                {
                    MessageBox.Show("Please Enter the name of Moderator!");
                    return;
                }
                if (mode > alloc)
                {
                    MessageBox.Show("NOT SAVED! Papers moderated greater than papers allocated!", "Alert!");
                    txtBox = panel1.Controls["tmode_" + i.ToString()] as TextBox;
                    txtBox.Focus();
                    return;
                }
                if (mode > c)
                {
                    MessageBox.Show("NOT SAVED! Papers moderated greater than papers checked!", "Alert!");
                    txtBox = panel1.Controls["tmode_" + i.ToString()] as TextBox;
                    txtBox.Focus();
                    return;
                }

                if (mod == ass.Split('(')[0])
                {
                    MessageBox.Show("NOT SAVED! Moderator and Assessor are both Same!", "Alert!");
                    cb.Focus();
                    return;
                }
                sql = "UPDATE allocation SET moderator = '" + mod + "', allocated = " + alloc + ", moderated = " + mode + ", todayModerated = " + tmode + " WHERE sid =" + sid + " AND id = " + i + ";";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                txtBox = panel1.Controls["mode_" + i.ToString()] as TextBox;
                txtBox.Text = mode.ToString();
            }
            m_dbConnection.Close();
            sumML.Text = sumModerated.ToString();
            sumAL.Text = sumAllocated.ToString();
            sumTML.Text = sumTodayModerated.ToString();

            if (!firstTime)
                MessageBox.Show("Saved!", "Alert!");
            else
                firstTime = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form0 fr = new Form0(acedemicYear, sem, exam, branch);
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        private void addNew_Click(object sender, EventArgs e)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();

            string ass;
            int alloc, chkd, tchkd;
            ComboBox comBox = newPanel.Controls["new_ass"] as ComboBox;
            ass = comBox.Text;
            TextBox txtBox = newPanel.Controls["new_chkd"] as TextBox;
            if (txtBox.Text == "")
                chkd = 0;
            else
                chkd = Int32.Parse(txtBox.Text);
            txtBox = newPanel.Controls["new_tchkd"] as TextBox;
            if (txtBox.Text == "")
                tchkd = 0;
            else
                tchkd = Int32.Parse(txtBox.Text);
            txtBox = newPanel.Controls["new_alloc"] as TextBox;
            if (txtBox.Text == "")
                alloc = 0;
            else
                alloc = Int32.Parse(txtBox.Text);
            if (ass == "--------- SELECT FACULTY --------")
            {
                MessageBox.Show("Please Enter Assessor's Name!", "Alert!");
                comBox = newPanel.Controls["new_ass"] as ComboBox;
                comBox.Focus();
                return;
            }

            if (chkd < tchkd)
            {
                MessageBox.Show("Papers checked today cannot be less than Total papers Checked!", "Alert!");
                txtBox = newPanel.Controls["new_chkd"] as TextBox;
                txtBox.Focus();
                return;
            }

            if (alloc < chkd)
            {
                MessageBox.Show("Papers checked greater than Papers Allocated!", "Alert!");
                txtBox = newPanel.Controls["new_chkd"] as TextBox;
                txtBox.Focus();
                return;
            }

            int sumAllocated = 0;
            for (int i = 1; i < total_entries; i++)
            {
                int Ialloc;
                TextBox txtBox1 = panel1.Controls["alloc_" + i.ToString()] as TextBox;
                if (txtBox1.Text == "")
                {
                    Ialloc = 0;
                }
                else
                    Ialloc = Int32.Parse(txtBox1.Text);
                sumAllocated += Ialloc;
            }
            if((sumAllocated + alloc) > total)
            {
                MessageBox.Show("Maximum " + (total - sumAllocated).ToString() +" can be allocated!");
                txtBox = newPanel.Controls["new_alloc"] as TextBox;
                txtBox.Focus();
                return;
            }
            string sql = "INSERT into allocation (sid, id, assessor, moderator, allocated, checked, todayChecked, moderated, todayModerated) values (" + sid + ", " + total_entries + ", '" + ass + "', '', " + alloc + ", " + chkd + ", " + tchkd + ", 0, 0);";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch(System.Exception ex)
            {
                if(ex.Message.Contains("UNIQUE"))
                    MessageBox.Show("Faculty named '" + ass + "' Already Exists!", "Alert!");
                comBox = newPanel.Controls["new_ass"] as ComboBox;
                comBox.Focus();
                m_dbConnection.Close();
                return;

            }
            new_ass.SelectedIndex = 0;
            new_chkd.Text = "";
            new_tchkd.Text = "";
            new_alloc.Text = "";
            m_dbConnection.Close();
            load();
            save_but_Click(sender, e);
        }

        public void load()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();
            
            string sql = "SELECT * FROM allocation where sid = " + sid + " AND id >= " + total_entries + " ORDER BY id";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            string[] facultyArray = new string[1000];
            string[] temp = File.ReadAllLines(Application.StartupPath + "\\Moderators.txt", Encoding.UTF8);
            for (int it = 0; it < temp.Length; it++)
            {
                facultyArray[it] = temp[it];
            }
            Array.Sort(facultyArray);

            while (reader.Read())
            {
                int t_id = (int)reader["id"];
                string t_ass = (string)reader["assessor"];
                string t_mod = (string)reader["moderator"];
                int t_all = (int)reader["allocated"];
                int t_chkd = (int)reader["checked"];
                int t_tchkd = (int)reader["todayChecked"];
                int t_mode = (int)reader["moderated"];
                int t_tmode = (int)reader["todayModerated"];
                if (cORm == "Checking")
                {
                    panel1.Controls.Add(makeBox(txtBoxStartPosition + 100, txtBoxStartPositionV, 250, t_ass, "ass_" + t_id.ToString(), false, false));
                }
                else
                {
                    ComboBox newCom = new ComboBox();
                    newCom.DropDownStyle = ComboBoxStyle.DropDownList;
                    newCom.Name = "mod_" + t_id.ToString();
                    newCom.Location = new System.Drawing.Point(txtBoxStartPosition, txtBoxStartPositionV);
                    newCom.Size = new System.Drawing.Size(250, 30);
                    newCom.Font = new Font("Arial", 11);
                    
                    
                    if(t_mod == "")
                    {
                        newCom.Items.Add("--------- SELECT FACULTY --------");
                        for (int iter = 0; iter < facultyArray.Length; iter++)
                        {
                            if (facultyArray[iter] != "" && facultyArray[iter] != null)
                                newCom.Items.Add(facultyArray[iter]);
                        }
                        newCom.SelectedIndex = 0;
                    }
                    else
                    {
                        newCom.Items.Add(t_mod);
                        newCom.SelectedIndex = 0;
                        newCom.Enabled = false;
                    }

                    panel1.Controls.Add(newCom);
                    panel1.Controls.Add(makeBox(txtBoxStartPosition + 250 + d, txtBoxStartPositionV, 250, t_ass + "(" + t_chkd + ")", "ass_" + t_id.ToString(), false, false));
                }
                if (cORm == "Checking")
                    panel1.Controls.Add(makeBox(txtBoxStartPosition - 100 + 500 + 2 * d, txtBoxStartPositionV, 50, t_chkd.ToString(), "chkd_" + t_id.ToString(), false, true));
                else
                    panel1.Controls.Add(makeBox(txtBoxStartPosition + 500 + 2 * d, txtBoxStartPositionV, 50, t_mode.ToString(), "mode_" + t_id.ToString(), false, true));
                if (cORm == "Checking")
                    panel1.Controls.Add(makeLabel(txtBoxStartPosition + 575 + 3 * d - 100, txtBoxStartPositionV, 10, "/"));
                else panel1.Controls.Add(makeLabel(txtBoxStartPosition + 575 + 3 * d, txtBoxStartPositionV, 10, "/"));
                if (cORm == "Checking")
                    panel1.Controls.Add(makeBox(txtBoxStartPosition + 585 + 4 * d - 100, txtBoxStartPositionV, 50, t_all.ToString(), "alloc_" + t_id.ToString(), true, true));
                else
                    panel1.Controls.Add(makeBox(txtBoxStartPosition + 585 + 4 * d, txtBoxStartPositionV, 50, t_all.ToString(), "alloc_" + t_id.ToString(), false, true));
                if (cORm == "Checking")
                    panel1.Controls.Add(makeBox(txtBoxStartPosition + 665 + 5 * d - 100, txtBoxStartPositionV, 50, t_tchkd.ToString(), "tchkd_" + t_id.ToString(), true, true));
                else
                    panel1.Controls.Add(makeBox(txtBoxStartPosition + 665 + 5 * d, txtBoxStartPositionV, 50, t_tmode.ToString(), "tmode_" + t_id.ToString(), true, true));
                txtBoxStartPositionV += 30;
                total_entries++;
            }

            m_dbConnection.Close();
            newPanel.Location = new Point(0, txtBoxStartPositionV);
            panel2.Location = new Point(0, txtBoxStartPositionV);
        }

        private void backBut_Click(object sender, EventArgs e)
        {
            Form0 fr = new Form0(acedemicYear, sem, exam, branch);
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }
    }
}
