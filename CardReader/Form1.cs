using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardReader.Classes;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;
using System.IO;

namespace CardReader
{
    public partial class Form1 : Form
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public string ID;
        public string stdName;
        public string CollegeNumber;
        public string Year;
        public string Program;
        public string ApplyDate;
        public string trimmedApplyDate;
        public string Pic;
        public string Class;
        public string clear = "";
        public string AccessGranted = "مسموح له الدخول";
        public string AccessDenied = "ممنوع من الدخول";
        public string AccessDeniedLoan1 = "  الرجاء المراجعة مع مكتب الحسابات ودفع القسط الأول";
        public string AccessDeniedLoan2 = "  الرجاء المراجعة مع مكتب الحسابات ودفع القسط الثاني";
        public string AccessDeniedLoan3 = "  الرجاء المراجعة مع مكتب الحسابات ودفع القسط الثالث";
        public string AccessDeniedLoan4 = "  الرجاء المراجعة مع مكتب الحسابات ودفع القسط الرابع";
        public string AccessDeniedApplyDate = "الرجاء مراجعة مكتب القبول لتحديد تاريخ القبول الخاص بالطالب ";
        public string AccessDeniedCheckRevenuesOffice = "الرجاء المراجعة مع مكتب الحسابات";
        public DateTime Batch1LoanOne;
        public DateTime Batch1LoanTwo;
        public DateTime Batch1LoanThree;
        public DateTime Batch1LoanFour;
        public DateTime Batch2LoanOne;
        public DateTime Batch2LoanTwo;
        public DateTime StatmentDate;
        public DateTime DateNow;
        public DateTime CApplyDate;
        public DateTime ApplyDate2019;
        public DateTime ApplyDate2018;
        public DateTime ApplyDate2017;
        public DateTime ApplyDate2020;
       
        public string loan1;
        public string loan2;
        public string loan3;
        public string loan4;
        public string loan11;
        public string loan22;
        public string PaymentType;
        public string StatDate;



        public static Thread SocketThread = new Thread(SocketServer.HttpServer);

        public string Loan { get; private set; }


        public Form1()
        {

            InitializeComponent();
        }


        //Step ( 1 )
        //Loading Form Components And Calling The RFID Server Listener..
        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        //Step ( 2 )
        //Open The RFID Server Listener
        private void Initialize()
        {

            SocketServer.form = this;
            SocketThread.Start();
           


        }

        //Step ( 3 )
        //Get All Bathces Installments ( Loans ) Dates Form Database Form Table ( Loan_settings ) and Table ( loan_settings2 ) ..  
        public void GetAllBatchesInstallmentsDates()
        {

            server = "192.168.1.4";
            database = "acmst";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);


            //Batch 1 Installments

            //Batch One Loan One 
            connection.Open();
            MySqlCommand cmd1 = new MySqlCommand("select loanDate from loan_settings WHERE id LIKE '%" + 1 + "%' ", connection);
            MySqlDataReader reader1 = cmd1.ExecuteReader();

            if (reader1.Read())
            {
                loan1 = reader1["loanDate"].ToString();


            }
            connection.Close();

            //Batch One Loan Two  

            connection.Open();
            MySqlCommand cmd2 = new MySqlCommand("select loanDate from loan_settings WHERE id LIKE '%" + 2 + "%' ", connection);
            MySqlDataReader reader2 = cmd2.ExecuteReader();

            if (reader2.Read())
            {
                loan2 = reader2["loanDate"].ToString();


            }
            connection.Close();

            //Batch One Loan Three 

            connection.Open();
            MySqlCommand cmd3 = new MySqlCommand("select loanDate from loan_settings WHERE id LIKE '%" + 3 + "%' ", connection);
            MySqlDataReader reader3 = cmd3.ExecuteReader();

            if (reader3.Read())
            {
                loan3 = reader3["loanDate"].ToString();


            }


            connection.Close();

            //Batch One Loan Four 

            connection.Open();
            MySqlCommand cmd4 = new MySqlCommand("select loanDate from loan_settings WHERE id LIKE '%" + 4 + "%' ", connection);
            MySqlDataReader reader4 = cmd4.ExecuteReader();

