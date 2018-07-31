namespace Assessment
{
    partial class Form0
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.semSelect = new System.Windows.Forms.ComboBox();
            this.subSelect = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.printReport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.totalToCheck = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.repDate = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // semSelect
            // 
            this.semSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.semSelect.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.semSelect.FormattingEnabled = true;
            this.semSelect.Location = new System.Drawing.Point(38, 30);
            this.semSelect.Name = "semSelect";
            this.semSelect.Size = new System.Drawing.Size(94, 25);
            this.semSelect.TabIndex = 0;
            this.semSelect.SelectedIndexChanged += new System.EventHandler(this.semSelect_SelectedIndexChanged);
            // 
            // subSelect
            // 
            this.subSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subSelect.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subSelect.FormattingEnabled = true;
            this.subSelect.Location = new System.Drawing.Point(38, 89);
            this.subSelect.Name = "subSelect";
            this.subSelect.Size = new System.Drawing.Size(364, 25);
            this.subSelect.TabIndex = 1;
            this.subSelect.SelectedIndexChanged += new System.EventHandler(this.subSelect_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(38, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Checking";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(575, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 518);
            this.panel1.TabIndex = 3;
            // 
            // printReport
            // 
            this.printReport.BackColor = System.Drawing.Color.LightGreen;
            this.printReport.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printReport.Location = new System.Drawing.Point(1245, 48);
            this.printReport.Name = "printReport";
            this.printReport.Size = new System.Drawing.Size(93, 60);
            this.printReport.TabIndex = 4;
            this.printReport.Text = "Generate Report";
            this.printReport.UseVisualStyleBackColor = false;
            this.printReport.Click += new System.EventHandler(this.printReport_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.totalToCheck);
            this.panel2.Controls.Add(this.semSelect);
            this.panel2.Controls.Add(this.subSelect);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(98, 204);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(437, 266);
            this.panel2.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button3.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(245, 207);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 37);
            this.button3.TabIndex = 5;
            this.button3.Text = "Moderation";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Total Papers:";
            // 
            // totalToCheck
            // 
            this.totalToCheck.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalToCheck.Location = new System.Drawing.Point(141, 145);
            this.totalToCheck.Name = "totalToCheck";
            this.totalToCheck.Size = new System.Drawing.Size(56, 24);
            this.totalToCheck.TabIndex = 3;
            this.totalToCheck.TextChanged += new System.EventHandler(this.totalToCheck_TextChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel3.Location = new System.Drawing.Point(613, 204);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(686, 266);
            this.panel3.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightPink;
            this.button2.Location = new System.Drawing.Point(28, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 30);
            this.button2.TabIndex = 7;
            this.button2.Text = "<--";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // repDate
            // 
            this.repDate.AutoSize = true;
            this.repDate.Font = new System.Drawing.Font("Arial", 11F);
            this.repDate.Location = new System.Drawing.Point(1023, 12);
            this.repDate.Name = "repDate";
            this.repDate.Size = new System.Drawing.Size(0, 17);
            this.repDate.TabIndex = 8;
            // 
            // Form0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.repDate);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.printReport);
            this.Controls.Add(this.panel1);
            this.Name = "Form0";
            this.Text = "Paper Assesment";
            this.Load += new System.EventHandler(this.Form0_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox semSelect;
        private System.Windows.Forms.ComboBox subSelect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button printReport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox totalToCheck;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label repDate;
    }
}