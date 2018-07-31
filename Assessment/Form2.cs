using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assessment
{
    public partial class Form2 : Form
    {
        string aced, bra, ex, se; 
        public Form2(string acedemicYear, string sem, string exam, string branch)
        {
            InitializeComponent();
            this.aced = acedemicYear;
            this.bra = branch;
            this.ex = exam;
            this.se = sem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Assessors.txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Moderators.txt");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            aySel.Items.Add("2018 - 2019");
            aySel.Items.Add("2019 - 2020");
            aySel.Items.Add("2020 - 2021");
            aySel.Items.Add("2021 - 2022");
            aySel.Items.Add("2022 - 2023");
            aySel.SelectedItem = aced;
            scSel.Items.Add("2016 - CBCS");
            scSel.SelectedIndex = 0;
            braSel.Items.Add("CMPN");
            braSel.Items.Add("INFT");
            braSel.Items.Add("ETRX");
            braSel.Items.Add("EXTC");
            braSel.Items.Add("BIOM");
            braSel.SelectedItem = bra;
            semSel.Items.Add("Sem 3");
            semSel.Items.Add("Sem 4");
            semSel.Items.Add("Sem 5");
            semSel.Items.Add("Sem 6");
            semSel.SelectedItem = se;
            exSel.SelectedItem = ex;
        }

        private void aySel_SelectedIndexChanged(object sender, EventArgs e)
        {
            exSel.Items.Clear();
            exSel.Items.Add("Dec " + aySel.Text.Substring(2, 2));
            exSel.Items.Add("May " + aySel.Text.Substring(9, 2));
            exSel.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ay = aySel.Text;
            string sem = semSel.Text;
            string exam = exSel.Text;
            string branch = braSel.Text;
            Form0 fr = new Form0(ay, sem, exam, branch);
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }
    }
}