            if (reader4.Read())
            {
                loan4 = reader4["loanDate"].ToString();



            }
            connection.Close();


            //Batch 2 Installment

            //Batch Two Loan One

            connection.Open();
            MySqlCommand cmd11 = new MySqlCommand("select loanDate from loan_settings2 WHERE id LIKE '%" + 1 + "%' ", connection);
            MySqlDataReader reader11 = cmd11.ExecuteReader();

            if (reader11.Read())
            {
                loan11 = reader11["loanDate"].ToString();


            }
            connection.Close();

            //Batch Two Loan Two
            connection.Open();
            MySqlCommand cmd22 = new MySqlCommand("select loanDate from loan_settings2 WHERE id LIKE '%" + 2 + "%' ", connection);
            MySqlDataReader reader22 = cmd22.ExecuteReader();

            if (reader22.Read())
            {
                loan22 = reader22["loanDate"].ToString();


            }
            connection.Close();

            
            //Now Removing Time From The Data Text Value To DateTime DataType
            Batch1LoanOne = Convert.ToDateTime(loan1);
            Batch1LoanTwo = Convert.ToDateTime(loan2);
            Batch1LoanThree = Convert.ToDateTime(loan3);
            Batch1LoanFour = Convert.ToDateTime(loan4);
            Batch2LoanOne = Convert.ToDateTime(loan11);
            Batch2LoanTwo = Convert.ToDateTime(loan22);

       }




        // Step ( 4 )
        // Call Primary Student Info..
        public void GetstudentInfo(string Cardid)
        {
            server = "192.168.1.4";
            database = "acmst";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select *from studants WHERE studentID LIKE '%" + Cardid + "%' ", connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                stdName = reader["arabicFullName"].ToString();
                CollegeNumber = reader["collegeNumber"].ToString();
                Year = reader["stdYear"].ToString();
                Class = reader["class"].ToString();
                Program = reader["program"].ToString();
                ApplyDate = reader["applyDate"].ToString();
                
                // MessageBox.Show(ApplyDate);
                // ApplyDate = ApplyDate.Replace("/", "2");

               // ApplyDate = ApplyDate.Replace(" 12:00:00 AM", "");

                //trimmedApplyDate = ApplyDate.Substring(4);
                //MessageBox.Show(trimmedApplyDate);
                //trimmedApplyDate = trimmedApplyDate.Replace("/", "");
                //MessageBox.Show(trimmedApplyDate);
                //trimmedApplyDate =String.Concat(trimmedApplyDate.Where(c => !Char.IsWhiteSpace(c)));

                Pic = reader["pic"].ToString();
                string id = reader["id"].ToString();
                ID = id;

                string Pictrimmed = Pic.Substring(23);
                this.SetText(this, nametext, stdName);
                this.SetText(this, collegetext, CollegeNumber);
                this.SetText(this, yeartext, Year);
                this.SetText(this, classtext, Class);
                this.SetText(this, programtext, Program);
                var pic = Convert.FromBase64String(Pictrimmed);
                using (MemoryStream ms = new MemoryStream(pic))
                {
                    StdPic.Image = Image.FromStream(ms);
                }


            }

            connection.Close();
        }

        //Step ( 5 )
        //Checking If Student Is have been moved to the new year 2019-2020.. 
        public void CheckStudentYear()
        {
            if (Year == "2019-2020")
            {

                CheckStudentBatch(ApplyDate);
            }
            else
            {
                string x = "الرجاء إتمام إجراءات الإنتقال للسنه التالية";
                this.SetText(this, label12, AccessDenied);
                this.SetText(this, label7, x);

                label12.ForeColor = Color.Red;
                for (int i = 1; i < 4; i++)
                    Console.Beep();
            }
        }


        //Step ( 6 )
        //Checking The Student Batch and send data ton the right method to check his installments ( Loans )
        public void CheckStudentBatch(string ApplyDate)
        {
            ApplyDate2020 = new DateTime(int.Parse("2020"), 1, 1);
            
            ApplyDate2018 = new DateTime(int.Parse("2018"), 1, 1);
           

            CApplyDate = Convert.ToDateTime(ApplyDate);


            if (CApplyDate < ApplyDate2018)
            {
                GetStudentBatch1LastStatmentDate(ID);
            }
       
            else if (CApplyDate < ApplyDate2020)
            {
                GetStudentBatch2LastStatmentDate(ID);
            }
            else
            {
               
                this.SetText(this, label12, AccessDenied);
                this.SetText(this, label7, AccessDeniedApplyDate);

                label12.ForeColor = Color.Red;
                for (int i = 1; i < 4; i++)
                    Console.Beep();
            }
        }


    




        // Step ( 7 or 8 )
        //Check The students from batch 2016-2017 and 2017-2018 who they have  4 Installments ( Loan )..
        public void GetStudentBatch1LastStatmentDate(String ID)
        {
            DateTime DateNow = DateTime.Now;
            server = "192.168.1.4";
            database = "acmst";
            uid = "root";
            password = "";

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT PaymentType ,StatmentDate From Paymnets WHERE PaymentFrom LIKE '%" + ID + "%' ORDER BY id DESC LIMIT 1", connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                PaymentType = reader["PaymentType"].ToString();
                StatDate = reader["StatmentDate"].ToString();
            }
           

            connection.Close();
            CheckStudentBatch1Status();


        }






        // Step ( 8 or 7 )
        //Check The students from batch 2018-2019 and 2019-2020 who they have just 2 Installments ( Loan )..
        public void GetStudentBatch2LastStatmentDate(String ID)
        {
            DateNow = DateTime.Now;
            server = "192.168.1.4";
            database = "acmst";
            uid = "root";
            password = "";

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT PaymentType ,StatmentDate From paymnets WHERE PaymentFrom LIKE '%" + ID + "%' ORDER BY id DESC LIMIT 1", connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                PaymentType = reader["PaymentType"].ToString();
                StatDate = reader["StatmentDate"].ToString();
                StatmentDate = Convert.ToDateTime(StatDate);

                connection.Close();

            }
            //MessageBox.Show(PaymentType);
            //if (PaymentType == "تسجيل")
            //{
            //    string x = "الرجاء دفع القسط الأول";
            //    this.SetText(this, label12, AccessDenied);
            //    this.SetText(this, label7, x);

            //    label12.ForeColor = Color.Red;
            //    for (int i = 1; i < 4; i++)
            //        Console.Beep();
            //}

            CheckStudentBatch2Status();
        }

        //Step ( 9 or 10)
        //Checking Student Batch 1 Payments And Grant His Access Or Deny Access
        public void CheckStudentBatch1Status()
        {
            if (PaymentType == "loan.1")
            {
                if (StatmentDate < Batch2LoanOne)
                {
                    this.SetText(this, label12, AccessGranted);


                    label12.ForeColor = Color.Green;
                    for (int i = 1; i < 2; i++)
                        Console.Beep();
                }
            }
                try
            {
                if (DateNow > Batch1LoanOne)
                {
                    //if (StatmentDate > Batch1LoanOne)
                    //{
                    //    this.SetText(this, label12, AccessDenied);
                    //    this.SetText(this, label7, AccessDeniedLoan1);

                    //    label12.ForeColor = Color.Red;
                    //    for (int i = 1; i < 4; i++)
                    //        Console.Beep();
                    //}

                    if (StatmentDate < Batch1LoanOne)
                    {

                        this.SetText(this, label12, AccessGranted);

                        this.SetText(this, label7, clear);

                        label12.ForeColor = Color.Green;
                        for (int i = 1; i < 4; i++)
                            Console.Beep();
                    }
                    if (StatmentDate > Batch1LoanTwo)
                    {
                        this.SetText(this, label12, AccessDenied);
                        this.SetText(this, label7, AccessDeniedLoan2);

                        label12.ForeColor = Color.Red;
                        for (int i = 1; i < 4; i++)
                            Console.Beep();
                    }
                    else if (StatmentDate < Batch1LoanTwo)
                    {

                        this.SetText(this, label12, AccessGranted);

                        this.SetText(this, label7, clear);

                        label12.ForeColor = Color.Green;
                        for (int i = 1; i < 4; i++)
                            Console.Beep();
                    }

                }

                if (StatmentDate > Batch1LoanThree)
                {
                    this.SetText(this, label12, AccessDenied);
                    this.SetText(this, label7, AccessDeniedLoan2);

                    label12.ForeColor = Color.Red;
                    for (int i = 1; i < 4; i++)
                        Console.Beep();
                }
                else if (StatmentDate < Batch1LoanThree)
                {
                    this.SetText(this, label7, clear);
                    this.SetText(this, label12, AccessGranted);

                    label12.ForeColor = Color.Green;
                    for (int i = 1; i < 2; i++)
                        Console.Beep();
                }

                
                    if (StatmentDate > Batch1LoanFour)
                    {

                        this.SetText(this, label12, AccessDenied);
                        this.SetText(this, label7, AccessDeniedLoan2);

                        label12.ForeColor = Color.Red;
                        for (int i = 1; i < 4; i++)
                            Console.Beep();
                    }
                    else if (StatmentDate < Batch1LoanFour)
                    {


                    this.SetText(this, label7, clear);
                    this.SetText(this, label12, AccessGranted);

                    label12.ForeColor = Color.Green;
                    for (int i = 1; i < 2; i++)
                        Console.Beep();
                }

                    //if (PaymentType == "تسجيل")
                    //{
                    //    string x = "الرجاء دفع القسط الأول";
                    //    this.SetText(this, label12, AccessDenied);
                    //    this.SetText(this, label7, x);

                    //    label12.ForeColor = Color.Red;
                    //    for (int i = 1; i < 4; i++)
                    //        Console.Beep();
                    //}

                    else
                    {

                        this.SetText(this, label12, AccessDenied);
                        this.SetText(this, label7, AccessDeniedCheckRevenuesOffice);

                        label12.ForeColor = Color.Red;
                        for (int i = 1; i < 4; i++)
                            Console.Beep();
                    }
                }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Step ( 10 or 9)
        //Checking Student Batch 2 Payments And Grant His Access Or Deny Access
        public void CheckStudentBatch2Status()
        {

            if (PaymentType=="loan.1")
            {
                if (StatmentDate < Batch2LoanOne)
                {
                    this.SetText(this, label12, AccessGranted);
                    //

                    label12.ForeColor = Color.Green;
                    for (int i = 1; i < 2; i++)
                        Console.Beep();
                }
            }
            try
            {

                //MessageBox.Show(StatmentDate.ToString());
               

                if (DateNow > Batch2LoanOne)
                    {
                    // if (StatmentDate < Batch2LoanOne)
                    //{
                    //    this.SetText(this, label12, AccessDenied);
                    //    this.SetText(this, label7, AccessDeniedLoan1);

                    //    label12.ForeColor = Color.Red;
                    //    for (int i = 1; i < 4; i++)
                    //        Console.Beep();
                    //}
                    if (StatmentDate > Batch2LoanOne)
                    {

                        this.SetText(this, label12, AccessGranted);

                        this.SetText(this, label7, clear);

                        label12.ForeColor = Color.Green;
                        for (int i = 1; i < 2; i++)
                            Console.Beep();
                    }
                        if (StatmentDate > Batch2LoanTwo)
                        {
                            

                        this.SetText(this, label12, AccessDenied);
                        this.SetText(this, label7, AccessDeniedLoan2);

                        label12.ForeColor = Color.Red;
                        for (int i = 1; i < 4; i++)
                            Console.Beep();
                    }
                }
                        else if (StatmentDate < Batch2LoanTwo)
                {

                    this.SetText(this, label12, AccessGranted);
                    this.SetText(this, label7, clear);

                    label12.ForeColor = Color.Green;
                    for (int i = 1; i < 2; i++)
                        Console.Beep();

                }
                        else
                        {

                            this.SetText(this, label12, AccessDenied);
                            this.SetText(this, label7, AccessDeniedCheckRevenuesOffice);

                            label12.ForeColor = Color.Red;
                            for (int i = 1; i < 4; i++)
                                Console.Beep();
                        }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    


        

        
        // A Very Usfeul Method
        //Setting Text to Lables While Debugging in Thread Mode

        delegate void SetTextCallback(Form f, Control ctrl, string text);
        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

    }
}
