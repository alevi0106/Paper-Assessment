using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;


namespace Assessment
{
    static class Program
    {
        static string mDbPath = Application.StartupPath +  "\\subjects.db";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!File.Exists(mDbPath))
            {
                SQLiteConnection.CreateFile(mDbPath);
            }

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();

            string sql = "CREATE TABLE IF NOT EXISTS subjects (dept varchar(5), id int, name varchar(50), sem int, sf varchar(10), PRIMARY KEY (dept, id));";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            int num = command.ExecuteNonQuery();
            if (num != -1)
            {
                fillCMPN();
                fillETRX();
                fillBIOM();
                fillEXTC();
                fillIT();
            }

            if (!File.Exists(Application.StartupPath + "\\Assessors.txt"))
            {
                FileStream f = File.Create(Application.StartupPath + "\\Assessors.txt");
                f.Close();
            }

            if (!File.Exists(Application.StartupPath + "\\Moderators.txt"))
            {
                FileStream f = File.Create(Application.StartupPath + "\\Moderators.txt");
                f.Close();
            }

            Application.Run(new Form2("2018 - 2019", "Sem 3", "Dec 18", "CMPN"));
        }

        public static void fillCMPN()
        {
            string sql;
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 1, 'Applied Mathematics 3', 3, 'AM 3');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 2, 'Digital Logic Design and Analysis', 3, 'DLDA');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 3, 'Discrete Mathematics', 3, 'DIS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 4, 'Electronic Circuits and Communication Fundamentals', 3, 'ECCF');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 5, 'Data Structures', 3, 'DS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 6, 'Applied Mathematics 4', 4, 'AM 4');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 7, 'Analysis of Algorithms', 4, 'AOA');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 8, 'Computer Organization and Architecture', 4, 'COA');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 9, 'Computer Graphics', 4, 'CG');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 10 ,'Operating Systems', 4, 'OS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 11, 'Microprocessor', 5, 'MP');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 12, 'Database Management System', 5, 'DBMS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 13, 'Computer Network', 5, 'CN');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 14, 'Theory of Computer Science', 5, 'TCS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 15, 'Department Level Optional Course 1', 5, 'OC 1');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 16, 'Software Engineering', 6, 'SE');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 17, 'System Programming and Compiler Construction', 6, 'SPCC');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 18, 'Data Warehousing & Mining', 6, 'DWM');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 19, 'Cryptography & System Security', 6, 'CSS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('CMPN', 20, 'Department Level Optional Course 2', 6, 'OC 2');";
            exec(sql);
        }

        public static void fillETRX()
        {
            string sql;
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 1, 'Applied Mathematics 3', 3, 'AM3');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 2, 'Electronic Devices and Circuits 1', 3, 'EDC1');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 3, 'Digital Circuit Design', 3, 'DCD');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 4, 'Electrical Network Analysis and Synthesis', 3, 'ENAS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 5, 'Electronics Instruments and Measurement', 3, 'EIM');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 6, 'Applied Mathematics 4', 4, 'AM4');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 7, 'Electronic Devices and Circuits 2', 4, 'EDC2');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 8, 'Microprocessors and Applications', 4, 'MP');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 9, 'Digital System Design', 4, 'DSD');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 10, 'Principles of Communication Engineering', 4, 'PCE');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 11, 'Linear Control Systems', 4, 'LCS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 12, 'Micro-controllers and Applications', 5, 'MCA');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 13, 'Digital Communication', 5, 'DC');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 14, 'Engineering Electromagnetics', 5, 'EE');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 15, 'Design with Linear Integrated Circuits', 5, 'DLIC');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 16, 'Department Level Optional courses 1', 5, 'OC1');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 17, 'Embedded System and RTOS', 6, 'ES');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 18, 'Computer Communication Network', 6, 'CN');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 19, 'VLSI Design', 6, 'VLSI');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 20, 'Signals and systems', 6, 'SS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('ETRX', 21, 'Department Level Optional courses 2', 6, 'OC2');";
            exec(sql);
        }

        public static void fillBIOM()
        {
            string sql;
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 1, 'Applied Mathematics 3', 3, 'AM 3');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 2, 'Basics of Human Physiology ', 3, 'BHP');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 3, 'Electrical Network Analysis and Synthesis', 3, 'ENAS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 4, 'Electronic Circuit Analysis and Design', 3, 'ECAD');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 5, 'Biomaterials, Prosthetics and Orthotics', 3, 'BPO');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 6, 'Applied Mathematics 4', 4, 'AM 4');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 7, 'Biomedical Transducers and Measuring Instruments', 4, 'BTMI');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 8, 'Linear Integrated Circuits', 4, 'LIC');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 9, 'Digital Electronics', 4, 'DE');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 10, 'Signals and Control Systems', 4, 'SCS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 11, 'Diagnostic & Therapeutic', 5, 'DT');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 12, 'Analog and Digital Circuit Design', 5, 'ADCD');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 13, 'Principles of Communication Engineering ', 5, 'PCE');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 14, 'Biomedical Digital Image Processing', 5, 'BDIP');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 15, 'Department Level Optional Course 1', 5, 'OC 1');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 16, 'Biomedical Monitoring Equipment', 6, 'BME');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 17, 'Microprocessors and Microcontrollers', 6, 'MM');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 18, 'Digital Image Processing ', 6, 'DIP');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 19, 'Medical Imaging-I ', 6, 'MI');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('BIOM', 20, 'Department Level Optional Course 2', 6, 'OC 2');";
            exec(sql);
        }

        public static void fillEXTC()
        {
            string sql;
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 1, 'Applied Mathematics 3', 3, 'AM 3');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 2, 'Electronic Devices and Circuits 1', 3, 'EDC 1');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 3, 'Digital System Design', 3, 'DSD');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 4, 'Circuit Theory and Networks', 3, 'CTN');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 5, 'Electronic Instrumentation and Control', 3, 'EIC');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 6, 'Applied Mathematics 4', 4, 'AM 4');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 7, 'Electronic Devices and Circuits 2', 4, 'EDC 2');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 8, 'Linear Integrated Circuits', 4, 'LIC');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 9, 'Signals & Systems', 4, 'SS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 10, 'Principles of Communication Engineering', 4, 'PCE');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 11, 'Microprocessor & Peripherals Interfacing', 5, 'MPI');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 12, 'Digital Communication', 5, 'DC');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 13, 'Electromagnetic Engineering', 5, 'EE');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 14, 'Discrete Time Signal Processing', 5, 'DTSP');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 15, 'Department Level Optional Course 1', 5, 'OC 1');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 16, 'Microcontroller & Applications', 6, 'MA');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 17, 'Computer Communication Networks', 6, 'CCN');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 18, 'Antenna & Radio Wave Propagation', 6, 'ARWP');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 19, 'Image Processing and Machine Vision', 6, 'IPMV');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('EXTC', 20, 'Department Level Optional Course 2', 6, 'OC 2');";
            exec(sql);
        }

        public static void fillIT()
        {
            string sql;
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 1, 'Applied Mathematics 3', 3, 'AM 3');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 2, 'Logic Design', 3, 'LD');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 3, 'Data Structures & Analysis', 3, 'DSA');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 4, 'Database Management System', 3, 'DBMS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 5, 'Principle of Communications', 3, 'PC');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 6, 'Applied Mathematics 4', 4, 'AM 4');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 7, 'Computer Networks', 4, 'CN 2');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 8, 'Operating Systems', 4, 'OS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 9, 'Computer Organization and Architecture', 4, 'COA');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 10, 'Automata Theory', 4, 'AT');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 11, 'Microcontroller and Embedded Programming', 5, 'MEP');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 12, 'Internet Programming', 5, 'IP');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 13, 'Advanced Data Management Technology', 5, 'ADMT');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 14, 'Cryptography & Network Security ', 5, 'CNS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 15, 'Department Level Optional Course 1', 5, 'OC 1');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 16, 'Software Engineering with Project Management', 6, 'SEPM');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 17, 'Data Mining and Business Intelligence', 6, 'DMBI');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 18, 'Cloud Computing & Services', 6, 'CCS');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 19, 'Wireless Networks', 6, 'WN');";
            exec(sql);
            sql = "INSERT INTO subjects (dept, id, name, sem, sf) VALUES ('IT', 20, 'Department Level Optional Course 2', 6, 'OC 2');";
            exec(sql);
        }

        public static void exec(string sql)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + mDbPath + ";Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
    }
}